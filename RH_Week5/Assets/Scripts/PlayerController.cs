using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    public float moveSpeed = 5f;
    [SerializeField] Transform focalPoint;
    private Vector3 startpos;
    private int score = 0;
    private int lives = 3;
    public TMP_Text scoretext;
    public TMP_Text livestext;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        rig = GetComponent<Rigidbody>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, forwardInput).normalized;
        rig.AddForce((focalPoint.forward * moveDirection.y + focalPoint.right * moveDirection.x) * moveSpeed);

        if (transform.position.y < -1.5f && !dead)
        {
            lives -= 1;
            livestext.text = "Lives: " + lives;
            if (lives > 0)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.position = startpos;
            }
            else
            {
                dead = true;
                Time.timeScale = 0.0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore()
    {
        score++;
        scoretext.text = "Enemies Killed: " + score;
    }
}
