using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RRG_Car_Move : MonoBehaviour
{
    public RRG_Jumping jumping;

    public Transform playerReference;

    float carSpeed = 10;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.position = new Vector3(13.06f, 2.05f, 0);
        carSpeed = carSpeed += randomMoveSpeed(15, 25) / 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        movementCar();

        if (playerReference.position.x - 60 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    void movementCar()
    {
        Vector3 carPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        carPosition = new Vector3(carSpeed, 0, 0) * Time.deltaTime;


        transform.Translate(carPosition);
    }

    public float randomMoveSpeed(int minSpeed, int maxSpeed)
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        return randomSpeed;
    }
}
