using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomfloatObstacle : MonoBehaviour {

    public GameObject obstacle;

    private void Awake()
    {
        int x = Random.Range(0, 10);

        if (x == 0)
        {
            Vector3 height = this.transform.position;
            height.y += 3;
            Instantiate(obstacle, height, this.transform.rotation);
        }
    }
}
