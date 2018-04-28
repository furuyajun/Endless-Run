using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;

public class SwipeDetector : MonoBehaviour, IInputDetector
{

    public Vector3 touchStartPos;
    public Vector3 touchEndPos;
    public string Direction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                                      Input.mousePosition.y,
                                      Input.mousePosition.z);
            GetDirection();
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (30 < directionY)
            {
                //上向きにフリック
                Direction = "up";
            }
            else if (-30 > directionY)
            {
                //下向きのフリック
                Direction = "down";
            }
        }else
            {
                //タッチを検出
                Direction = "touch";
            }
    }


    public InputDirection? DetectInputDirection()
    {
        if (Direction == "up")
        {
            Direction = "";
            return InputDirection.Top;
        }
        else if (Direction == "down")
        {
            Direction = "";
            return InputDirection.Bottom;
        }
        else if (Direction == "right")
        {
            Direction = "";
            return InputDirection.Right;
        }
        else if (Direction == "left")
        {
            Direction = "";
            return InputDirection.Left;
        }
        else if (Input.acceleration.x > 0)
		{
			return InputDirection.gyroRight;
		}
        else if (Input.acceleration.x < 0)
		{
			return InputDirection.gyroLeft;
		}
        else
            return null;
    }
}