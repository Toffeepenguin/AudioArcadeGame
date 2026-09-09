using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLO_TransitionScript : MonoBehaviour
{
    RectTransform posControl;

    private void Start()
    {
        posControl = GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.Translate(0, -25 * Time.deltaTime, 0);
    }

    public void Move() {
        posControl.localPosition = new Vector2(0, 2000);
    }
}
