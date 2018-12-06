using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class OSCFilterSample : OSCFilter
{

    public override void OnReceivedOSC(OSCMessage message)
    {
        
        foreach(var msg in message.Data)
        {
            Debug.Log(msg.ToString());
        }

    }
}
