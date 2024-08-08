using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TargetBallSpawner : MonoBehaviour
{
    [Header("Normal game objects")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Material[] coloredMaterials;
    private string[] tags = { "Blue", "Green", "Red", "Yellow", "Orange", "Purple", };
    private int colorIndex;

    [Header("Cats level objects")]
    [SerializeField] bool isSpecial;
    [SerializeField] GameObject[] animalPrefabs;

    [Header("Utilities")]
    [SerializeField] int minTargetRange;
    [SerializeField] int maxTargetRange;
    private int difficulty;    

    [Header("Effects and UI")]
    [SerializeField] GameObject targetText;
    private Text targetCountText;

    [SerializeField] AudioClip newTargetSound;
    [SerializeField] AudioClip destroySound;    

    [SerializeField] ParticleSystem destroyParticle;

    public int targetCount { get; private set; }
        
    private GameObject target;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        targetCountText = targetText.GetComponent<Text>();
        level = GameObject.Find("Level Manager").GetComponent<LevelManager>().levelsNumber;

        if (GameObject.Find("Level Manager").GetComponent<LevelManager>().lastLevel)
        {
            difficulty = 3;
        }
        else
        {
            difficulty = GameManager.Instance.difficulties[level - 1];
        }

        StartSpawn();
    }

    private void Update()
    {   
        if (GameManager.Instance.isGameActive == false)
        {
            StopAllCoroutines();
        }
    }

    public void StartSpawn()
    {
        if (isSpecial)
        {
            SpawnRandomAnimal();
        }
        else
        {
            SpawnRandomBall();
        }
    }

    void SpawnRandomBall()
    {   
        if (GameManager.Instance.isGameActive)
        {
            GetRandomNumber(coloredMaterials.Length);

            ballPrefab.GetComponent<MeshRenderer>().material = coloredMaterials[colorIndex];
            ballPrefab.gameObject.tag = tags[colorIndex];

            GetTargetCount();

            StartCoroutine("ShowTargetCount");

            target = Instantiate(ballPrefab, transform.position, ballPrefab.transform.rotation);
        }       
    }

    void SpawnRandomAnimal()
    {
        if (GameManager.Instance.isGameActive)
        {
            GetRandomNumber(2);

            animalPrefabs[colorIndex].gameObject.tag = tags[colorIndex];

            GetTargetCount();

            StartCoroutine("ShowTargetCount");

            target = Instantiate(animalPrefabs[colorIndex], transform.position, animalPrefabs[colorIndex].transform.rotation);
            target.name = "Target Ball(Clone)";
        }
    }

    int GetRandomNumber(int range)
    {
        int newIndex = Random.Range(0, range);        

        while (newIndex == colorIndex)
        {
            newIndex = Random.Range(0, range);
        }

        colorIndex = newIndex;

        return colorIndex;
    }

    int GetTargetCount()
    {
        switch(difficulty)
        {
            case 0:
                targetCount = Random.Range(minTargetRange, maxTargetRange);
                break;
            case 1:
                targetCount = Random.Range(minTargetRange, maxTargetRange + 1);
                break;
            default:                
                int i = Random.Range(0, 3);
                int[] ranges = { 1, 1, 2 };
                targetCount = ranges[i];
                break;
        }        

        return targetCount;
    }

    IEnumerator ShowTargetCount()
    {
        targetCountText.text = targetCount.ToString();
        yield return new WaitForSeconds(1.2f);
        
        AudioManager.Instance.PlaySound(newTargetSound, transform, 1f);
        targetText.SetActive(true);        
    }

    public void RespawnTargetBall()
    {
        targetText.SetActive(false);

        destroyParticle.Play();
        AudioManager.Instance.PlaySound(destroySound, transform, 1f);
        Destroy(target);

        if (isSpecial)
        {
            SpawnRandomAnimal();
        }
        else
        {
            SpawnRandomBall();
        }        
    }
}
