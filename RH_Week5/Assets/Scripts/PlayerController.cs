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
    public TMP_Text infiniteText;
    public GameObject hintText;
    public AudioSource audiosource;
    public AudioClip pushsound;
    public AudioClip lazersound;
    public AudioClip powerupsound;

    // Start is called before the first frame update
    void Start()
    {
        //Please excuse the long start function!

        //if 'LoadingLevel' is null, or scene is the Menu, set LoadingLevel to "Menu"
        if (!PlayerPrefs.HasKey("LoadingLevel") || SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerPrefs.SetString("LoadingLevel", "Menu");
            levelname = "Menu";
        }
        else
        {
            //else set levelname to the saved 'LoadingLevel'
            levelname = PlayerPrefs.GetString("LoadingLevel");
        }
        if (levelname != "Menu" && levelname != "InfiniteScene" && levelname != "InfiniteMultiplayer")
        {
            //if lives shouldn't be infinite, set to 3
            lives = 3;
        }
        if(levelname == "Menu" || levelname == "MyScene1" || levelname == "InfiniteScene")
        {
            //if single player level, destroy mulitiplayer players from scene
            if(playernum != "")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //else if multiplayer level, destroy single player 'Player'
            if (playernum == "")
            {
                Destroy(gameObject);
            }
        }

        if (levelname == "MyScene1")
        {
            //if in single player level, set initial Lives text
            if (playernum == "")
            {
                livestext.text = "Lives: " + lives;
            }
        }
        else if (levelname != "InfiniteScene" && levelname != "Menu" && levelname != "InfiniteMultiplayer")
        {//if in multiplayer level, set initial Lives texts
            if (playernum == "1")
            {
                livestext.text = "Blue Lives: " + lives;
            }
            else if (playernum == "2")
            {
                livestext.text = "Green Lives: " + lives;
            }
        }

        if (levelname == "InfiniteScene" || levelname == "InfiniteMultiplayer")
        {
            //if infinite test level, show controls on screen of how to go back to menu
            hintText.SetActive(true);
        }
        else if (levelname == "MyScene1")
        {
            //show name of level in top corner, in case player forgets
            infiniteText.text = "[Single Player]";
        }
        else if (levelname == "BattleMultiplayer")
        {
            infiniteText.text = "[Battle Multiplayer]";
        }
        else if (levelname == "CompetitiveMultiplayer")
        {
            infiniteText.text = "[Competitive Multiplayer]";
        }
        else if (levelname == "CoOpMultiplayer")
        {
            infiniteText.text = "[Co-Op Multiplayer]";
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
                //if single player, remove life
                lives -= 1;
                livestext.text = "Lives: " + lives;
            }
            else if(levelname != "InfiniteScene" && levelname != "Menu" && levelname != "InfiniteMultiplayer")
            {
                //if multiplayer and not infinite mode, remove life
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
                //respawn player
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
                        //if multiplayer battle and one of them died, end game
                        Time.timeScale = 0.0f;
                        //Do high scores
                        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().DoScores(transform);
                    }
                    else
                    {
                        //else, if co-op game, destroy the dead player and let the other continue playing
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
            //'M' to go to Menu
            if (Input.GetKeyDown(KeyCode.M))
            {
                PlayerPrefs.SetString("LoadingLevel", "Menu");
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //we hit the powerup, so add indicator and start the timer
        if (other.transform.CompareTag("PowerUp"))
        {
            audiosource.PlayOneShot(powerupsound);
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
            audiosource.PlayOneShot(lazersound);
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
            audiosource.PlayOneShot(pushsound);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position).normalized * powerForce, ForceMode.Impulse);
        }
    }
}
