using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JRO_Camera : MonoBehaviour
{
    [SerializeField] private new Vector3 CameraSpeed;
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        CameraSpeed = new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position += CameraSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "JRO_PlayerSprite")
        {
            IAmSpeed(1.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "JRO_PlayerSprite")
        {
            IAmSpeed(0.3f);
        }
    }

    private void IAmSpeed(float y)
    {
        CameraSpeed = new Vector3(0, y, 0);
    }
}
