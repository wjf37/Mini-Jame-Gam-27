using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI timeText;
    private bool timerRunning = false;
    private GameManager gameManager;
    private string timeSeparator;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive){
            timerRunning = true;
            if (timerRunning == true){
                if (timeRemaining > 0){
                    DisplayTime(timeRemaining);
                    timeRemaining -= Time.deltaTime;
                }
                else{
                    timeRemaining = 0;
                    timerRunning = false;
                    gameManager.GameOver();
                    DisplayTime(timeRemaining);
                }
            }
        }

    }

    void DisplayTime(float timeRemaining){
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        if (seconds<10){
            timeSeparator = ":0";
        }
        else{
            timeSeparator = ":";
        }
        timeText.text = minutes + timeSeparator + seconds;
    }
}
