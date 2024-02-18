using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    [SerializeField] GameObject allDoneText;
    private ResourceManager resourceManager;
    private TextMeshProUGUI woodCounterText;
    private TextMeshProUGUI stoneCounterText;
    private TextMeshProUGUI pplCounterText;
    private TextMeshProUGUI inkCounterText;
    private List<int> resourceList = new();
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        woodCounterText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        stoneCounterText = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        pplCounterText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        inkCounterText = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        resourceManager.resourcesUpdated += HandleListUpdated;
    }

    void HandleListUpdated(List<int> passedList)
    {
        resourceList = passedList;
        UpdateUI();
    }

    void UpdateUI()
    {
        int woodCount = resourceList[0];
        int stoneCount = resourceList[1];
        int pplCount = resourceList[2];
        int inkCount = resourceList[3];

        woodCounterText.text = "Collect Wood: "+ woodCount + "/3";
        stoneCounterText.text = "Collect Stone: "+ stoneCount + "/3";
        pplCounterText.text = "Recruit People: "+ pplCount + "/3";
        inkCounterText.text = "Make Charcoal: "+ inkCount + "/3";
        
        if ( woodCount >= 3 && stoneCount >= 3 && pplCount >= 3 && inkCount >= 3 )
        {
            StartCoroutine(AllTasksDone());
        }
    }

    IEnumerator AllTasksDone()
    {
        allDoneText.SetActive(true);
        yield return new WaitForSeconds(3);
        gameManager.GameOver();
    }
}
