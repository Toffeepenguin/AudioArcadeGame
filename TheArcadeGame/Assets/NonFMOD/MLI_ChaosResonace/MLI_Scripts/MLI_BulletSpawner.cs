using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_BulletSpawner : MonoBehaviour
{
    public GameObject MLI_BulletPrefab;
    public Transform MLI_SpawnPoint;

    public Vector2 AngleToVector2(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        Vector2 Out = new Vector2(y, x).normalized;
        return Out;
    }

    public void SpawnBullet(bool MLI_player_fired, float MLI_speed, float MLI_size, int MLI_damage, Vector2 MLI_dir, Color MLI_bullet_colour)
    {
        if (MLI_BulletPrefab == null || MLI_SpawnPoint == null)
        {
            Debug.LogWarning("Bullet prefab or Spawn point is not assigned.");
            return;
        }

        GameObject MLI_bullet = Instantiate(MLI_BulletPrefab, MLI_SpawnPoint.position, MLI_SpawnPoint.rotation);

        MLI_BulletLogic mli_bulletLogic = MLI_bullet.GetComponent<MLI_BulletLogic>();
        if (mli_bulletLogic != null)
        {
            mli_bulletLogic.MLI_Damage = MLI_damage;
            mli_bulletLogic.MLI_bullet_speed = MLI_speed;
            mli_bulletLogic.MLI_bullet_size = MLI_size;
            mli_bulletLogic.MLI_bullet_dir = MLI_dir;
            mli_bulletLogic.MLI_player_fired = MLI_player_fired;
            mli_bulletLogic.MLI_colour = MLI_bullet_colour;
            mli_bulletLogic.UpdateValues();
        }
    }
}
