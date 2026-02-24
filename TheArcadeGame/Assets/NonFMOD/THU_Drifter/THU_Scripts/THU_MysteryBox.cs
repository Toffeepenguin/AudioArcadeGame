using UnityEngine;

public class THU_MysteryBox : THU_PowerUps
{
    [Header("Mystery Box Settings")]
    public GameObject THU_birdPrefab;
    public GameObject THU_oilPrefab;
    public GameObject THU_pillowPrefab;

    [Header("UI Settings")]
    public Sprite birdPowerUpIcon;
    public Sprite oilPowerUpIcon;
    public Sprite pillowPowerUpIcon;
    public Sprite mysteryBoxIcon;

    private GameObject[] powerUps;
    private Sprite[] powerUpIcons;

    private bool hasDeployed = false;
    private bool isMysteryBoxActive = false;

    private void Awake()
    {
        powerUps = new GameObject[] { THU_birdPrefab, THU_oilPrefab, THU_pillowPrefab };
        powerUpIcons = new Sprite[] { birdPowerUpIcon, oilPowerUpIcon, pillowPowerUpIcon };

        foreach (var powerUp in powerUps)
        {
            if (powerUp != null)
            {
                powerUp.SetActive(false);
            }
        }
    }

    public override void Deploy(GameObject deployer, GameObject target)
    {
        if (hasDeployed)
        {
            Debug.Log("Mystery box already deployed! Skipping deployment.");
            return;
        }

        Debug.Log("Deploying mystery box!");

        if (deployer != null)
        {
            GameObject deployedPowerUp = GrantRandomPowerUp(deployer);

            if (deployedPowerUp != null)
            {
                Debug.Log($"Power-up {deployedPowerUp.name} deployed!");
                hasDeployed = true;
            }
        }
        else
        {
            Debug.LogWarning("Deployer is null!");
        }
    }

    public override void ApplyEffect(THU_PlayerMovement player)
    {
        Debug.Log("Mystery Box collected by Player!");

        if (!isMysteryBoxActive)
        {
            isMysteryBoxActive = true;

            int randomIndex = Random.Range(0, powerUps.Length);
            GameObject selectedPowerUp = powerUps[randomIndex];
            Sprite selectedIcon = powerUpIcons[randomIndex];

            var uiManager = FindObjectOfType<THU_Drifter_UIManager>();
            if (uiManager != null && selectedIcon != null)
            {
                uiManager.UpdatePowerUpUI(selectedIcon);
            }
            else
            {
                Debug.LogWarning("UI Manager or selected icon is null!");
            }

            ActivatePowerUp(selectedPowerUp, player.gameObject);
        }
    }

    public override void ApplyEffect(THU_EnemyMovement enemy)
    {
        Debug.Log("Mystery Box collected by Enemy!");

        if (!isMysteryBoxActive)
        {
            isMysteryBoxActive = true;

            int randomIndex = Random.Range(0, powerUps.Length);
            GameObject selectedPowerUp = powerUps[randomIndex];

            ActivatePowerUp(selectedPowerUp, enemy.gameObject);
        }
    }


    private GameObject GrantRandomPowerUp(GameObject deployer)
    {
        if (powerUps.Length == 0 || powerUpIcons.Length == 0)
        {
            Debug.LogWarning("No power-ups or icons assigned to Mystery Box!");
            return null;
        }

        int randomIndex = Random.Range(0, powerUps.Length);
        GameObject selectedPowerUp = powerUps[randomIndex];

        if (selectedPowerUp != null)
        {
            Debug.Log($"Mystery Box selected PowerUp: {selectedPowerUp.name}");

            selectedPowerUp.SetActive(true);
            selectedPowerUp.transform.position = deployer.transform.position + Vector3.forward * 2;

            var powerUpScript = selectedPowerUp.GetComponent<THU_PowerUps>();
            if (powerUpScript != null)
            {
                powerUpScript.Deploy(deployer, null);
            }

            return selectedPowerUp;
        }
        else
        {
            Debug.LogError("Selected power-up prefab is null!");
            return null;
        }
    }

    private void ActivatePowerUp(GameObject powerUpPrefab, GameObject deployer)
    {
        if (powerUpPrefab != null)
        {
            var powerUpScript = powerUpPrefab.GetComponent<THU_PowerUps>();

            if (powerUpScript != null)
            {

                powerUpScript.Deploy(deployer, null);
            }
            else
            {
                Debug.LogError("Power-up prefab does not have a THU_PowerUps script attached!");
            }
        }
        else
        {
            Debug.LogWarning("Power-up prefab is null!");
        }
    }
}
