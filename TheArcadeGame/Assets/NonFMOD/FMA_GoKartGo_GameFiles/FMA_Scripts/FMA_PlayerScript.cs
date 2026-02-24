using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    InputSubscription GetInput;
    Rigidbody2D rb;

    Vector2 PlayerMovement;
    float Speed = 20f;


    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        PlayerMovement = new Vector2(GetInput.NormalizedMovementInput.x, GetInput.NormalizedMovementInput.y);
        rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * Speed;

        if (GetInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }
}
