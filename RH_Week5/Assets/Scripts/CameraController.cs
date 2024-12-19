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

        mouseSensitivity = PlayerPrefs.GetFloat("Mouse Sensitivity");
        //lock Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //get camParent
        camParent = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");

        //rotate focalpoint and cameraparent separately
        Vector2 rotateDirection = new Vector2(mouseXInput, mouseYInput).normalized;
        transform.Rotate((Vector3.up * rotateDirection.x) * mouseSensitivity * Time.deltaTime);
        camParent.Rotate((Vector3.right * -rotateDirection.y) * mouseSensitivity * Time.deltaTime);
        
        //clamp rotation
        if (camParent.localEulerAngles.x > 84 && camParent.localEulerAngles.x <= 225)
        {
            camParent.localRotation = Quaternion.Euler(84, 0, 0);
        }
        else if (camParent.localEulerAngles.x > 225)
        {
            camParent.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
