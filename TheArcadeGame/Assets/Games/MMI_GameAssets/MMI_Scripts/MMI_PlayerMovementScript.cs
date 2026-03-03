// By Green Knight Entertainment - Elliot Greenwood
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class MMI_PlayerMovementScript : MonoBehaviour
{

    InputSubscription _Input;
    Animator Animator;
    Rigidbody2D rb;
    AudioSource SFXPlayer;
    [SerializeField] Animator ScreenShake;

    [Header("Input Parameters")]
    [SerializeField] bool CanMove = true;
    [SerializeField] bool CanShoot = true;
    [SerializeField] bool PiecingProjectile = false;
    [SerializeField] bool MultiShot = false;
    [SerializeField] bool HighFireRate = false;

    [Header("Health System Parameters")]
    [SerializeField] int PlayerHealthValue = 1000;
    int CurrentPlayerHealthValue;
    [SerializeField] Image PlayerHealthUIFiller;
    private List<MMI_EnemyProjectile> ProjectilesInContact = new List<MMI_EnemyProjectile>();
    float DamageTickRate;
    int SumOfDamageIntake;
    bool PlayerDeath;


    [Header("Movement Parameters")]
    float CurrentMovementSpeed;
    [SerializeField] float MovementSpeed = 15f;
    [SerializeField] float SlowedSpeed => MovementSpeed / 3f;
    
   

    [Header("Shooting Parameters")]
    [SerializeField] Transform ProjectileFirePointObject;
    [SerializeField] Transform ProjectileFirePointObjectL;
    [SerializeField] Transform ProjectileFirePointObjectR;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] GameObject ProjectilePrefabPiercing;
    
     float ProjectileFireRateCounter;


    [Header("Audio Parameters")]
    [SerializeField] AudioClip PlayerImpactSound;
    
    [SerializeField] AudioClip[] ShootingSounds;

    // [Header("Player Buffs and Debuffs")]
    // float StunEffectDuration;
    // float SlowEffectDuration;
    // bool PlayerIsSlowed = false;
    // bool PlayerIsStunned = false;



    MMI_BuffsDebuffs BuffDebuffObject;
    [SerializeField] Image MultiShotFiller;
    bool MultishotBuff = false;
    float MSDuration;
    float MSpass;
    [SerializeField] Image PierceFiller;
    bool PiercingBuff = false;
    float PDuration;
    float Ppass;
    [SerializeField] Image FireRateFiller;
    bool FireRateBuff = false;
    float FRDuration;
    float FRpass;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _Input = GetComponent<InputSubscription>();
        Animator = GetComponentInChildren<Animator>();
        SFXPlayer = GetComponent<AudioSource>();
    }


    private void Start()
    {
        // SlowEffectDuration = 0f;
        // StunEffectDuration = 0f;

        CurrentPlayerHealthValue = PlayerHealthValue;
        PlayerHealthUIFiller.fillAmount = (float)CurrentPlayerHealthValue / PlayerHealthValue;

        MultiShotFiller.fillAmount = 0;
        PierceFiller.fillAmount = 0;
        FireRateFiller.fillAmount = 0;


    }

    private void Update()
    {


        HandleDamageIntake();
        HandlePlayerBuffsAndDebuffs();

        if (CanMove)
        {
            HandlePlayerMovement();
        }
        if (CanShoot)
        {
            HandleProjectileShooting();
        }


    }

    private void LateUpdate()
    {
        HandleAnimations();
    }



    //=========================================
    // Health Systems
    //=========================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
    
        MMI_EnemyProjectile CollidingProjectile = collision.GetComponent<MMI_EnemyProjectile>();
        if (CollidingProjectile != null && !ProjectilesInContact.Contains(CollidingProjectile))
        {
            // Accumulate the damage from the projectile
            ProjectilesInContact.Add(CollidingProjectile);
        
        }
        
        
        
        if (collision.gameObject.layer == 11)
        {
            BuffDebuffObject = collision.GetComponent<MMI_BuffsDebuffs>();
            


            if (BuffDebuffObject.Multishot)
            {

                MultishotBuff = true;
                MSpass = BuffDebuffObject.EffectDuration;
                MSDuration = MSpass;
            }
            if (BuffDebuffObject.Piercing)
            {
                PiercingBuff = true;
                Ppass = BuffDebuffObject.EffectDuration;
                PDuration = Ppass;
            }
            if (BuffDebuffObject.FireRate)
            {
                FireRateBuff = true;
                FRpass = BuffDebuffObject.EffectDuration;
                FRDuration = FRpass;
            }
            
            BuffDebuffObject = null;
            MMI_ActionListerner.OnPowerupCollected();
            Destroy(collision.gameObject);
            
        }


    
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
            MMI_EnemyProjectile CollidingProjectile = collision.GetComponent<MMI_EnemyProjectile>();
        if (CollidingProjectile != null && ProjectilesInContact.Contains(CollidingProjectile))
        {
            // Subtract the damage from the projectile when the player exits the trigger area
            ProjectilesInContact.Remove(CollidingProjectile);

        }
        
    }



    void HandleDamageIntake()
    {
        
        DamageTickRate -= Time.deltaTime;
        SumOfDamageIntake = ProjectilesInContact.Sum(CollidingProjectile => CollidingProjectile.CurrentDamageValue);


        List<MMI_EnemyProjectile> InactiveProjectiles = ProjectilesInContact.Where(CollidingProjectile => CollidingProjectile == null).ToList();
        foreach (MMI_EnemyProjectile CollidingProjectile in InactiveProjectiles)
        {
            ProjectilesInContact.Remove(CollidingProjectile);
        }

        if (SumOfDamageIntake > 0 && DamageTickRate <= 0f)
        {
            CurrentPlayerHealthValue -= SumOfDamageIntake;

            MMI_GameManager.Score -= 50 * SumOfDamageIntake;

            PlayerHealthUIFiller.fillAmount = (float)CurrentPlayerHealthValue / PlayerHealthValue;

            SFXPlayer.PlayOneShot(PlayerImpactSound, 1f);

            ScreenShake.SetTrigger("PlayerHit");

            MMI_ActionListerner.OnProjectilesTaken();


            DamageTickRate = 0.5f;
        }
        
        if (CurrentPlayerHealthValue <= 0 && !PlayerDeath)
        {
            PlayerDeath=true;
            MMI_ActionListerner.OnPlayerDeath();
            
        }



    }




    void HandlePlayerBuffsAndDebuffs()
    {
        if (MSDuration > 0) //multishot
        {
            MSDuration -= Time.deltaTime;
            MultiShotFiller.fillAmount = MSDuration / MSpass;
        }
        if (MSDuration <= 0 && MultishotBuff)
        {
            MultishotBuff = false;
            MultiShot = false;
            //player debuff sound
        }


        if (PDuration > 0) //multishot
        {
            PDuration -= Time.deltaTime;
            PierceFiller.fillAmount = PDuration / Ppass;
        }
        if (PDuration <= 0 && PiercingBuff)
        {
            PiecingProjectile = false;
            PiercingBuff = false;
            //player debuff sound
        }


        if (FRDuration > 0) //multishot
        {
            FRDuration -= Time.deltaTime;
            FireRateFiller.fillAmount = FRDuration / FRpass;
        }
        if (FRDuration <= 0 && FireRateBuff)
        { 
            FireRateBuff = false;
            HighFireRate = false;
            //player debuff sound
        }



        //=================================
        if (MultishotBuff && !MultiShot)
        {
            MultiShot = true;
        }
        if (PiercingBuff && !PiecingProjectile)
        {
            PiecingProjectile = true;
        }
        if (FireRateBuff && !HighFireRate)
        {
            HighFireRate = true;
        }
        


    }







    //=========================================
    // Movement
    //=========================================
    void HandlePlayerMovement()
    {

        rb.linearVelocity = new Vector2(_Input.NormalizedMovementInput.x, _Input.NormalizedMovementInput.y) * MovementSpeed;






    }



    //=========================================
    // Shooting Inputs
    //=========================================


    void HandleProjectileShooting()
    {
        
        ProjectileFireRateCounter -= Time.deltaTime;

        if (_Input.SpaceInput && ProjectileFireRateCounter <= 0f)
        {
            if (!HighFireRate)
            {
                ProjectileFireRateCounter = 0.25f;
            }
            else
            {
                ProjectileFireRateCounter = 0.1f;
            }
            

            

            AudioClip clip = ShootingSounds[Random.Range(0, ShootingSounds.Length)];
            SFXPlayer.PlayOneShot(clip, 0.1f);

            if (!MultiShot)
            {
                if (!PiecingProjectile)
                {
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefab, ProjectileFirePointObject.position, ProjectileFirePointObject.rotation);
                }
                else
                {
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefabPiercing, ProjectileFirePointObject.position, ProjectileFirePointObject.rotation);
                }
                
            }
            else
            {
                if (!PiecingProjectile)
                {
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefab, ProjectileFirePointObject.position, ProjectileFirePointObject.rotation);
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefab, ProjectileFirePointObjectL.position, ProjectileFirePointObjectL.rotation);
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefab, ProjectileFirePointObjectR.position, ProjectileFirePointObjectR.rotation);
                }
                else
                {
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefabPiercing, ProjectileFirePointObject.position, ProjectileFirePointObject.rotation);
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefabPiercing, ProjectileFirePointObjectL.position, ProjectileFirePointObjectL.rotation);
                    MMI_ActionListerner.OnShotFired();
                    Instantiate(ProjectilePrefabPiercing, ProjectileFirePointObjectR.position, ProjectileFirePointObjectR.rotation);
                }
            }



        }  
    }
    


    //=========================================
    // temp
    //=========================================


    void HandleAnimations()
    {
       

    }




}
