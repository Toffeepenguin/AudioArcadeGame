using TMPro;
using UnityEngine;

public class FlashingUI : MonoBehaviour
{
    protected TextMeshProUGUI text;

    [SerializeField] protected float flash_rate;
    [SerializeField] private bool continuous;
    [SerializeField] private int target_flash_count;
    [SerializeField] private bool start_flashing;
    [SerializeField] private bool end_disabled;

    private float timer;
    private int current_flash_count;
    private bool flashing;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ResetFlashing();
        if (start_flashing) StartFlashing();
    }

    private void Update()
    {
        if (!flashing || text == null) return;
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
        continuous = (target_flash_count <= 0);
        ResetFlashing();

        flashing = true;
        if (text != null) text.enabled = true;
    }

    public void ResetFlashing()
    {
        timer = 0f;
        current_flash_count = 0;
    }

    public void StopFlashing()
    {
        flashing = false;
        if (text != null) text.enabled = !end_disabled;
    }
}