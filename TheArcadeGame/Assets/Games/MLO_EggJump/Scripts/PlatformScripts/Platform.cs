using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshRenderer platform_renderer;
    private BoxCollider platform_collider;
    private Color temp_col;
    public Vector2 transition_times;
    private bool fallen = true;

    void Awake()
    {
        platform_renderer = GetComponent<MeshRenderer>();
        platform_collider = GetComponent<BoxCollider>();
        temp_col = platform_renderer.material.color;
        platform_renderer.material.color = new Color(temp_col.r, temp_col.g, temp_col.b, 0);
    }

    public void Rise(Vector3 pos)
    {
        if (!fallen) return;
        transform.position = pos;
        fallen = false;
        StopAllCoroutines();
        StartCoroutine(RiseRoutine());
    }

    public void Fall()
    {
        if (fallen) return;
        fallen = true;
        StopAllCoroutines();
        StartCoroutine(FallRoutine());
    }

    private IEnumerator RiseRoutine()
    {
        if (platform_collider != null) platform_collider.enabled = true;
        float elapsed = 0f;
        while (elapsed < transition_times.x)
        {
            float t = elapsed / transition_times.x;
            float sin = Mathf.Sin(t * Mathf.PI / 2f);
            temp_col.a = sin;
            platform_renderer.material.color = temp_col;
            transform.position = new Vector3(transform.position.x, sin * 3f - 6f, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FallRoutine()
    {
        if (platform_collider != null) platform_collider.enabled = false;
        float elapsed = 0f;
        while (elapsed < transition_times.y)
        {
            if (transform.position.y <= -6.9f) break;
            float t = elapsed / transition_times.y;
            float cos = Mathf.Cos(t * Mathf.PI / 2f);
            temp_col.a = cos;
            platform_renderer.material.color = temp_col;
            transform.position = new Vector3(transform.position.x, cos * 3f - 6f, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

