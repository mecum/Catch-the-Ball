using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    private float horizontalInput;
    private float xRange = 7;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);        

        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
    }    
}
