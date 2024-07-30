using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        if (GameManager.Instance.difficulty < 3)
        {
            GameManager.Instance.difficulty += 1;
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

        winScore = 2 * GameManager.Instance.difficulty;
        winScoreText.text = winScore.ToString();

        if (GameManager.Instance.difficulty == 3)
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
            timeIsOver.SetActive(true);
            SaveScore();            
        }                
    }

    void GameOver()
    {        
        isGameActive = false;
        gameOverScreen.SetActive(true);
        gameAudio.PlayOneShot(looseClip);
        gameOverParticle.Play();
        GameManager.Instance.difficulty -= 1;                
    }

    public void SaveScore()
    {
        GameManager.Instance.score8 = score;
    }
}
