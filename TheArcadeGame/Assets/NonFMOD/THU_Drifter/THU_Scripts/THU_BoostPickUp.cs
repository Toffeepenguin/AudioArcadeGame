using UnityEngine;

public class THU_BoostPickUp : MonoBehaviour
{
    public float BoostAmount = 50f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<THU_PlayerMovement>(out THU_PlayerMovement player))
        {

            player.AddBoostEnergy(BoostAmount);

            Destroy(gameObject);
        }
    }
}
