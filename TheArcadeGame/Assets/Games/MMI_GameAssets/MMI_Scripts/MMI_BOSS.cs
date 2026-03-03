using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MMI_BOSS : MonoBehaviour
{
    Rigidbody2D rb;
    Animator Anim;
    AudioSource SFXPlayer;
    PolygonCollider2D col;

    [SerializeField] AudioClip[] BossImpactSounds;
    [SerializeField] AudioClip BossDeathSound;
    float AudioDelay;

    [Header("Health System Parameters")]
    int EnemyHealthValue = 10000000;//10000000;
    int CurrentEnemyHealthValue;
    [SerializeField] Image EnemyHealthUIFiller;
    private List<MMI_PLProj> ProjectilesInContact = new List<MMI_PLProj>();
    float DamageTickRate;
    int SumOfDamageIntake;
    bool BossDefeated;

    [Header("States Parameters")]
    bool BossState0 = true;
    bool BossState1 = false;
    bool BossState2 = false;
    bool BossState3 = false;
    bool BossState4 = false;
    bool BossState5 = false;
    bool BossState6 = false;
    bool BossState7 = false;
    bool BossStateEND = false;

    float StatePause = 3f;

    [Header("Attack Pattern Parameters")]
    [SerializeField] Transform Player;
    float PatternSwitchCounter;
    int PatterID;

    [SerializeField] GameObject AimedProj;
    [SerializeField] Transform AimedFRP;

    [SerializeField] GameObject WaveProj;
    [SerializeField] Transform WaveFRP;
    bool RorateAngleClockwise;
    float RotationValue = 180f;

    [SerializeField] GameObject CorridorProjectileR;
    [SerializeField] GameObject CorridorProjectileL;
    [SerializeField] Transform CorridorLFRP;
    [SerializeField] Transform CorridorRFRP;

    [SerializeField] GameObject WallProjectile;
    [SerializeField] Transform[] WallFirePoints;

    [SerializeField] GameObject SwordProjectile;
    [SerializeField] Transform[] SwordFRPsR1;
    [SerializeField] Transform[] SwordFRPsR2;
    [SerializeField] Transform[] SwordFRPsL1;
    [SerializeField] Transform[] SwordFRPsL2;
    [SerializeField] Transform[] SwordFRPsTOP1;
    [SerializeField] Transform[] SwordFRPsTOP2;
    [SerializeField] Transform[] SwordFRPsBOT1;
    [SerializeField] Transform[] SwordFRPsBOT2;
    [SerializeField] Transform[] AllBorderFirePoints;
    [SerializeField] Transform[] TornadoFirePoints;

    [SerializeField] GameObject MinionObject;

    bool Centered = false;
    bool InPatternMode = false;

    float AimedProjFireRate = 0f;
    float WaveProjectileFireRate = 0f;
    float CorridorFireRate = 0f;
    float WallProjFireRate = 0f;
    float SwordProjFireRate = 0f;
    float MinionSpawnRate = 0f;
    float ShurikenFireRate = 0f;
    float SingleSwordFireRate = 0f;


    [Header("Movement Parameters")]
    [SerializeField] Transform OriginalPoint;
    [SerializeField] Transform MiddlePoint;
    [SerializeField] Transform[] Stage3Points;
    [SerializeField] Transform[] Stage4Points;
    [SerializeField] Transform[] Stage56Points;
    
    int PointID;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        SFXPlayer = GetComponent<AudioSource>();
        col = GetComponent<PolygonCollider2D>();

    }

    private void Start()
    {
        CurrentEnemyHealthValue = EnemyHealthValue;
        EnemyHealthUIFiller.fillAmount = (float)CurrentEnemyHealthValue / EnemyHealthValue;
        BossDefeated = false;

        //temp
        //CurrentEnemyHealthValue = 500;
        // 10m - 8m
        // 8  - 6
        // 6 - 4.5
        // 4.5 - 3
        // 3 - 2
        // 2 - 1
        // 1 - 0
    }

    private void Update()
    {
        if (!BossDefeated)
        {
            HandleDamageIntake();
        }

        HandleStates();

        
    }

    private void FixedUpdate()
    {
       
        if (StatePause > 0f && transform.position != OriginalPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, OriginalPoint.position, 15 * Time.deltaTime);
            if (Vector2.Distance(transform.position, OriginalPoint.position) < 0.05f)
            {
                transform.position = OriginalPoint.position;
            }
        }

        if (BossState3)
        {
            HandleState3Movement();
        }
        if (BossState4)
        {
            HandleState4Movement();
        }
        if (BossState5)
        {
            HandleState56Movement();
        }
        if (BossState6)
        {
            HandleState56Movement();
        }
        if (BossState7)
        {
            HandleState7Movement();
        }
    }


    //==============================================================================================    
    // HANDLE COLLISIONS
    //==============================================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MMI_PLProj CollidingProjectile = collision.GetComponent<MMI_PLProj>();
        if (CollidingProjectile != null && !ProjectilesInContact.Contains(CollidingProjectile))
        {
            // Accumulate the damage from the projectile
            ProjectilesInContact.Add(CollidingProjectile);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MMI_PLProj CollidingProjectile = collision.GetComponent<MMI_PLProj>();
        if (CollidingProjectile != null && ProjectilesInContact.Contains(CollidingProjectile))
        {
            // Subtract the damage from the projectile when the player exits the trigger area
            ProjectilesInContact.Remove(CollidingProjectile);
        }
    }



    //==============================================================================================    
    // HANDLE HEALTH SYSTEM
    //==============================================================================================

    void HandleDamageIntake()
    {
       // AudioDelay -= Time.deltaTime;
        DamageTickRate -= Time.deltaTime;
        SumOfDamageIntake = ProjectilesInContact.Sum(CollidingProjectile => CollidingProjectile.CurrentDamageValue);

        List<MMI_PLProj> InactiveProjectiles = ProjectilesInContact.Where(CollidingProjectile => CollidingProjectile == null).ToList();
        foreach (MMI_PLProj CollidingProjectile in InactiveProjectiles)
        {
            ProjectilesInContact.Remove(CollidingProjectile);
        }
        if (SumOfDamageIntake > 0 && DamageTickRate <= 0f)
        {
            CurrentEnemyHealthValue -= SumOfDamageIntake;
            MMI_GameManager.Score += SumOfDamageIntake;

            //Debug.Log("Enemy Health: " + CurrentEnemyHealthValue);
            //Debug.Log("Enemy Damage Intake: " + SumOfDamageIntake);
            EnemyHealthUIFiller.fillAmount = (float)CurrentEnemyHealthValue / EnemyHealthValue;

            Anim.SetTrigger("TheEyeIsDamaged");

            


            //if (AudioDelay <= 0f)
            //{
                AudioClip clip = BossImpactSounds[Random.Range(0, BossImpactSounds.Length)];
                SFXPlayer.PlayOneShot(clip, 0.25f);
                //AudioDelay = 0.5f;
           // }


            DamageTickRate = 0.15f;
        }

        if (CurrentEnemyHealthValue <= 0 && !BossDefeated)
        {
            Anim.SetBool("EnemyDeath", true);
            BossDefeated = true;
            col.enabled = false;
            Invoke("DeclareGameOver", 3f);
        }


    }


    void DeclareGameOver()
    {
        MMI_ActionListerner.OnBossDefeat();
    }



    //==============================================================================================    
    // HANDLE STATE SYSTEM
    //==============================================================================================



    void HandleStates()
    {
        StatePause -= Time.deltaTime;
        // 10m - 8m
        // 8  - 6
        // 6 - 4.5
        // 4.5 - 3
        // 3 - 2
        // 2 - 1
        // 1 - 0


        //add a public bool to start combat post monolog.
        if (BossState0)
        {
            BossState0 = false;
            StatePause = 5f;

        }

        if (StatePause <= 0 && !BossState1 && CurrentEnemyHealthValue <= 10000000 && CurrentEnemyHealthValue > 8000000)
        {
            BossState1 = true;
            Debug.Log("Stage1");

            ResetVariables();
            PatterID = 0;
            PatternSwitchCounter = 0f;
        }
        if (BossState1 && CurrentEnemyHealthValue <= 8000000)
        {
            BossState1 = false;
            StatePause = 2f;
        }

        if (StatePause <= 0 && !BossState2 && CurrentEnemyHealthValue <= 8000000 && CurrentEnemyHealthValue > 6000000)
        {
            
            BossState2 = true;
            Debug.Log("Stage2");


            ResetVariables();
            PatterID = 0;
            PatternSwitchCounter = 0f;
        }
        if (BossState2 && CurrentEnemyHealthValue <= 6000000)
        {
            BossState2 = false;
            StatePause = 2f;
        }

        if (StatePause <= 0 && !BossState3 && CurrentEnemyHealthValue <= 6000000 && CurrentEnemyHealthValue > 4500000)
        {
            BossState3 = true;
            Debug.Log("Stage3");


            ResetVariables();
            PatterID = 0;
            PatternSwitchCounter = 0f;
        }
        if (BossState3 && CurrentEnemyHealthValue <= 4500000)
        {
            BossState3 = false;
            StatePause = 2f;
        }
        if (StatePause <= 0 && !BossState4 && CurrentEnemyHealthValue <= 4500000 && CurrentEnemyHealthValue > 3000000)
        {
            BossState4 = true;
            Debug.Log("Stage4");


            ResetVariables();
            PatterID = 0;
            PatternSwitchCounter = 0f;
        }
        if (BossState4 && CurrentEnemyHealthValue <= 3000000)
        {
            BossState4 = false;
            StatePause = 2f;
        }
        if (StatePause <= 0 && !BossState5 && CurrentEnemyHealthValue <= 3000000 && CurrentEnemyHealthValue > 2000000)
        {
            
            BossState5 = true;
            Debug.Log("Stage5");
            PatterID = 0;
            PatternSwitchCounter = 0f;

            ResetVariables();
        }
        if (BossState5 && CurrentEnemyHealthValue <= 2000000)
        {
            BossState5 = false;
            StatePause = 2f;
        }
        if (StatePause <= 0 && !BossState6 && CurrentEnemyHealthValue <= 2000000 && CurrentEnemyHealthValue > 1000000)
        {
            
            BossState6 = true;
            Debug.Log("Stage6");
            PatterID = 0;
            PatternSwitchCounter = 0f;

            ResetVariables();
        }
        if (BossState6 && CurrentEnemyHealthValue <= 1000000)
        {
            BossState6 = false;
            StatePause = 2f;
        }
        if (StatePause <= 0 && !BossState7 && CurrentEnemyHealthValue <= 1000000 && CurrentEnemyHealthValue > 0)
        {
            
            BossState7 = true;
            Debug.Log("Stage7");
            PatterID = 0;
            PatternSwitchCounter = 0f;

            ResetVariables();
        }
        if (StatePause <= 0 && BossState7 && !BossStateEND && CurrentEnemyHealthValue <= 0) //add a public bool to start combat post monolog.
        {
            
            BossState7 = false;
            BossStateEND = true;
            Debug.Log("LE BOSS DEFEATED"); 
            PatterID = 0;
            PatternSwitchCounter = 0f;

            SFXPlayer.PlayOneShot(BossDeathSound, 1.5f);

            ResetVariables();
        }

        
        if (BossState1)
        {
            HandleState1(9); //17
        }
        if (BossState2)
        {
            HandleState2(10);
        }
        if (BossState3)
        {
            HandleState3(8);
        }
        if (BossState4)
        {
            HandleState4(7);
        }
        if (BossState5)
        {
            HandleState5(10);
        }
        if (BossState6)
        {
            HandleState6(7);
        }
        if (BossState7)
        {
            HandleState7(5);
        }
        
    }
    //==============================================================================================    
    // STATES
    //==============================================================================================

    void HandleState1(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

       

        
        switch (PatterID)
        {
            case 1:
                //TornadoProjectiles(75f, 0.05f);
                WaveProjectiles(360f, 0.1f);
                AimedAtPlayerProjectiles(1f);
                break;
            case 2:
                
                SwordProjectiles(2f);

                break;
            case 3:
                PatterID = 1;
                break;
            default:
                break;
        }

        
    }

    //========================================================================

    void HandleState2(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:
                
                CorridorProjectiles(35f, 0.15f);
                AimedAtPlayerProjectiles(2f);

                break;
            case 2:
                WaveProjectiles(720f, 0.05f);
                AimedAtPlayerProjectiles(0.75f);
                break;
            case 3:
                BorderShurikens(0.25f);
                AimedAtPlayerProjectiles(1f);
                break;
            case 4:
                PatterID = 1;
                break;
            default:
                break;
        }


    }

    //========================================================================

    void HandleState3(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:
                MinionSpawner(2.5f);
                WaveProjectiles(180f, 0.25f);
                break;
            case 2:
                CorridorProjectiles(35f, 0.125f);
                AimedAtPlayerProjectiles(2.5f);
                break;
            case 3:
                BorderShurikens(0.15f);
                break;
            case 4:
                PatterID = 1;
                break;
            default:
                break;
        }


    }

    void HandleState3Movement()
    {
        
            if (Vector2.Distance(transform.position, Stage3Points[PointID].position) < 0.02f)
            {
                PointID++;
                if (PointID == Stage3Points.Length)
                {
                    PointID = 0;
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, Stage3Points[PointID].position, 1.5f * Time.deltaTime);
        
    }


    //========================================================================

    void HandleState4(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:

                TornadoProjectiles(50f, 0.05f);
                AimedAtPlayerProjectiles(1f);
                break;
            case 2:
                
                WallProjectiles(3f);
                BorderSwords(0.5f);
                break;
            case 3:
                //TornadoProjectiles(50f, 0.05f);
                AimedAtPlayerProjectiles(0.5f);

                
                break;
            case 4:
                WaveProjectiles(360f, 0.1f);
                MinionSpawner(2.5f);
                SwordProjectiles(5f);
                break;
            case 5:
                PatterID = 1;
                break;
            default:
                break;
        }


    }

    void HandleState4Movement()
    {
        if (!InPatternMode)
        {
            if (Vector2.Distance(transform.position, Stage4Points[PointID].position) < 0.02f)
            {
                PointID++;
                if (PointID == Stage4Points.Length)
                {
                    PointID = 0;
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, Stage4Points[PointID].position, 4f * Time.deltaTime);
        }
    }

    void HandleState56Movement()
    {
        if (!InPatternMode)
        {
            if (Vector2.Distance(transform.position, Stage56Points[PointID].position) < 0.02f)
            {
                PointID++;
                if (PointID == Stage56Points.Length)
                {
                    PointID = 0;
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, Stage56Points[PointID].position, 3f * Time.deltaTime);
        }
    }

    //========================================================================

    void HandleState5(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:
                BorderShurikens(0.1f);
                AimedAtPlayerProjectiles(2.9f);
                break;
            case 2:
                AimedAtPlayerProjectiles(0.5f);
                TornadoProjectiles(75f, 0.05f);
                break;
            case 3:

                

                WallProjectiles(2f);
                //AimedAtPlayerProjectiles(2.8f);
                SwordProjectiles(3f);
                break;
            case 4:

                AimedAtPlayerProjectiles(2.6f);
                MinionSpawner(2f);
                break;
            case 5:

                AimedAtPlayerProjectiles(2.5f);
                CorridorProjectiles(45f, 0.1f);

                break;
            case 6:
                PatterID = 1;

                break;
            default:
                break;
        }

    }
    //========================================================================

    void HandleState6(float SwitchTime)
    {
        //SwordProjectiles(fr)
        //WallProjectiles(fr)
        //MinionSpawner(fr)
        //WaveProjectiles(rot, fr)
        //AimedAtPlayerProjectiles(fr)
        //CorridorProjectiles(rot, fr)
        //BorderShurikens(fr)
        //BorderSwords(fr)

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:
                WaveProjectiles(720, 0.1f);
                SwordProjectiles(1.5f);

                break;
            case 2:
                WallProjectiles(2f);
                AimedAtPlayerProjectiles(0.75f);
                break;
            case 3:
                BorderShurikens(0.1f);
                WaveProjectiles(35, 0.25f);
                break;
            case 4:
                MinionSpawner(2f);
                TornadoProjectiles(75f, 0.05f);
                break;
            case 5:
                BorderShurikens(0.075f);
                WaveProjectiles(55, 0.5f);
                break;
            case 6:
                AimedAtPlayerProjectiles(0.25f);
                break;
            case 7:
                PatterID = 1;
                break;
            default:
                break;
        }


    }
    //========================================================================

    void HandleState7(float SwitchTime)
    {
        //SwordProjectiles(fr);
        //WallProjectiles(fr);
        //MinionSpawner(fr);
        //WaveProjectiles(rot, fr);
        //AimedAtPlayerProjectiles(fr);
        //CorridorProjectiles(rot, fr);
        //BorderShurikens(fr);
        //BorderSwords(fr);

        if (PatternSwitchCounter > 0f)
        {
            PatternSwitchCounter -= Time.deltaTime;
        }

        if (PatternSwitchCounter <= 0f)
        {
            PatterID++;
            PatternSwitchCounter = SwitchTime;
            ResetVariables();
        }

        switch (PatterID)
        {
            case 1:
                SwordProjectiles(0.9f);
                break;
            case 2:
                WallProjectiles(1f);
                break;
            case 3:
                MinionSpawner(1.5f);
                break;
            case 4:
                BorderSwords(0.1f);
                break;
            case 5:
                AimedAtPlayerProjectiles(0.2f);
                break;
            case 6:
                CorridorProjectiles(70f, 0.1f);
                break;
            case 7:
                TornadoProjectiles(80f, 0.05f);
                break;
            case 8:
                BorderShurikens(0.075f);
                break;
            case 9:
                WaveProjectiles(720, 0.03f);
                break;
            case 10:
                PatterID = 1;
                break;
            default:
                break;
        }
        

    }

    void HandleState7Movement()
    {
        if (!InPatternMode)
        {


            if (Vector2.Distance(transform.position, Stage56Points[PointID].position) < 0.02f)
            {
                PointID++;
                if (PointID == Stage56Points.Length)
                {
                    PointID = 0;
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, Stage56Points[PointID].position, 5f * Time.deltaTime);
        }
    }


    //==============================================================================================    
    // PATTERNS
    //==============================================================================================

    void ResetVariables()
    {
        InPatternMode = false;
        RotationValue = 180;
        WaveFRP.transform.rotation = Quaternion.Euler(0, 0, RotationValue);
        CorridorLFRP.transform.rotation = Quaternion.Euler(0, 0, RotationValue);
        CorridorRFRP.transform.rotation = Quaternion.Euler(0, 0, RotationValue);
        RorateAngleClockwise = false;
    }


    //SwordProjectiles(fr)
    //WallProjectiles(fr)
    //MinionSpawner(fr)
    //WaveProjectiles(rot, fr)
    //AimedAtPlayerProjectiles(fr)
    //CorridorProjectiles(rot, fr)
    //BorderShurikens(fr)
    //BorderSwords(fr)



    //=====================================================================================================
    void SwordProjectiles(float FireRate)
    {
        if (SwordProjFireRate > 0)
        {
            SwordProjFireRate -= Time.deltaTime;
        }

        if (SwordProjFireRate <= 0)
        {
            int RNG = Random.Range(1,9);

            switch (RNG)
            {
                case 1:
                    for (int i = 0; i < SwordFRPsR1.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsR1[i].position, SwordFRPsR1[i].rotation);
                    }
                    break;
                case 2:
                    for (int i = 0; i < SwordFRPsR2.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsR2[i].position, SwordFRPsR2[i].rotation);
                    }
                    break;
                case 3:
                    for (int i = 0; i < SwordFRPsL1.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsL1[i].position, SwordFRPsL1[i].rotation);
                    }
                    break;
                case 4:
                    for (int i = 0; i < SwordFRPsL2.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsL2[i].position, SwordFRPsL2[i].rotation);
                    }
                    break;
                case 5:
                    for (int i = 0; i < SwordFRPsTOP1.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsTOP1[i].position, SwordFRPsTOP1[i].rotation);
                    }
                    break;
                case 6:
                    for (int i = 0; i < SwordFRPsTOP2.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsTOP2[i].position, SwordFRPsTOP2[i].rotation);
                    }
                    break;
                case 7:
                    for (int i = 0; i < SwordFRPsBOT1.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsBOT1[i].position, SwordFRPsBOT1[i].rotation);
                    }
                    break;
                case 8:
                    for (int i = 0; i < SwordFRPsBOT2.Length; i++)
                    {
                        Instantiate(SwordProjectile, SwordFRPsBOT2[i].position, SwordFRPsBOT2[i].rotation);
                    }
                    break;

                default :
                    break;


            }

            
            SwordProjFireRate = FireRate;
        }
    }


    //=====================================================================================================
    void WallProjectiles(float FireRate)
    {
        if (WallProjFireRate > 0)
        {
            WallProjFireRate -= Time.deltaTime;
        }

        if (WallProjFireRate <= 0)
        {

            int RNG = Random.Range(0, 5);

            Instantiate(WallProjectile, WallFirePoints[RNG].position, WallFirePoints[RNG].rotation);



            WallProjFireRate = FireRate;
        }
    }

    //=====================================================================================================
    void MinionSpawner(float FireRate)
    {
        if (MinionSpawnRate > 0)
        {
            MinionSpawnRate -= Time.deltaTime;
        }

        if (MinionSpawnRate <= 0)
        {

            float XRNG = Random.Range(-8.5f, 8.5f);
            float YRNG = Random.Range(-3f, 7.5f);

            Instantiate(MinionObject, new Vector2(XRNG, YRNG), transform.rotation);



            MinionSpawnRate = FireRate;
        }
    }


    //=====================================================================================================
    void WaveProjectiles(float RotationSpeed, float FireRate)
    {

        // 285 - 75 
        if (RorateAngleClockwise && RotationValue <= 75f)
        {
            RorateAngleClockwise = false;
        }
        if (!RorateAngleClockwise && RotationValue >= 285f)
        {
            RorateAngleClockwise = true;
        }

        if (RorateAngleClockwise)
        {
            RotationValue -= RotationSpeed * Time.deltaTime;
        }
        else
        {
            RotationValue += RotationSpeed * Time.deltaTime;
        }
        WaveFRP.transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationValue));






        if (WaveProjectileFireRate > 0)
        {
            WaveProjectileFireRate -= Time.deltaTime;
        }

        if (WaveProjectileFireRate <= 0)
        {

            Instantiate(WaveProj, WaveFRP.position, WaveFRP.rotation);

            WaveProjectileFireRate = FireRate;
        }

    }
    //=====================================================================================================
    void AimedAtPlayerProjectiles(float FireRate)
    {
        if (AimedProjFireRate > 0)
        {
            AimedProjFireRate -= Time.deltaTime;
        }

        if (AimedProjFireRate <= 0)
        {

            Vector2 direction = (Player.position - AimedFRP.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            AimedFRP.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Instantiate(AimedProj, AimedFRP.position, AimedFRP.rotation);



            AimedProjFireRate = FireRate;
        }
    }


    //=====================================================================================================


    void CorridorProjectiles(float RotationSpeed, float FireRate)
    {

        
        if (RorateAngleClockwise && RotationValue <= 135f)
        {
            RorateAngleClockwise = false;
        }
        if (!RorateAngleClockwise && RotationValue >= 190f)
        {
            RorateAngleClockwise = true;
        }

        if (RorateAngleClockwise)
        {
            RotationValue -= RotationSpeed * Time.deltaTime;
        }
        else
        {
            RotationValue += RotationSpeed * Time.deltaTime;
        }
        CorridorLFRP.transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationValue));
        CorridorRFRP.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -RotationValue));






        if (CorridorFireRate > 0)
        {
            CorridorFireRate -= Time.deltaTime;
        }

        if (CorridorFireRate <= 0)
        {

            Instantiate(CorridorProjectileL, CorridorLFRP.position, CorridorLFRP.rotation);
            Instantiate(CorridorProjectileR, CorridorRFRP.position, CorridorRFRP.rotation);

            CorridorFireRate = FireRate;
        }

    }

    //=====================================================================================================


    void BorderShurikens(float FireRate)
    {


        if (ShurikenFireRate > 0)
        {
            ShurikenFireRate -= Time.deltaTime;
        }

        if (ShurikenFireRate <= 0)
        {
            int RNG_LR = Random.Range(1, 3);

            int RNGTransform = Random.Range(0, AllBorderFirePoints.Length);


            if (RNG_LR == 1)
            {
                Instantiate(CorridorProjectileL, AllBorderFirePoints[RNGTransform].position, AllBorderFirePoints[RNGTransform].rotation);


            }
            if (RNG_LR == 2)
            {
                
                Instantiate(CorridorProjectileR, AllBorderFirePoints[RNGTransform].position, AllBorderFirePoints[RNGTransform].rotation);
            }
            
            

            ShurikenFireRate = FireRate;
        }

    }

    //=====================================================================================================


    void BorderSwords(float FireRate)
    {


        if (SingleSwordFireRate > 0)
        {
            SingleSwordFireRate -= Time.deltaTime;
        }

        if (SingleSwordFireRate <= 0)
        {
            int RNGTransform = Random.Range(0, AllBorderFirePoints.Length);
            Instantiate(SwordProjectile, AllBorderFirePoints[RNGTransform].position, AllBorderFirePoints[RNGTransform].rotation);
            
            SingleSwordFireRate = FireRate;
        }

    }

    //=====================================================================================================
    void TornadoProjectiles(float RotationSpeed, float FireRate)
    {

        
        if (PatternSwitchCounter <= 2f && Centered)
        {
            
            InPatternMode = false;
            transform.position = Vector2.MoveTowards(transform.position, OriginalPoint.position, 10f * Time.deltaTime);
            if (Vector2.Distance(transform.position, OriginalPoint.position) < 0.05f)
            {
                transform.position = OriginalPoint.position;
                Centered = false;
            }
        }
        if (PatternSwitchCounter > 2f && !Centered) 
        {
            InPatternMode = true;
            transform.position = Vector2.MoveTowards(transform.position, MiddlePoint.position, 10f * Time.deltaTime);
            if (Vector2.Distance(transform.position, MiddlePoint.position) < 0.05f)
            {
                transform.position = MiddlePoint.position;
                Centered = true;
            }
        }



        RotationValue += RotationSpeed * Time.deltaTime;
        

        WaveFRP.transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationValue));






        if (WaveProjectileFireRate > 0)
        {
            WaveProjectileFireRate -= Time.deltaTime;
        }

        if (Centered && InPatternMode && WaveProjectileFireRate <= 0)
        {

            Instantiate(WaveProj, TornadoFirePoints[0].position, TornadoFirePoints[0].rotation);
            Instantiate(WaveProj, TornadoFirePoints[1].position, TornadoFirePoints[1].rotation);
            Instantiate(WaveProj, TornadoFirePoints[2].position, TornadoFirePoints[2].rotation);
            Instantiate(WaveProj, TornadoFirePoints[3].position, TornadoFirePoints[3].rotation);

            WaveProjectileFireRate = FireRate;
        }

    }



}
