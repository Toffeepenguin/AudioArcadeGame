using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHA_Spinner : MonoBehaviour
{

    public Vector3 rotationSpeed = new Vector3(0, 0, 100);
    [SerializeField] private float flingForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Puck"))
        {
            // Calculate the fling direction based on the contact point
            Vector2 collisionPoint = collision.contacts[0].point;
            Vector2 paddleCenter = transform.position;
            Vector2 flingDirection = (collisionPoint - paddleCenter).normalized;

            // Apply force in the calculated fling direction
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(flingDirection * flingForce, ForceMode2D.Impulse);

            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(rotationSpeed))
        }
    }
}
