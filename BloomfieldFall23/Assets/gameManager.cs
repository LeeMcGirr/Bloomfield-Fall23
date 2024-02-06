using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("Timer Vars")]
    public float timer;
    public TextMeshProUGUI currentTime;
    public float enemyTimer;
    public float spawnInterval = 2f;
    public float timeLimit = 60f;

    [Header("Player Vars")]
    public GameObject[] myPlayers;
    public float myScore;
    public TextMeshProUGUI scoreText;
    carController myController1;
    carController myController2;

    [Header("Collectibles")]
    public GameObject myEnemy;
    //declare bounding box for my scene
    public Vector2 myXbounds;
    public Vector2 myYbounds;

    [Header("UI/UX Menus")]
    public TextMeshProUGUI startText;
    bool gameStarted;
    public GameObject gameOver;
    public TextMeshProUGUI finalScore;




    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        timer = 0f;
        myController1 = myPlayers[0].GetComponent<carController>();
        myController2 = myPlayers[1].GetComponent<carController>();


        //set the player off on start, so start menu works
        //myPlayers[0].SetActive(false);
        //myPlayers[1].SetActive(false);

        //this for loop does the same thing as the two lines above
        //but it also allows us to easily scale up to 4+ players
        //without writing any new code, thanks to the Array[].Length property
        for(int i = 0; i < myPlayers.Length; i++)
        {
            myPlayers[i].SetActive(false);
        }

        //disabling player move controller- components use .enabled instead of the SetActive() method
        myController1.enabled = false;
        scoreText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //player score can be accessed after game over, so check it every frame
        myScore = myController1.GetScore();

        //Input to check for game start
        if(Input.GetKey(KeyCode.Space)) 
        { StartGame(); }
        //Input to check for game OVER
        else if(timer > timeLimit)
        { EndGame(); }

        //game code - 
        if (gameStarted)
        {
            scoreText.text = myScore.ToString();

            //deltaTime is the amount of time that has passed from one Update() to the next
            timer += Time.deltaTime;

            //Mathf is a class in unity that has a variety of math functions built in
            float timeLeft = Mathf.Round(timeLimit - timer);
            //set the UI timer text to time left
            currentTime.text = timeLeft.ToString();

            //our enemyTimer will be used to spawn red circle prefabs
            enemyTimer += Time.deltaTime;

            //this turns off the player when the time limit is reached
            if (timer > timeLimit)
            {
                myPlayers[0].SetActive(false);
            }

            //this spawns an enemy on a given interval, then resets the timer
            Vector3 targetPos = new Vector3(Random.Range(myXbounds.x, myXbounds.y), Random.Range(myYbounds.x, myYbounds.y), 0);
            if (enemyTimer > spawnInterval)
            {
                enemyTimer = 0f;
                Instantiate(myEnemy, targetPos, Quaternion.identity);
            }
        }
    }

    //called when button is pressed to start the game
    //hide any game-related objects while game is off, turn them on using this function when
    //player chooses to start the game
    public void StartGame()
    {
        //gameObjects use the .SetActive(bool) method to turn on/off

        for (int i = 0; i < myPlayers.Length; i++)
        {
            myPlayers[i].SetActive(true);
        }
        //components use the .enabled property to turn on/off
        myController1.enabled = true;
        scoreText.enabled = true;
        startText.enabled = false;
        gameStarted = true;

        //also disable final score text in case player is restarting a new play after the first
        gameOver.SetActive(false);

    }

    public void EndGame()
    {
        Debug.Log("ended game");

        for (int i = 0; i < myPlayers.Length; i++)
        {
            myPlayers[i].SetActive(false);
        }


        finalScore.text = myScore.ToString();  
        myController1.enabled = false;
        scoreText.enabled = false;
        startText.enabled = false;
        gameOver.SetActive(true);
        gameStarted = false;
        timer = 0f;

    }

    public float GetTime()
    {
        return timer;
    }
}
