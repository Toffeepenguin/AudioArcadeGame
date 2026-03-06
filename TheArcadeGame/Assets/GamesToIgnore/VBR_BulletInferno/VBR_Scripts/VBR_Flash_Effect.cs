using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VBR_Flash_Effect : MonoBehaviour
{
    [Header ("Flash materials and time")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material redFlashMaterial;
    [SerializeField] private Material greenFlashMaterial;
    [SerializeField] private float flashDuration;

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine whiteFlashRoutine;
    private Coroutine redFlashRoutine;
    private Coroutine greenFlashRoutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //this saves the original material that the sprite has
        originalMaterial = spriteRenderer.material;
    }
    //this handles the sprite when called to turn white briefly
    public void whiteFlash()
    {
        if (whiteFlashRoutine != null)
        {
            StopCoroutine(whiteFlashRoutine);
        }
        whiteFlashRoutine = StartCoroutine(WhiteFlashRoutine());
    }
    private IEnumerator WhiteFlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        whiteFlashRoutine = null;
    }

    //this handles the sprite when called to turn red briefly
    public void redFlash()
    {
        if (redFlashRoutine != null)
        {
            StopCoroutine(whiteFlashRoutine);
        }
        redFlashRoutine = StartCoroutine(RedFlashRoutine());
    }
    private IEnumerator RedFlashRoutine()
    {
        spriteRenderer.material = redFlashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        redFlashRoutine = null;
    }

    //this handles the sprite when called to turn red briefly
    public void greenFlash()
    {
        if (greenFlashRoutine != null)
        {
            StopCoroutine(greenFlashRoutine);
        }
        greenFlashRoutine = StartCoroutine(GreenFlashRoutine());
    }
    private IEnumerator GreenFlashRoutine()
    {
        spriteRenderer.material = greenFlashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        greenFlashRoutine = null;
    }
}
