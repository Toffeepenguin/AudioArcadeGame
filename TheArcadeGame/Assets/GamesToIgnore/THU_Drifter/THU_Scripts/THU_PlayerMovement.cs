using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static THU_PowerUpType;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputSubscription))]
public class THU_PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 2.5f;
    public float speedBoostMultiplier = 1.5f;
    public float driftFactor = 0.5f;
    public float decelerationFactor = 1f;
    public float collisionSlowFactor = 0.05f;

    [Header("Boost Settings")]
    [SerializeField] private float boostDrainRate = 30f;
    [SerializeField] private float boostRechargeRate = 1f;
    [SerializeField] private float boostCooldownDuration = 3f;
    private float boostEnergy = 30f;
    private const float MaxBoostEnergy = 30f;

    [Header("Oil Settings")]
    [SerializeField] private GameObject oilSpillPrefab;
    [SerializeField] private float oilLifetime = 5f;

    [Header("Audio Settings")]
    public AudioClip turnAudioClip;
    public AudioClip driveAudioClip;

    private float currentSpeed;
    private float boostCooldownTimer;
    private bool isBoosting;
    private bool isInSlowZone;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private THU_PlayerInventory inventory;
    private THU_Drifter_UIManager uiManager;
    private InputSubscription inputSubscription;
    public THU_PowerUpTypes powerUpType;

    private AudioSource drivingAudioSource;
    private AudioSource turningAudioSource;

    private THU_PowerUps activePowerUp;
    private Coroutine activePowerUpCoroutine;

    private THU_Oil heldBarrel;
    private bool hasBarrel;



    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputSubscription = GetComponent<InputSubscription>();
        uiManager = FindObjectOfType<THU_Drifter_UIManager>();
        inventory = GetComponent<THU_PlayerInventory>();

        if (!uiManager)
        {
            Debug.LogError("UI Manager is missing in the scene!");
        }

        SetupAudioSources();
    }

    private void Start()
    {
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        HandleBoosting();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseActivePowerUp();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void SetupAudioSources()
    {
        drivingAudioSource = gameObject.AddComponent<AudioSource>();
        turningAudioSource = gameObject.AddComponent<AudioSource>();

        drivingAudioSource.clip = driveAudioClip;
        drivingAudioSource.loop = true;
        drivingAudioSource.playOnAwake = false;

        turningAudioSource.clip = turnAudioClip;
        turningAudioSource.loop = false;
        turningAudioSource.playOnAwake = false;
    }

    private void HandleMovement()
    {
        if (!CanMove || !uiManager || !uiManager.isGameRunning)
        {
            StopMovement();
            return;
        }

        if (moveInput.magnitude > 0)
        {
            Vector2 desiredVelocity = moveInput.normalized * currentSpeed;
            ApplyMovement(desiredVelocity);
            RotatePlayer();

            PlayDrivingAudio();
        }
        else
        {
            ApplyDeceleration();
            StopDrivingAudio();
        }
    }

    private void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        StopDrivingAudio();
    }

    private void ApplyMovement(Vector2 desiredVelocity)
    {
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredVelocity, driftFactor);
    }

    private void ApplyDeceleration()
    {
        if (rb.linearVelocity.magnitude > 0)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationFactor);
        }
    }

    private void RotatePlayer()
    {
        if (rb.linearVelocity.magnitude <= 0) return;

        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if (!turningAudioSource.isPlaying)
        {
            turningAudioSource.Play();
        }
    }

    private void HandleBoosting()
    {
        if (boostCooldownTimer > 0)
        {
            boostCooldownTimer -= Time.deltaTime;
        }

        if (inputSubscription != null && inputSubscription.ShiftInput && CanBoost() && gameObject.CompareTag("Player"))
        {
            StartBoost();
        }
        else
        {
            StopBoost();
        }

        RechargeBoostEnergy();
    }


    private bool CanBoost()
    {
        return uiManager != null && uiManager.GetCurrentBoostEnergy() > 0 && boostCooldownTimer <= 0;
    }

    private void StartBoost()
    {
        isBoosting = true;
        currentSpeed = baseSpeed * speedBoostMultiplier;
        DrainBoostEnergy();
    }

    private void StopBoost()
    {
        if (isBoosting)
        {
            isBoosting = false;
            currentSpeed = baseSpeed;
            boostCooldownTimer = boostCooldownDuration;
        }
    }

    private void DrainBoostEnergy()
    {
        if (isBoosting && uiManager != null)
        {
            float newEnergy = uiManager.GetCurrentBoostEnergy() - boostDrainRate * Time.deltaTime;
            uiManager.SetBoostEnergy(newEnergy);

            if (newEnergy <= 0)
            {
                StopBoost();
            }
        }
    }

    private void RechargeBoostEnergy()
    {
        if (!isBoosting && boostCooldownTimer <= 0 && uiManager != null)
        {
            float newEnergy = uiManager.GetCurrentBoostEnergy() + boostRechargeRate * Time.deltaTime;
            uiManager.SetBoostEnergy(newEnergy);
        }
    }

    private void PlayDrivingAudio()
    {
        if (!drivingAudioSource.isPlaying)
        {
            drivingAudioSource.Play();
        }
    }

    private void StopDrivingAudio()
    {
        if (drivingAudioSource.isPlaying)
        {
            drivingAudioSource.Stop();
        }
    }

    private void UseActivePowerUp()
    {
        if (!gameObject.CompareTag("Player")) return;

        if (activePowerUp != null)
        {
            DeployActivePowerUp();
        }
        else
        {
            Debug.LogWarning("No active power-up to use!");
        }
    }


    private void DeployActivePowerUp()
    {
        if (activePowerUp is THU_PowerUps deployablePowerUp)
        {
            deployablePowerUp.Deploy(gameObject, null);
            RemoveActivePowerUp();
            Debug.Log("Power-up deployed!");
        }
        else
        {
            Debug.LogWarning("Active power-up is not deployable.");
        }
    }

    private void RemoveActivePowerUp()
    {
        activePowerUp = null;
        if (uiManager != null)
        {
            uiManager.ClearPowerUpUI();
        }
    }

    public void StopMovementTemporarily(float duration)
    {
        StartCoroutine(StopMovementCoroutine(duration));
    }

    private IEnumerator StopMovementCoroutine(float duration)
    {
        CanMove = false;
        yield return new WaitForSeconds(duration);
        CanMove = true;
    }
    public void ReduceSpeedTemporarily(float speedReductionFactor, float duration)
    {
        StartCoroutine(ReduceSpeedCoroutine(speedReductionFactor, duration));
    }

    private IEnumerator ReduceSpeedCoroutine(float speedReductionFactor, float duration)
    {
        float originalSpeed = baseSpeed;
        baseSpeed *= speedReductionFactor;
        yield return new WaitForSeconds(duration);
        baseSpeed = originalSpeed;
    }
    public void AssignPowerUp(THU_PowerUps powerUp)
    {
        activePowerUp = powerUp;

        if (uiManager != null)
        {
            uiManager.UpdatePowerUpUI(powerUp?.PowerUpIcon);
        }

        Debug.Log($"Power-up {powerUp.name} assigned to the player!");
    }
    public void ActivatePowerUp(THU_PowerUps powerUp)
    {
        if (powerUp == null)
        {
            Debug.LogWarning("Attempted to activate a null power-up!");
            return;
        }

        activePowerUp = powerUp;

        if (uiManager != null)
        {
            uiManager.UpdatePowerUpUI(powerUp.PowerUpIcon);
        }

        Debug.Log($"Activated power-up: {powerUp.name}");
    }

    public void AddBoostEnergy(float amount)
    {
        boostEnergy = Mathf.Clamp(boostEnergy + amount, 0, MaxBoostEnergy);
        if (uiManager != null)
        {
            uiManager.SetBoostEnergy(boostEnergy);
        }

        Debug.Log($"Boost energy increased by {amount}. Current energy: {boostEnergy}");
    }
    public THU_PowerUps GetPowerUpFromType(THU_PowerUpTypes type)
    {
        return THU_PowerUpType.GetPowerUpInstance(type);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with {collision.name}");

        if (collision.CompareTag("Interactable"))
        {
            THU_PowerUps powerUp = collision.GetComponent<THU_PowerUps>();
            if (powerUp != null)
            {
                powerUp.ApplyEffect(this);
                AssignPowerUp(powerUp);
                Destroy(collision.gameObject);
            }
        }
    }

}
