using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MAL_MenuSoundEffects : MonoBehaviour
{
    [SerializeField] EventReference CraneGameMenuSFX;

    public void CraneGameSelectLevel_SFX()
    {
        RuntimeManager.PlayOneShot(CraneGameMenuSFX);
    }
}
