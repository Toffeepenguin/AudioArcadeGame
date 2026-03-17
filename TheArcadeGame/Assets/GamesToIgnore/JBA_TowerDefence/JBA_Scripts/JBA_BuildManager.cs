using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBA_BuildManager : MonoBehaviour
{
    public static JBA_BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] JBA_towerPrefabs;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return JBA_towerPrefabs[selectedTower];
    }
}
