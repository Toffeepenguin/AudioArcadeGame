using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THU_PowerUpType : MonoBehaviour
{
    public enum THU_PowerUpTypes
    {
        None,
        Bird,
        Oil,
        Pillow,
        MysteryBox
    }

    public static string GetPowerUpDescription(THU_PowerUpTypes type)
    {
        switch (type)
        {
            case THU_PowerUpTypes.Bird:
                return "Bird Power-Up: Distracts enemies!";
            case THU_PowerUpTypes.Oil:
                return "Oil Power-Up: Slows movement!";
            case THU_PowerUpTypes.Pillow:
                return "Pillow Power-Up: Temporary immobilization!";
            case THU_PowerUpTypes.MysteryBox:
                return "Mystery Box: Contains a random power-up!";
            default:
                return "No power-up applied.";
        }
    }

    public static THU_PowerUps GetPowerUpInstance(THU_PowerUpTypes type)
    {
        switch (type)
        {
            case THU_PowerUpTypes.Bird:
                return FindObjectOfType<THU_Bird>();
            case THU_PowerUpTypes.Oil:
                return FindObjectOfType<THU_Oil>();
            case THU_PowerUpTypes.Pillow:
                return FindObjectOfType<THU_Pillow>();
            default:
                Debug.LogWarning($"No matching power-up class found for type: {type}");
                return null;
        }
    }


}
