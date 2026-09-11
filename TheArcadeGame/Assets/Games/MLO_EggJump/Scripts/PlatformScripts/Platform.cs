using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Platform : MonoBehaviour
{
    private MeshRenderer platform_renderer;
    private BoxCollider platform_collider;
    private MaterialPropertyBlock prop_block;
    public Vector2 transition_times;
    protected bool fallen = true;

    void Awake()
    {
        platform_renderer = GetComponent<MeshRenderer>();
        platform_collider = GetComponent<BoxCollider>();
        prop_block = new MaterialPropertyBlock();
        SetAlpha(0f);
    }

    public void Rise(Vector3 pos)
    {
        if (!fallen) return;
        transform.SetPositionAndRotation(pos, Quaternion.identity);
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
            SetAlpha(sin);
            transform.position = new Vector3(transform.position.x, sin * 3f - 6f, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);
        transform.position = new Vector3(transform.position.x, -3f, transform.position.z);
    }

    private IEnumerator FallRoutine()
    {
        if (platform_collider != null) platform_collider.enabled = false;
        float elapsed = 0f;
        float tilt = 15f;
        Quaternion target_rotation = Quaternion.Euler(
            Random.Range(-tilt, tilt), 0f,
            Random.Range(-tilt, tilt));
        while (elapsed < transition_times.y)
        {
            if (transform.position.y <= -6.9f) break;
            float t = elapsed / transition_times.y;
            float cos = Mathf.Cos(t * Mathf.PI / 2f);
            SetAlpha(cos);
            transform.SetPositionAndRotation(
                new Vector3(transform.position.x, cos * 3f - 6f, transform.position.z), 
                Quaternion.Slerp(Quaternion.identity, target_rotation, t));
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f);
        transform.SetPositionAndRotation(new Vector3(transform.position.x, -6f, transform.position.z), Quaternion.identity);
    }

    protected void SetAlpha(float alpha)
    {
        platform_renderer.GetPropertyBlock(prop_block);
        Color c = platform_renderer.sharedMaterial.color;
        c.a = alpha;
        prop_block.SetColor("_Color", c);
        platform_renderer.SetPropertyBlock(prop_block);
    }
}

