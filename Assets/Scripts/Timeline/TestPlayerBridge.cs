using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestPlayerBridge : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        TestMarker tm = notification as TestMarker;
        Debug.Log("OnNotify");
    }

}
