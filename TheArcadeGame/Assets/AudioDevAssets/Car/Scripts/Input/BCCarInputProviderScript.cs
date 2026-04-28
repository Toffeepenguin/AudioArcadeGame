using UnityEngine;
using static GameUtilitiesObject;

public class BCCarInputProviderObject : MonoBehaviour, ICarInputProviderObject
{
    public CarInput cached;

    public void SetInput(CarInput input)
    {
        cached = input;
    }

    public CarInput GetInput()
    {
        return cached;
    }    
}
