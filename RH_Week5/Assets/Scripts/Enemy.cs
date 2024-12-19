using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rigmass = 1f;
    public GameObject[] players;
    private GameObject spawnManager;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody, set mass, and find player
        rig = GetComponent<Rigidbody>();
        rig.mass = rigmass;

        players = GameObject.FindGameObjectsWithTag("Player");
        spawnManager = GameObject.FindWithTag("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        player = null;
        for (int i = 0; i < players.Length; i++)
        {
           if(players[i] != null)
            {
                if (player == null)
                {
                    player = players[i];
                }
                else if((players[i].transform.position - transform.position).magnitude < (player.transform.position - transform.position).magnitude)
                {
                    player = players[i];
                }
            }
        }
        //Move enemy towards player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rig.AddForce(lookDirection * moveSpeed);

        if (transform.position.y < -1.5f)
        {
            //if fell off platform, destory this enemy
            if (!transform.CompareTag("Minion"))
            {
                spawnManager.GetComponent<SpawnManager>().AddScore();
            }
            Destroy(gameObject);
        }
    }
}
