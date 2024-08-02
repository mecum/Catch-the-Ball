using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelsNumber;
    public bool lastLevel;
    private int difficulty;
    [SerializeField] Text LivesCount;
    private int lives;
    [SerializeField] Text ScoreCount;
    private int score;
    private int subScore;

    public Text winScoreText;
    public int winScore;
    [SerializeField] GameObject gameWinScreen;

    private bool isTimeBound = false;
    public float timeLeft;
    [SerializeField] Text timeText;
    [SerializeField] GameObject timeIsOver;

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

        difficulty = GameManager.Instance.difficulties[levelsNumber - 1];

        if (difficulty < 3)
        {
            difficulty += 1;
        }        
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

        if (!lastLevel)
        {
            winScore = 2 * difficulty;
            winScoreText.text = winScore.ToString();
        }        

        if (difficulty == 3)
        {
           isTimeBound = true;
        }
    }

    private void Update()
    {
        if (isTimeBound)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                isTimeBound = false;
                GameWon();
            }

            int timer = Mathf.RoundToInt(timeLeft);
            timeText.gameObject.SetActive(true);
            timeText.text = timer.ToString();
        }        
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

                if (!lastLevel)
                {
                    CheckWin();
                }                
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

        if (difficulty == 3)
        {
            timeIsOver.SetActive(true);
            SaveScore();            
        }   

        SaveDifficulty();
    }

    void GameOver()
    {        
        isGameActive = false;
        gameOverScreen.SetActive(true);
        gameAudio.PlayOneShot(looseClip);
        gameOverParticle.Play();
        difficulty -= 1;
        SaveDifficulty();

        if (lastLevel)
        {
            SaveScore();
        }
    }

    void SaveDifficulty()
    {
        GameManager.Instance.difficulties[levelsNumber - 1] = difficulty;

    }

    public void SaveScore()
    {
        GameManager.Instance.scores[levelsNumber - 1] = score;
    }
}
