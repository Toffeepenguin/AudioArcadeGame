using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IHA_Player : MonoBehaviour
{
    public Vector2 movementInput;
    public float speed = 10f;
    public float minXBound;
    public float maxXBound;
    public float minYBound;
    public float maxYBound;

    private Rigidbody2D rb;

    public AudioClip hitSound;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (movementInput == new Vector2(0, 0))
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        KeepInBounds();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>(); //reads direction 
        rb.linearVelocity = movementInput * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Puck"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(1000 * movementInput);
        }
        audio.PlayOneShot(hitSound);

    }

    private void KeepInBounds()
    {
        if (gameObject.transform.position.x >= maxXBound)
        {
            gameObject.transform.position = new Vector3(maxXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x <= minXBound)
        {
            gameObject.transform.position = new Vector3(minXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.y >= maxYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, maxYBound, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.y <= minYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, minYBound, gameObject.transform.position.z);
        }
    }

}
