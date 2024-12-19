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
    private int lives = 999;
    public TMP_Text livestext;
    private bool dead = false;
    private bool poweredup = false;
    public GameObject powerUpIndicator;
    private float powerUpRotateSpeed = 120f;
    private float powerForce = 50f;
    private float powerUpTime = 7f;
    public Transform enemyParent;
    public GameObject lazerPrefab;
    private Coroutine powerCoroutine;
    private string levelname;
    [SerializeField] private string playernum = "";

    // Start is called before the first frame update
    void Start()
    {

        levelname = SceneManager.GetActiveScene().name;
        if (levelname != "Menu" && levelname != "InfiniteScene")
        {
            lives = 3;
        }
        //save startpos
        startpos = transform.position;
        rig = GetComponent<Rigidbody>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float forwardInput = Input.GetAxis("Vertical" + playernum);
        float horizontalInput = Input.GetAxis("Horizontal" + playernum);
        
        Vector2 moveDirection = new Vector2(horizontalInput, forwardInput).normalized;
        
        //move player
        rig.AddForce((focalPoint.forward * moveDirection.y + focalPoint.right * moveDirection.x) * moveSpeed);

        //rotate the indicator and keep aligned with player
        if (powerUpIndicator.activeInHierarchy)
        {
            powerUpIndicator.transform.Rotate(Vector3.up * powerUpRotateSpeed * Time.deltaTime);
            powerUpIndicator.transform.position = transform.position;
        }

        //lose a life
        if (transform.position.y < -1.5f && !dead)
        {
            if (levelname == "MyScene1")
            {
                lives -= 1;
                livestext.text = "Lives: " + lives;
            }
            else if(levelname != "InfiniteScene" && levelname != "Menu")
            {
                lives -= 1;
                if (playernum == "1")
                {
                    livestext.text = "Blue Lives: " + lives;
                }
                else if (playernum == "2")
                {
                    livestext.text = "Green Lives: " + lives;
                }
            }
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
                if (transform.parent.childCount > 1)
                {
                    if (levelname == "BattleMultiplayer" || levelname == "CompetitiveMultiplayer")
                    {
                        Time.timeScale = 0.0f;
                        //Do high scores
                        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().DoScores(transform);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Time.timeScale = 0.0f;
                    //Do high scores
                    GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().DoScores(transform);
                }
            }
        }
        if (levelname != "Menu")
        {
            //'R' to restart
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            //'Q' to go to Menu
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //we hit the powerup, so add indicator and start the timer
        if (other.transform.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            //If we already have it, stop the old coroutine
            if (powerCoroutine != null)
            {
                StopCoroutine(powerCoroutine);
            }
            poweredup = true;
            powerUpIndicator.SetActive(true);
            powerCoroutine = StartCoroutine(PowerUpTime());
        }
        //if we hit the Fire power up, shoot a lazer at every enemy
        if (other.transform.CompareTag("FireUp"))
        {
            Destroy(other.gameObject);
            for(int i = 0; i < enemyParent.childCount; i++)
            {
                Vector3 direction = enemyParent.GetChild(i).position - transform.position;
                Instantiate(lazerPrefab, transform.position, Quaternion.LookRotation(direction));
            }
        }
    }

    IEnumerator PowerUpTime()
    {
        //powerup removal timer
        yield return new WaitForSeconds(powerUpTime);
        powerUpIndicator.SetActive(false);
        poweredup = false;
        powerCoroutine = null;
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
