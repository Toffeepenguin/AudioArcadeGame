using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//ref https://www.youtube.com/watch?v=eHrbL_oShnw accessed 02/12/2024
public class JRO_Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObject = new HashSet<GameObject>();

    [SerializeField] private Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalObject.Contains(collision.gameObject))
        {
            return;
        }
        if (destination.TryGetComponent(out JRO_Portal destinationPortal))
        {
            destinationPortal.portalObject.Add(collision.gameObject);
        }

        collision.transform.position = destination.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObject.Remove(collision.gameObject);
    }
}
