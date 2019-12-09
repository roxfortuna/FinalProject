using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public TimeLeft script;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    public AudioSource musicSource;

    public AudioClip sound1;

    public AudioClip sound2;

    private bool gameOver;
    private bool restart;
    private int score;
    private DestroyByContact destroyContact;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        {
            if (TimeLeft.timeLeft <= 0)
            Time.timeScale = 0;
            gameOver = true;
        }

        
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'F' for Restart";
                restart = true;
                break;
            }
            {
                GameObject destroyContactObject = GameObject.FindGameObjectWithTag("Player");
                if (destroyContactObject != null)
                {
                    destroyContact = destroyContactObject.GetComponent<DestroyByContact>();
                    musicSource.clip = sound1;
                    musicSource.Play();
               
                }
            }
        }
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
      
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You Win! Game Created By Rox Follett.";
            gameOver = true;
            restart = true;
            musicSource.clip = sound2;
            musicSource.Play();
           

        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = sound1;
        musicSource.Play();

    }
}
