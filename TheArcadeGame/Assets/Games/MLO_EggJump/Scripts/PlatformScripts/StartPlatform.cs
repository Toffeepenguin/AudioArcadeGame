using UnityEngine;

public class StartPlatform : Platform
{
    private Vector3 default_position = new(0f, -3f, 0f);

    private void Start()
    {
        SetAlpha(1f);
        fallen = false;
    }

    public void PlatformReset()
    {
        Rise(default_position);
    }
}
