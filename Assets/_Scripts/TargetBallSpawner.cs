using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TargetBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Material[] coloredMaterials;
    private string[] tags = { "Blue", "Green", "Red", "Yellow" };
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

    // Start is called before the first frame update
    void Start()
    {
        targetCountText = targetText.GetComponent<Text>();
        difficulty = GameManager.Instance.difficulty;
        SpawnRandomBall();
        targetAudio = GetComponent<AudioSource>();
    }
    
    void SpawnRandomBall()
    {    
        GetRandomNumber();

        ballPrefab.GetComponent<MeshRenderer>().material = coloredMaterials[colorIndex];
        ballPrefab.gameObject.tag = tags[colorIndex];

        GetTargetCount();        
        
        StartCoroutine("ShowTargetCount");

        target = Instantiate(ballPrefab, transform.position, ballPrefab.transform.rotation);        
    }

    int GetRandomNumber()
    {
        int newIndex = Random.Range(0, coloredMaterials.Length);        

        while (newIndex == colorIndex)
        {
            newIndex = Random.Range(0, coloredMaterials.Length);
        }

        colorIndex = newIndex;

        return colorIndex;
    }

    int GetTargetCount()
    {
        switch(difficulty)
        {
            case 2:
                targetCount = Random.Range(minTargetRange, maxTargetRange + 1);
                break;
            case 3:
                int i = Random.Range(0, 3);
                int[] ranges = { 1, 1, 2 };
                targetCount = ranges[i];
                break;
            default:
                targetCount = Random.Range(minTargetRange, maxTargetRange);
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

        SpawnRandomBall();
    }
}
