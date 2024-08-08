using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void Awake()
    {
        GameManager.Instance.PauseGame();        
    }

    public void StartGame()
    {
        GameManager.Instance.UnpauseGame();              
    }
}
