using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    public InputSubscription _Input; // make public to drag in via inspector

    public float AAS_forwardForce = 2000f;

    public float AAS_sidewaysForce = 500f;

    public float jumpForce = 10f;

    [SerializeField] AAS_GameManager gameManager;


    // Update is called once per frame


    private void Start()
    {

    }

    /*
    private void Awake()
    {
        _Input = GameObject.Find("AAS_GameManager").GetComponent<InputSubscription>(); Dont need this cus were gonna do it through inspector
    }
    */

    void FixedUpdate()
    {
        rb.AddForce(0, 0, AAS_forwardForce * Time.deltaTime);

        if (transform.position.y < -1f)
        {
            gameManager.EndGame();
        }
    }

    private void Update()
    {
        if (_Input.NormalizedMovementInput.x > 0)
        {
            rb.AddForce(AAS_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (_Input.NormalizedMovementInput.x < 0)
        {
            rb.AddForce(-AAS_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}