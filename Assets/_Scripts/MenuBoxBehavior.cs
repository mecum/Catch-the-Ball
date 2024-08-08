using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoxBehavior : MonoBehaviour
{
    [SerializeField] AudioClip ballClip;
    
    private void OnTriggerEnter(Collider other)
    {        
        AudioManager.Instance.PlaySound(ballClip, transform, 1f);
    }
}
