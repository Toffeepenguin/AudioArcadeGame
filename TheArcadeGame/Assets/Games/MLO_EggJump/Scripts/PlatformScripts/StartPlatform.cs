using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    bool fall;
    bool falling;
    float spawn_time;
    MeshRenderer rndr;

    void Start()
    {
        fall = false;
        rndr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (fall)
        {
            spawn_time = Time.time;
            falling = true;
            fall = false;
        }
        if (falling && transform.position.y > -7f && Time.time - spawn_time < .5f)
        {
            rndr.material.color = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, Mathf.Cos((Time.time - spawn_time) % 1 * Mathf.PI));
            transform.position = new Vector3(transform.position.x, Mathf.Cos((Time.time - spawn_time) % 1 * Mathf.PI * 2) * 2 - 5, transform.position.z);
        }
    }

    public void Fall()
    {
        fall = true;
    }

    public void PlatformReset()
    {
        fall = false;
        transform.position = new Vector3(0, -3, 0);
        rndr.material.color = new Color(rndr.material.color.r, rndr.material.color.g, rndr.material.color.b, 1);
    }
}
