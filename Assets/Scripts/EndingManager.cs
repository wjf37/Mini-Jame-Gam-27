using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endingText1;
    [SerializeField] TextMeshProUGUI endingText2;
    [SerializeField] TextMeshProUGUI endingText3;
    [SerializeField] TextMeshProUGUI endingText4;
    [SerializeField] TextMeshProUGUI endingText5;
    private int winCondition;

    // Start is called before the first frame update
    void Start()
    {
        winCondition = CarryOver.Instance.winCondition;
        switch(winCondition)
        {
            case 0:
                endingText1.gameObject.SetActive(true);
                break;
            case 1:
                endingText2.gameObject.SetActive(true);
                break;
            case 2:
                endingText3.gameObject.SetActive(true);
                break;
            case 3:
                endingText4.gameObject.SetActive(true);
                break;
            case 4:
                endingText5.gameObject.SetActive(true);
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
