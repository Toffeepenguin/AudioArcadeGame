using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    int age;
    float spawn_time;
    bool fall = false;
    MeshRenderer rndr;

    void Start()
    {
        age = 0;
        spawn_time = Time.time;
        rndr = GetComponent<MeshRenderer>();
        rndr.material.color = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, 0);
    }

    void Update()
    {
        if (Time.time - spawn_time <.5f)
        {
            rndr.material.color = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, Mathf.Sin((Time.time - spawn_time) % 1 * Mathf.PI));
            transform.position = new Vector3(transform.position.x, Mathf.Sin((Time.time - spawn_time) % 1 * Mathf.PI) * 2 - 5, transform.position.z);
        }
        if (age >= 3 && !fall)
        {
            GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x,
            GetComponent<BoxCollider>().center.y - 4, GetComponent<BoxCollider>().center.z);
            fall = true;
            spawn_time = Time.time;
        }
        if (age >= 3 && transform.position.y > -6.9f && Time.time - spawn_time < .5f)
        {
            rndr.material.color = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, Mathf.Cos((Time.time - spawn_time) % 1 * Mathf.PI));
            transform.position = new Vector3(transform.position.x, Mathf.Cos((Time.time - spawn_time) % 1 * Mathf.PI * 2) * 2 - 5, transform.position.z);
        }
    }

    public void updateAge()
    {
        age++;
    }
}
