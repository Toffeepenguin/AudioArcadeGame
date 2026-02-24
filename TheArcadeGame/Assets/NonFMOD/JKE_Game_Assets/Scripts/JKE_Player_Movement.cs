using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JKE_Player_Movement : MonoBehaviour
{
    InputSubscription GetInput;
    //https://www.youtube.com/watch?v=lVtL_xD-gTg&list=PLfX6C2dxVyLylMufxTi7DM9Vjlw5bff1c&index=2
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform basePos;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float jumpTime = 0.2f;
    
    private bool isGrounded = false;
    private bool isJumping = false;
    public float jumpTimer;
    public bool JumpCheck;
    JKE_GameManager gm;


    private void Start()
    {
        GetInput = GetComponent<InputSubscription>();
        rb = GetComponent<Rigidbody2D>();
        gm = JKE_GameManager.Instance;
    }
    void Update()
    {
        if (gm.isPlaying)
        {
            //JUMP
            JumpCheck = GetInput.SpaceInput;
            isGrounded = Physics2D.OverlapCircle(basePos.position, groundDistance, groundLayer);

            if (isGrounded && GetInput.SpaceInput && !isJumping)
            {
                isJumping = true;
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimer = jumpTime;
            }
            //Longer jump held, higher jump goes
            if (isJumping && GetInput.SpaceInput)
            {
                if (jumpTimer >= 0f)
                {
                    rb.linearVelocity = Vector2.up * jumpForce;
                    jumpTimer -= Time.deltaTime;
                }
                else if (jumpTimer <= 0f)
                {
                    isJumping = false;
                }
            }

            if (!GetInput.SpaceInput)
            {
                isJumping = false;
            }
        }
    }
}
