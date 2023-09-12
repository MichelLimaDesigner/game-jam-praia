using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject batPrefab;

    [SerializeField]
    private float spawnerInterval = 3.5f;

    [SerializeField] 
    private Transform initialPoint;

    [SerializeField] 
    private Transform finalPoint;



    void Start(){
        StartCoroutine(SpawnEnemy(spawnerInterval, batPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        // Calcula uma posição intermediária entre initialPoint e finalPoint
        Vector3 spawnPosition = Vector3.Lerp(initialPoint.position, finalPoint.position, Random.value);
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

}
