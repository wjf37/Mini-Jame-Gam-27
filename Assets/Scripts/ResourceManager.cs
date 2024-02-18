using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int woodCounter;
    public int WoodCounter { get; private set;}
    private int stoneCounter;
    public int StoneCounter { get; private set; }
    private int pplCounter;
    public int PplCounter { get; private set; }
    private int inkCounter;
    public int InkCounter { get; private set; }
    public List<int> resourceList = new(); 
    [SerializeField] GameObject noResourceText;
    [SerializeField] AudioClip collectSound;
    private AudioSource audioSource;    
    public GameObject plusOnePrefab;
    public delegate void ResourcesUpdatedDelegate(List<int> resourceList);
    public event ResourcesUpdatedDelegate resourcesUpdated;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        woodCounter = stoneCounter = pplCounter = inkCounter = 0;
        resourceList.Add(woodCounter);
        resourceList.Add(stoneCounter);
        resourceList.Add(pplCounter);
        resourceList.Add(inkCounter);
    }

    public bool ModifyResource(string resource, int amount)
    {
        bool  modifySuccess = false;
        switch (resource)
        {
            case "wood":
                if (MinusCheck(woodCounter, amount)) { return modifySuccess; }
                woodCounter += amount;
                UpdateResourceList(0, woodCounter);
                return modifySuccess = true;
            case "stone":
                if (MinusCheck(stoneCounter, amount)) { return modifySuccess;}
                stoneCounter += amount;
                UpdateResourceList(1, stoneCounter);
                return modifySuccess = true;
            case "ppl":
                if (MinusCheck(pplCounter, amount)) { return modifySuccess;}
                pplCounter += amount;
                UpdateResourceList(2, pplCounter);
                return modifySuccess = true;
            case "ink":
                if (MinusCheck(inkCounter, amount)) { return modifySuccess;}
                inkCounter += amount;
                UpdateResourceList(3, inkCounter);
                return modifySuccess = true;
            default:
                Debug.LogWarning("Unknown resource type");
                return modifySuccess;
        }

        // Optionally: Check if the resource amount doesn't go below zero
        // and handle any other resource-specific logic.
    }

    private bool MinusCheck(int resource, int amount){
        //if taking away the amount from the resource is enough to get it into minus, break from case statement
        bool isMinus =false;
        if ( resource<Mathf.Abs(amount) && amount<0 )
        {
            StartCoroutine(ShowNoResourceText());
            isMinus = true;
        }
        return isMinus;
    }

    IEnumerator ShowNoResourceText()
    {
        noResourceText.SetActive(true);
        yield return new WaitForSeconds(3);
        noResourceText.SetActive(false);
    }

    public void AddResourcePopup(Vector3 pos)
    {
        audioSource.PlayOneShot(collectSound,0.7f);
        StartCoroutine(TimedResourcePopup(pos));
    }

    IEnumerator TimedResourcePopup(Vector3 pos)
    {
        GameObject popup = Instantiate(plusOnePrefab, pos, plusOnePrefab.transform.rotation);
        yield return new WaitForSeconds(1);
        Destroy(popup);
    }

    private void UpdateResourceList(int index, int counter)
    {
        resourceList[index] = counter;
        resourcesUpdated?.Invoke(resourceList);
    }
}
