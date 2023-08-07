using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject SpawningEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for(int i = 0; i< 100; i++)
        {
            yield return new WaitForSeconds(2);
            Instantiate(SpawningEnemy, transform.position, Quaternion.identity);
        }

    }
}
