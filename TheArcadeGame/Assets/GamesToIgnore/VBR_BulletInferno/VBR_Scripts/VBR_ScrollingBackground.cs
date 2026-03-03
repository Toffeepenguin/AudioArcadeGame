using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    private float textureWidth;
    private void Start()
    {
        textureSetup();
    }
    private void FixedUpdate()
    {
        scroll();
        checkReset();
    }
    private void textureSetup()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }
    private void scroll()
    {
        transform.position += new Vector3(scrollingSpeed, 0f, 0f);
    }
    private void checkReset()
    {
        if ((Mathf.Abs(transform.position.x) - textureWidth) > 0)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }
}
