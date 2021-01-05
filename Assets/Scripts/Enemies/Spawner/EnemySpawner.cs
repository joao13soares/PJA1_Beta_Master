using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesAvailableToSpawn;
    [SerializeField] private List<Transform> spawnPoints;


    public void SpawnEnemy()
    {
        GameObject randomEnemy = enemiesAvailableToSpawn[Random.Range(0, enemiesAvailableToSpawn.Count - 1)];
        GameObject newEnemyClone = Instantiate(randomEnemy);


        newEnemyClone.transform.parent = this.transform;

        newEnemyClone.transform.localPosition =
            spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.localPosition;
        
        newEnemyClone.SetActive(true);
    }
}
