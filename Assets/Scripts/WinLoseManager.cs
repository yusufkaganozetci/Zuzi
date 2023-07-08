using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinLoseManager : MonoBehaviour
{
    [SerializeField] GameObject scoreTable;
    private IWinnable iWinnable;
    private SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        iWinnable = GameObject.FindGameObjectWithTag("Winnable").GetComponent<IWinnable>();
    }

    public IEnumerator ManageWinOrLose(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        scoreTable.SetActive(true);
        iWinnable.HandleSituation();
    }

    public void OnPressContinueButton()
    {
        sfxManager.PlayClickSFX();
        iWinnable.OnPressContinue();
    }

}