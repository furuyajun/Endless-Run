using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Obstacle : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == Constants.PlayerTag)
        {
            GameManager.Instance.Die();
        }
    }
}
