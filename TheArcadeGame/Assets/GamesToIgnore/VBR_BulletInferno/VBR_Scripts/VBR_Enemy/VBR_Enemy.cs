using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VBR_Enemy : MonoBehaviour
{
    public Transform player;
    private VBR_UiManager uiManager;

    [SerializeField] private VBR_Flash_Effect enemyFlash;
    [SerializeField] private GameObject smokeEffectPrefab;

    [Header("Movement Speed Parameters")]
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float timeSpeedMultiplier = 0.05f;
    [SerializeField] private float maxSpeed = 5f;

    [Header("Enemy Health Parameters")]
    [SerializeField] private float enemyHealth = 3f;

    [Header("Enemy Score Parameters")]
    [SerializeField] private int enemyScore = 100;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip itemDropSound;

    [Header("Item drop parameters")]
    [SerializeField] private GameObject smallHealthUp;
    [SerializeField] private GameObject bigHealthUp;
    [SerializeField] private GameObject attackSpeedUp;

    [Header("Item Spawn-in particle effects parameters")]
    [SerializeField] private GameObject spawnEffectR;
    [SerializeField] private GameObject spawnEffectO;
    [SerializeField] private GameObject spawnEffectB;


    private AudioSource audioSource;

    private float spawnTime;

    private Animator enemyAnimator;
    private bool isDead = false;
    private bool toggleEffect = false;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<VBR_Player>().transform;
        uiManager = GameObject.Find("Ui").GetComponent<VBR_UiManager>();
        audioSource = GetComponent<AudioSource>();
        spawnTime = Time.time;
        enemyAnimator.speed = 0;
    }
    private void FixedUpdate()
    {

        // Update the enemy's speed based on time alive
        float moveSpeed = timeSpeedScale();

        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards the player
        // Check if the enemy can move
        if (!isDead)
        {
            transform.position += direction * moveSpeed * Time.fixedDeltaTime;
            enemyAnimationHandler(direction.x, direction.y);
        }
        else
        {
            enemyDeathHandler();
        }
        
    }
    private float timeSpeedScale()
    {
        float timeAlive = Time.time - spawnTime;
        return Mathf.Min(enemySpeed + (timeAlive * timeSpeedMultiplier), maxSpeed);
    }

    private string GetEnemyMovementDirection(float x, float y)
    {
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        if (angle >= -45f && angle < 45f)
            return "Right";
        else if (angle >= 45f && angle < 135f)
            return "Up";
        else if ((angle >= 135f && angle <= 180f) || (angle >= -180f && angle < -135f))
            return "Left";
        else
            return "Down";
    }
    private void enemyAnimationHandler(float x, float y)
    {
        //
        //0 - down
        //1 - Up
        //2 - Left
        //3 - Right
        //
        string direction = GetEnemyMovementDirection(x, y);
        switch (direction)
        {
            case ("Up"):
                enemyAnimator.SetInteger("EnemyState", 1);
                enemyAnimator.speed = 1;
                break;
            case ("Down"):
                enemyAnimator.SetInteger("EnemyState", 0);
                enemyAnimator.speed = 1;
                break;
            case ("Left"):
                enemyAnimator.SetInteger("EnemyState", 2);
                enemyAnimator.speed = 1;
                break;
            case ("Right"):
                enemyAnimator.SetInteger("EnemyState", 3);
                enemyAnimator.speed = 1;
                break;
        }
    }
    private void enemyDeathHandler()
    {
        enemyAnimator.SetBool("IsDead", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        Destroy(gameObject, 2.2f);
        //anything that only needs to be toggled once needs to go in here
        if (!toggleEffect)
        {
            audioSource.PlayOneShot(enemyDeathSound, 0.15f);
            randomItemDrop();
            uiManager.addScore(enemyScore);
            //creates a smoke effect after 2 seconds
            Invoke(nameof(createSmokeEffect), 2f);
            toggleEffect = true;
        }
    }
    private void createSmokeEffect()
    {
        Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
    }
    public void changeHealth(float damageVal)
    {
        //
        //this handles the passing of damage from the bullet
        //
        enemyHealth -= damageVal;

        if (enemyHealth <= 0)
        {
            isDead = true;
            enemyFlash.redFlash();
        }
        else
        {
            enemyFlash.whiteFlash();
        }
    }

    private void randomItemDrop()
    {
        //
        //Creating rng for item drops
        //big health = 10%
        //small health = 20%
        //attack speed = 10%
        //
        float temp = Random.Range(1, 20);
        switch (temp)
        {
            case 1:
                audioSource.PlayOneShot(itemDropSound, 0.5f);
                Instantiate(spawnEffectR, transform.position, Quaternion.identity);
                Instantiate(bigHealthUp, transform.position, Quaternion.identity);
                break;
            case 4:
                audioSource.PlayOneShot(itemDropSound, 0.5f);
                Instantiate(spawnEffectO, transform.position, Quaternion.identity);
                Instantiate(smallHealthUp, transform.position, Quaternion.identity);
                break;
            case 6:
                audioSource.PlayOneShot(itemDropSound, 0.5f);
                Instantiate(spawnEffectO, transform.position, Quaternion.identity);
                Instantiate(smallHealthUp, transform.position, Quaternion.identity);
                break;
            case 10:
                audioSource.PlayOneShot(itemDropSound, 0.5f);
                Instantiate(spawnEffectB, transform.position, Quaternion.identity);
                Instantiate(attackSpeedUp, transform.position, Quaternion.identity);
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                VBR_Player player = GameObject.Find("Player").GetComponent<VBR_Player>();
                enemyAnimator.SetBool("IsAttacking", true);
                player.playerDamage();
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                enemyAnimator.SetBool("IsAttacking", false);
                break;
        }
    }
}