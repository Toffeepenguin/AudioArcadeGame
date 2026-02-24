using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SWA_Boss : MonoBehaviour
{
    public GameObject BossBulletPref;
    public Transform[] firePoints;
    public SWA_GameObjManager gameObjManager;
    public GameObject BossStateUI;
    public Image hpFrount;

    public float shootInterval = 3f;
    public float shootTimer = 0f;
    public float BulletSpeed = 5f;
    public bool isBossAlive = true;
    public float moveSpeed = 2f;
    public float moveArea = 20f;
    public int maxHp = 50;
    public int currentHp = 0;

    private Vector2 targetPos;
    private float changeTargetTime = 5f;
    private float timer = 0f;

    private Vector2 originPos;

    private void Awake()
    {
        isBossAlive = true;
        currentHp = maxHp;
    }


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        setRandomTargetPos();
        maxHp = 50;
        currentHp = maxHp;
        UpdateBossHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObjManager.isBossSpawned)
        {
            // movement
            MoveToTarget();

            // use timer to change the target pos in random
            timer += Time.deltaTime;
            if (timer >= changeTargetTime)
            {
                setRandomTargetPos();
                timer = 0f;
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                BossShoot();
                Debug.Log("Shoot function working well.");
                shootTimer = 0.0f;
            }

            BossStateUI.SetActive(true);
        }
    }


    // function setRandomTargetPos
    private void setRandomTargetPos()
    {
        Vector2 randomOffset = Random.insideUnitSphere * moveArea;
        targetPos = originPos + randomOffset;

    }

    // movement function
    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        
        // if get close to target, then change target
        if (transform.position.x <= -13 || transform.position.x >= 13)
        {
            setRandomTargetPos();
        }

        if (transform.position.y <= -6 || transform.position.y >= 6)
        {
            setRandomTargetPos();
        }

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            setRandomTargetPos();
        }
    }

    // if get shoot by player, decrease hp
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            currentHp -= 1;
            UpdateBossHP();
            checkIfDead();
        }
    }


    // check hp, decide destroy or alive
    private void checkIfDead()
    {
        if (currentHp == 0)
        {
            Destroy(gameObject);
            BossStateUI.SetActive(false);
            isBossAlive = false;
        }
    }

    private void BossShoot()
    {
        if (firePoints.Length == 0)
        {
            Debug.LogWarning("Fire point or screen center didn't sign");
            return;
        }

        // choose a fire point in random
        int ranIndex = Random.Range(0, 4);

        // init BossBullets
        GameObject bossBullet = Instantiate(BossBulletPref, firePoints[ranIndex].position, firePoints[ranIndex].rotation);

        Rigidbody2D rb = bossBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoints[ranIndex].up * BulletSpeed;
        }
    }

    private void UpdateBossHP()
    {
        if (hpFrount != null)
        {
            float hpRatio = (float)currentHp / maxHp;
            hpFrount.rectTransform.localScale = new Vector3(hpRatio, 1, 1);
        }
    }

}
