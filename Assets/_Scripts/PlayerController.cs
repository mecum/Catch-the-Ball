using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LevelManager LevelManager;
    [SerializeField] float speed = 15;
    private float horizontalInput;
    private float xRange = 7;
    private bool isGameActive;
    private string currentTag;

    private void Start()
    {
        isGameActive = LevelManager.isGameActive;
    }

    private void Update()
    {
        isGameActive = LevelManager.isGameActive;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        while (isGameActive)
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

            transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        }
    }

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

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (isGameActive)
        {
            transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        }
    }

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
