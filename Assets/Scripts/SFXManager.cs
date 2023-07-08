using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource firstAudioSource;
    [SerializeField] AudioSource secondAudioSource;

    [SerializeField] AudioClip clickSFX;
    [SerializeField] float clickSFXVolume;

    [SerializeField] AudioClip objectCollectedSFX;
    [SerializeField] float objectCollectedSFXVolume;

    [SerializeField] AudioClip jumpSFX;
    [SerializeField] float jumpSFXVolume;

    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathSFXVolume;

    [SerializeField] AudioClip sceneCompletedSFX;
    [SerializeField] float sceneCompletedSFXVolume;

    [SerializeField] AudioClip sceneFailedSFX;
    [SerializeField] float sceneFailedSFXVolume;

    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void PlayClickSFX()
    {
        firstAudioSource.PlayOneShot(clickSFX, clickSFXVolume * soundManager.GetCurrentSFXVolume());
    }

    public void PlayJumpSFX()
    {
        if (!secondAudioSource.isPlaying)
        {
            secondAudioSource.PlayOneShot(jumpSFX, jumpSFXVolume * soundManager.GetCurrentSFXVolume());
        }
    }

    public void PlayDeathSFX()
    {
        firstAudioSource.PlayOneShot(deathSFX, deathSFXVolume * soundManager.GetCurrentSFXVolume());
    }

    public void PlayObjectCollectedSFX()
    {
        firstAudioSource.PlayOneShot(objectCollectedSFX, objectCollectedSFXVolume * soundManager.GetCurrentSFXVolume());
    }

    public void PlaySceneCompletedSFX()
    {
        firstAudioSource.PlayOneShot(sceneCompletedSFX, sceneCompletedSFXVolume * soundManager.GetCurrentSFXVolume());
    }

    public void PlaySceneFailedSFX()
    {
        firstAudioSource.PlayOneShot(sceneFailedSFX, sceneFailedSFXVolume * soundManager.GetCurrentSFXVolume());
    }
}