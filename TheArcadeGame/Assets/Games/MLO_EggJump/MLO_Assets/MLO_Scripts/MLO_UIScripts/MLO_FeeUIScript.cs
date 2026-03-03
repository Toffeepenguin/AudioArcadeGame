using TMPro;
using UnityEngine;

public class MLO_FeeUIScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 1 > .5f) { 
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
        }
    }
}
