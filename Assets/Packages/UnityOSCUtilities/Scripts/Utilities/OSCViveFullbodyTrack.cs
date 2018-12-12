using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class OSCViveFullbodyTrack : OSCFilter {

    public enum Mode
    {
        Send, Receive
    }

    public Mode mode = Mode.Receive;
    TransformPacket packet = new TransformPacket();
    
    protected override void Start()
    {
        if (mode == Mode.Receive)
        {
            OSCManager.Instance.FilterManager.Filters.Add(this);
        }
    }
	
	void Update () {

        if (mode == Mode.Send)
        {
            packet.position = transform.position;
            packet.euler = transform.rotation.eulerAngles;

            OSCManager.Instance.SendOSC(OSCAddress, packet.ToString());

        }

	}

    public override void OnReceivedOSC(OSCMessage message)
    {
        if (mode != Mode.Receive) return;

        packet.StringPacketToTransform((string) message.Data[0]);

        transform.position = packet.position;
        transform.rotation = Quaternion.Euler(packet.euler);
    }
}
