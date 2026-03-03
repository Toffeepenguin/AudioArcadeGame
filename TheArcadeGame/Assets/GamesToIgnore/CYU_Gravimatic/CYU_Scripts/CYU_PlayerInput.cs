using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CYU_PlayerInput : MonoBehaviour
{

    [SerializeField] InputSubscription CYU_Input;

    Rigidbody2D CYU_playerRB;

    public Animator CYU_playerAnimator;
    public SpriteRenderer CYU_playerSprite;

    float CYU_playerSpeed = 10f;
    float CYU_yVelocity = 15f;

    Vector2 CYU_playerMovement;

    [SerializeField] AudioSource CYU_flipSound;

    // Start is called before the first frame update
    void Awake()
    {
        CYU_playerRB = GetComponent<Rigidbody2D>();
        CYU_playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CYU_playerMovement = new Vector2(CYU_Input.NormalizedMovementInput.x, CYU_yVelocity);
        CYU_playerRB.linearVelocity = new Vector2(CYU_playerMovement.x * CYU_playerSpeed, CYU_yVelocity);

        if (CYU_Input.NormalizedMovementInput.x < -0.01)
        {
            CYU_playerAnimator.SetBool("CYU_isMoving", true);
            CYU_playerSprite.flipX = true;
        }
        if (CYU_Input.NormalizedMovementInput.x > 0.01)
        {
            CYU_playerAnimator.SetBool("CYU_isMoving", true);
            CYU_playerSprite.flipX = false;
        }
        if (CYU_Input.NormalizedMovementInput.x == 0)
        {
            CYU_playerAnimator.SetBool("CYU_isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "TopWall" || collision.gameObject.name == "BottomWall")
        {
            CYU_yVelocity = -CYU_yVelocity;
            CYU_flipSound.Play();
        }
    }
}
