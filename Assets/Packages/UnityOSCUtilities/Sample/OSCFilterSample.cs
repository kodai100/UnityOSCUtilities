using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class OSCFilterSample : OSCFilter
{

    public override void OnReceivedOSC(object value)
    {
        Debug.Log(value.ToString());
    }
}
