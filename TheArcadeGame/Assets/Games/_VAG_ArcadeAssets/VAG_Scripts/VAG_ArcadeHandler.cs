using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VAG_ArcadeHandler : MonoBehaviour
{
    BoxCollider col;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject ArcadeUI;
    [SerializeField] GameObject ArcadeLockedUI;
    [SerializeField] Text TrophyTextReq;

    [SerializeField] int RequiredAmountToUnlock = 0;

    bool Unlocked;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;


        TrophyTextReq.text = (RequiredAmountToUnlock - VAG_TROPHIES.ActiveTrophies).ToString();

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 3f)
        {
            if (VAG_TROPHIES.ActiveTrophies >= RequiredAmountToUnlock)
            {
                ArcadeUI.SetActive(true);
                ArcadeLockedUI.SetActive(false);
            }
            else
            {
                ArcadeUI.SetActive(false);
                ArcadeLockedUI.SetActive(true);
            }
            
        }
        else
        {
            ArcadeUI.SetActive(false);
            ArcadeLockedUI.SetActive(false);
        }


        if (!Unlocked && VAG_TROPHIES.ActiveTrophies >= RequiredAmountToUnlock)
        {
            Unlocked = true;
            col.enabled = true;
        }

    }

   








}
