using System;
using UnityEngine;

public class GameUtilitiesObject
{
    [Serializable]
    public struct CarInput
    {
        public float throttle;
        public float clutch;
        public bool shift_up;
        public bool shift_down;
        public float brake;
        public Vector2 steering;
        public Vector2 look;
        public bool handbrake;
    }

    public static float GripSurface(int layer)
    {
        string layer_name = LayerMask.LayerToName(layer);
        return layer_name switch
        {
            "Road" => 1f,
            "Grass" => .8f,
            "Dirt" => .6f,
            "Gravel" => .3f,
            _ => 1f
        };
    }
}
