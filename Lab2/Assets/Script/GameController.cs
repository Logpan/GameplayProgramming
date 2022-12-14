using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject spwanValues;
    public GameObject playerSpawnPos;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> collectable = new List<GameObject>();
    public GameObject ammo;
    public GameObject player;
    public float StartWait;
    public float WaveWait;
    public int enemyCount;
    public int maxVelocity;
    public int nbLives;
    public int nbAmmo = 30;
    private int iScore;


    public TextMeshProUGUI score;
    public Canvas EndScreen;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI txtAmmo;
    public Button btnRetry;

    void Awake()
    {
        instance = this;
        player = Instantiate(player, playerSpawnPos.transform.position, Quaternion.identity );
        score.gameObject.SetActive(true);
        lives.gameObject.SetActive(true);
        txtAmmo.gameObject.SetActive(true);
        txtAmmo.text = "Ammo : " + nbAmmo;
        score.text = "Score : 0";
        lives.text = "Lives : " + nbLives;
        endScore.text = score.text;
        EndScreen.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnWaves());
    }

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spwanValues.transform.position.x, spwanValues.transform.position.x), spwanValues.transform.position.y, spwanValues.transform.position.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject temp = Instantiate(enemies[Random.Range(0,enemies.Count)], spawnPosition, spawnRotation);
                float rd = Random.Range(1, 0.3f);
                temp.transform.localScale = new Vector3(rd, rd, rd);
                yield return new WaitForSeconds(StartWait);
            }
            yield return new WaitForSeconds(WaveWait);
        }
    }

    public void AddScore(int points)
    {
        iScore += points;
        score.text = "Score : " + iScore;
    }

    public void DestroyPlayer()
    {
        nbLives--;
        if(nbLives < 1)
        {
            Destroy(player);
            Time.timeScale = 0;
            Finished();
        }
        lives.text = "Lives : " + nbLives;
    }

    public void Finished()
    {
        score.gameObject.SetActive(false);
        lives.gameObject.SetActive(false);
        endScore.text = score.text;
        EndScreen.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        txtAmmo.text = "Ammo : " + nbAmmo;
    }
}
