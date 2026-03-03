using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JBA_Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform JBA_turretRotationPoint;
    [SerializeField] private LayerMask JBA_enemyMask;
    [SerializeField] private GameObject JBA_bulletPrefab;
    [SerializeField] private Transform JBA_firingPoint;
    [SerializeField] private AudioSource JBA_audioSource;

    [Header("Attributes")]
    [SerializeField] private float JBA_targetingRange = 3f;
    [SerializeField] private float JBA_rotationSpeed = 10f;
    [SerializeField] private float JBA_bps = 1f;

    private Transform JBA_target;
    private float JBA_timeUntilFire;

    private void Update()
    {
        if (JBA_target == null)
        {
            JBA_FindTarget();
            return;
        }
        JBA_RotateTowardsTarget();

        if(!JBA_CheckTargetIsInRange())
        {
            JBA_target = null;
        } else
        {
            JBA_timeUntilFire += Time.deltaTime;

            if (JBA_timeUntilFire > 1f / JBA_bps)
            {
                JBA_Shoot();
                JBA_timeUntilFire = 0f;
            }
        }
    }

    private void JBA_Shoot()
    {
        GameObject JBA_bulletObj = Instantiate(JBA_bulletPrefab, JBA_firingPoint.position, Quaternion.identity);
        JBA_Bullet JBA_bulletScript = JBA_bulletObj.GetComponent<JBA_Bullet>();
        JBA_bulletScript.JBA_SetTarget(JBA_target);
        JBA_audioSource.Play();
    }

    private void JBA_FindTarget()
    {
        RaycastHit2D[] JBA_hits = Physics2D.CircleCastAll(transform.position, JBA_targetingRange, (Vector2)
            transform.position, 0f, JBA_enemyMask);

        if (JBA_hits.Length > 0)
        {
            JBA_target = JBA_hits[0].transform;
        }
    }

    private bool JBA_CheckTargetIsInRange()
    {
        return Vector2.Distance(JBA_target.position, transform.position) <= JBA_targetingRange;
    }

    private void JBA_RotateTowardsTarget()
    {
        float JBA_angle = Mathf.Atan2(JBA_target.position.y - transform.position.y, JBA_target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion JBA_targetRotation = Quaternion.Euler(new Vector3(0f, 0f, JBA_angle));
        JBA_turretRotationPoint.rotation = Quaternion.RotateTowards(JBA_turretRotationPoint.rotation, JBA_targetRotation, JBA_rotationSpeed * Time.deltaTime);
    }

   //private void OnDrawGizmosSelected()
   //{
   //    Handles.color = Color.cyan;
   //    Handles.DrawWireDisc(transform.position, transform.forward, JBA_targetingRange);
   //}
}
