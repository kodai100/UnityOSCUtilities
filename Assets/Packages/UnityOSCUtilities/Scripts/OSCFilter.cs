using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityOSC;

[System.Serializable]
public abstract class OSCFilter : MonoBehaviour {

    public string FilterName = "";
    public string OSCAddress = "";
    
    void Start()
    {
        OSCFilterManager.Instance.Filters.Add(this);
    }

    public abstract void OnReceivedOSC(object obj);
}
