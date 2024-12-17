using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private float spawnRange = 9f;
    private int enemiesToSpawn = 5;
    public PlayerController playerController;
    public Transform enemyParent;
    public GameObject powerUpPrefab;
    private GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextWave();
    }

    Vector3 GenerateSpawnPotition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnX, 0.12f, spawnZ);
        return spawnPosition;
    }

    private void Update()
    {
        if(enemyParent.childCount == 0)
        {
            SpawnNextWave();
        }
    }

    void SpawnNextWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemynum = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemynum], GenerateSpawnPotition(), enemyPrefabs[enemynum].transform.rotation, enemyParent);
        }
        if (powerUp)
        {
            Destroy(powerUp);
        }
        SpawnPowerUp();
        enemiesToSpawn += 1;
    }

    void SpawnPowerUp()
    {
        powerUp = Instantiate(powerUpPrefab, GenerateSpawnPotition(), powerUpPrefab.transform.rotation);
    }

    /*public void Respawn(Transform enemy)
    {
        enemy.transform.position = GenerateSpawnPotition();
        Rigidbody enemyrig = enemy.GetComponent<Rigidbody>();
        enemyrig.velocity = Vector3.zero;
        enemyrig.angularVelocity = Vector3.zero;
        playerController.AddScore();
    }*/
}
