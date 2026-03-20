using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MAL_PickUpCube : MonoBehaviour
{
    private bool isTouching;
    public bool pickUp;
    public GameObject box;
    private bool Performed = false; //Forces space button to be a button

    private InputSubscription _Input;

    [SerializeField] GameObject Chain;

    [SerializeField] EventReference PickupEvent;
    void Awake()
    {
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Input.SpaceInput && isTouching && !Performed)
        {
            //GetComponent<FMODUnity.StudioEventEmitter>().Play();
            if (pickUp)
            {
                pickUp = false;
                box.GetComponent<Rigidbody2D>().gravityScale = 1;
                box.GetComponent<Rigidbody2D>().linearDamping = 1;
                PlaySoundEffect(PickupEvent, 1);
                //RuntimeManager.PlayOneShot(PickupEvent);
            }
            else
            {
                pickUp = true;
                box.GetComponent<Rigidbody2D>().gravityScale = 0;
                box.GetComponent<Rigidbody2D>().linearDamping = 5;
                PlaySoundEffect(PickupEvent, 0);
                //RuntimeManager.PlayOneShot(PickupEvent);
            }
            Performed = true;
            
        }
        else if (!_Input.SpaceInput && Performed) //Checks for release of space
        {
            Performed = false;
        }

        if (isTouching && pickUp)
        {
            Chain.transform.position = new Vector2(box.gameObject.transform.position.x, box.transform.position.y + (box.transform.lossyScale.y / 2) + 4.81f);

            box.transform.rotation = Quaternion.Euler(0, 0, 0);// <- Might be needed not sure
        }

        if (gameObject.transform.position.y < -3 && box != null) //Drops box in water
        {
            pickUp = false;
            box.GetComponent<Rigidbody2D>().gravityScale = 1;
            box.GetComponent<Rigidbody2D>().linearDamping = 1;
            isTouching = false;
            box = null;
        }

        if (_Input.MenuInput)
        {
            SceneManager.LoadScene("MAL_Select");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Box") && !isTouching)
        {
            box = collision.gameObject;
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!pickUp) //Stops any bugs where the game thinks its not holding the block but is
        {
            box = null;
            isTouching = false;
        }
    }

    private void PlaySoundEffect(EventReference SoundEffect, int UpOrDown)
    {
        EventInstance instance = RuntimeManager.CreateInstance(SoundEffect);
        instance.setParameterByName("HasPickedUp", UpOrDown);
        instance.start();
        instance.release();
        
    }
}
