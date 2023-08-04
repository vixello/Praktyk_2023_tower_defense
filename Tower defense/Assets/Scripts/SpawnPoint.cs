using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemy;
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
            Instantiate(enemy, transform.position, Quaternion.identity);
        }

    }
}
