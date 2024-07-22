using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public GameManager GameManager;
    public int xRange;

    public float startDelay = 2.0f;
    public float spawnInterval;    

    public bool isGameActive;       

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = GameManager.isGameActive;

        StartCoroutine(SpawnBalls());
    }    

    IEnumerator SpawnBalls()
    {
        while (GameManager.isGameActive == true)
        {  
            int ballIndex = Random.Range(0, ballPrefabs.Length);
            int xPos = Random.Range(-xRange, (xRange + 1));

            Vector3 spawnPos = new Vector3(xPos, transform.position.y, 0);

            Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);            

            yield return new WaitForSeconds(CalculateIntervalTime());
        }        
    }

    float CalculateIntervalTime()
    {
        if (spawnInterval > 0.5f)
        {
            spawnInterval = spawnInterval - (0.3f * Time.deltaTime);

            return spawnInterval;
        }
        else
        {
            return 0.5f;
        }        
    }

}
