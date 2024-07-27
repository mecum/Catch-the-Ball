using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{        
    // Update is called once per frame
    void Update()
    { 
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }        
}
