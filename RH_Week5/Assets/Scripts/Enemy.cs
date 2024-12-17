using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rigmass = 1f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.mass = rigmass;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rig.AddForce(lookDirection * moveSpeed);

        if(transform.position.y < -1.5f)
        {
            player.GetComponent<PlayerController>().AddScore();
            Destroy(gameObject);
        }
    }
}
