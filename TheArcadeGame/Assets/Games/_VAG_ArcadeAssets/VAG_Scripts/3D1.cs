using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event3DExample: MonoBehaviour 
{
  private FMOD.Studio.EventInstance instance;
  
  void Start() 
  {
    instance = FMODUnity.RuntimeManager.CreateInstance("event:/3DEvent");
    FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
    instance.start();
  }
  
  void Update() 
  {
    // instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
  }
}