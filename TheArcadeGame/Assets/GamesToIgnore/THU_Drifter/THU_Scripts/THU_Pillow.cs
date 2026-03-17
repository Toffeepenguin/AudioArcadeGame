using UnityEngine;

public class THU_Pillow : THU_PowerUps
{
    public GameObject pillowPrefab;
    private bool isActivated = false;

    public override void Deploy(GameObject deployer, GameObject target)
    {
        Debug.Log("Deploying pillow!");

        if (isActivated)
        {
            Debug.LogWarning("Pillow already deployed, skipping deployment.");
            return;
        }

        if (pillowPrefab != null && deployer != null)
        {
            Vector3 deployPosition = deployer.transform.position + deployer.transform.forward * 2f;
            GameObject pillowInstance = Instantiate(pillowPrefab, deployPosition, Quaternion.identity);

            var collider = pillowInstance.GetComponent<Collider>();
            if (collider != null) collider.enabled = false;

            Destroy(pillowInstance, 5f);

            isActivated = true;
        }
        else
        {
            Debug.LogWarning("Pillow Prefab or Deployer is null!");
        }
    }

    public override void ApplyEffect(THU_PlayerMovement player)
    {
        if (!isActivated)
        {
            Debug.Log("Player stopped temporarily by Pillow!");
            player.StopMovementTemporarily(2f);
            isActivated = true;
        }
    }

    public override void ApplyEffect(THU_EnemyMovement enemy)
    {
        if (!isActivated)
        {
            Debug.Log("Enemy stopped temporarily by Pillow!");
            enemy.StopMovementTemporarily(2f);
            isActivated = true;
        }
    }
}
