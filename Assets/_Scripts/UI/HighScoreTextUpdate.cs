using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreTextUpdate : MonoBehaviour
{
    [SerializeField] int level;
    private int score;    

    // Start is called before the first frame update
    void Start()
    {
        score = GameManager.Instance.scores[level - 1];

        if (score > 0)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(8, 90, 8, 255);
            GetComponent<TextMeshProUGUI>().text = "High Score: " + score;
        }
    }
}
