using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static THU_PowerUpType;

[ExecuteAlways]
public class THU_EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2.5f;
    public float speedBoostMultiplier = 1.5f;
    public float driftFactor = 0.5f;
    private float currentSpeed;
    private float baseSpeed;
    private bool isBoosting = false;
    private bool isSlowedDown = false;

    [Header("Boost Settings")]
    public float boostEnergy = 0f;
    public float boostEnergyThreshold = 10f;
    public float boostDuration = 5f;
    public float boostEnergyGainRate = 2f;
    private bool canBoost = true;

    [Header("Avoid and Collect Objects")]
    public float detectionRadius = 5f;
    public float avoidanceRadius = 3f;
    public float powerUpSeekMultiplier = 1.2f;
    private THU_PowerUps currentPowerUp;

    private float powerUpCooldown = 5f;
    private float nextPowerUpTime = 0f;


    private Vector3 avoidanceVector = Vector3.zero;
    private Vector3 directionToTarget = Vector3.zero;
    private GameObject targetPowerUp;

    [Header("Pathfinding")]
    public Transform[] waypoints;
    private List<Vector3> smoothPath = new List<Vector3>();
    private int currentSegmentIndex = 0;
    private float t = 0f;

    private Rigidbody2D rb;
    private bool isGameActive = true;

    private float powerUpTargetCooldown = 1f;
    private float nextPowerUpSeekTime = 0f;

    public bool CanMove { get; set; } = true;

    private void Start()
    {
        baseSpeed = speed;
        currentSpeed = speed;

        if (waypoints.Length < 2)
        {
            Debug.LogError("At least 2 waypoints are required for smooth pathfinding!");
            return;
        }

        foreach (Transform waypoint in waypoints)
        {
            if (waypoint == null)
            {
                Debug.LogError("One or more waypoints are null. Please assign all waypoints correctly.");
                return;
            }
        }

        smoothPath = GenerateSmoothPath(waypoints, resolution: 10);
        if (smoothPath.Count > 0)
            transform.position = smoothPath[0];
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from the GameObject!");
        }
    }

    private void Update()
    {
        if (!isGameActive) return;

        GainBoostEnergy();
        DetectNearbyObjects();
        SeekPowerUpOrAvoidPlayer();
        MoveAlongPath();

        if (boostEnergy >= boostEnergyThreshold && canBoost)
        {
            StartCoroutine(ActivateBoost());
        }
    }

    private void DetectNearbyObjects()
    {
        if (!CanMove || !gameObject.CompareTag("Enemy")) return;

        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        avoidanceVector = Vector3.zero;
        targetPowerUp = null;

        foreach (Collider2D obj in nearbyObjects)
        {
            if (obj.CompareTag("Player"))
            {
                Vector3 directionAway = (transform.position - obj.transform.position).normalized;
                avoidanceVector += directionAway / Vector3.Distance(transform.position, obj.transform.position);
            }

            if (obj.CompareTag("Interactable"))
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (targetPowerUp == null || distance < Vector3.Distance(transform.position, targetPowerUp.transform.position))
                {
                    targetPowerUp = obj.gameObject;
                }
            }
        }

        avoidanceVector = avoidanceVector.normalized;
    }

    private void SeekPowerUpOrAvoidPlayer()
    {
        if (!CanMove) return;

        if (targetPowerUp != null && Time.time >= nextPowerUpSeekTime)
        {
            directionToTarget = (targetPowerUp.transform.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, directionToTarget, Time.deltaTime * 2f);
            currentSpeed = baseSpeed * powerUpSeekMultiplier;
            nextPowerUpSeekTime = Time.time + powerUpTargetCooldown;
        }
        else
        {
            if (avoidanceVector != Vector3.zero)
            {
                transform.up = Vector3.Lerp(transform.up, avoidanceVector, driftFactor);
            }

            currentSpeed = baseSpeed;
        }
    }

    private void MoveAlongPath()
    {
        if (smoothPath.Count < 2 || !CanMove) return;

        Vector3 startPoint = smoothPath[currentSegmentIndex];
        Vector3 endPoint = smoothPath[GetWrappedSegmentIndex(currentSegmentIndex + 1)];

        float segmentDistance = Vector3.Distance(startPoint, endPoint);
        t += Time.deltaTime * currentSpeed / segmentDistance;

        if (t >= 1f)
        {
            t = 0f;
            currentSegmentIndex = GetWrappedSegmentIndex(currentSegmentIndex + 1);

            if (currentSegmentIndex >= smoothPath.Count)
            {
                currentSegmentIndex = smoothPath.Count - 1;
                Debug.LogWarning("Path index out of range. Correcting...");
            }
        }

        transform.position = Vector3.Lerp(startPoint, endPoint, t);

        Vector3 pathDirection = (endPoint - startPoint).normalized;
        Vector3 finalDirection = Vector3.Lerp(pathDirection, avoidanceVector, driftFactor);
        transform.up = finalDirection.normalized;
    }


    private List<Vector3> GenerateSmoothPath(Transform[] waypoints, int resolution)
    {
        if (resolution < 1) resolution = 1;

        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 p0 = waypoints[GetWrappedIndex(i - 1)].position;
            Vector3 p1 = waypoints[i].position;
            Vector3 p2 = waypoints[GetWrappedIndex(i + 1)].position;
            Vector3 p3 = waypoints[GetWrappedIndex(i + 2)].position;

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                path.Add(CatmullRom(p0, p1, p2, p3, t));
            }
        }
        return path;
    }

    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * ((2f * p1) +
                       (-p0 + p2) * t +
                       (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
                       (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    private int GetWrappedSegmentIndex(int index)
    {
        return index % smoothPath.Count;
    }

    private int GetWrappedIndex(int index)
    {
        return (index + waypoints.Length) % waypoints.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") && collision.gameObject == targetPowerUp)
        {
            Debug.Log("Enemy collected a power-up!");
            var powerUp = collision.GetComponent<THU_PowerUps>();
            if (powerUp != null)
            {
                ApplyPowerUp(powerUp);
            }
            Destroy(collision.gameObject);
            targetPowerUp = null;
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with a player!");

            Vector3 directionAwayFromPlayer = (transform.position - collision.transform.position).normalized;

            float evasionDistance = avoidanceRadius; 
            Vector3 evasionTarget = transform.position + directionAwayFromPlayer * evasionDistance;

            StartCoroutine(MoveAwayFromPlayer(evasionTarget, 0.5f));
        }
    }

    private IEnumerator MoveAwayFromPlayer(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);

        if (targetPowerUp != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetPowerUp.transform.position);
        }

        if (smoothPath != null && smoothPath.Count > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < smoothPath.Count - 1; i++)
            {
                Gizmos.DrawLine(smoothPath[i], smoothPath[i + 1]);
            }
        }
    }

    public void SlowDown(float slowFactor)
    {
        if (!isSlowedDown)
        {
            currentSpeed *= slowFactor;
            isSlowedDown = true;
            Debug.Log($"Enemy slowed down to {currentSpeed} (factor: {slowFactor})");

            StartCoroutine(RestoreSpeedAfterDelay(3f));
        }
    }

    private System.Collections.IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentSpeed = baseSpeed;
        isSlowedDown = false;
        Debug.Log("Enemy speed restored to normal.");
    }

    public GameObject GetNearestPlayer()
    {
        var players = new GameObject[] { GameObject.Find("THU_Player") };
        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player;
            }
        }

        return nearestPlayer;
    }

    public void ApplyPowerUp(THU_PowerUps powerUp)
    {
        if (powerUp == null)
        {
            Debug.LogWarning("Attempted to activate a null power-up on an enemy!");
            return;
        }

        if (Time.time < nextPowerUpTime)
        {
            Debug.Log("Enemy is on cooldown and cannot use a power-up yet!");
            return;
        }

        nextPowerUpTime = Time.time + powerUpCooldown;
        powerUp.ApplyEffect(this);
        Debug.Log($"Enemy activated power-up: {powerUp.name}");
    }

    public void OnPlayerPowerUp(THU_PowerUps powerUp)
    {
        powerUp.ApplyEffect(this);
        Debug.Log("Enemy affected by player's power-up!");
    }

    public void SetCurrentPowerUp(THU_PowerUps powerUp, bool isTemporary)
    {
        if (currentPowerUp != null)
        {
            Debug.LogWarning("Replacing existing power-up.");
        }

        currentPowerUp = powerUp;
        Debug.Log($"Enemy power-up set: {currentPowerUp.name}, Temporary: {isTemporary}");

        if (isTemporary)
        {
            StopAllCoroutines();
            StartCoroutine(ClearPowerUpAfterDuration(5f));
        }
    }

    private IEnumerator ClearPowerUpAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (currentPowerUp != null)
        {
            Debug.Log("Enemy power-up expired.");
            currentPowerUp = null;
        }
    }


    public void ResetEnemy()
    {
        CanMove = false;
        transform.position = waypoints[0].position;
        currentSpeed = baseSpeed;
    }

    private void GainBoostEnergy()
    {
        if (!gameObject.CompareTag("Enemy")) return;

        boostEnergy += boostEnergyGainRate * Time.deltaTime;
        boostEnergy = Mathf.Clamp(boostEnergy, 0f, boostEnergyThreshold);
    }


    private IEnumerator ActivateBoost()
    {
        isBoosting = true;
        canBoost = false;
        boostEnergy = 0f;

        float originalSpeed = currentSpeed;
        currentSpeed = baseSpeed * speedBoostMultiplier;
        Debug.Log("Enemy Boost Activated!");

        yield return new WaitForSeconds(boostDuration);

        currentSpeed = originalSpeed;
        isBoosting = false;
        canBoost = true;
        Debug.Log("Enemy Boost Deactivated.");
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
    public void ActivatePowerUp(THU_PowerUps powerUp)
    {
        if (powerUp == null)
        {
            Debug.LogWarning("Attempted to activate a null power-up on an enemy!");
            return;
        }

        powerUp.ApplyEffect(this);
        Debug.Log($"Enemy activated power-up: {powerUp.name}");
    }


    private void ApplyPowerUpEffect(THU_EnemyMovement enemy, THU_PowerUpTypes type)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Enemy is null. Cannot apply power-up effect.");
            return;
        }

        THU_PowerUps powerUp = THU_PowerUpType.GetPowerUpInstance(type);

        if (powerUp == null)
        {
            Debug.LogWarning($"No valid power-up instance found for type: {type}");
            return;
        }

        enemy.ActivatePowerUp(powerUp);
        Debug.Log($"Applied power-up {type} to enemy.");
    }

}

