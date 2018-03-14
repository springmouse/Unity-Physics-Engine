using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject spawnInObject;
    public Transform spawnPoint;

    public float waitTime;
    float waitedTime;

    public void Update()
    {
        waitedTime += Time.deltaTime;
    }

    public void SpawnObjects()
    {
        if (waitedTime > waitTime)
        {
            Instantiate(spawnInObject, spawnPoint.position, Quaternion.identity);
            waitedTime = 0;
        }
    }
}
