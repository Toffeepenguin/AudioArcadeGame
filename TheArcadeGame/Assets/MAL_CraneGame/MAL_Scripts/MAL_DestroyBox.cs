using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAL_DestroyBox : MonoBehaviour
{
    [SerializeField] GameObject splash;
    [SerializeField] private AudioClip SFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Box"))
        {
            if (collision.transform.position.y + collision.transform.localScale.y/2 < -5.25)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                if (collision.transform.position.y + collision.transform.localScale.y / 2 < -2.75)
                {
                    MAL_SFXManager.instance.PlaySoundFXClip(SFX, transform, 1f);
                }
                Instantiate(splash, new Vector3(collision.transform.position.x, collision.transform.position.y-0.75f, -5), Quaternion.identity);
            }
        }
    }
}
