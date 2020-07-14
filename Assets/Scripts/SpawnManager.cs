using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;
    
    private bool _stopSpawning = false;     

    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(5.0f);
            Vector3 spawnPosition = new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition , Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            
        }

    }

    IEnumerator SpawnPowerupsRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5, 13));
            Vector3 spawnPosition = new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], spawnPosition, Quaternion.identity);            

        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
