using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class health : MonoBehaviour
{
    public delegate void HealthChangeHandler(object source, int oldHealth, int newHealth);
    public event HealthChangeHandler OnHealthChange;

    [SerializeField] int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
