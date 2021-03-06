﻿using System;
using UnityEngine;
using UnityOSC;

class TransformPacket
{
    public Vector3 position = Vector3.zero;
    public Vector3 euler = Vector3.zero;

    string s = "";
    string[] receive;

    public void StringPacketToTransform(string p)
    {
        receive = p.Split(new Char[] { ',' });

        int i = 0;
        foreach (var v in receive)
        {
            if(i < 3)
            {
                position[i] = float.Parse(v);
            }
            else
            {
                euler[i-3] = float.Parse(v);
            }
            i++;
        }
        
    }

    public override string ToString()
    {
        s = "";

        s += position.x + "," + position.y + "," + position.z + ",";
        s += euler.x + "," + euler.y + "," + euler.z;

        return s;
    }
}

public class OSCTransformSync : OSCFilter {

    public enum Mode
    {
        Send, Receive
    }

    public Mode mode = Mode.Receive;
    TransformPacket packet = new TransformPacket();

    public int skip = 0;
    int frame = 0;
    
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
            frame++;

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
