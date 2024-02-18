using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutResource : Resource
{
    private ResourceManager resourceManager;
    

    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    public override void CollectResource()
    {
        Destroy(gameObject);
        resourceManager.AddResourcePopup(transform.position);
        resourceManager.ModifyResource("ppl", 1);

    }
}
