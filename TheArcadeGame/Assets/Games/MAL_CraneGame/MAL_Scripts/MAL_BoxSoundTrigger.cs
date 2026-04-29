using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MAL_BoxSoundTrigger : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxSpeedSinceLastHit;
    [SerializeField] EventReference boxHitEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeedSinceLastHit) { maxSpeedSinceLastHit = rb.linearVelocity.magnitude; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Chain") && !collision.gameObject.name.Contains("Water"))
        {
            PlaySoundEffect(boxHitEvent);
            maxSpeedSinceLastHit = 0;
        }
    }


    private void PlaySoundEffect(EventReference SoundEffect)
    {
        EventInstance instance = RuntimeManager.CreateInstance(SoundEffect);
        instance.setParameterByName("BoxHitSpeed", maxSpeedSinceLastHit);
        instance.setParameterByName("BoxPosition", (transform.position.x + 8.33f) / 16.66f);
        instance.start();
        instance.release();

    }
}
