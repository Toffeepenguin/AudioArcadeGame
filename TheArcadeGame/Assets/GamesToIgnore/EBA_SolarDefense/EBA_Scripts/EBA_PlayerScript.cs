using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class EBA_PlayerScript : MonoBehaviour
{
    [SerializeField] InputSubscription Getinput;
    [SerializeField] GameObject EBA_Bullet;
    [SerializeField] Transform fireposition;
    Rigidbody2D Erb;
    [SerializeField] GameObject EBA_gameOverMenu;
 public AudioSource source;
  public AudioClip shootclip;

    float speed = 10f;

    Vector2 PlayerMovement;
    

    float leftBoundary = -8.2f;
    float rightBoundary = 8f;
    float topBoundary = 11.6f;
    float bottomBoundary = 2f;

    [SerializeField] float cooldownTimer = 0.5f;
    float cooldown = 0f;
    void Awake()
    {
       Erb = GetComponent<Rigidbody2D>();
   
    }

    void Update()
    {
        //player movement
        PlayerMovement = new Vector2(Getinput.NormalizedMovementInput.x,Getinput.NormalizedMovementInput.y);

        Erb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * speed;

        //keep player within bounds
        Vector2 clampedPosition = Erb.position;
        
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomBoundary, topBoundary);
        Erb.position = clampedPosition;

        cooldown += Time.deltaTime;
        //press space to shoot
        if(Getinput.SpaceInput && cooldown >= cooldownTimer)
        {        
            shoot();
            cooldown = 0f;
            Time.timeScale = 1;
            
        }
    }

    void shoot() //shoot at player position
    {
        Instantiate(EBA_Bullet,fireposition.position,fireposition.rotation);

        if (source != null && shootclip != null)
        {
            source.PlayOneShot(shootclip);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EBA_Enemy(Clone)")
        {
            Destroy(gameObject);
            EBA_gameOverMenu.SetActive(true);

        }
    }


}
