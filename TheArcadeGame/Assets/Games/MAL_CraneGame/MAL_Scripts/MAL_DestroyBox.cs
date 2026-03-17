using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAL_DestroyBox : MonoBehaviour
{
    [SerializeField] GameObject splash;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Box"))
        {
            Instantiate(splash, new Vector3(collision.transform.position.x, collision.transform.position.y - 0.75f, -5), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Box"))
        {
            Destroy(collision.gameObject);
        }
    }
}
