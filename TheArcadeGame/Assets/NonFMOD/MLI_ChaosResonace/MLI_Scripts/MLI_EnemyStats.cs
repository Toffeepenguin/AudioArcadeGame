using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_EnemyStats : MonoBehaviour
{

    private MLI_PlayerStats mli_points;

    public float MLI_SPEED = 7.5f;
    public int MLI_ENEMY_HEALTH = 3;
    public float MLI_POINT_VALUE = 10f;

    public float MLI_FIRERATE = 0.5f;
    public float MLI_BULLET_SPEED = 10.0f;
    public int MLI_BULLET_DAMAGE = 1;
    public float MLI_BULLET_SIZE = 0.3f;

    void Start()
    {
        GameObject player = GameObject.Find("MLI_Player");
        if (player != null)
        {
            mli_points = player.GetComponent<MLI_PlayerStats>();
            if (mli_points == null)
            {
                Debug.LogError("MLI_PlayerStats component not found on MLI_Player!");
            }
        }
        else
        {
            Debug.LogError("MLI_Player GameObject not found in the scene!");
        }
    }
    
    public void TakeDamage(int damage)
    {
        MLI_ENEMY_HEALTH -= damage;
        if (MLI_ENEMY_HEALTH <= 0)
        {
            // Handle Enemy death
            Destroy(this.gameObject);
            if (mli_points != null)
            {
                mli_points.MLI_POINTS += Mathf.RoundToInt(MLI_POINT_VALUE * mli_points.MLI_POINT_MULT);
                mli_points.MLI_POINT_CHANGE = true;
                mli_points.MLI_Timer = 0f;
            }
            else
            {
                Debug.LogWarning("Player stats reference is missing; unable to add points!");
            }
        }
    }
}
