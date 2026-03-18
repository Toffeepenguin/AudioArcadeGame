using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MAL_MoveChain : MonoBehaviour
{
    private Rigidbody2D Rb;
    private InputSubscription _Input;

    public MAL_PickUpCube CubePickUp;
    Vector2 PlayerInput = Vector2.zero;

    private FMODUnity.StudioEventEmitter ChainSoundEmitter;
    // Start is called before the first frame update
    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        ChainSoundEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y < 1.25)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().linearVelocity.x, 0);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 1.25f);
        }
        else if (gameObject.transform.position.y > 9.9f) // Just do a collider you dumb skull idiot (Part of wall Prefab)
        {
            Debug.Log("1");
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().linearVelocity.x, 0);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 9.88f);
        }
        else
        {
            if (CubePickUp.pickUp == true)
            {
                CubePickUp.box.GetComponent<Rigidbody2D>().AddForce(PlayerInput * 35);
            }
            else
            {
                Rb.AddForce(PlayerInput * 35);
            }
        }
        if (ChainSoundEmitter != null)
        {
            ChainSoundEmitter.SetParameter("ChainSpeed", Mathf.Abs(Rb.linearVelocityX));
            Debug.Log(ChainSoundEmitter.Params[0].Name);
        }
    }
    private void Update()
    {
        PlayerInput += new Vector2(_Input.NormalizedMovementInput.x / 35, _Input.NormalizedMovementInput.y / 35);
        if (_Input.NormalizedMovementInput.x != 0 && _Input.NormalizedMovementInput.y != 0)
        {
            PlayerInput = new Vector2(Mathf.Clamp(PlayerInput.x, -0.71f, 0.71f), Mathf.Clamp(PlayerInput.y, -0.71f, 0.71f));
        }
        else
        {
            PlayerInput = new Vector2(Mathf.Clamp(PlayerInput.x, -1f, 1f), Mathf.Clamp(PlayerInput.y, -1f, 1f));
        }
        if (_Input.NormalizedMovementInput.x == 0)
        {
            PlayerInput.x = 0;
        }
        if (_Input.NormalizedMovementInput.y == 0)
        {
            PlayerInput.y = 0;
        }

        if (_Input.ConfirmInput)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
