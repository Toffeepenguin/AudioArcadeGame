using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCH_startScreenButtonsControl : MonoBehaviour
{
    public GameObject howToPlayPanel; //Array used to store all tutorial panels
    [SerializeField] private AudioSource minimumVolume;
    [SerializeField] private AudioSource maximumVolume;
    // Start is called before the first frame update
    public void SCH_StartBtn()
    {
        howToPlayPanel.SetActive(true); //Skip to the last page
        Debug.Log("Starting");
    }

    public void SCH_QuitBtn()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit");
    }

    public void SCH_MinimumVolumeTest()
    {
        Debug.Log("minimum");
        minimumVolume.Play();
        maximumVolume.Stop();
    }
    public void SCH_MaximumVolumeTest()
    {
        maximumVolume.volume = 0.2f;
        Debug.Log("maximum");
        maximumVolume.Play();
        minimumVolume.Stop();   
    }
    public void SCH_StartGameplay()
    {
        SceneManager.LoadScene("SCH_DroptheBeatScene");
    }
}

/*References:
 * samyam (2021) Controller and Keyboard Menu Navigation w/ Input System - Unity Tutorial. [video]
 * Available at: https://youtu.be/Hn804Wgr3KE?si=BkNS8SGTHEbVvTfp [Accessed 1 December 2024]
 */