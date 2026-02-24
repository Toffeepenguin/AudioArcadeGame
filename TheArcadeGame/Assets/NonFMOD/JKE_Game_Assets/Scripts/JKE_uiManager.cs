using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JKE_uiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    JKE_GameManager gm;
    private void Start()
    {
        gm = JKE_GameManager.Instance;
    }

    private void OnGUI()
    {
        scoreUI.text = gm.intScore();
    }
}
