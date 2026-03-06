using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JKE_Player_Collision : MonoBehaviour
{
    public JKE_GameManager Instance;
    public JKE_Trophy_Manager TrophyManager;
    public int finalScore;

    private void Update()
    {
        finalScore = Instance.ActualScore;
        if (finalScore > 5000)
        {
            TrophyManager.playerWinSet(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            SceneManager.LoadScene("JKE_Scene");
        }
    }
}
