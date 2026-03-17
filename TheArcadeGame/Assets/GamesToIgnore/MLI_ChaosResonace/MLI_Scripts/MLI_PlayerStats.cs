using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MLI_PlayerStats : MonoBehaviour
{
    public float MLI_SPEED = 7.5f;
    public int MLI_PLAYER_HEALTH = 3;
    public int MLI_POINTS;
    public float MLI_POINT_MULT = 1f;
    public bool MLI_POINT_CHANGE = false;

    public float MLI_FIRERATE = 0.5f;
    public float MLI_BULLET_SPEED = 10.0f;
    public float MLI_BULLET_SIZE = 1.2f;
    public int MLI_BULLET_DAMAGE = 1;

    public float MLI_Timer = 0f;
    public void TakeDamage(int damage)
    {
        MLI_PLAYER_HEALTH -= damage;
        if (MLI_PLAYER_HEALTH <= 0)
        {
            // Handle player death
            //Destroy(this.gameObject);
            this.enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject.GetComponent<MLI_BulletSpawner>());
            Object.DontDestroyOnLoad(this);
            SceneManager.LoadScene("MLI_GameOver");
            Time.timeScale = 0f;
        }
    }

    public void Update()
    {
        if (MLI_POINT_CHANGE)
        {
            MLI_Timer += Time.deltaTime;
            if (MLI_Timer <= 2f)
            {
                MLI_POINT_MULT = 1.5f;
            }
            else
            {
                MLI_POINT_CHANGE = false;
            }
        }
        else 
        { 
            MLI_POINT_MULT = 1f;
            MLI_Timer = 0f;
        }

        if (MLI_POINTS >= 1200)
        {
            if (PlayerPrefs.GetInt("MLI_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("MLI_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }

    }
}
