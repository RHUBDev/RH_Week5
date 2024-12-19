using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float rotateSpeed = 200f;

    private void Update()
    {
        //rotate the spawned powerup
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
