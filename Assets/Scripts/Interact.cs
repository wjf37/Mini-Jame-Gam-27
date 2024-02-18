using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Resource resource;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && resource != null)
        {
            resource.CollectResource();
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        resource = collider2D.gameObject.GetComponent<Resource>();
    }

    void OnTriggerExit2D()
    {
        resource = null;
    }
}
