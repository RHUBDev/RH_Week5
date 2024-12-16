using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    private Transform camParent;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camParent = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");
        Vector2 rotateDirection = new Vector2(mouseXInput, mouseYInput).normalized;
        transform.Rotate((Vector3.up * rotateDirection.x) * mouseSensitivity * Time.deltaTime);
        camParent.Rotate((Vector3.right * -rotateDirection.y) * mouseSensitivity * Time.deltaTime);
    }
}
