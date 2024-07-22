using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text LivesCount;
    public Text ScoreCount;
    public GameObject gameOverScreen;    

    public GameObject targetSpawner;    
    public bool isGameActive = true;
    public ParticleSystem gameOverParticle;

    public AudioClip looseClip;
    public AudioClip countClip;
    public AudioClip scoreClip;
    public AudioClip wrongBallClip;
    private AudioSource gameAudio;

    private int lives;
    private int score;
    public int subScore;

    // Start is called before the first frame update
    void Start()
    {            
        lives = 3;        
        LivesCount.text = lives.ToString();

        score = 0;
        ScoreCount.text = score.ToString();

        subScore = 0;

        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }          

    public void CalculateCollision()
    {
        int targetCount = targetSpawner.GetComponent<TargetBallSpawner>().targetCount;

        if (isGameActive)
        {
            if (subScore == (targetCount - 1))
            {
                score += 1;
                gameAudio.PlayOneShot(countClip);
                gameAudio.PlayOneShot(scoreClip);
                ScoreCount.text = score.ToString();
                subScore = 0;

                targetSpawner.GetComponent<TargetBallSpawner>().RespawnTargetBall();
            }
            else
            {
                subScore += 1;
                gameAudio.PlayOneShot(countClip);
            }
        }               
    }

    public void DecreaseLives()
    {
        lives -= 1;

        if (lives == 2)
        {            
            LivesCount.color = Color.yellow;
        }
        else if(lives == 1)
        {
            LivesCount.color = Color.red;
        }
        else if (lives == 0)
        {
            LivesCount.color = Color.black;            
            gameAudio.PlayOneShot(looseClip);
            gameOverParticle.Play();
            isGameActive = false;
            gameOverScreen.SetActive(true);
        }

        gameAudio.PlayOneShot(wrongBallClip);
        LivesCount.text = lives.ToString();
    }
}
