using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HLL_Movement : MonoBehaviour
{

    [SerializeField] InputSubscription GetInput;
    Rigidbody2D rb;

    Vector2 PlayerMovement;
    float Speed = 10f; // speed of player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMovement = new Vector2(GetInput.NormalizedMovementInput.x , GetInput.NormalizedMovementInput.y); // PlayerMovement movement on x and y axis
        rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * Speed;

        if (GetInput.MenuInput)
        {
            SceneManager.LoadScene("HLL_Splashscreen");
        }
    }

}
