using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetVolume(float level)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(level) * 20f);
    }
}
