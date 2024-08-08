using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LastLevelButtonBehavior : MonoBehaviour
{           
    [SerializeField] int dependency1;
    [SerializeField] int dependency2;

    [SerializeField] Animator fader;

    private int score1;
    private int diff2;

    // Start is called before the first frame update
    void Start()
    {
        score1 = GameManager.Instance.scores[dependency1 - 1];
        diff2 = GameManager.Instance.difficulties[dependency2 - 1];

        if (score1 > 0 || diff2 > 0)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(Fading(name));        
    }

    IEnumerator Fading(string scene)
    {
        fader.SetTrigger("exit");

        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene(scene);
    }
}
