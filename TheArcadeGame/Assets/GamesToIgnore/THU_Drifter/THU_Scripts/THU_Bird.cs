using UnityEngine;

public class THU_Bird : THU_PowerUps
{
    public GameObject birdPrefab;
    public int birdCount = 3;
    private bool isActivated = false;

    public override void Deploy(GameObject deployer, GameObject target)
    {
        Debug.Log("Deploying birds to slow down the front runner!");

        if (isActivated)
        {
            Debug.LogWarning("Birds already deployed, skipping deployment.");
            return;
        }

        if (birdPrefab != null && deployer != null)
        {
            for (int i = 0; i < birdCount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 2f, Random.Range(-1f, 1f));
                GameObject birdInstance = Instantiate(birdPrefab, deployer.transform.position + randomOffset, Quaternion.identity);

                var collider = birdInstance.GetComponent<Collider>();
                if (collider != null) collider.enabled = false;

                Destroy(birdInstance, 5f);
            }
            isActivated = true;
        }
        else
        {
            Debug.LogWarning("Bird Prefab or Deployer is null!");
        }
    }

    public override void ApplyEffect(THU_PlayerMovement player)
    {
        if (!isActivated)
        {
            Debug.Log("Birds slowing down player at the front!");
            player.ReduceSpeedTemporarily(0.7f, 5f);
            isActivated = true;
        }
    }

    public override void ApplyEffect(THU_EnemyMovement enemy)
    {
        if (!isActivated)
        {
            Debug.Log("Birds slowing down enemy at the front!");
            enemy.ReduceSpeedTemporarily(0.7f, 5f);
            isActivated = true;
        }
    }
}
