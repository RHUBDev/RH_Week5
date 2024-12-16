using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rig;
    public float moveSpeed = 5f;
    private GameObject player;
    private SpawnManager spawnmanager;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        spawnmanager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rig.AddForce(lookDirection * moveSpeed);

        if(transform.position.y < -1.5f)
        {
            spawnmanager.Respawn(transform);
        }
    }
}
