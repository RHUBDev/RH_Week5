using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    private float spawnRange = 9f;
    private int enemiesToSpawn = 5;
    public Transform enemyParent;
    public GameObject[] powerUpPrefabs;
    private GameObject powerUp;
    private int wave = 0;
    private int bosswave = 4;
    private int nextBoss = 0;
    public TMP_Text scoretext;
    private int wavekills = 0;
    public TMP_Text endtext;
    public GameObject hintstext;
    private string levelname;
    public AudioSource audiosource;
    public AudioClip winsound;
    public AudioClip losesound;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("LoadingLevel") || SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerPrefs.SetString("LoadingLevel", "Menu");
            levelname = "Menu";
        }
        else
        {
            levelname = PlayerPrefs.GetString("LoadingLevel");
        }
        //wave += 1;
        //Spawn random enemies and powerup on start 
        //SpawnNextWave();
    }

    Vector3 GenerateSpawnPotition(float yPos)
    {
        //return random position on the platform
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnX, yPos, spawnZ);
        return spawnPosition;
    }

    private void Update()
    {
        if (levelname != "BattleMultiplayer")
        {
            if (enemyParent.childCount == 0)
            {
                //increment wave number
                wave += 1;
                //once enemies dead, spawn the next wave, or spawn the boss fight if next wave is a multiple of 'bosswave' number
                if (wave % bosswave == 0)
                {
                    SpawnBossBattle();
                }
                else
                {
                    SpawnNextWave();
                }
                //reset kills this wave
                wavekills = 0;
                scoretext.text = "Wave:Kills: " + wave + ":" + wavekills;
            }
        }
    }

    void SpawnNextWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            //spawn enemies
            int enemynum = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemynum], GenerateSpawnPotition(enemyPrefabs[enemynum].transform.position.y), enemyPrefabs[enemynum].transform.rotation, enemyParent);
        }
        if (powerUp)
        {
            //destroy old powerup
            Destroy(powerUp);
        }
        //spawn new powerup
        SpawnPowerUp();
        //increase number of enemies by 1 for next round
        enemiesToSpawn += 1;
    }
    
    void SpawnBossBattle()
    {
        //spawn boss
        Instantiate(bossPrefabs[nextBoss], GenerateSpawnPotition(bossPrefabs[nextBoss].transform.position.y), bossPrefabs[nextBoss].transform.rotation, enemyParent);
        //change boss for next time
        nextBoss += 1;
        if(nextBoss >= bossPrefabs.Length)
        {
            nextBoss = 0;
        }
        if (powerUp)
        {
            //destroy old powerup
            Destroy(powerUp);
        }
        //spawn new powerup
        SpawnPowerUp();
    }
    
    void SpawnPowerUp()
    {
        //spawn random power up
        int powerUpnum = Random.Range(0, powerUpPrefabs.Length);
        powerUp = Instantiate(powerUpPrefabs[powerUpnum], GenerateSpawnPotition(powerUpPrefabs[powerUpnum].transform.position.y), powerUpPrefabs[powerUpnum].transform.rotation);
    }

    public void AddScore()
    {
        //enemy killed, add a point
        wavekills++;
        scoretext.text = "Wave:Kills: " + wave + ":" + wavekills;
    }

    public void DoScores(Transform player)
    {
        //if single player, do scores
        if (levelname == "MyScene1")
        {
            //show restart/quit keys in hud
            hintstext.SetActive(true);
            int highwave = 0;
            int highkills = 0;

            //get old highscores
            if (PlayerPrefs.HasKey("HighWave"))
            {
                highwave = PlayerPrefs.GetInt("HighWave");
                highkills = PlayerPrefs.GetInt("HighKills");
            }
            //show unlucky text if failed to beat old score
            if ((wave < highwave) || (wave == highwave && wavekills <= highkills))
            {
                audiosource.PlayOneShot(losesound);
                endtext.text = "Unlucky!\nScore: Wave:" + wave + ", Kills: " + wavekills + "\nHighScore: Wave: " + highwave + ", Kills: " + highkills;
            }
            //if beat high scores, set new ones, and show win text
            else if ((wave == highwave && wavekills > highkills) || (wave > highwave))
            {
                audiosource.PlayOneShot(winsound, 0.5f);
                endtext.text = "New High Score!\nScore: Wave:" + wave + ", Kills: " + wavekills + "\nOld Score: Wave: " + highwave + ", Kills: " + highkills;
                PlayerPrefs.SetInt("HighWave", wave);
                PlayerPrefs.SetInt("HighKills", wavekills);
            }
        }
        //if co-op, do scores
        else if (levelname == "CoOpMultiplayer")
        {
            //show restart/quit keys in hud
            hintstext.SetActive(true);
            int highwave = 0;
            int highkills = 0;

            //get old highscores
            if (PlayerPrefs.HasKey("MultiHighWave"))
            {
                highwave = PlayerPrefs.GetInt("MultiHighWave");
                highkills = PlayerPrefs.GetInt("MultiHighKills");
            }
            //show unlucky text if failed to beat old score
            if ((wave < highwave) || (wave == highwave && wavekills <= highkills))
            {
                audiosource.PlayOneShot(losesound);
                endtext.text = "Unlucky!\nScore: Wave:" + wave + ", Kills: " + wavekills + "\nHighScore: Wave: " + highwave + ", Kills: " + highkills;
            }
            //if beat high scores, set new ones, and show win text
            else if ((wave == highwave && wavekills > highkills) || (wave > highwave))
            {
                audiosource.PlayOneShot(winsound);
                endtext.text = "New High Score!\nScore: Wave:" + wave + ", Kills: " + wavekills + "\nOld Score: Wave: " + highwave + ", Kills: " + highkills;
                PlayerPrefs.SetInt("MultiHighWave", wave);
                PlayerPrefs.SetInt("MultiHighKills", wavekills);
            }
        }
        //if multiplayer battle, show winner
        else if (levelname == "BattleMultiplayer" || levelname == "CompetitiveMultiplayer")
        {
            audiosource.PlayOneShot(losesound);
            //show restart/quit keys in hud
            hintstext.SetActive(true);
            string otherplayername = "Blue Player";
            if (player.name == "Blue Player")
            {
                otherplayername = "Green Player";
            }
            //show win text
            endtext.text = otherplayername + " Won!\n" + player.name + " Lost.";
        }
    }
}
