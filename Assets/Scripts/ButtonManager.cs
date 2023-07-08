using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject optionsPopup;
    [SerializeField] GameObject scoreTable;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        sfxSlider.value = soundManager.GetCurrentSFXVolume();
        musicSlider.value = soundManager.GetCurrentMusicVolume();
    }

    public void OnPressLoadCanopus()
    {
        sfxManager.PlayClickSFX();
        FindObjectOfType<SceneLoader>().LoadScene(1);
    }

    public void OnOptionsButtonPressed()
    {
        sfxManager.PlayClickSFX();
        optionsPopup.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnOptionsPopupCloseButtonPressed()
    {
        sfxManager.PlayClickSFX();
        optionsPopup.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeSFXVolume(float sfxVolume)
    {
        soundManager.SetCurrentSFXVolume(sfxVolume);
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        soundManager.SetCurrentMusicVolume(musicVolume);
    }

    public void ShowScoreTable()
    {
        scoreTable.SetActive(true);
    }
}
