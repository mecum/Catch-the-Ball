using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public GameManager GameManager;
    public int xRange;

    public float startDelay = 2.0f;
    private float spawnInterval;

    private int newIndex;

    private bool isGameActive;
    private int[] lastIndexes = new int[2];

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
            int ballIndex = GetRandomNumber();

            int xPos = Random.Range(-xRange, (xRange + 1));

            Vector3 spawnPos = new Vector3(xPos, transform.position.y, 0);

            Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);            

            yield return new WaitForSeconds(CalculateIntervalTime());
        }        
    }

    int GetRandomNumber()
    {        
        newIndex = Random.Range(0, ballPrefabs.Length);

        if (lastIndexes[0] == newIndex || lastIndexes[1] == newIndex)
        {
            newIndex = Random.Range(0, ballPrefabs.Length);            
        }

        lastIndexes[1] = lastIndexes[0];
        lastIndexes[0] = newIndex;

        return newIndex;
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
