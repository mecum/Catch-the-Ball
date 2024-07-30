using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public int score8;

    public GameObject[] stars;
    public int diff;

    // Start is called before the first frame update
    void Start()
    {
        score8 = GameManager.Instance.score8;
        diff = GameManager.Instance.difficulty;
        if (diff > 0)
        {
            ShowStars();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (score8 > 0)
        {
            levelText.color = new Color32(8, 90, 8, 255);
            levelText.text = "High Score: " + score8;
        }
    } 
    
    void ShowStars()
    {
        for (int i = 0; i < (diff); i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}