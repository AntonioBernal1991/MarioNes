using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameHUD : MonoBehaviour
{
    public float timeRemaining = 300;
    public bool timerIsRunning = false;
    public Mario mario;

    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textWorld;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textLives;

    public int scoreCounter = 0;
    public int coinCounter = 0;

 
    void Start()
    {  
        timerIsRunning = true;
        textScore.text = "0";
        textCoins.text = "0";
    }


    void Update()
    {
        print(scoreCounter);
        UpdateLives();
        CountTimer();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Adds 100 points after killing an Enemy.
        if (collision.transform.CompareTag("Enemy"))
        {

            Vector3 targetDir = this.transform.position - collision.transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);
            if (angle < 50.0f)
            {
                scoreCounter += 100;
             
            }

        }

        //Adds 100 points after killing a Koopa.
        if (collision.transform.CompareTag("Koopa"))
        {
            Vector3 targetDir = this.transform.position - collision.transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);
            if (angle < 50.0f && collision.gameObject.GetComponent<DetectorCollision>().isHit == false)
            {
                collision.gameObject.GetComponent<DetectorCollision>().isHit = true;
                scoreCounter += 100;
            

            }




        }

        //Adds 100 points after breaking a Block.
        if (collision.transform.CompareTag("BreakBlock") && this.GetComponent<Mario>().isBig == true)
        {
            Vector3 targetDir1 = this.transform.position - collision.transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            if (angle1 > 160f )
            {              
                scoreCounter += 100;       
            }
        }

        //Adds 100 points after hitting  a Surprise Box.
        if (collision.transform.CompareTag("SurpiseBoxM") )
        {
            Vector3 targetDir1 = this.transform.position - collision.transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            if (angle1 > 160f && collision.gameObject.GetComponent<DetectorCollision>().isHit == false)
            {
                collision.gameObject.GetComponent<DetectorCollision>().isHit = true;
                scoreCounter += 100;
             
            }
        }

        //Adds 100 points after hitting  a Surprise Box and adds a coin.
        if (collision.transform.CompareTag("SurpiseBox") )
        {
            Vector3 targetDir1 = this.transform.position - collision.transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            if (angle1 > 160f && collision.gameObject.GetComponent<DetectorCollision>().isHit == false)
            {
                collision.gameObject.GetComponent<DetectorCollision>().isHit = true;
                scoreCounter += 100;
                coinCounter++;
             

            }
        }
    }
    //Time countdown
    public void CountTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                textTime.text = " " + Mathf.Round(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    //Updates Mario´s lives;
    public void UpdateLives()
    {
        textLives.text = "" + mario.lives;
        textScore.text = " " + scoreCounter;
        textCoins.text = "" + coinCounter;
    }
}
