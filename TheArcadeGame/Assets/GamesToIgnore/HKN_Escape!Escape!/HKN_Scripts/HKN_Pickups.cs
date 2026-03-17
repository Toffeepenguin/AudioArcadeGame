using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HKN_Pickups : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
            if (collision.tag == "Player")
            {   
                // get dmg to work (wed)
                HKN_GameManager.playerHealth = HKN_GameManager.playerHealth + 2;
                Debug.Log("Pickup Picked Up");
                Debug.Log(HKN_GameManager.playerHealth);
            
                Destroy (gameObject);
            }
        
    }

}
