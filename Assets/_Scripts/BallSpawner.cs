using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ballPrefabs;
    [SerializeField] LevelManager LevelManager;
    [SerializeField] int xRange;
        
    private float spawnInterval = 1.5f;

    private int newIndex;

    public bool isGameActive;
    private int[] lastIndexes = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = LevelManager.isGameActive;

        StartCoroutine(SpawnBalls());
    }

    private void Update()
    {
        isGameActive = LevelManager.isGameActive;
    }

    IEnumerator SpawnBalls()
    {
        while (isGameActive == true)
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
