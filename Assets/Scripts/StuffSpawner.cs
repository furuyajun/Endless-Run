using UnityEngine;
using System.Collections;

public class StuffSpawner : MonoBehaviour
{
    public Transform[] StuffSpawnPoints;
    public GameObject[] Bonus;

    public bool RandomX = false;
    public float minX = -2f, maxX = 2f;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < StuffSpawnPoints.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                CreateObject(StuffSpawnPoints[i].position, Bonus[Random.Range(0, Bonus.Length)]);
            }

        }


    }

    void CreateObject(Vector3 position, GameObject prefab)
    {
        if (RandomX)
            position += new Vector3(Random.Range(minX, maxX), 0, 0);

        Instantiate(prefab, position, Quaternion.identity);
    }

}
