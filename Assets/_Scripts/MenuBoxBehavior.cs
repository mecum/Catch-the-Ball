using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoxBehavior : MonoBehaviour
{
    [SerializeField] AudioClip ballClip;
    private AudioSource gameAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }    

    private void OnTriggerEnter(Collider other)
    {
        gameAudio.PlayOneShot(ballClip);
    }
}
