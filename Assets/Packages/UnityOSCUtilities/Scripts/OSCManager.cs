using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityOSC;

public enum OSCMode
{
    Send, Receive, SendAndReceive
}

public class OSCManager : SingletonMonoBehaviour<OSCManager>
{
    
    public OSCMode mode = OSCMode.SendAndReceive;

    // FilterManagerないときはこっちを使う
    public UnityAction<OSCMessage> OnReceivedOSC;

    OSCReceiver receiver;
    OSCSender sender;
    public OSCFilterManager FilterManager;
    
    [SerializeField]
    string  senderId = "",
            receiverId = "",
            targetIp = "127.0.0.1";
    
    [SerializeField]
    int targetPort = 7000,
        receiverPort = 10000;

    protected override void Awake()
    {

        base.Awake();

        FilterManager = new OSCFilterManager();

        switch (mode)
        {
            case OSCMode.Send:
                sender = CreateSender(senderId, IPAddress.Parse(targetIp), targetPort);
                break;
            case OSCMode.Receive:
                receiver = CreateReceiver(receiverId, receiverPort, FilterManager.OnReceivedOSC);
                break;
            case OSCMode.SendAndReceive:
                sender = CreateSender(senderId, IPAddress.Parse(targetIp), targetPort);
                receiver = CreateReceiver(receiverId, receiverPort, FilterManager.OnReceivedOSC);
                break;
            default:
                break;
        }

    }

    OSCReceiver CreateReceiver(string id, int port, UnityAction<OSCMessage> OnReceiveOSC)
    {
        var r = new OSCReceiver();
        r.Init(id, port);
        r.onListenToOSCMessage += OnReceiveOSC;

        return r;
    }

    OSCSender CreateSender(string id, IPAddress ip, int port)
    {
        var s = new OSCSender();
        s.Init(id, port, ip);
        return s;
    }

    public void SendOSC<T>(string address, T value)
    {
        if (mode != OSCMode.Receive)
            sender.Send(address, value);
    }

    public void SendOSC<T>(string address, List<T> values)
    {
        if(mode != OSCMode.Receive)
            sender.Send(address, values);
    }

    void Update()
    {
        if(mode != OSCMode.Send)
            receiver.Listen();
    }
}
