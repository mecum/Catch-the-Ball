using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TargetBallSpawner : MonoBehaviour
{
    [SerializeField] bool isSpecial;
    [SerializeField] GameObject[] animalPrefabs;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Material[] coloredMaterials;
    private string[] tags = { "Blue", "Green", "Red", "Yellow", "Orange", "Purple", };
    private int colorIndex;

    [SerializeField] int minTargetRange;
    [SerializeField] int maxTargetRange;
    private int difficulty;

    [SerializeField] GameObject targetText;
    private Text targetCountText;

    [SerializeField] AudioClip newTargetSound;
    private AudioSource targetAudio;

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
        
        if (isSpecial)
        {
            SpawnRandomAnimal();
        }
        else
        {
            SpawnRandomBall();
        }
        
        targetAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameObject.Find("Level Manager").GetComponent<LevelManager>().isGameActive == false)
        {
            StopAllCoroutines();
        }
    }

    void SpawnRandomBall()
    {   
        if (GameObject.Find("Level Manager").GetComponent<LevelManager>().isGameActive)
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
        if (GameObject.Find("Level Manager").GetComponent<LevelManager>().isGameActive)
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
        targetAudio.PlayOneShot(newTargetSound);
        targetText.SetActive(true);        
    }

    public void RespawnTargetBall()
    {
        targetText.SetActive(false);

        destroyParticle.Play();
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
