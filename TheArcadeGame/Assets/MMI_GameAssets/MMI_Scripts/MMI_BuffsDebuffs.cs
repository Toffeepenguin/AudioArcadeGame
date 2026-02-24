using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MMI_BuffsDebuffs : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 4f;


    public bool Multishot = false;
    public bool Piercing = false;
    public bool FireRate = false;




    public float EffectDuration = 5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    { 
        rb.linearVelocity = Vector2.down * speed;
    }




}
