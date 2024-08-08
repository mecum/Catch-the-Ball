using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

public class UsualBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ballPrefabs;
    [SerializeField] LevelManager LevelManager;
    [SerializeField] int xRange;
    [SerializeField] float spawnInterval;    

    private int newIndex;

    private bool isGameActive;
    private int[] lastIndexes;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = GameManager.Instance.isGameActive;
        lastIndexes = new int[ballPrefabs.Length - 1];

        StartCoroutine(SpawnBalls());
    }

    private void Update()
    {
        isGameActive = GameManager.Instance.isGameActive;
    }

    IEnumerator SpawnBalls()
    {
        while (isGameActive == true)
        {
            int ballIndex = GetRandomNumber();

            int xPos = UnityEngine.Random.Range(-xRange, (xRange + 1));

            Vector3 spawnPos = new Vector3(xPos, transform.position.y, 0);

            Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    int GetRandomNumber()
    {
        newIndex = UnityEngine.Random.Range(0, ballPrefabs.Length);

        while (Array.Exists(lastIndexes, element => element == newIndex))
        {
            newIndex = UnityEngine.Random.Range(0, ballPrefabs.Length);
        } 
        
        for (int i = lastIndexes.Length - 1; i > 0; i--)
        {
            lastIndexes[i] = lastIndexes[i-1];
        }
        
        lastIndexes[0] = newIndex;

        return newIndex;
    }
}
