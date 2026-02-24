using UnityEngine;
using static THU_PowerUpType;

[RequireComponent(typeof(Collider2D))]
public class THU_PowerUpPickUp : MonoBehaviour
{
    [Header("PowerUp Type")]
    public THU_PowerUpTypes powerUpType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        THU_PlayerMovement player = collision.gameObject.GetComponent<THU_PlayerMovement>();
        if (player != null)
        {
            HandlePlayerInteraction(player);
            return;
        }

        THU_EnemyMovement enemy = collision.gameObject.GetComponent<THU_EnemyMovement>();
        if (enemy != null)
        {
            HandleEnemyInteraction(enemy);
        }
    }

    private void HandlePlayerInteraction(THU_PlayerMovement player)
    {
        Debug.Log($"Power-up {powerUpType} collided with player!");

        THU_PowerUpTypes resolvedType = ResolvePowerUpType();
        THU_PowerUps powerUpInstance = THU_PowerUpType.GetPowerUpInstance(resolvedType);

        if (powerUpInstance != null)
        {
            Debug.Log($"Player picked up {resolvedType} power-up.");
            player.AssignPowerUp(powerUpInstance);
        }
        else
        {
            Debug.LogWarning($"No valid power-up instance for {resolvedType}.");
        }

        Destroy(gameObject);
    }

    private void HandleEnemyInteraction(THU_EnemyMovement enemy)
    {
        Debug.Log($"Power-up {powerUpType} collided with enemy!");

        THU_PowerUpTypes resolvedType = ResolvePowerUpType();
        THU_PowerUps powerUpInstance = THU_PowerUpType.GetPowerUpInstance(resolvedType);

        if (powerUpInstance != null)
        {
            Debug.Log($"Enemy activated {resolvedType} power-up.");
            enemy.ActivatePowerUp(powerUpInstance);
        }
        else
        {
            Debug.LogWarning($"No valid power-up instance for {resolvedType}.");
        }

        Destroy(gameObject);
    }

    private THU_PowerUpTypes ResolvePowerUpType()
    {
        if (powerUpType == THU_PowerUpTypes.MysteryBox)
        {
            THU_PowerUpTypes[] possiblePowerUps = {
                THU_PowerUpTypes.Bird,
                THU_PowerUpTypes.Oil,
                THU_PowerUpTypes.Pillow
            };
            int randomIndex = Random.Range(0, possiblePowerUps.Length);
            return possiblePowerUps[randomIndex];
        }
        return powerUpType;
    }
}
