using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool gameOverFlag = false;
    [SerializeField] GameObject pauseScreen;
    private bool paused = false;
    private Vector2 spawnRangeTopL =  new(-33f,26f);
    private Vector2 spawnRangeBotR = new(42f,-20f);
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused){
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
                isGameActive = false;
                AudioListener.pause = true;
            }
            else{
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
                isGameActive = true;
                AudioListener.pause = false;
            }
        }
    }

    public void GameOver(){
        isGameActive = false;
        Time.timeScale = 0;
    }

    public void B2Menu(){
        SceneManager.LoadScene(0);
        //add other vars 
    }
}

