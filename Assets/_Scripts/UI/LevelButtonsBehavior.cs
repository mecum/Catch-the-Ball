using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonsBehavior : MonoBehaviour
{
    [SerializeField] bool firstLevel;    
    [SerializeField] bool doubleDependency;
    [SerializeField] int dependency1;
    [SerializeField] int dependency2;

    [SerializeField] Animator fader;

    private int diff1;
    private int diff2;

    // Start is called before the first frame update
    void Start()
    {
        if (!firstLevel)
        {
            diff1 = GameManager.Instance.difficulties[dependency1 - 1];

            if (doubleDependency)
            {
                diff2 = GameManager.Instance.difficulties[dependency2 - 1];
            }

            if (diff1 > 0 || diff2 > 0)
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
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
