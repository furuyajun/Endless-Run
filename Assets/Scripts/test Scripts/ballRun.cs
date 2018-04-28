using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballRun : MonoBehaviour
{

    private float speed = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var dir = Vector3.zero;

        // ターゲット端末の縦横の表示に合わせてremapする
        dir.x = Input.acceleration.x;
       

       
        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        if (Input.acceleration.x>10||Input.acceleration.x < 10)
            transform.Translate(dir * speed);

    }
}
