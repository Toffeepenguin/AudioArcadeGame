using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCH_defaultButtonSelect : MonoBehaviour
{
    public Button defaultButton;
    // Start is called before the first frame update
    void Awake()
    {
        defaultButton.Select();
    }
}
