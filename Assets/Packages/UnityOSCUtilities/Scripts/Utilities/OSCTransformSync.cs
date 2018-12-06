using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class OSCTransformSync : OSCFilter {

    public enum Mode
    {
        Send, Receive
    }

    public Mode mode = Mode.Receive;

    List<float> trans = new List<float>();

    protected override void Start()
    {
        if (mode == Mode.Receive)
        {
            OSCManager.Instance.FilterManager.Filters.Add(this);
        }
        else
        {
            trans.Add(transform.position.x);
            trans.Add(transform.position.y);
            trans.Add(transform.position.z);
            trans.Add(transform.rotation.x);
            trans.Add(transform.rotation.y);
            trans.Add(transform.rotation.z);
        }
    }
	
	void Update () {

        if (mode == Mode.Send)
        {

            trans[0] = transform.position.x;
            trans[1] = transform.position.y;
            trans[2] = transform.position.z;
            trans[3] = transform.rotation.eulerAngles.x;
            trans[4] = transform.rotation.eulerAngles.y;
            trans[5] = transform.rotation.eulerAngles.z;

            OSCManager.Instance.SendOSC(OSCAddress, trans);

        }

	}

    public override void OnReceivedOSC(OSCMessage message)
    {
        if (mode != Mode.Receive) return;

        transform.position = new Vector3((float)message.Data[0], (float)message.Data[1], (float)message.Data[2]);
        transform.rotation = Quaternion.Euler((float)message.Data[3], (float)message.Data[4], (float)message.Data[5]);
    }
}
