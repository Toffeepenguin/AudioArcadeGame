using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMA_CoinCollisionDetection : MonoBehaviour
{
    public FMA_CoinManager cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }
}
