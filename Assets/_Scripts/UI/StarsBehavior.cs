using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehavior : MonoBehaviour
{
    [SerializeField] int levelsNumber;
    private int difficulty;

    private GameObject[] stars = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i] = gameObject.transform.GetChild(i).gameObject;
        }

        difficulty = GameManager.Instance.difficulties[levelsNumber -1];


        if (difficulty > 0)
        {
            ShowStars();
        }
    }

    void ShowStars()
    {
        for (int i = 0; i < (difficulty); i++)
        {
            stars[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        difficulty = GameManager.Instance.difficulties[levelsNumber - 1];
    }
}
