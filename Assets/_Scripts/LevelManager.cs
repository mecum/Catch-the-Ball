using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Text LivesCount;
    private int lives;
    [SerializeField] Text ScoreCount;
    private int score;
    private int subScore;

    public Text winScoreText;
    public int winScore;
    [SerializeField] GameObject gameWinScreen;

    [SerializeField] GameObject gameOverScreen;
    public bool isGameActive {get; private set;}
    [SerializeField] ParticleSystem gameOverParticle;

    [SerializeField] GameObject targetSpawner;    

    [SerializeField] AudioClip looseClip;
    [SerializeField] AudioClip countClip;
    [SerializeField] AudioClip scoreClip;
    [SerializeField] AudioClip wrongBallClip;
    private AudioSource gameAudio;

    private void Awake()
    {
        isGameActive = true;
        GameManager.Instance.difficulty += 1;
        gameAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {  
        lives = 3;
        LivesCount.text = lives.ToString();

        score = 0;
        ScoreCount.text = score.ToString();

        subScore = 0;

        winScore = 3 * GameManager.Instance.difficulty;
        winScoreText.text = winScore.ToString();
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
                CheckWin();
            }
            else
            {
                subScore += 1;
                gameAudio.PlayOneShot(countClip);
            }
        }
    }

    void CheckWin()
    {
        if (score == winScore)
        {
            GameWon();
        }
    }

    public void DecreaseLives()
    {
        lives -= 1;

        gameAudio.PlayOneShot(wrongBallClip);
        LivesCount.text = lives.ToString();

        if (lives == 2)
        {
            LivesCount.color = Color.yellow;
        }
        else if (lives == 1)
        {
            LivesCount.color = Color.red;
        }
        else if (lives == 0)
        {
            LivesCount.color = Color.black;
            GameOver();
        }        
    }

    void GameWon()
    {
        isGameActive = false;        
        gameWinScreen.SetActive(true);

        if (GameManager.Instance.difficulty == 3)
        {
            SaveScore();
        }
        else
        {
            GameManager.Instance.difficulty += 1;
        }        
    }

    void GameOver()
    {
        gameAudio.PlayOneShot(looseClip);
        gameOverParticle.Play();
        isGameActive = false;
        gameOverScreen.SetActive(true);
        SaveScore();
    }

    public void SaveScore()
    {
        GameManager.Instance.score8 = score;
    }
}
