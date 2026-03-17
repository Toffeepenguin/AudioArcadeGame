using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class JRO_Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "JRO_PlayerSprite")
        {
            JRO_GameManager.Score++;
            if (JRO_GameManager.HighScore > JRO_GameManager.Score)
            {
                JRO_GameManager.HighScore++;
            }
            basicplayercontroller.movespeed += 0.2f;
            Destroy(gameObject);
        }
    }
}
