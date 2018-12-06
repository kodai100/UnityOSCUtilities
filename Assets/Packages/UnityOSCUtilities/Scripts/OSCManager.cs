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
    OSCFilterManager filterManager;

    [SerializeField]
    string  senderId = "",
            receiverId = "",
            targetIp = "127.0.0.1";
    
    [SerializeField]
    int targetPort = 7000,
        receiverPort = 10000;

    private void Awake()
    {

        base.Awake();

        filterManager = GetComponent<OSCFilterManager>();

        switch (mode)
        {
            case OSCMode.Send:
                sender = CreateSender(senderId, IPAddress.Parse(targetIp), targetPort);
                break;
            case OSCMode.Receive:
                if (filterManager)
                {
                    receiver = CreateReceiver(receiverId, receiverPort, filterManager.OnReceivedOSC);
                }
                else
                {
                    receiver = CreateReceiver(receiverId, receiverPort, OnReceivedOSC);
                }
                break;
            case OSCMode.SendAndReceive:
                sender = CreateSender(senderId, IPAddress.Parse(targetIp), targetPort);
                if (filterManager)
                {
                    receiver = CreateReceiver(receiverId, receiverPort, filterManager.OnReceivedOSC);
                }
                else
                {
                    receiver = CreateReceiver(receiverId, receiverPort, OnReceivedOSC);
                }
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
        sender.Send(address, value);
    }

    void Update()
    {
        receiver.Listen();
    }
}
