using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCH_Slider : MonoBehaviour
{
    public float movementSpeed = 10f;
    private float currentSpeed = 0f;
    private bool withinSlidingArea = false;

    private Rigidbody2D rb;
    GameObject inputObj;
    GameObject scoreBoardObj;
    private InputSubscription inputScp;
    private SCH_score scoreScp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputObj = GameObject.Find("SCH_InputSystem");
        scoreBoardObj = GameObject.Find("SCH_ScoreBoard");
        inputScp = inputObj.GetComponent<InputSubscription>();
        scoreScp = scoreBoardObj.GetComponent<SCH_score>();
        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //check for boundaries
        if (!withinSlidingArea) 
        {
            if (rb.position.y < -10 && inputScp.AnalogMovementInput.y < 0)
            {
                currentSpeed = 0;
                //Debug.Log("minimum");
            }
            else if(rb.position.y > 37 && inputScp.AnalogMovementInput.y > 0)
            {
                currentSpeed = 0;
                //Debug.Log("Exceed");
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }
        else
        {
            currentSpeed = movementSpeed;
        }
        rb.linearVelocity = new Vector2(0, inputScp.AnalogMovementInput.y * currentSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            //Debug.Log("Hitted");
            scoreScp.SCH_totalScore++;
            scoreScp.SCH_stringCombo++;
            Destroy (collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Interactable"))
        {
            withinSlidingArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            withinSlidingArea = false;
            //Debug.Log(rb.position.y);
        }
    }
}
