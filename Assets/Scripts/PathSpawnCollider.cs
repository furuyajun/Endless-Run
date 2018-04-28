using UnityEngine;
using System.Collections;

public class PathSpawnCollider : MonoBehaviour {

    public float positionY = 0.81f;
    public Transform[] PathSpawnPoints;
    public GameObject Path;
    public GameObject DangerousBorder;

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            
            int randomSpawnPoint1 = Random.Range(0,4);
            if (randomSpawnPoint1 == 3)
            {
                //２レーン作成
                Instantiate(Path, PathSpawnPoints[0].position, PathSpawnPoints[0].rotation);
                Instantiate(Path, PathSpawnPoints[2].position, PathSpawnPoints[2].rotation);
                Vector3 rotation = PathSpawnPoints[1].rotation.eulerAngles;
                rotation.y += 90;
                Vector3 position = PathSpawnPoints[1].position;
                position.y += positionY;
                Instantiate(DangerousBorder, position, Quaternion.Euler(rotation));
            }
            else
            {
                //１レーン作成
                int randomSpawnPoint = Random.Range(0, PathSpawnPoints.Length);
                for (int i = 0; i < PathSpawnPoints.Length; i++)
                {                   
                    if (i == randomSpawnPoint)
                        Instantiate(Path, PathSpawnPoints[i].position, PathSpawnPoints[i].rotation);
                    else
                    {
                        Vector3 rotation = PathSpawnPoints[i].rotation.eulerAngles;
                        rotation.y += 90;
                        Vector3 position = PathSpawnPoints[i].position;
                        position.y += positionY;
                        Instantiate(DangerousBorder, position, Quaternion.Euler(rotation));
                    }
                }
            }
        }
    }

}
