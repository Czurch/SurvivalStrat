using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string title;
    public string toolTip;
    public Sprite image;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Is called when the object is picked up
    void passiveEffect()
    { 
    }

    // Is called when the object is being used
    void activeEffect()
    { 
    }
}
