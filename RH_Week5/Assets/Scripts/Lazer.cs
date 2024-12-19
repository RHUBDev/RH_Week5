using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float moveSpeed = 100;
    private float forceAmount = 50;

    // Update is called once per frame
    void Update()
    {
        //move lazer forwards
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if hit enemy, add a force, and destroy lazer
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount, ForceMode.Impulse);
        }
    }
}
