using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using MTFrame.MTEvent;
using System;
using Newtonsoft.Json;

public class WaitPanel : BasePanel
{
    public static WaitPanel Instance;

    //返回首页倒计时参数
    private float BackTime;
    private float Back_Time;
    private bool IsBack;

    private float HuaGuiBackTime;
    private float _HuaGuiBackTime;
    private bool IsHuaGuiBack;

    //数据传输参数
    //byte[] sendData = new byte[2]; //发送的数据，必须为字节
    //bool issend = false;
    //float sendHz = 0;

    //UI页面
    public WaitBtn[] waitBtns;

    public SwitchBtnPanel btnPanel;

    public Sprite[] BtnSprites;

    private int LocalPos;

    //顺序按照：秦汉春秋-晋代玄风-南朝密码-隋唐沧桑-明清印记-宋元气象-民国故事-抗战计略
    private int Pos0, Pos1, Pos2, Pos3, Pos4, Pos5, Pos6, Pos7;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        if(Config.Instance)
        {
            BackTime = Config.Instance.configData.Backtime;
            //sendHz = Config.Instance.configData.SentHz;
            HuaGuiBackTime = Config.Instance.configData.HuaGui_BackTime;
            LocalPos = Config.Instance.configData.FirstPos;
            Pos0 = Config.Instance.configData.秦汉春秋;
            Pos1 = Config.Instance.configData.晋代玄风;
            Pos2 = Config.Instance.configData.南朝密码;
            Pos3 = Config.Instance.configData.隋唐沧桑;
            Pos4 = Config.Instance.configData.明清印记;
            Pos5 = Config.Instance.configData.宋元气象;
            Pos6 = Config.Instance.configData.民国故事;
            Pos7 = Config.Instance.configData.抗战计略;
        }

        if (UDP.instance)
        {
            UDP.instance.UDPHuaGuiPositionEvent += GetMessage;
        }

        //SendMessages(LocalPos);

        btnPanel = UIManager.GetPanel<SwitchBtnPanel>(WindowTypeEnum.Screen);
    }

    public override void InitFind()
    {
        base.InitFind();
        waitBtns = FindTool.FindChildNode(transform, "buttons").GetComponentsInChildren<WaitBtn>();
        BtnSprites = Resources.LoadAll<Sprite>("光点");
    }

    public override void InitEvent()
    {
        base.InitEvent();
        foreach (var item in waitBtns)
        {
            item.OnClick += OnClick;
        }
    }

    private void OnClick(BaseButton obj)
    {
        SendMessages(int.Parse(obj.name));
        UIState.SwitchPanel((PanelName)Enum.Parse(typeof(PanelName), obj.name));
        btnPanel.SetButtonSprite(int.Parse(obj.name));
    }

    public override void Open()
    {
        base.Open();
        if(btnPanel)
        btnPanel.Hide();
        foreach (var item in waitBtns)
        {
            item.loadSprite.enabled = true;
        }
    }

    public override void Hide()
    {
        base.Hide();
        btnPanel.Open();
        foreach (var item in waitBtns)
        {
            item.loadSprite.enabled = false;
        }
    }

    private void Update()
    {
        //SendToUDP();

        if (Back_Time > 0 && IsBack)
        {
            Back_Time -= Time.deltaTime;

            if (Back_Time <= 0)
            {
                IsBack = false;
                UIState.SwitchPanel(PanelName.WaitPanel);
                HuaGuiBackCountStart();
            }
            if (Input.GetMouseButton(0))
            {
                Back_Time = BackTime;
            }
        }

        if(IsOpen && _HuaGuiBackTime > 0 && IsHuaGuiBack)
        {
            _HuaGuiBackTime -= Time.deltaTime;
            if(_HuaGuiBackTime <= 0)
            {
                IsHuaGuiBack = false;
                SendMessages(LocalPos);
            }
        }
    }

    /// <summary>
    /// 返回待机页倒计时——开始
    /// </summary>
    public void StarCountDown()
    {
        IsBack = true;
        Back_Time = BackTime;
    }

    public void StopCountDown()
    {
        IsBack = false;
        Back_Time = BackTime;
    }

    private void HuaGuiBackCountStart()
    {
        IsHuaGuiBack = true;
        _HuaGuiBackTime = HuaGuiBackTime;
    }

    private void HuaGuiBackCountStop()
    {
        IsHuaGuiBack = false;
        _HuaGuiBackTime = HuaGuiBackTime;
    }

    //顺序按照：秦汉春秋-晋代玄风-南朝密码-隋唐沧桑-明清印记-宋元气象-民国故事-抗战计略
    public void SendMessages(int index)
    {

        //sendData = new byte[2]; //发送的数据，必须为字节
        //issend = true;
        switch (index)
        {
            case 0:
                //秦汉春秋
                //sendData = new byte[] { 0xFE, 0x17 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos0)));
                LogMsg.Instance.Log("发送-秦汉春秋");
                break;
            case 1:
                //晋代玄风
                //sendData = new byte[] { 0xFE, 0x16 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos1)));
                LogMsg.Instance.Log("发送-晋代玄风");
                break;
            case 2:
                //南朝密码
                //sendData = new byte[] { 0xFE, 0x15 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos2)));
                LogMsg.Instance.Log("发送-南朝密码");
                break;
            case 3:
                //隋唐沧桑
                //sendData = new byte[] { 0xFE, 0x14 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos3)));
                LogMsg.Instance.Log("发送-隋唐沧桑");
                break;
            case 4:
                //明清印记
                //sendData = new byte[] { 0xFE, 0x13 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos4)));
                LogMsg.Instance.Log("发送-明清印记");
                break;
            case 5:
                //宋元气象
                //sendData = new byte[] { 0xFE, 0x12 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos5)));
                LogMsg.Instance.Log("发送-宋元气象");
                break;
            case 6:
                //民国故事
                //sendData = new byte[] { 0xFE, 0x11 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos6)));
                LogMsg.Instance.Log("发送-民国故事");
                break;
            case 7:
                //抗战计略
                //sendData = new byte[] { 0xFE, 0x10 };
                Tcp_Client.instance.SendMessages(JsonConvert.SerializeObject(SetRoot(Pos7)));
                LogMsg.Instance.Log("发送-抗战计略");
                break;
            default:
                break;
        }
    }
    //float times = 0;
    //void SendToUDP()
    //{
    //    if (issend)
    //    {
    //        times += Time.deltaTime;
    //        if (times > sendHz && sendData != null)
    //        {
    //            if(UDP.instance)
    //            UDP.instance.SocketSend(sendData);
    //            times = 0;
    //        }
    //    }
    //}

    private void GetMessage(string pos)
    {
        int st;
        try
        {
            st = Int32.Parse(pos);

            UIState.SwitchPanel((PanelName)Enum.Parse(typeof(PanelName), pos));
            btnPanel.SetButtonSprite(int.Parse(pos));

            SendMessages(st);
        }
        catch
        {
            Debug.Log("此字节不是数字!");
        }
        
    }

    public Root SetRoot(int num)
    {
        Root root = new Root();
        root.command = 21;
        List<int> vs = new List<int>(3);
        vs.Add(2);
        vs.Add(0);
        vs.Add(num);
        root.param = vs;
        return root;
    }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public int command { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<int> param { get; set; }
}
