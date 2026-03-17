using UnityEngine;

public class THU_Oil : THU_PowerUps
{
    public GameObject oilSplashPrefab;
    private bool canDeploy = false;

    public void ActivatePickup()
    {
        canDeploy = true;
    }

    public override void Deploy(GameObject deployer, GameObject target)
    {
        if (!canDeploy)
        {
            Debug.LogWarning("Oil Splash cannot be deployed. It must be picked up first!");
            return;
        }

        Debug.Log("Deploying oil splash!");
        if (oilSplashPrefab != null && deployer != null)
        {
            GameObject oilInstance = Instantiate(oilSplashPrefab, deployer.transform.position, Quaternion.identity);
            Destroy(oilInstance, 5f);
            canDeploy = false;
        }
        else
        {
            Debug.LogWarning("Oil Splash Prefab or Deployer is null!");
        }
    }

    public override void ApplyEffect(THU_PlayerMovement player)
    {
        Debug.Log("Oil Splash power-up collected by player!");
        ActivatePickup();
    }

    public override void ApplyEffect(THU_EnemyMovement enemy)
    {
        Debug.Log("Oil Splash power-up collected by enemy!");
        ActivatePickup();
    }
}
