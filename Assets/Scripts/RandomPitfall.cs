using UnityEngine;
using System.Collections;

public class RandomPitfall : MonoBehaviour {

    private void Awake()
    {
        int x = Random.Range(0, 10);

        if (x == 0)
        {
            Destroy(gameObject);
        }
    }
}
