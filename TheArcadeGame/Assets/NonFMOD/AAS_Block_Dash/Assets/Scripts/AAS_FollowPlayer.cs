
using UnityEngine;

public class AAS_FollowPlayer : MonoBehaviour
{

    public Transform player;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
