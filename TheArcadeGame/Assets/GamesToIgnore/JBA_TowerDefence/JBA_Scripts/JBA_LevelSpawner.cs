using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class JBA_LevelSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] JBA_enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int JBA_baseEnemies = 8;
    [SerializeField] private float JBA_enemiesPerSecond = 0.5f;
    [SerializeField] private float JBA_timeBetweenWaves = 5f;
    [SerializeField] private float JBA_difficultyScalingFactor = 0.75f;
    

    [Header("Events")]
    public static UnityEvent JBA_onEnemyDestroy = new UnityEvent();

    public JBA_GameOverScreen JBA_GameOverScreen;
    public JBA_Money JBA_Money;

    private int JBA_currentWave = 1;
    private float JBA_timeSinceLastSpawn;
    private int JBA_enemiesAlive;
    private int JBA_enemiesLeftToSpawn;
    private bool JBA_isSpawning = false;
    private bool JBA_Trophy = false;

    private void Awake()
    {
        JBA_onEnemyDestroy.AddListener(JBA_EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(JBA_StartWave());
    }

    private void Update()
    {
        Debug.Log(JBA_LevelHandler.JBA_Score);

        if (JBA_LevelHandler.JBA_Score >= 300)
        {
            if (PlayerPrefs.GetInt("JBA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("JBA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }

        if (!JBA_isSpawning) return;

        JBA_GameOverScreen.Setup(JBA_LevelHandler.JBA_Score);

        JBA_Money.Setup(JBA_LevelHandler.JBA_Money, JBA_LevelHandler.JBA_main.JBA_PlayerHealth);

        JBA_timeSinceLastSpawn += Time.deltaTime;

        if (JBA_timeSinceLastSpawn >= (1f / JBA_enemiesPerSecond) && JBA_enemiesLeftToSpawn > 0)
        {
            JBA_SpawnEnemy();
            JBA_enemiesLeftToSpawn--;
            JBA_enemiesAlive++;
            JBA_timeSinceLastSpawn = 0f;
        }

        if (JBA_enemiesAlive == 0 && JBA_enemiesLeftToSpawn == 0)
        {
            JBA_EndWave();
        }
    }

    private void JBA_EndWave()
    {
        JBA_isSpawning = false;
        JBA_timeSinceLastSpawn = 0f;
        JBA_currentWave++;
        StartCoroutine(JBA_StartWave());
        JBA_LevelHandler.JBA_Score += 10;
        JBA_LevelHandler.JBA_Money += 100;
        Debug.Log(JBA_LevelHandler.JBA_Health);
        JBA_Bullet.JBA_bulletDamage -= 0.1f;
        JBA_LevelHandler.JBA_BulletClear = true;

    }

    private void JBA_EnemyDestroyed()
    {
        JBA_enemiesAlive--;
        JBA_LevelHandler.JBA_Score += 2;
        JBA_LevelHandler.JBA_Money += 10;
    }

    private void JBA_SpawnEnemy()
    {
        GameObject JBA_prefabToSpawn = JBA_enemyPrefabs[0];
        Instantiate(JBA_prefabToSpawn, JBA_LevelHandler.JBA_main.JBA_startPoint.position, Quaternion.identity);
    }

    private IEnumerator  JBA_StartWave()
    {
        yield return new WaitForSeconds(JBA_timeBetweenWaves);
        JBA_isSpawning = true;
        JBA_enemiesLeftToSpawn = JBA_enemiesPerWave();
    }

    private int JBA_enemiesPerWave()
    {
        return Mathf.RoundToInt(JBA_baseEnemies * Mathf.Pow(JBA_currentWave, JBA_difficultyScalingFactor));
    }
}
