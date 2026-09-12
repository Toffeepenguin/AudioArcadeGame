using System;
//using FMODUnityResonance;
using UnityEngine;


public static class FMODAudioUtilsObject
{
    public static FMOD.Studio.EventInstance Get3DAttRef(FMODUnity.EventReference sound, GameObject game_object)
    {
        FMOD.Studio.EventInstance event_instance = FMODUnity.RuntimeManager.CreateInstance(sound);
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(game_object);
        event_instance.set3DAttributes(attributes);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(event_instance, game_object);
        PlayInstance(event_instance);
        //Debug.Log($"ATTENUATED SOUND: {sound.Path}", game_object);
        return event_instance;
    }

    public static void PlayInstance(FMOD.Studio.EventInstance event_instance)
    {
        event_instance.start();
        event_instance.release();
    }
}
