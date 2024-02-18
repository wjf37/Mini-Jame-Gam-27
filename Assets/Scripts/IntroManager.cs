using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI introText1;
    [SerializeField] TextMeshProUGUI introText2;
    [SerializeField] TextMeshProUGUI introText3;
    private List<TextMeshProUGUI> texts = new();
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        texts.Add(introText1);
        texts.Add(introText2);
        texts.Add(introText3);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            i++;
            if( i == 4 )
            {
                SceneManager.LoadScene(2);
            }
            texts[i-1].gameObject.SetActive(true);
        }
        
    }
}
