using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LTA_CarHandler : MonoBehaviour
{
    Rigidbody LTA_rb;
    InputSubscription getInput;
    AudioSource CarSound;
    float LTA_accelerationMultiplier = 2;
    float LTA_breaksMultiplier = 10;
    float LTA_steeringMultiplier = 1;
    float LTA_maxSteerVelocity = 2;

    Vector2 input = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        LTA_rb = GetComponent<Rigidbody>();
        getInput = GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        //////////added by Izzy////////////
        if (getInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        //////////////////////////////////
        ///
        LTA_Steer();
    }

    private void FixedUpdate()
    {
        
        if (getInput.NormalizedMovementInput.y > 0f)
        {
            LTA_Accelerate();
        }
        else { LTA_rb.linearDamping = 0.2f; }
        
        if(getInput.NormalizedMovementInput.y < 0f)
        {
            LTA_Brake();
        }
    }

    void LTA_Accelerate()
    {
        LTA_rb.linearDamping = 0;

        LTA_rb.AddForce(LTA_rb.transform.forward * LTA_accelerationMultiplier * getInput.NormalizedMovementInput.y);
    }

    void LTA_Brake()
    {
        if(LTA_rb.linearVelocity.z <= 0)
        {
            return;
        }

        LTA_rb.AddForce(LTA_rb.transform.forward * LTA_breaksMultiplier * getInput.NormalizedMovementInput.y);
    }

    void LTA_Steer()
    {
        if (Mathf.Abs(getInput.NormalizedMovementInput.x) > 0f)
        {
            float speedBaseSteerLimit = LTA_rb.linearVelocity.z / 1.0f;
            //speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);

            //normalize the x velocity
            float normalizedX = LTA_rb.linearVelocity.x / LTA_maxSteerVelocity;

            //doesn't allow to get bigger than 1 in magnitude
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);

            LTA_rb.linearVelocity = new Vector3(normalizedX * LTA_maxSteerVelocity, 0, LTA_rb.linearVelocity.z);

            //move right
            LTA_rb.AddForce(LTA_rb.transform.right * LTA_steeringMultiplier * getInput.NormalizedMovementInput.x * speedBaseSteerLimit);
        }
        else if (Mathf.Abs(getInput.NormalizedMovementInput.x) < 0f)
        {
            float speedBaseSteerLimit = LTA_rb.linearVelocity.z / 5.0f;
            //speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);

            //normalize the x velocity
            float normalizedX = LTA_rb.linearVelocity.x / LTA_maxSteerVelocity;

            //doesn't allow to get bigger than 1 in magnitude
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);

            LTA_rb.linearVelocity = new Vector3(normalizedX * LTA_maxSteerVelocity, 0, LTA_rb.linearVelocity.z);

            // move left
            LTA_rb.AddForce(-LTA_rb.transform.right * LTA_steeringMultiplier * getInput.NormalizedMovementInput.x * speedBaseSteerLimit);
        }
        else
        {
            LTA_rb.linearVelocity = Vector3.Lerp(LTA_rb.linearVelocity, new Vector3(0, 0, LTA_rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    public void LTA_SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();

        input = inputVector;
    }
}
