using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{ 
    public GameManager GameManager;
        
    private string currentTag;

    private void Start()
    {
                
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = GameObject.Find("Target Ball(Clone)");
        currentTag = target.tag;

        if (other.CompareTag(currentTag))
        {            
            GameManager.CalculateCollision();
        }
        else
        {
            GameManager.DecreaseLives();
        }

        Destroy(other.gameObject);
    }
}
