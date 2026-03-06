using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_HealthUp : MonoBehaviour
{
    private VBR_Player player;

    [Header("Duration Parameters")]
    [SerializeField] private float lifeTime = 15f;

    [Header("Blinking Parameters")]
    [SerializeField] private float totalBlinkDuration = 5.0f;
    [SerializeField] private float maxBlinkDuration = 0.125f;
    [SerializeField] private float minBlinkDuration = 0.05f;

    private bool isExpiring = false;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip pickUpSound;

    [Header("Health Amount Parameters")]
    [SerializeField] private float healthAmount;

    private Vector3 IniPos;
    private AudioSource audioSource;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<VBR_Player>();
        audioSource = GetComponent<AudioSource>();
        IniPos = transform.position;

        Invoke(nameof(startBlinkEffect), lifeTime - totalBlinkDuration);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position = new Vector3(IniPos.x, Mathf.Sin(Time.time * 10) * 0.1f + IniPos.y, 0);
    }
    private void startBlinkEffect()
    {
        if (!isExpiring)
        {
            isExpiring = true;
            StartCoroutine(blinkEffect());
        }
    }
    private IEnumerator blinkEffect()
    {
        float blinkDuration = lifeTime - totalBlinkDuration;
        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            float progress = elapsedTime / blinkDuration;
            float currentInterval = Mathf.Lerp(maxBlinkDuration, minBlinkDuration, progress);
            yield return new WaitForSeconds(currentInterval);
            gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            elapsedTime += progress;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                StopAllCoroutines();
                audioSource.PlayOneShot(pickUpSound, 0.15f);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                player.playerHealthUp(healthAmount);
                Destroy(gameObject, 1f);
                break;
        }
    }
}
