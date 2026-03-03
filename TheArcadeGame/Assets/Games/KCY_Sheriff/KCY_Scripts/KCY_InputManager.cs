using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCY_InputManager : MonoBehaviour
{
    InputSubscription getInput;

    private void Awake()
    {
        getInput = GetComponent<InputSubscription>();
    }
}
