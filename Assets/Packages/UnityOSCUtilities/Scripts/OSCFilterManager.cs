using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public interface IOSCReceivable
{
    void OnReceivedOSC(OSCMessage message);
}

public class OSCFilterManager : SingletonMonoBehaviour<OSCFilterManager>, IOSCReceivable{

    [HideInInspector] public List<OSCFilter> Filters = new List<OSCFilter>();

    public void OnReceivedOSC(OSCMessage message)
    {

        foreach (var filter in Filters){
            
            if(filter.OSCAddress == message.Address)
            {
                foreach (var data in message.Data)
                {
                    filter.OnReceivedOSC(data);
                }

            }
        }
    }

}
