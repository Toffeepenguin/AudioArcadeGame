using System.Runtime.Remoting.Messaging;
using FMODUnity;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public EventReference FMOD_trophy_sound;
    public bool collected = false;

    [Header("Debug")]
    [SerializeField] private bool debug_collected;

    private const string TROPHY_PREF_KEY = "MLO_Trophy_Int";

    private void Start()
    {
        if (PlayerPrefs.GetInt(TROPHY_PREF_KEY, 0) == 1)
        {
            collected = true;
        }
        if (debug_collected) collected = false;
    }

    public void TriggerTrophy(Transform player_transform)
    {
        collected = true;
        FMODAudioUtilsObject.Get3DAttRef(FMOD_trophy_sound, player_transform.gameObject);
        if (PlayerPrefs.GetInt(TROPHY_PREF_KEY) != 1)
        {
            PlayerPrefs.SetInt(TROPHY_PREF_KEY, 1);
            PlayerPrefs.Save();
        }
    }
}