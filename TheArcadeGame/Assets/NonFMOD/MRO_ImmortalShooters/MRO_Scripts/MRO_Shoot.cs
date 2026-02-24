using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public float fireRateBoost = 0.2f;
    public float powerupDuration = 5f;

    private AudioManager audioManager;  
    public AudioClip shootClip;         

    private void Start()
    {
       
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        
        if (audioManager != null)
        {
            audioManager.PlaySFX(shootClip, 0.5f); 
        }

        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        nextFireTime = Time.time + fireRate;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(IncreaseFireRate());
        }
    }

    private IEnumerator IncreaseFireRate()
    {
        fireRate -= fireRateBoost;
        yield return new WaitForSeconds(powerupDuration);
        fireRate += fireRateBoost;
    }
}
