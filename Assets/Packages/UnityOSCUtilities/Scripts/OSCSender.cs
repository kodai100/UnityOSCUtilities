using System.Collections.Generic;
using System.Net;

public class OSCSender
{
    string m_clientId;
    bool m_isInit;

    public void Init(string clientId, int port, IPAddress ip)
    {
        m_isInit = true;
        m_clientId = clientId;
        OSCHandler.Instance.CreateClient(clientId, ip, port);
    }

    public void Send<T>(string address, T value)
    {
        if (m_isInit == false)
            return;

        OSCHandler.Instance.SendMessageToClient(m_clientId, address, value);
    }

    public void Send<T>(string address, List<T> values)
    {
        if (m_isInit == false)
            return;

        OSCHandler.Instance.SendMessageToClient(m_clientId, address, values);
    }
}