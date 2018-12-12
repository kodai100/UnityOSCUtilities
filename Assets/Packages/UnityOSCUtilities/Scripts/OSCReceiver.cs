using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityOSC;

public class OSCReceiver
{
    public event UnityAction<OSCMessage> onListenToOSCMessage;

    bool m_isInit;
    OSCPacket m_lastPacket;

    private Queue queue;

    public void Init(string serverId, int port)
    {
        if (m_isInit) return;

        m_isInit = true;
        OSCHandler.Instance.CreateServer(serverId, port);

        queue = new Queue();
        queue = Queue.Synchronized(queue);

        OSCHandler.Instance.PacketReceivedEvent += OnPacketReceived;
    }

    void OnPacketReceived(OSCServer server, OSCPacket packet)
    {
        queue.Enqueue(packet);

    }
    

    public void Listen()
    {
        // ListenToOSCMessage();
    }


    void ListenToOSCMessage()
    {

        while (0 < queue.Count)
        {

            OSCPacket packet = queue.Dequeue() as OSCPacket;
            
            if (packet.IsBundle())
            {
                OSCBundle bundle = packet as OSCBundle;

                foreach (OSCMessage msg in bundle.Data)
                {

                    onListenToOSCMessage(msg);
                }
            }
            else
            {
                OSCMessage msg = packet as OSCMessage;

                onListenToOSCMessage(msg);
            }
        }


    }

}