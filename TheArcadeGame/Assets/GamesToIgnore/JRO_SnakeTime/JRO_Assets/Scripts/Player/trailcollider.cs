using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class trailcollider : MonoBehaviour
{
    TrailRenderer tR;
    EdgeCollider2D eC;
    basicplayercontroller bpc;

    private void Awake()
    {
        tR = GetComponent<TrailRenderer>();

        GameObject colliderGameObject = new GameObject("TrailCollider",typeof(EdgeCollider2D));
        eC = colliderGameObject.GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetColliderPointsFromTrail(tR, eC);
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        for (int position = 0; position < trail.positionCount; position++)
        {
            points.Add(trail.GetPosition(position));
        }
        collider.SetPoints(points);
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (collision.gameObject.name == "JRO_PlayerSprite")
    //    //{
    //    //    bpc.
    //    //}
    //    Debug.Log(collision.name);
    //    Debug.Log("IM TRIGGERED TAIL");
    //}

    //private void OnDestroy()
    //{
    //    Destroy(tR.gameObject);
    //}

}
