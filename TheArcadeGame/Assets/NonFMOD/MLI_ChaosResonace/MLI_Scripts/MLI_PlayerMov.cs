using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MLI_PlayerMov : MonoBehaviour
{
    [SerializeField] InputSubscription GetInput;
    Rigidbody2D MLI_rb;
    Vector2 MLI_PlayerMovement;

    MLI_PlayerStats MLI_stats;
    MLI_BulletSpawner MLI_weapon;

    float MLI_Timer;

    private void Awake()
    {
        MLI_stats = GetComponent<MLI_PlayerStats>();
        MLI_weapon = GetComponent<MLI_BulletSpawner>();
        MLI_rb = GetComponent<Rigidbody2D>();

        MLI_Timer = MLI_stats.MLI_FIRERATE;
    }

    private void Update()
    {
        //added by Izzy//
        if (GetInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        //////
        if (this.enabled)
        {
            MLI_PlayerMovement = new Vector2(GetInput.NormalizedMovementInput.x, GetInput.NormalizedMovementInput.y);
            MLI_rb.linearVelocity = new Vector2(MLI_PlayerMovement.x, MLI_PlayerMovement.y) * MLI_stats.MLI_SPEED;
            if (GetInput.ShiftInput)
            {
                if (MLI_Timer < MLI_stats.MLI_FIRERATE)
                {
                    MLI_Timer += Time.deltaTime;
                }
                else
                {

                    Vector2 mli_dir = MLI_weapon.AngleToVector2(0);
                    MLI_weapon.SpawnBullet(true, MLI_stats.MLI_BULLET_SPEED, MLI_stats.MLI_BULLET_SIZE, MLI_stats.MLI_BULLET_DAMAGE, mli_dir, Color.white);

                    MLI_Timer = 0;
                }
            }
            else
            {
                MLI_Timer = MLI_stats.MLI_FIRERATE;
            }
        }
        else
        {
            MLI_rb = null;
            MLI_weapon = null;
        }
    }
}
