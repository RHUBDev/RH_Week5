using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int bossnum;
    public GameObject minionPrefab;
    private float minionForce = 2f;
    private float minionSpawnDistance = 2f;
    private Transform enemyParent;
    private float spawnTime = 0.5f;
    private float lazerTime = 1;
    private GameObject player;
    public GameObject lazerPrefab;

    private void Start()
    {
        if (bossnum == 0)
        {
            //if bossnum == 0, start spawning minions
            enemyParent = GameObject.Find("EnemyParent").transform;
            InvokeRepeating("SpawnMinion", spawnTime, spawnTime);
        }
        else if(bossnum == 1)
        {
            //if bossnum == 1, start shooting lazers
            player = GameObject.FindWithTag("Player");
            InvokeRepeating("ShootLazer", lazerTime, lazerTime);
        }
    }

    void SpawnMinion()
    {
        //spawn minion randomly beside boss every few seconds
        Vector3 randomDirection = Quaternion.Euler(0, Random.Range(0f, 360f), 0) * Vector3.forward;
        GameObject minion = Instantiate(minionPrefab, transform.position + randomDirection * minionSpawnDistance, minionPrefab.transform.rotation, enemyParent);
        minion.GetComponent<Rigidbody>().AddForce(randomDirection * minionForce, ForceMode.Impulse);
    }

    void ShootLazer()
    {
        //spawn lazer every few seconds
        Vector3 direction = player.transform.position - transform.position;
        Instantiate(lazerPrefab, transform.position, Quaternion.LookRotation(direction));
    }
}
