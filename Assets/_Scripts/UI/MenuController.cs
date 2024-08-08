using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;

    [SerializeField] Animator fader;

    public void LoadNextScene()
    {
        StartCoroutine(Fading(SceneManager.GetActiveScene().buildIndex + 1));        
    }

    public void ReloadThisScene()
    {
        StartCoroutine(Fading(SceneManager.GetActiveScene().buildIndex));        
    }

    public void LoadLevelMenu()
    {
        StartCoroutine(Fading(1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Fading(0));        
    }        

    public void CloseGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void PlaySoundEffect()
    {
        AudioManager.Instance.PlaySound(buttonSound, transform, 1f);
    }

    IEnumerator Fading(int scene)
    {
        fader.SetTrigger("exit");

        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene(scene);
    }
}
