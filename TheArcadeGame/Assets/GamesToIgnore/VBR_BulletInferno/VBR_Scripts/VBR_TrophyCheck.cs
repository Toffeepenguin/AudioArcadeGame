using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_TrophyCheck : MonoBehaviour
{
    public static Boolean VBR_TrophyGet = false;

    public void VBR_TrophyGetSet(Boolean check)
    {
        VBR_TrophyGet=check;
        Debug.Log(VBR_TrophyGet);
    }
}
