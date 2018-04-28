using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float Speed = 6.0f;
    public float gravity = 20f;

    public float SideWaysSpeed = 5.0f;
    public float JumpSpeed = 8.0f;

    private CharacterController controller;

    IInputDetector inputDetector = null;
    
    private GameObject cube_child;

    public bool cubesliding = false;
    public float cube_SlideLength = 0;

    private bool CameraRightRotation = false;
    private bool CameraLeftRotation = false;

    private Quaternion CurveStartangle;

    public float CurveCameraSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        moveDirection = transform.forward;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;

        inputDetector = GetComponent<IInputDetector>();
        controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {

        switch (GameManager.Instance.GameState)
        {
            case GameState.Start:
               //画面タッチでゲームスタートボタン(マウス左クリック）
                if (Input.GetMouseButtonUp(0))
                {
                    var instance = GameManager.Instance;
                    instance.GameState = GameState.Playing;
                }
                break;
            case GameState.Playing:
                    CheckHeight();
                    
                    PlayerActioncontroller();

                if (CameraRightRotation == true)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, CurveStartangle * Quaternion.Euler(0, 90, 0), CurveCameraSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, CurveStartangle * Quaternion.Euler(0, 90, 0)) <= 0.1)
                        CameraRightRotation = false;
                }

                    if (CameraLeftRotation == true)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, CurveStartangle * Quaternion.Euler(0, -90, 0), CurveCameraSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, CurveStartangle * Quaternion.Euler(0, -90, 0)) <= 0.1)
                        CameraLeftRotation = false;                  
                }

                //playerに重力を適用
                moveDirection.y -= gravity * Time.deltaTime;
                   
                    //playerが走る
                    controller.Move(moveDirection * Time.deltaTime);
                break;
            case GameState.Dead:                
                if (Input.GetMouseButtonUp(0))
                {
                    //restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            default:
                break;
        }
	}

    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            GameManager.Instance.Die();
        }
    }

    private void PlayerActioncontroller()
    {
        var inputDirection = inputDetector.DetectInputDirection();
       
        if (controller.isGrounded && inputDirection.HasValue && inputDirection == InputDirection.Top)
            
        {
            moveDirection.y = JumpSpeed;
        }

        if (controller.isGrounded && inputDirection.HasValue && inputDirection == InputDirection.Bottom && !cubesliding)

        {
            controller.height = controller.height * 0.5f;
            controller.center = controller.center - new Vector3(0.0f, controller.height * 0.5f, 0.0f);
            /*仮スライディング(本番はアニメーション）*/
            cube_child = transform.Find("Player1").gameObject;
            cube_child.transform.position += new Vector3(0f, -0.5f, 0f);
            cube_child.transform.localScale = new Vector3(1f, 0.5f, 1f);

            cubesliding = true;
        }

        if (cubesliding)
        {
                cube_SlideLength += Time.deltaTime;

                if(cube_SlideLength > 1)
                {
                    controller.height = controller.height * 2f;
                    controller.center = controller.center + new Vector3(0.0f, controller.height * 0.25f, 0.0f);
                    cube_child.transform.position += new Vector3(0f, +0.5f, 0f);
                    cube_child.transform.localScale = new Vector3(1f, 1f, 1f);

                    cubesliding = false;
                cube_SlideLength = 0;
                }
        }

        //ジャイロで左右移動
        if (controller.isGrounded && inputDirection.HasValue)
        {

			if (inputDirection == InputDirection.gyroLeft)
            {
                
                transform.Translate(Vector3.right * Input.acceleration.x);

            }
			else if (inputDirection == InputDirection.gyroRight)
            {
               
                transform.Translate(Vector3.right * Input.acceleration.x);

            }
        }

        //カーブでplayerとカメラを回転
        if (GameManager.Instance.CanSwipe && inputDirection.HasValue &&
         controller.isGrounded && inputDirection == InputDirection.Right)
        {
            CurveStartangle = transform.rotation;
            CameraRightRotation = true;

            moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
            GameManager.Instance.CanSwipe = false;         
        }
        else if (GameManager.Instance.CanSwipe && inputDirection.HasValue &&
         controller.isGrounded && inputDirection == InputDirection.Left)
        {
            CurveStartangle = transform.rotation;
            CameraLeftRotation = true;

            moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
            GameManager.Instance.CanSwipe = false;
        }

    }
}
