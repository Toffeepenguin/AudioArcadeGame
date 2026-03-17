using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HKN_EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationspeed;

    private Rigidbody2D _rigidbody;
    private HKN_Seen _HKN_Seen;
    private Vector2 _targetDirection;

    
    public HKN_Roam Patrol;
    public Text RUN;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _HKN_Seen = GetComponent<HKN_Seen>();
        RUN.text = "ESCAPE NOW ";
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_HKN_Seen.AwareOfPlayer)
        {
            _targetDirection = _HKN_Seen.DirectionToPlayer;
            Patrol.enabled = false;
        }
        else
        {
            _targetDirection = Vector2.zero;
            Patrol.enabled = true;
            _rigidbody.SetRotation(0);
            RUN.enabled = true;
            new WaitForSeconds(5);
            RUN.enabled = false;

        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationspeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _rigidbody.linearVelocity = transform.up * _speed;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log(HKN_GameManager.playerHealth);
            HKN_GameManager.playerHealth = HKN_GameManager.playerHealth - 1;

            if (HKN_GameManager.playerHealth == 0)
            {
                Destroy(coll.gameObject);
                Destroy(gameObject);
                SceneManager.LoadScene("HKN_DeathScreen");

            }

        }


    }
}
