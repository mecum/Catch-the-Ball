using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public int score8;

    // Start is called before the first frame update
    void Start()
    {
        score8 = GameManager.Instance.score8;
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
}
