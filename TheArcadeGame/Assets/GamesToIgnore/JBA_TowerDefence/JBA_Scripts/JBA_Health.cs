using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBA_Health : MonoBehaviour
{
    public float JBA_hitPoints = 10f;

    public void JBA_TakeDamage(float dmg)
    {
        JBA_hitPoints -= dmg;

        if (JBA_hitPoints <= 0)
        {
            JBA_LevelSpawner.JBA_onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
