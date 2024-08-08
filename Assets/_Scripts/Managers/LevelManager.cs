using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level settings")]
    [SerializeField] public int levelsNumber;
    [SerializeField] public bool lastLevel;
    private int difficulty;
    [SerializeField] GameObject targetSpawner;

    [Header("HUD")]
    [SerializeField] Text LivesCount;
    public int lives;
    [SerializeField] Text ScoreCount;
    private int score;
    private int subScore;

    [Header("Scores")]
    [SerializeField] GameObject winScoreScreen;
    [SerializeField] Text winScoreText;
    [SerializeField] int winScore1;
    [SerializeField] int winScore2;
    private int winScore;

    [Header("Timebound Objectives")]    
    [SerializeField] public float timeLeft;
    private bool isTimeBound = false;
    [SerializeField] GameObject timeObjectiveScreen;
    [SerializeField] Text timeObjectiveText;
    [SerializeField] Text timeText;    

    [Header("Game Over Screens")]
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] GameObject timeIsOver;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] ParticleSystem gameWonParticle;
    [SerializeField] ParticleSystem gameOverParticle;  

    [Header("Audio")]
    [SerializeField] AudioClip looseClip;
    [SerializeField] AudioClip countClip;
    [SerializeField] AudioClip scoreClip;
    [SerializeField] AudioClip wrongBallClip;    

    private void Awake()
    {
        GameManager.Instance.isGameActive = true;

        if (!lastLevel)
        {
            difficulty = GameManager.Instance.difficulties[levelsNumber - 1];

            if (difficulty < 3)
            {
                difficulty += 1;
            }

            if (difficulty == 3)
            {
                isTimeBound = true;
            }

            if (isTimeBound)
            {
                timeObjectiveScreen.SetActive(true);
                timeObjectiveText.text = timeLeft.ToString();
            }
            else
            {
                winScoreScreen.SetActive(true);
            }
        }        
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
            ManageWinScore();            
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

    void ManageWinScore()
    {    
        switch (difficulty)
        {
            case 1:
                winScore = winScore1;                
                    break;
            case 2:
                winScore = winScore2;                
                break;
            default:
                winScore = 20 * difficulty;
                break;
        }

        winScoreText.text = winScore.ToString();
    }

    public void CalculateCollision()
    {
        int targetCount = targetSpawner.GetComponent<TargetBallSpawner>().targetCount;

        if (GameManager.Instance.isGameActive)
        {
            if (subScore == (targetCount - 1))
            {
                score += 1;
                AudioManager.Instance.PlaySound(countClip, transform, 1f);
                AudioManager.Instance.PlaySound(scoreClip, transform, 1f);
                
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
                AudioManager.Instance.PlaySound(countClip, transform, 1f);                
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
        lives = lives - 1;
                
        AudioManager.Instance.PlaySound(wrongBallClip, transform, 1f);
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
        GameManager.Instance.isGameActive = false;        
        gameWinScreen.SetActive(true);
        gameWonParticle.Play();

        if (difficulty == 3)
        {
            timeIsOver.SetActive(true);
            SaveScore();            
        }   

        SaveDifficulty();
    }

    void GameOver()
    {        
        GameManager.Instance.isGameActive = false;
        gameOverScreen.SetActive(true);
       
        AudioManager.Instance.PlaySound(looseClip, transform, 1f);
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
