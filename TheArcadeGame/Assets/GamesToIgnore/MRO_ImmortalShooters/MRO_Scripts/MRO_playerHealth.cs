using UnityEngine;
using UnityEngine.UI;

public class MRO_playerHealth : MonoBehaviour
{
    public MRO_UI ImmortalShooters_UI; 
    public AudioManager audioManager;
    public float maxHealth;
    public Image healthBar;
    public int health;
    private int powerUpsCollected = 0; 

    void Start()
    {
        maxHealth = health;
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)health / maxHealth, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
            {
                ImmortalShooters_UI.GameOver(powerUpsCollected); 
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Interactable"))
        {
            Destroy(collision.gameObject);
            powerUpsCollected++; 

            if (audioManager != null && audioManager.collectables != null)
            {
                audioManager.PlaySFX(audioManager.collectables);
            }
            else
            {
                Debug.LogWarning("Collectables sound clip or AudioManager is not assigned!");
            }
        }
    }

   
    public int GetPowerUpsCollected()
    {
        return powerUpsCollected;
    }
}
