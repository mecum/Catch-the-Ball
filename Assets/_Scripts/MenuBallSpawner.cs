using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ballPrefabs;    
    [SerializeField] float xRange;
    [SerializeField] float zRange;

    private float spawnInterval = 0.5f;    

    private bool isGameActive = true;
        
    void Start()
    {       
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        while (isGameActive == true)
        {
            int ballIndex = Random.Range(0, ballPrefabs.Length);

            float xPos = Random.Range(-xRange, xRange);
            float zPos = Random.Range(-zRange, zRange);

            Vector3 spawnPos = transform.position + new Vector3(xPos, 0, zPos);

            Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);

            yield return new WaitForSeconds(CalculateIntervalTime());
        }
    }

    float CalculateIntervalTime()
    {
        if (spawnInterval < 6.5f)
        {
            spawnInterval = spawnInterval + (0.4f * Time.deltaTime);

            return spawnInterval;
        }
        else
        {
            return 6.5f;
        }
    }
}
