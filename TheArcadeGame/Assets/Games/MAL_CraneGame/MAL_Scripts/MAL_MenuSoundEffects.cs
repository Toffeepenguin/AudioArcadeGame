using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MAL_MenuSoundEffects : MonoBehaviour
{
    public static MAL_MenuSoundEffects instance { get; private set; }

    [SerializeField] EventReference CraneGameMenuSFX;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }
    }
    public void PlayCraneGameMenu_SFX()
    {
        RuntimeManager.PlayOneShot(CraneGameMenuSFX);
    }
}
