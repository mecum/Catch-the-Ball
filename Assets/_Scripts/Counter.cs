using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{ 
    [SerializeField] LevelManager LevelManager;
        
    private string currentTag;
        
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = GameObject.Find("Target Ball(Clone)");
        currentTag = target.tag;

        if (other.CompareTag(currentTag))
        {            
            LevelManager.CalculateCollision();
        }
        else
        {
            LevelManager.DecreaseLives();
        }

        Destroy(other.gameObject);
    }
}
