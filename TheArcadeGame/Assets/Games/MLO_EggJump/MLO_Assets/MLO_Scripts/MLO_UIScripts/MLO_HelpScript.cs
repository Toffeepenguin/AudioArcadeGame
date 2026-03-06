using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MLO_HelpScript : MonoBehaviour
{
    Renderer help_rndr;
    bool help = false;
    bool stop_help = false;
    float lerp_count;
    // Start is called before the first frame update
    void Start()
    {
        help_rndr = GetComponent<Renderer>();
    }

    public void GetHelp()
    {
        help = true;
    }

    public void StopHelp()
    {
        stop_help = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (help)
        {
            lerp_count += Time.deltaTime;
            help_rndr.material.color = new Color(help_rndr.material.color.r, help_rndr.material.color.g, help_rndr.material.color.b, lerp_count);
        }
        if (lerp_count > 1 && help)
        {
            help_rndr.material.color = new Color(help_rndr.material.color.r, help_rndr.material.color.g, help_rndr.material.color.b, 1);
            lerp_count = 0;
            help = false;
        }
        if (stop_help && help_rndr.material.color.a == 0)
        {
            lerp_count = 0;
            stop_help = false;
        }
        if (stop_help)
        {
            lerp_count += Time.deltaTime;
            help_rndr.material.color = new Color(help_rndr.material.color.r, help_rndr.material.color.g, help_rndr.material.color.b, 1 - lerp_count);
        }
        if (lerp_count > 1 && stop_help)
        {
            help_rndr.material.color = new Color(help_rndr.material.color.r, help_rndr.material.color.g, help_rndr.material.color.b, 0);
            lerp_count = 0;
            stop_help = false;
        }
    }
}
