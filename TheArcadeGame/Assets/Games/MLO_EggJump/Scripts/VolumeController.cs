using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("FMOD VCA Paths")]
    [SerializeField] private string musicVcaPath = "vca:/JumpingCapsMusic";
    [SerializeField] private string sfxVcaPath = "vca:/JumpingCapsSFX";

    private FMOD.Studio.VCA musicVca;
    private FMOD.Studio.VCA sfxVca;

    private void Start()
    {
        musicVca = FMODUnity.RuntimeManager.GetVCA(musicVcaPath);
        sfxVca = FMODUnity.RuntimeManager.GetVCA(sfxVcaPath);

        musicSlider.minValue = 0f;
        musicSlider.maxValue = 1f;
        sfxSlider.minValue = 0f;
        sfxSlider.maxValue = 1f;

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVca.setVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVca.setVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}