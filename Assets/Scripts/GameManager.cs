using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool gameOverFlag = false;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject[] resourcePrefabs;
    [SerializeField] int mapResourceNum = 30;
    private float resourceSpace = 8f;
    private bool paused = false;
    private Vector2 spawnRangeTopL =  new(-33f,26f);
    private Vector2 spawnRangeBotR = new(42f,-20f);
    // Start is called before the first frame update
    void Start()
    {
        SpawnResources();
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

    private void SpawnResources()
    {
        //get a position in spawn range and check if other resources are too around before spawning
        //if no other resources in range then spawn. else get another pos.
        //spawn by calling ChooseResource() to generate the index. Hut has lower spawn rate. 
        float checkedAgain = 0f; //if the function can't put a resource down in 10 tries then there is probably no suitable space and it should break out.
        for (int i = 0; i<mapResourceNum; i++)
        {
            Vector2 randomPos = PosGen();
            if (CheckProx(randomPos))
            {
                Instantiate(resourcePrefabs[ChooseResource()]);
            }
            else
            {
                i--;
                checkedAgain++;
            }

            if (checkedAgain>10)
            {
                i = 100;
            }
        }
    }

    private Vector2 PosGen()
    {
        Vector2 newPos = new(Random.Range(spawnRangeTopL.x, spawnRangeBotR.x), Random.Range(spawnRangeTopL.y, spawnRangeBotR.y)); 
        
        return newPos;
    }

    private int ChooseResource()
    {
        //tree,stone = same chance. hut = 10% chance 0 = tree, 1 = stone, 2 = hut
        int index = 0;
        if (Random.Range(0f,1f)<10)
        {
            index = 2;
        }
        else
        {
            index = Random.Range(0,2);
        }
        return index;
    }

    private bool CheckProx(Vector2 position)
    {   
        //have a raycast that checks for other resources in range
        //generates a vector2 direction according to every x degrees and check for a raycast hit for each
        //if hit, break out of loop and send true in range
        //else send false not in range
        bool inRange = false;
        float deg = 0f;
        float degStep = 10f;
        do
        {
            RaycastHit2D hit = Physics2D.Raycast(position, DegToVec2(deg), resourceSpace); //resourceSpace = radius to check for resources in
            if (hit.collider !=null)
            {
                inRange = true;
                break;
            }
            deg += degStep;
        } while (deg<=360);

        return inRange;
    }

    private Vector2 DegToVec2(float deg)
    {
        float radians = deg / 57.29578f;

        Vector2 dirVec = new Vector2((float)Mathf.Cos(radians), (float)Mathf.Sin(radians));

        return dirVec;
    }
}

