using UnityEngine;
using static THU_PowerUpType;

public abstract class THU_PowerUps : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public THU_PowerUpTypes powerUpType;
    public GameObject deployedObjectPrefab;
    public float deployOffset = 2f;
    public Sprite PowerUpIcon;

    protected THU_PlayerMovement playerReference;
    private GameObject target;

    public abstract void Deploy(GameObject deployer, GameObject target);

    public virtual void ApplyEffect(THU_PlayerMovement player)
    {
        Debug.Log($"Applying {powerUpType} effect to player.");
    }

    public virtual void ApplyEffect(THU_EnemyMovement enemy)
    {
        Debug.Log($"Applying {powerUpType} effect to enemy.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out THU_PlayerMovement player))
        {
            playerReference = player;
            target = FindNearestEnemy(player.transform.position);

            var powerUpInstance = THU_PowerUpType.GetPowerUpInstance(powerUpType);
            if (powerUpInstance != null)
            {
                player.AssignPowerUp(powerUpInstance);
            }
            else
            {
                Debug.LogWarning($"No valid power-up instance found for type: {powerUpType}");
            }
        }
        else if (collision.TryGetComponent(out THU_EnemyMovement enemy))
        {
            target = FindNearestPlayer(enemy.transform.position);

            var powerUpInstance = THU_PowerUpType.GetPowerUpInstance(powerUpType);
            if (powerUpInstance != null)
            {
                powerUpInstance.ApplyEffect(enemy);
                Deploy(enemy.gameObject, target);
            }
            else
            {
                Debug.LogWarning($"No valid power-up instance found for type: {powerUpType}");
            }
        }
    }

    private GameObject FindNearestEnemy(Vector3 position)
    {
        var enemies = FindObjectsOfType<THU_EnemyMovement>();
        return FindClosestObject(position, enemies);
    }

    private GameObject FindNearestPlayer(Vector3 position)
    {
        var players = FindObjectsOfType<THU_PlayerMovement>();
        return FindClosestObject(position, players);
    }

    private GameObject FindClosestObject(Vector3 position, Component[] objects)
    {
        GameObject closest = null;
        float shortestDistance = float.MaxValue;

        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(position, obj.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = obj.gameObject;
            }
        }

        return closest;
    }

    protected void DeployObject(GameObject deployer, GameObject target)
    {
        if (deployedObjectPrefab == null)
        {
            Debug.LogWarning("Deployed object prefab is null!");
            return;
        }

        Vector3 spawnPosition = deployer.transform.position + Vector3.up * deployOffset;
        Instantiate(deployedObjectPrefab, spawnPosition, Quaternion.identity);
    }


}
