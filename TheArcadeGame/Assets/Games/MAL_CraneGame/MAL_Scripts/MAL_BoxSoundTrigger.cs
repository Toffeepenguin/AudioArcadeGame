using UnityEngine;

public class MAL_BoxSoundTrigger : MonoBehaviour
{
    [SerializeField] FMODUnity.StudioEventEmitter BoxHit;

    private Rigidbody2D rb;
    private float maxSpeedSinceLastHit;

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
        BoxHit.SetParameter("BoxHitSpeed", maxSpeedSinceLastHit);
        BoxHit.Play();

        maxSpeedSinceLastHit = 0;
    }
}
