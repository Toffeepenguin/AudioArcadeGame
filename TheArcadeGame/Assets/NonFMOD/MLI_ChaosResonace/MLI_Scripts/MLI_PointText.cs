using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MLI_PointText : MonoBehaviour
{
    MLI_PlayerStats mli_points;
    [SerializeField] Text MLI_TextBox;

    private void Awake()
    {
        mli_points = GameObject.Find("MLI_Player").GetComponent<MLI_PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        MLI_TextBox.text = mli_points.MLI_POINTS.ToString();
    }
}
