using System;
using UnityEngine;
using static GameUtilitiesObject;

public class PlayerCarInputProviderObject : MonoBehaviour, ICarInputProviderObject
{
    public CarInput cached;
    public InputObject inputs;

    public void SetInput(CarInput input)
    {
        cached.throttle = inputs.throttle;
        cached.clutch = inputs.clutch;
        cached.shift_up = inputs.shift_up;
        cached.shift_down = inputs.shift_down;
        cached.brake = inputs.brake;
        cached.steering = inputs.steering;
        cached.look = inputs.look;
        cached.handbrake = inputs.handbrake;
    }

    public CarInput GetInput()
    {
        return cached;
    }
}
