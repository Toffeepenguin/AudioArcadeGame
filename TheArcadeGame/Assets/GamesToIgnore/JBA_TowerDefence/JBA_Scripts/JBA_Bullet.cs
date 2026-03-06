using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBA_Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D JBA_rb;

    [Header("Attributes")]
    [SerializeField] private float JBA_bulletSpeed = 5f;

    public static float JBA_bulletDamage = 1; 

    private Transform JBA_target;

    public void JBA_SetTarget(Transform _target)
    {
        JBA_target = _target;
    }

    private void Update()
    {
        if (!JBA_target) return;

        Vector2 JBA_direction = (JBA_target.position - transform.position).normalized;

        JBA_rb.linearVelocity = JBA_direction * JBA_bulletSpeed;

        if (JBA_LevelHandler.JBA_BulletClear == true)
        {
            Destroy(gameObject);
            JBA_LevelHandler.JBA_BulletClear = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with " + other.gameObject.name);
        if (other.gameObject.name == "Enemy(Clone)")
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<JBA_Health>().JBA_TakeDamage(JBA_bulletDamage);
            Destroy(gameObject);
        }
    }
}
