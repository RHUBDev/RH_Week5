using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    public float moveSpeed = 5f;
    [SerializeField] Transform focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, forwardInput).normalized;
        rig.AddForce((focalPoint.forward * moveDirection.y + focalPoint.right * moveDirection.x) * moveSpeed);
    }
}
