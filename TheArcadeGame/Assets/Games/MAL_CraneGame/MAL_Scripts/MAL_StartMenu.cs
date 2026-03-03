using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class MAL_StartMenu : MonoBehaviour
{
    public Transform Bg;
    private Transform Menu;
    private InputSubscription _Input;
    private bool does = false;

    private float progress = 0;

    private float timePassed = 0;
    private void Awake()
    {
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        Menu = Bg;
    }
    // Update is called once per frame
    void Update()
    {
        if (_Input.SpaceInput && SceneManager.GetActiveScene().name == "MAL_Menu") //Change from Menu scene
        {
            does = true;
        }
        if (does)
        {
            //Debug.Log(timePassed);
            /*Bg.transform.position = Vector3.Lerp(Menu.transform.position, new Vector3(0, 0.5f, 0), timePassed / 160);
            Bg.transform.localScale = Vector3.Lerp(Menu.transform.localScale, new Vector3(9, 10, 1), timePassed/160);*/
            Bg.transform.position = Vector3.Lerp(Menu.transform.position, new Vector3(0, 0.5f, 0), Time.deltaTime * 1);
            Bg.transform.localScale = Vector3.Lerp(Menu.transform.localScale, new Vector3(9, 10, 1),Time.deltaTime * 1 );
            timePassed += Time.deltaTime;
            if (timePassed > 2.5f)
            {
                timePassed = 0;
                SceneManager.LoadScene("MAL_Select");
            }
        }
        if (_Input.SpaceInput && SceneManager.GetActiveScene().name.Contains("Level"))
        {
            SceneManager.LoadScene("MAL_Select");
        }
    }
}
