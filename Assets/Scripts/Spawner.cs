using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    Transform originalFollow;
    bool isActive;

    [SerializeField] List<GameObject> enemiesToSpawn = new List<GameObject>();




    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActive)
        {
            originalFollow = followCam.Follow;
            followCam.Follow = transform;
            StartCoroutine(ManageEnemies());
        }
    }

    IEnumerator ManageEnemies()
    {
        isActive = true;
        List<GameObject> spawnedEnemies = new List<GameObject>();
        foreach( GameObject enemy in enemiesToSpawn)
        {
            GameObject instance = Instantiate(enemy);
            spawnedEnemies.Add(instance);
        }
        while(spawnedEnemies.Count <= 0) yield return new WaitForEndOfFrame();
        followCam.Follow = originalFollow;
        Destroy(gameObject);
    }
}
