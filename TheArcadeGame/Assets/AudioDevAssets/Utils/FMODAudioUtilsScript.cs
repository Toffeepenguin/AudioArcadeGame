using System;
using UnityEngine;

public static class FMODAudioUtilsObject
{
    public static FMOD.Studio.EventInstance Get3DAttRef(FMODUnity.EventReference sound, GameObject game_object)
    {
        FMOD.Studio.EventInstance event_instance = FMODUnity.RuntimeManager.CreateInstance(sound);
        event_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(game_object));
        event_instance.start();
        event_instance.release();
        return event_instance;
    }
}
