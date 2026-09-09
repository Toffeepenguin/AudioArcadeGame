using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MLO_TrophyUIScript : MonoBehaviour
{
    bool run;
    TextMeshProUGUI rndr;
    float lerp_count;

    void Start()
    {
        run = false;
        lerp_count = 0f;
        rndr = GetComponent<TextMeshProUGUI>();
        rndr.faceColor = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, 0);
    }

    public void RunUI()
    {
        run = true;
    }

    void Update()
    {
        if (run)
        {
            lerp_count += Time.deltaTime;
            rndr.faceColor = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, 1.5f - lerp_count / 2);
        }
        if (lerp_count > 3 && run)
        {
            rndr.faceColor = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, 0);
            lerp_count = 0;
            run = false;
        }
    }
}
