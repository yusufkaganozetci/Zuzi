
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicPlayerAudioSource;

    private float sfxVolume = 1;
    private float musicVolume = 1;
    
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SoundManager");
        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public float GetCurrentSFXVolume()
    {
        return this.sfxVolume;
    }

    public float GetCurrentMusicVolume()
    {
        return this.musicVolume;
    }

    public void SetCurrentSFXVolume(float newSFXVolume)
    {
        this.sfxVolume = newSFXVolume;
    }

    public void SetCurrentMusicVolume(float newMusicVolume)
    {
        this.musicVolume = newMusicVolume;
        musicPlayerAudioSource.volume = newMusicVolume;
    }

}