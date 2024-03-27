using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //restart işlemleri için, sahneyi tekrar çalıştırma

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public int spawnCount;
    public float spawnWait;
    public float startSpawn;
    public float waveWait;

    public Text scoreText;
    public int score;

    public Text gameOverText;
    public Text restartText;

    public Text quitText;

    private bool gameOver;
    private bool restart;

    public void Update()
    {
        if(restart == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0); //int değer alır build settings ile sahne eklenip sğada yazan rakam alınır
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
                Debug.Log("Oyun Kapandı!");
            }
        }
    }

    IEnumerator SpawnValues()
    {
        yield return new WaitForSeconds(startSpawn);
        while (true)
        {   
            for(int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 0, 10); //Astroidin çerçeve aralığı
                Quaternion spawnRotation = Quaternion.identity;  //new Quaternion();, rotasyon ayarıdır ama prefabda var ek değer belirmeme gerek yok, identitiy rotasyon verne demek

                Instantiate(hazard, spawnPosition, spawnRotation); //Astroid hareket işlemleri

                //Coroutine -- fonk benzer ama değil javadaki threade benzer
                //1. IEnumerator döndürmek zorundadır.
                //2. En az 1 adet yield ifadesi bulunmak zorundadır
                //3. Coroutinler çağrılırken mutlaka StartCoroutine metoduyla çağrılmalıdır.

                yield return new WaitForSeconds(spawnWait); //astrid fırlatma aşamasını saniyede bir , ya da sen ne kadar istersen, yapar.
            }
            yield return new WaitForSeconds(waveWait); //bekleme işlemleri için coroutine kullanılır.
            if(gameOver == true)
            {
                restartText.text = "Press 'R' for Restart";
                quitText.text = "Press 'Q' for Quit";
                restart = true;
                break;
            }
        }
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
   
    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        quitText.text = "";
        gameOver = false;
        restart = false;
        StartCoroutine(SpawnValues());
        
    }

    
}
