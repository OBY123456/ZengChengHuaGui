using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;

public class UDP : MonoBehaviour
{ 

    public static UDP instance;

    Socket socket; //目标socket
    EndPoint remoteEnd; //对方udp
    IPEndPoint localEnd; //本地udp
    [HideInInspector]
    public string recvStr; //接收的字符串
    string sendStr; //发送的字符串
    byte[] recvData = new byte[1024]; //接收的数据，必须为字节
    byte[] sendData = new byte[1024]; //发送的数据，必须为字节
    int recvLen; //接收的数据长度
    Thread connectThread; //连接线程
    [HideInInspector]
    public bool isStartSend=false;

    //本地ip地址
    string localIPAddress;
    int localPort;
    //对方ip地址
    string remoteIPAdress;
    int remotePort;

    private static readonly object receiveLock = new object();

    public  delegate void UDPHuaGuiPositionDelegate(string pos);
    
    public event UDPHuaGuiPositionDelegate UDPHuaGuiPositionEvent;

    bool isReceive = false;
    
    //是否接收滑轨消息
    bool isReceiveUDP = false;

    bool issend;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //isReceiveUDP = bool.Parse(Configs.instance.LoadText("是否接受滑轨消息", "true/false"));
        isReceiveUDP = true;

        if(Config.Instance)
        {
            localIPAddress = Config.Instance.configData.UDPip;
            remoteIPAdress = "127.0.0.1";
            localPort = int.Parse(Config.Instance.configData.UDPPort.ToString());
            remotePort = int.Parse("8888");
        }


        if (isReceiveUDP)
        {
            InitSocket(); //在这里初始化
        }
    }
    
    /// <summary>
    /// 初始化
    /// </summary>
    void InitSocket()
    {
        //定义侦听本地IP、端口
        localEnd = new IPEndPoint(IPAddress.Parse(localIPAddress),localPort);
        //定义套接字类型,在主线程中定义 
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //绑定ip 
        socket.Bind(localEnd);
        //定义对方udp端口、IP
        IPEndPoint sender = new IPEndPoint(IPAddress.Parse(remoteIPAdress), remotePort);
        remoteEnd = (EndPoint)sender;
        //开启线程连接

        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
      
        isServerActive = true;
    }

    /// <summary>
    /// 本地UDP向对方发送消息
    /// </summary>
    /// <param name="sendStr"></param>
    //public void SocketSend(byte[] Data)
    //{
    //    //清空发送缓存 
    //    // sendData = new byte[1024];
    //    //sendData = new byte[] {0xFF, 0xFF};   //查寻数据
    //    // sendData = new byte[]{0xFE, 0x01};   //左移
    //    //sendData = new byte[] {0xFE, 0x02};   //右移
    //    // sendData = new byte[] {0xFE, 0x03};   //停止

    //    //数据类型转换 
    //    //sendData = Encoding.UTF8.GetBytes(sendStr);
    //    //sendData[0]
    //    //发送给指定UDP
    //    sendData = Data;
    //    socket.SendTo(Data, Data.Length, SocketFlags.None, remoteEnd);
    //    LogMsg.Instance.Log("发送成功");
    //}

    bool isServerActive = false;
    string index = "";
    /// <summary>
    /// 本地UDP接收来自对方的UDP消息
    /// </summary>
    void SocketReceive()
    {
        //进入接收循环 
        while (isServerActive)
        {
            lock (receiveLock)
            {
                //对data清零 
                recvData = new byte[1024];
                try
                {
                    //获取服务端端数据
                    recvLen = socket.ReceiveFrom(recvData, ref remoteEnd);
                    if (isServerActive == false)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    LogMsg.Instance.Log(e.Message);
                }
                /*测试用*/
                //print("接收到消息");
                index = Encoding.UTF8.GetString(recvData, 0, recvLen);
                if (!string.IsNullOrEmpty(index) && !string.IsNullOrWhiteSpace(index))
                {
                    isReceive = true;
                    LogMsg.Instance.Log("接收到消息===" + index);
                }
                //index = int.Parse(st);
                
                /*测试用*/

                //if (recvLen == 2)
                //{
                //    index = recvData[0] | recvData[1];
                //    print(index);
                //}
                //if (recvLen == 7)
                //{
                //    index = recvData[1] << 24 | recvData[2] << 16 | recvData[3] << 8 | recvData[4];
                //    print("位置" + index);
                //}
                //isReceive = true;
            }
        }
    }

    private void Update()
    {
        if (isReceive)
        {
            isReceive = false;
            UDPHuaGuiPositionEvent(index);
        }
    }


    //连接关闭
    void SocketQuit()
    {
        if (socket != null)
        {
            socket.Close();
        }
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //Debug.LogWarning("local：断开连接");
    }
    
    void OnApplicationQuit()
    {
        isServerActive = false;
         SocketQuit();
        Thread.Sleep(25);
    }
}
