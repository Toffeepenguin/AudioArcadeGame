using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCH_panalContinue : MonoBehaviour
{
    GameObject inputObj;
    private InputSubscription inputScp;
    private float time = 0f;

    public GameObject volumeControlPanel; //Array used to store all tutorial panels
    // Start is called before the first frame update
    void Awake()
    {
        inputObj = GameObject.Find("SCH_InputSystem");
        inputScp = inputObj.GetComponent<InputSubscription>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + 0.5)
        {
            if (inputScp.SpaceInput)
            {
                Debug.Log("Continue");
                volumeControlPanel.SetActive(true); //Skip to the last page
                gameObject.SetActive(false);
            }
        }
    }
}
