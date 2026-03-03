using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VAG_LoadingScreen : MonoBehaviour
{

    [SerializeField] GameObject LoaderUI;
    [SerializeField] Image progressUI;

    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneCoroutine(index));
        PlayerPrefs.SetInt("GameMachineID", index);
        PlayerPrefs.Save();
    }

    public IEnumerator LoadSceneCoroutine(int index)
    {
        progressUI.fillAmount = 0;
        LoaderUI.SetActive(true);

        AsyncOperation SyncOperator = SceneManager.LoadSceneAsync(index);
        SyncOperator.allowSceneActivation = false;
        float progress = 0;
        while (!SyncOperator.isDone)
        {
            progress = Mathf.MoveTowards(progress, SyncOperator.progress, Time.deltaTime);
            progressUI.fillAmount = progress;
            if (progress >= 0.9f)
            {
                progressUI.fillAmount = 1;
                SyncOperator.allowSceneActivation = true;
            }
            yield return null;


        }


    }
}
