using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputModule : UnityEngine.EventSystems.StandaloneInputModule
{
    public override void Process()
    {
        Debug.Log("Process");
    }
}
