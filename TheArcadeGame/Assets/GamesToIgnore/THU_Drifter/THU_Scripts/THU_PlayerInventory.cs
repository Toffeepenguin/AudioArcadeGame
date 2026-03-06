using UnityEngine;
using static THU_PowerUpType;

public class THU_PlayerInventory : MonoBehaviour
{
    private THU_PowerUpTypes currentPowerUp = THU_PowerUpTypes.None;
    public static bool powerUpInUse = false;
    private bool hasActivePowerUp;

    private void Update()
    {
        if (currentPowerUp != THU_PowerUpTypes.None && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Player activated power-up: {currentPowerUp}");
            UsePowerUpOnPlayer();
        }
    }

    public void AddPowerUp(THU_PowerUpTypes powerUpType)
    {
        if (powerUpInUse)
        {
            Debug.Log("Cannot pick up a new power-up. One is already active.");
            return;
        }

        if (currentPowerUp == THU_PowerUpTypes.None)
        {
            currentPowerUp = powerUpType;
            Debug.Log($"Power-Up {powerUpType} collected!");
        }
        else
        {
            Debug.Log("You already have an active power-up. Use it before picking up another.");
        }
    }

    private void UsePowerUpOnPlayer()
    {
        if (currentPowerUp != THU_PowerUpTypes.None)
        {
            ApplyPowerUpEffectOnPlayer(currentPowerUp);
            currentPowerUp = THU_PowerUpTypes.None;
            powerUpInUse = false;
        }
        else
        {
            Debug.Log("No power-up to use!");
        }
    }

    public void EnemyUsesPowerUp(THU_PowerUpTypes powerUpType, THU_EnemyMovement enemy)
    {
        if (powerUpType != THU_PowerUpTypes.None)
        {
            Debug.Log($"Enemy automatically used power-up: {powerUpType}");
            ApplyPowerUpEffectOnEnemy(powerUpType, enemy);
        }
    }

    private void ApplyPowerUpEffectOnPlayer(THU_PowerUpTypes powerUpType)
    {
        switch (powerUpType)
        {
            case THU_PowerUpTypes.Bird:
                Debug.Log("Bird Power-Up Used by Player!");
                break;

            case THU_PowerUpTypes.Oil:
                Debug.Log("Oil Power-Up Used by Player!");
                break;

            case THU_PowerUpTypes.Pillow:
                Debug.Log("Pillow Power-Up Used by Player!");
                break;

            case THU_PowerUpTypes.MysteryBox:
                Debug.Log("Mystery Box Power-Up Used by Player!");
                break;

            default:
                Debug.LogWarning("Unknown PowerUpType for Player!");
                break;
        }
    }

    private void ApplyPowerUpEffectOnEnemy(THU_PowerUpTypes powerUpType, THU_EnemyMovement enemy)
    {
        switch (powerUpType)
        {
            case THU_PowerUpTypes.Bird:
                Debug.Log("Bird Power-Up Used on Enemy!");
                enemy.SlowDown(1.2f);
                break;

            case THU_PowerUpTypes.Oil:
                Debug.Log("Oil Power-Up Used on Enemy!");
                enemy.SlowDown(0.5f);
                break;

            case THU_PowerUpTypes.Pillow:
                Debug.Log("Pillow Power-Up Used on Enemy!");
                enemy.SlowDown(0.3f);
                break;

            case THU_PowerUpTypes.MysteryBox:
                Debug.Log("Mystery Box Power-Up Used on Enemy!");
                enemy.SlowDown(1.5f);
                break;

            default:
                Debug.LogWarning("Unknown PowerUpType for Enemy!");
                break;
        }
    }

    public void ClearActivePowerUp()
    {
        Debug.Log("Active power-up cleared.");
    }

    public bool HasActivePowerUp()
    {
        return hasActivePowerUp;
    }

    public void SetActivePowerUp(bool value)
    {
        hasActivePowerUp = value;
    }
}
