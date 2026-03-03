using UnityEngine;

public class AAS_PlayerCollision : MonoBehaviour{

    public AAS_PlayerMovement movement;
    
    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<AAS_GameManager>().EndGame();
            
        }
    }
}
