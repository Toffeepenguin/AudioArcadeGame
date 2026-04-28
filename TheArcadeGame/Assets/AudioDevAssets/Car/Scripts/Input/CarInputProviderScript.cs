using UnityEngine;
using static GameUtilitiesObject;

public interface ICarInputProviderObject
{
    public void SetInput(CarInput input);

    public CarInput GetInput();
}
