using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMA_TriggerPoint : MonoBehaviour
{
    public GameObject uiGameobject;
    private void Start()
    {
        uiGameobject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Finish" )
        {
            uiGameobject.SetActive(true);
        }
    }
}
