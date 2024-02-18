using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirePit : MonoBehaviour
{
    [SerializeField] Sprite[] firePitSprites;
    [SerializeField] AudioClip lightFire;
    [SerializeField] AudioClip fireExtinguish;
    private ResourceManager resourceManager;
    private TextMeshPro popup;
    private int firePitState; //0=no stone 1=stones no fire 2=fire 3= charcoal
    private SpriteRenderer spriteRenderer;
    private bool inRange;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        firePitState = 0;
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        popup = transform.GetChild(0).GetComponent<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = firePitSprites[firePitState];
        inRange = false;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange) 
        { 
            ChangeState(firePitState); 
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Interact"))
        {
            FirepitPopup(firePitState);
            popup.gameObject.SetActive(true);
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        popup.gameObject.SetActive(false);
        inRange = false;
    }

    private void FirepitPopup(int fPState)
    {
    
        switch (fPState)
        {
            case 0:
                popup.text = "Use 1 stone to make a fire pit";
                break;

            case 1:
                popup.text = "Use 1 wood to make a fire";
                break;

            case 2:
                popup.text = "Put the fire out";
                break;

            case 3:
                popup.text = "Collect charcoal";
                break;

            default:
                Debug.LogWarning("Unknown state");
                break;
        }
        
    }

    private void ChangeState(int fPState)
    {
        switch(fPState)
        {
            case 0:
                if (resourceManager.ModifyResource("stone", -1))
                {
                    firePitState = 1;
                    spriteRenderer.sprite = firePitSprites[firePitState];
                    break;
                }
                else { break; }
            case 1:
                if (resourceManager.ModifyResource("wood", -1))
                {
                    firePitState = 2;
                    spriteRenderer.sprite = firePitSprites[firePitState];
                    audioSource.PlayOneShot(lightFire);
                    break;
                }
                else { break; }
            case 2:
                firePitState = 3;
                spriteRenderer.sprite = firePitSprites[firePitState];
                break;
            case 3:
                resourceManager.ModifyResource("ink", 1);
                firePitState = 1;
                spriteRenderer.sprite = firePitSprites[firePitState];
                audioSource.PlayOneShot(fireExtinguish);
                break;
            default:
                Debug.LogWarning("Unknown state");
                break;    
        }
        FirepitPopup(firePitState);
    }
}
