using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLO_TransitionScript : MonoBehaviour
{
    RectTransform posControl;
    [SerializeField] private int speed;
    [SerializeField] private int spawn_height;

    private void Start()
    {
        posControl = GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    public void Move() {
        posControl.localPosition = new Vector2(0, spawn_height);
    }
}
