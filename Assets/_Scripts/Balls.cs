using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    [SerializeField] AudioClip ballClip;
    
    private bool isFirstCollision = true;
         
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            if (isFirstCollision)
            {                
                AudioManager.Instance.PlaySound(ballClip, transform, 1f);
                isFirstCollision = false;
            }
        }  
    }

    // Update is called once per frame
    void Update()
    { 
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
             

        if (GameObject.Find("Level Manager") != null && GameManager.Instance.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }        
}
