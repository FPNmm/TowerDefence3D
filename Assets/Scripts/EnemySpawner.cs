using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TowerBuildingController towerBuildingController;
    [SerializeField] private DeadzoneController deadzoneController;
    [SerializeField] private int waveSize;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject enemyPrefab;

    private float spawnTime;
    private bool isInBuildingPhase = true;
    private int amountOfEnemiesSpawned;

    private List<EnemyController> enemyList = new List<EnemyController>();

    private void Start()
    {
        spawnTime = spawnInterval;
    }

    private void Update()
    {
        switch (isInBuildingPhase)
        {
            case true:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        towerBuildingController.EnterEnemyPhase();
                        isInBuildingPhase = false;
                    }
                    break;
                } 
            case false:
                {
                    if (amountOfEnemiesSpawned >= waveSize)
                    {
                        isInBuildingPhase = true;
                        amountOfEnemiesSpawned = 0;
                        towerBuildingController.EnterBuildingPhase();
                    }

                    if (spawnTime <= 0f) SpawnEnemy();
                    else spawnTime -= Time.deltaTime;
                    break;
                }
        }
    }

    private void SpawnEnemy()
    {
        EnemyController enemyController = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform).GetComponent<EnemyController>();
        enemyList.Add(enemyController);
        enemyController.GetComponent<NavMeshAgent>().SetDestination(deadzoneController.transform.position);

        spawnTime = spawnInterval;
        amountOfEnemiesSpawned++;
    }
}
