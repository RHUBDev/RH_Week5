using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    public float moveSpeed = 3f;
    [SerializeField] Transform focalPoint;
    private Vector3 startpos;
    private int score = 0;
    private int lives = 3;
    public TMP_Text scoretext;
    public TMP_Text livestext;
    private bool dead = false;
    private bool poweredup = false;
    public GameObject powerUpIndicatorPrefab;
    public GameObject powerUpIndicator;
    private float powerUpRotateSpeed = 120f;
    private float powerForce = 50f;
    private float powerUpTime = 7f;

    // Start is called before the first frame update
    void Start()
    {
        //save startpos
        startpos = transform.position;
        rig = GetComponent<Rigidbody>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        Vector2 moveDirection = new Vector2(horizontalInput, forwardInput).normalized;
        
        //move player
        rig.AddForce((focalPoint.forward * moveDirection.y + focalPoint.right * moveDirection.x) * moveSpeed);

        //rotate the indicator and keep aligned with player
        if (powerUpIndicator)
        {
            powerUpIndicator.transform.Rotate(Vector3.up * powerUpRotateSpeed * Time.deltaTime);
            powerUpIndicator.transform.position = transform.position;
        }

        //lose a life
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
                //if dead, end game
                dead = true;
                Time.timeScale = 0.0f;
            }
        }
        //'R' to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore()
    {
        //enemy killed, add a point
        score++;
        scoretext.text = "Enemies Killed: " + score;
    }

    private void OnTriggerEnter(Collider other)
    {
        //we hit the powerup, so add indicator and start the timer
        if (other.transform.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            poweredup = true;
            powerUpIndicator = Instantiate(powerUpIndicatorPrefab, transform.position, Quaternion.identity);
            StartCoroutine(PowerUpTime());
        }
    }

    IEnumerator PowerUpTime()
    {
        //powerup removal timer
        yield return new WaitForSeconds(powerUpTime);
        Destroy(powerUpIndicator);
        poweredup = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if hit enemy while powered up, add force
        if (collision.transform.CompareTag("Enemy") && poweredup)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position).normalized * powerForce, ForceMode.Impulse);
        }
    }
}
