using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOver : MonoBehaviour
{
    public static CarryOver Instance { get; private set; }
    public int winCondition;
    // Start is called before the first frame update
    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
