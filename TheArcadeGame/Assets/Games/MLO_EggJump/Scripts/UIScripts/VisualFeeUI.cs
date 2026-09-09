using TMPro;
using UnityEngine;

public class MLO_FeeUIScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        if (Time.time % 1 > .5f) text.enabled = false;
        else text.enabled = true;
    }
}
