
using UnityEngine;

public class TTA_Camera : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + new UnityEngine.Vector3(0, 1, -5); ;
        }
    }

    
}
