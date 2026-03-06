using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SWA_Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    public SWA_MenuController menu;
    public float size = 1.0f;
    public float minSize = 1.5f;
    public float maxSize = 3.0f;
    public float speed = 50f;
    public float maxLifeTime = 30.0f;
    public bool getHit = false;

    private SpriteRenderer _spriteRender;
    private Rigidbody2D _rb;
    

    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _spriteRender.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 30.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rb.mass = this.size * 2.0f;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
            getHit = true;
        }

        if (collision.gameObject.name.Contains("BossBullet"))
        {
            Destroy(this.gameObject);
        }
    }
       

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        SWA_Enemy half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
