using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class JBA_Cursor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D JBA_rb;
    [SerializeField] private float JBA_bps = 1f;
    [SerializeField] private AudioSource JBA_audioSource;
    InputSubscription getInput;
    Vector2 PlayerMovement;
    float speed = 5f;
    bool onPlot = false;
    GameObject tower;
    private float JBA_timeUntilFire;
    public GameObject JBA_gameOver;

    // Start is called before the first frame update
    void Start()
    {
        getInput = GetComponent<InputSubscription>();
        JBA_gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (JBA_LevelHandler.JBA_GameOver == true)
        {
            JBA_gameOver.SetActive (true);
        }
        if (JBA_LevelHandler.JBA_GameOver == false)
        {
            JBA_gameOver.SetActive(false);
        }
        PlayerMovement = new Vector2(getInput.NormalizedMovementInput.x, getInput.NormalizedMovementInput.y);
        JBA_rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * speed;
        JBA_timeUntilFire += Time.deltaTime;
        if (getInput.EInput || getInput.ConfirmInput)
        {
            if (onPlot == true)
            {
                if (JBA_timeUntilFire > 1f / JBA_bps && JBA_LevelHandler.JBA_Money >= 100) {
                    GameObject towerBuild = JBA_BuildManager.main.GetSelectedTower();
                    tower = Instantiate(towerBuild, transform.position, Quaternion.identity);
                    onPlot = false;
                    JBA_timeUntilFire = 0f;
                    JBA_LevelHandler.JBA_Money -= 100;
                    JBA_audioSource.Play();
                }
            }
        }

        if (getInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Collided");
        if (other.gameObject.name == "JBA_Plot"|| other.gameObject.name == "JBA_Plot (1)" || other.gameObject.name == "JBA_Plot (2)" || other.gameObject.name == "JBA_Plot (3)" || other.gameObject.name == "JBA_Plot (4)" || other.gameObject.name == "JBA_Plot (5)")
        {
            onPlot = true;
        }
        else
        {
            onPlot = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onPlot = false;
    }
}
