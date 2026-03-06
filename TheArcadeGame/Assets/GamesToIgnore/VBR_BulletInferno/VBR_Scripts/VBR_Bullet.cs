using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_Bullet : MonoBehaviour
{
    //setting lifetime of the bullet to 10 seconds
    private float lifeTime = 10f;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip obstacleHitSound;
    private AudioSource audioSource;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Obstacle":
                //this audio is to prevent bug
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(obstacleHitSound, 0.05f);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;
            case "Enemy":
                VBR_Enemy vBR_Enemy = collision.GetComponent<VBR_Enemy>();
                audioSource.PlayOneShot(enemyHitSound, 0.125f);
                if (vBR_Enemy != null)
                {
                    vBR_Enemy.changeHealth(1f);
                }
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;
        }
        
    }
}
