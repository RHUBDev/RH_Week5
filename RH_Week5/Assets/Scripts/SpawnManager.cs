using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
    private int enemiesToSpawn = 5;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPotition(), enemyPrefab.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPotition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnX, 0.12f, spawnZ);
        return spawnPosition;
    }

    public void Respawn(Transform enemy)
    {
        enemy.transform.position = GenerateSpawnPotition();
        Rigidbody enemyrig = enemy.GetComponent<Rigidbody>();
        enemyrig.velocity = Vector3.zero;
        enemyrig.angularVelocity = Vector3.zero;
        playerController.AddScore();
    }
}
