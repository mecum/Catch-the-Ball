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
        SpawnRandomBall();
        targetAudio = GetComponent<AudioSource>();
    }
    
    void SpawnRandomBall()
    {    
        GetRandomNumber();

        ballPrefab.GetComponent<MeshRenderer>().material = coloredMaterials[colorIndex];
        ballPrefab.gameObject.tag = tags[colorIndex];

        targetCount = Random.Range(minTargetRange, maxTargetRange);
        
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
