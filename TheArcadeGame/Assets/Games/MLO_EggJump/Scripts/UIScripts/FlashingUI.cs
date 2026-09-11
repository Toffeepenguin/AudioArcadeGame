using TMPro;
using UnityEngine;

public class FlashingUI : MonoBehaviour
{
    protected TextMeshProUGUI text;

    [SerializeField] protected float flash_rate;
    [SerializeField] private bool continuous;
    [SerializeField] private int target_flash_count;

    private float timer;
    private int current_flash_count;
    private bool flashing = true;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ResetFlashing();
    }

    private void Update()
    {
        if (!flashing) return;
        Tick();
    }

    protected void Tick()
    {
        timer += Time.deltaTime;
        if (timer >= flash_rate)
        {
            timer -= flash_rate;
            text.enabled = !text.enabled;
            if (!text.enabled && !continuous)
            {
                current_flash_count++;
                if (current_flash_count >= target_flash_count) StopFlashing();
            }
        }
    }

    public void StartFlashing()
    {
        if (target_flash_count <= 0) continuous = true;
        else continuous = false;
        ResetFlashing();
    }

    public void ResetFlashing()
    {
        timer = 0f;
        current_flash_count = 0;
        flashing = true;
        if (text != null) text.enabled = true;
    }

    public void StopFlashing()
    {
        flashing = false;
        if (text != null) text.enabled = true;
    }
}