using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnTIme;
    float m_elapsedSpawnTime;


	void Update ()
    {
        m_elapsedSpawnTime += Time.deltaTime;

        if (m_elapsedSpawnTime > spawnTIme)
        {
            Instantiate(objectsToSpawn[0], transform.position, Quaternion.identity);
            m_elapsedSpawnTime = 0;
        }
	}
}
