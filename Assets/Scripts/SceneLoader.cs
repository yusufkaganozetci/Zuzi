using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;

    private void Start()
    {
        Time.timeScale = 1;
        transitionAnimator.SetTrigger("PlayOpening");
    }

    public void LoadScene(int index)
    {
        
        StartCoroutine(LoadSceneWithTransition(0, index));
        //SceneManager.LoadScene(index);
    }

    public IEnumerator LoadSceneWithTransition(float delay, int index)
    {
        
        yield return new WaitForSecondsRealtime(delay);
        //transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.SetTrigger("PlayClosing");
        yield return new WaitForSecondsRealtime(1);
        //yield return new WaitWhile(() => transitionAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == -1203198838);
        SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadScene(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadScene(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadScene(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadScene(6);
        }

    }
}
