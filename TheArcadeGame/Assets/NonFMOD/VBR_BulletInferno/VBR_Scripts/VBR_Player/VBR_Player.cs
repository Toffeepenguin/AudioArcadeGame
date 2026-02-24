using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class VBR_Player : MonoBehaviour
{
    private InputSubscription _Input;
    private Rigidbody2D rb;

    //Inputs available
    private Vector2 PlayerMovement;
    private bool SpacePressed;
    private bool ShiftPressed;

    

    private VBR_UiManager uiManager;
    private VBR_PauseMenu pauseMenu;

    [SerializeField] private VBR_Flash_Effect playerFlash;

    [Header("Player Health Parameters")]
    [SerializeField] public float playerHealth = 100f;
    [SerializeField] private float playerInvulTimer = 1f;
    private bool canBeHurt = true;

    [Header("Movement Speed Parameters")]
    [SerializeField] private float PlayerMS = 5f;
    [SerializeField] private float PlayerSprintMulitplier = 2f;

    [Header("Bullet Parameters")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 500f;
    [SerializeField] private float shootCountdown = .01f;
    private float shootInterval = 2f;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip playerShootSound;
    private AudioSource audioSource;

    //Animation parameters
    private Animator playerAnimator;
    private string currentAnimation;


    //this variable holds the direction that the player is facing
    private Vector2 direction;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.speed = 0;
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        uiManager = GameObject.Find("Ui").GetComponent<VBR_UiManager>();
        pauseMenu = GameObject.Find("GameManager").GetComponent<VBR_PauseMenu>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        checkAnimation();
    }
    private void Update()
    {
        if (!pauseMenu.isPaused)
        {
            checkAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (!pauseMenu.isPaused)
        {
            shootInterval -= shootCountdown;
            if (shootInterval <= 0f)
            {
                shootInterval = 0f;
            }
            playerMovement();
            if (SpacePressed)
            {
                if (shootInterval == 0f)
                {
                    audioSource.PlayOneShot(playerShootSound, 0.125f);
                    shootBullet();
                    shootInterval = 2f;
                }
            }
        }

        
    }
    private void playerMovement()
    {
        //Handling player movements
        PlayerMovement = new Vector2(_Input.NormalizedMovementInput.x, _Input.NormalizedMovementInput.y);
        //Checking if space has been pressed
        SpacePressed = _Input.SpaceInput;
        //Checking if shift is being held
        ShiftPressed = _Input.ShiftInput;

        if (ShiftPressed)
        {
            rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * PlayerMS * PlayerSprintMulitplier;
            direction = new Vector2(PlayerMovement.x, PlayerMovement.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * PlayerMS;
            direction = new Vector2(PlayerMovement.x, PlayerMovement.y);
        }
    }
    private string GetPlayerMovementDirection(float x, float y)
    {
        //
        //This function returns the direction which the player is moving towards
        //
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        Vector2 idleCheck = new Vector2(x, y);
        if (idleCheck == Vector2.zero)
        {
            return "Idle";
        }

        if (angle >= -22.5f && angle < 22.5f) { return "Right"; }
        else if (angle >= 22.5f && angle < 67.5f) { return "Up-Right"; }
        else if (angle >= 67.5f && angle < 112.5f) { return "Up"; }
        else if (angle >= 112.5f && angle < 157.5f) { return "Up-Left"; }
        else if (angle >= -67.5f && angle < -22.5f) { return "Down-Right"; }
        else if (angle >= -112.5f && angle < -67.5f) { return "Down"; }
        else if (angle >= -157.5f && angle < -112.5f) { return "Down-Left"; }
        else return "Left";
    }
     private void changeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation != animation) 
        {
            currentAnimation = animation;
            playerAnimator.CrossFade(animation, crossFade);
        }
    }
    private void checkAnimation()
    {
        string dir = GetPlayerMovementDirection(direction.x, direction.y);
        if (SpacePressed) 
        {
            switch (dir)
            {
                case "Right":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Left");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Right");
                    }
                    break;
                case "Left":
                    
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Right");
                    }
                    else 
                    { 
                        changeAnimation("VBR_Shoot_Left"); 
                    }
                    break;
                case "Up":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Down");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Up");
                    }
                    break;
                case "Up-Left":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Down_Right");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Up_Left");
                    }
                    break;
                case "Up-Right":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Down_Left");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Up_Right");
                    }
                    break;
                case "Down":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Up");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Down");
                    }
                    break;
                case "Down-Right":
                    if (ShiftPressed) 
                    {
                        changeAnimation("VBR_Shoot_Up_Left");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Down_Right");
                    }
                    break;
                case "Down-Left":
                    if (ShiftPressed)
                    {
                        changeAnimation("VBR_Shoot_Up_Right");
                    }
                    else
                    {
                        changeAnimation("VBR_Shoot_Down_Left");
                    }
                    break;
                case "Idle":
                    changeAnimation("VBR_Shoot_Down");
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case "Right":
                    changeAnimation("VBR_Walk_DownRight");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Left":
                    changeAnimation("VBR_Walk_DownLeft");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Up":
                    changeAnimation("VBR_Walk_Up");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Up-Left":
                    changeAnimation("VBR_Walk_UpLeft");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Up-Right":
                    changeAnimation("VBR_Walk_UpRight");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Down":
                    changeAnimation("VBR_Walk_Down");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Down-Right":
                    changeAnimation("VBR_Walk_DownRight");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Down-Left":
                    changeAnimation("VBR_Walk_DownLeft");
                    if (ShiftPressed) { playerAnimator.speed = 2; }
                    else { playerAnimator.speed = 1; }
                    break;
                case "Idle":
                    changeAnimation("VBR_Player_Idle");
                    if (ShiftPressed) { playerAnimator.speed = 1; }
                    else { playerAnimator.speed = 1; }
                    break;
            }
        }
        
    }
   
    private void shootBullet()
    {
        //
        //This function creates the bullet prefab and shoots it in the direction of the player
        //if the player is holding shift, the shooting direction is reversed
        //
        
        Vector2 shootDirection = direction;

        if (shootDirection == Vector2.zero)
        {
            shootDirection = Vector2.down;
        }
        else if (ShiftPressed)
        {
            shootDirection *= -1;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = shootDirection * bulletSpeed;
    }

    public void playerDamage()
    {
        if (canBeHurt)
        {
            playerHealth -= 20f;
            Debug.Log("Current player health: " + playerHealth);
            canBeHurt = false;

            playerFlash.redFlash();
            audioSource.PlayOneShot(playerHurtSound, 0.25f);
            Invoke(nameof(playerCanBeHurt), playerInvulTimer);
            uiManager.healthUpdate(playerHealth);
        }
    }
    public void playerHealthUp(float amount)
    {
        playerHealth += amount;
        if (playerHealth >= 100f)
        {
            playerHealth = 100f;
        }
        uiManager.healthUpdate(playerHealth);
    }
    public void playerAttackSpeedUp(float amount)
    {
        shootCountdown += amount;
        if (shootCountdown >= 0.25f)
        {
            shootCountdown = 0.25f;
        }
    }
    private void playerCanBeHurt()
    {
        canBeHurt = true;
    }

}
