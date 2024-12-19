using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    private float moveSpeed = 20;
    private float forceAmount = 20;

    // Update is called once per frame
    void Update()
    {
        //move lazer forwards
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if hit enemy, add a force, and destroy lazer
        if (other.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount, ForceMode.Impulse);
        }
    }
}
