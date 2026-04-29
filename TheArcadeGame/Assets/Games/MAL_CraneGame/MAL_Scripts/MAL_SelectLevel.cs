using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAL_SelectLevel : MonoBehaviour
{
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Exit;
    public FMODUnity.StudioEventEmitter Music;
    private static FMODUnity.StudioEventEmitter forDelete;
    private InputSubscription _Input;
    private bool startCount = false;
    private int counter;
    private static int counterforPlays;
    private void Awake()
    {
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        counterforPlays += 1;
        Debug.Log(counterforPlays);
        if (counterforPlays == 1)
        {
            Music.Play();
            forDelete = Music;
            Debug.Log("Played Music");
        }
    }

    public void LoadLevel1()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level1");
        }
    }
    public void LoadLevel2()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level2");
        }
    }
    public void LoadLevel3()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level3");
        }
    }
    public void LoadLevel4()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level4");
        }
    }
    public void LoadLevel5()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level5");
        }
    }
    public void LoadLevel6()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level6");
        }
    }
    public void LoadLevel7()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            SceneManager.LoadScene("MAL_Level7");
        }
    }

    public void LoadSettings()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            Instantiate(Settings);
        }
    }
    public void LoadExit()
    {
        if (!GameObject.Find("MAL_Settings(Clone)") && !GameObject.Find("MAL_Exit(Clone)"))
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            Instantiate(Exit);
            startCount = true;
        }
    }
    private void Update()
    {
        if (_Input.MenuInput)
        {
            if (GameObject.Find("MAL_Exit(Clone)"))
            {
                Destroy(GameObject.Find("MAL_Exit(Clone)"));
                startCount = false;
                counter = 0;
            }

            if (GameObject.Find("MAL_Settings(Clone)"))
            {
                Destroy(GameObject.Find("MAL_Settings(Clone)"));
            }
        }

        if (_Input.ConfirmInput && GameObject.Find("MAL_Exit(Clone)") && counter >= 5)
        {
            MAL_MenuSoundEffects.instance.PlayCraneGameMenu_SFX();
            forDelete.Stop();
            counterforPlays = 0;
            StartCoroutine("stupidwait");
            //Application.Quit();
        } //Another Menu like level complete (enter to esc) or (esc to quit) 
    }

    private IEnumerator stupidwait()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (startCount)
        {
            counter += 1;
        }
    }
}