using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAL_CollectCoins : MonoBehaviour
{
    private float time;
    [SerializeField] MAL_PickUpCube isBoxHeld;
    [SerializeField] GameObject goldSplash;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Box"))
        {
            if (collision.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 0.1)
            {
                if (isBoxHeld.box == null || (isBoxHeld.box != null && isBoxHeld.box.name != collision.gameObject.name))
                {
                    time += Time.deltaTime;
                    Debug.Log(time);
                    if (time >= 1)
                    {
                        Instantiate(goldSplash, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5), Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Box"))
        {
            time = 0;
        }
    }
}
