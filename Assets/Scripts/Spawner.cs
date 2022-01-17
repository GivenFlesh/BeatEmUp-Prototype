using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    Transform originalFollow;
    bool isActive;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

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
        foreach( GameObject enemy in enemiesToSpawn)
        {
            GameObject instance = Instantiate(enemy);
            spawnedEnemies.Add(instance);
            instance.GetComponent<EnemyAI>().spawnerSource = this;
        }
        while(spawnedEnemies.Count > 0)
        { yield return new WaitForEndOfFrame(); }
        followCam.Follow = originalFollow;
        Destroy(gameObject);
    }
}
