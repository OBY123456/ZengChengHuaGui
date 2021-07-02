using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Text;

public class Tcp_Client : MonoBehaviour
{
    public static Tcp_Client instance;

    int portNo = 10088;
    IPAddress localAdd;
    Thread ReceiveThread;
    Thread pingThread;
    TcpClient tcpClient;
    private string ReceiveMsg;
    string ip;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {

        if(Config.Instance)
        {
            portNo = Config.Instance.configData.TcpPort;
            ip = Config.Instance.configData.Tcpip;
        }

        try
        {
            localAdd = IPAddress.Parse(ip);
            tcpClient = new TcpClient();
            tcpClient.Connect(localAdd, portNo);
            ReceiveThread = new Thread(new ThreadStart(Receive));
            ReceiveThread.Start();
        }
        catch (Exception ex)
        {
            LogMsg.Instance.Log(ex.Message);
            pingThread = new Thread(new ThreadStart(ping));
            pingThread.Start();
        }
    }
    private void ping()
    {
        while (true)
        {
            try
            {
                tcpClient.Connect(localAdd, portNo);
                if (tcpClient.Connected)
                {
                    ReceiveThread = new Thread(new ThreadStart(Receive));
                    ReceiveThread.Start();
                    pingThread.Abort();
                    pingThread = null;
                }
            }
            catch (Exception ex)
            {
                LogMsg.Instance.Log(ex.Message);
            }
            Thread.Sleep(1000);
        }
    }
    Queue<string> GetVs = new Queue<string>();
    private void Receive()
    {
        while (this)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] vs = new byte[4096];
            int count = networkStream.Read(vs, 0, vs.Length);
            ReceiveMsg = Encoding.UTF8.GetString(vs, 0, count);
            LogMsg.Instance.Log("接收到服务器端的信息为:" + ReceiveMsg);
            //GetVs.Enqueue(ReceiveMsg);
        }
    }
    /// <summary>
    /// 向服务器发送数据（发送聊天信息）
    /// </summary>
    /// <param name="message"></param>
    public void SendMessages(string msg)
    {
        try
        {
            //tcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
            NetworkStream ns = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            ns.Write(data, 0, data.Length);
            LogMsg.Instance.Log("信息发送成功: " + msg);
            ns.Flush();
        }
        catch (Exception ex)
        {
            LogMsg.Instance.Log("Error:" + ex.Message);
        }
    }
    public void Update()
    {
        //lock (GetVs)
        //{
        //    if (GetVs.Count > 0)
        //    {
        //        string data = GetVs.Dequeue();
        //    }
        //}
    }

    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            tcpClient.Close();
        }
        if (ReceiveThread != null)
        {
            ReceiveThread.Interrupt();
            ReceiveThread.Abort();
        }

        if (pingThread != null)
        {
            pingThread.Interrupt();
            pingThread.Abort();
        }
    }
}