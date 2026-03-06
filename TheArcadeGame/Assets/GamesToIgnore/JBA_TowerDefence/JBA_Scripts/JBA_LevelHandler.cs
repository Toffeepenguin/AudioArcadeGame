using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JBA_LevelHandler : MonoBehaviour
{
    public static JBA_LevelHandler JBA_main;

    public Transform JBA_startPoint; //Define the start for enemies to spawn from
    public Transform[] JBA_path; //Array for the points which the enemies will follow
    public int JBA_PlayerHealth = 10;
    public static int JBA_Score;
    public static bool JBA_GameOver;
    public static int JBA_Money;
    public static int JBA_Health;
    public static bool JBA_BulletClear;

    private void Awake()
    {
        JBA_main = this;
        JBA_GameOver = false;
        JBA_Score = 0;
        JBA_Money = 100;
        JBA_Health = 2;
        JBA_PlayerHealth = 10;
    }
}
