
using UnityEngine;
//Added by Izzy//
using UnityEngine.EventSystems;
///////////////

public class EBA_Health : MonoBehaviour
{
    [SerializeField] GameObject EBA_gameOverMenu;
    [SerializeField] GameObject EBA_player;

    //Added by Izzy//
    public EventSystem eventSystem;
    public GameObject restartBtn;
    ////////////

    private void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EBA_Enemy(Clone)")
        {
           Destroy(gameObject);
           GameObject.Destroy(EBA_player); //destroy object 
            
            Debug.Log("Destroy health");
            EBA_gameOverMenu.SetActive(true);
            //Added by Izzy//
            eventSystem.SetSelectedGameObject(restartBtn);
            ////////////

        }
    }
}
