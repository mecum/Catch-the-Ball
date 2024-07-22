using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TargetBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Material[] coloredMaterials;
    
    public float startDelay = 0.5f;
    public GameObject targetText;

    public AudioClip newTargetSound;
    private AudioSource targetAudio;

    public ParticleSystem destroyParticle;

    public int targetCount;
    private string[] tags = { "Blue", "Green", "Red", "Yellow" };
    private GameObject target;
    private Text targetCountText;

    // Start is called before the first frame update
    void Start()
    {
        targetCountText = targetText.GetComponent<Text>();
        SpawnRandomBall();
        targetAudio = GetComponent<AudioSource>();
    }
    
    void SpawnRandomBall()
    {        
        int colorIndex = Random.Range(0, coloredMaterials.Length);
        ballPrefab.GetComponent<MeshRenderer>().material = coloredMaterials[colorIndex];
        ballPrefab.gameObject.tag = tags[colorIndex];

        targetCount = Random.Range(1, 3);
        
        StartCoroutine("ShowTargetCount");

        target = Instantiate(ballPrefab, transform.position, ballPrefab.transform.rotation);        
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
