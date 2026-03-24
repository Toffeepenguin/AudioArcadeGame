using System;
using UnityEngine;


public static class FMODAudioUtilsObject
{
    public static FMOD.Studio.EventInstance Get3DAttRef(FMODUnity.EventReference sound, GameObject game_object)
    {
        // 1. Create the instance
        FMOD.Studio.EventInstance event_instance = FMODUnity.RuntimeManager.CreateInstance(sound);
        
        // 2. IMMEDIATELY set the position manually from the GameObject
        // This fixes the "Defaulting to 0,0,0" issue
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(game_object);
        event_instance.set3DAttributes(attributes);
        
        // 3. Attach it so it follows the player during the jump arc
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(event_instance, game_object);
        
        // 4. Start and Release
        event_instance.start();
        event_instance.release();
        
        return event_instance;
    }
}
