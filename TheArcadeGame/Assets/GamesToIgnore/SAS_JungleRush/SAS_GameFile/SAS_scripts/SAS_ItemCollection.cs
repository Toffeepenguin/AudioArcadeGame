using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
        private int watermelon = 0;
    [SerializeField] private Text watermelonText;

    private void Update()
    {
        if (watermelon >= 8)
        {
            SceneManager.LoadScene("SAS_WinMenu");

            if (PlayerPrefs.GetInt("SAS_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("SAS_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
            Debug.Log("Thropy1");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("watermelon"))
        {
            Destroy(collision.gameObject);
            watermelon++;
            watermelonText.text = "watermelon: " + watermelon;



            
           
        }

        

    }
    
        
}




