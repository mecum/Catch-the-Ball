using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    [SerializeField] AudioClip ballClip;
    private AudioSource gameAudio;

    public bool isFirstCollision = true;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            if (isFirstCollision)
            {
                gameAudio.PlayOneShot(ballClip);
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

        //isGameActive = GameObject.Find("Level Manager").GetComponent<LevelManager>().isGameActive;

        if (GameObject.Find("Level Manager") != null && GameObject.Find("Level Manager").GetComponent<LevelManager>().isGameActive == false)
        {
            Destroy(gameObject);
        }
    }        
}
