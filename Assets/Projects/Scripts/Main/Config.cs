using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigData
{
    /// <summary>
    /// 返回待机页时间
    /// </summary>
    public float Backtime;

    /// <summary>
    /// 滑轨返回初始位置时间
    /// </summary>
    public float HuaGui_BackTime;

    /// <summary>
    /// 页面切换时间
    /// </summary>
    public float PanelSwitchTime;

    /// <summary>
    /// 子页面上动画的时间
    /// </summary>
    //public float AnimaTime;

    /// <summary>
    /// 子页面打开的动画时间
    /// </summary>
    public float PanelOpenTime;

    /// <summary>
    /// 本机UDP端口号
    /// </summary>
    public int UDPPort;

    /// <summary>
    /// 本机UDP ip
    /// </summary>
    public string UDPip;

    //public int HuaGuiTCPPort;

    //public string HuaGuiTCPip;

    public int TcpPort;

    public string Tcpip;

    /// <summary>
    /// 默认位置
    /// </summary>
    public int FirstPos;

    /// <summary>
    /// 消息发送频率
    /// </summary>
    //public float SentHz;

    /// <summary>
    /// 是否显示鼠标
    /// </summary>
    public bool isCursor;

    public string Code;

    //顺序按照：秦汉春秋-晋代玄风-南朝密码-隋唐沧桑-明清印记-宋元气象-民国故事-抗战计略
    public int 秦汉春秋;
    public int 晋代玄风;
    public int 南朝密码;
    public int 隋唐沧桑;
    public int 明清印记;
    public int 宋元气象;
    public int 民国故事;
    public int 抗战计略;
}


public class Config : MonoBehaviour
{
    public static Config Instance;

    public ConfigData configData  = new ConfigData();

    private string File_name = "config.txt";
    private string Path;

    private void Awake()
    {
        Instance = this;
        configData = new ConfigData();
#if UNITY_STANDALONE_WIN
        Path = Application.streamingAssetsPath + "/" + File_name;
        if (FileHandle.Instance.IsExistFile(Path))
        {
            string st = FileHandle.Instance.FileToString(Path);
            LogMsg.Instance.Log(st);
            configData = JsonConvert.DeserializeObject<ConfigData>(st);
        }
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        Path = Application.persistentDataPath + "/" + File_name;
        if(FileHandle.Instance.IsExistFile(Path))
        {
            string st = FileHandle.Instance.FileToString(Path);
            configData = JsonConvert.DeserializeObject<ConfigData>(st);
        }
        else
        {
            Path = Application.streamingAssetsPath + "/" + File_name;
            if (FileHandle.Instance.IsExistFile(Path))
            {
                string st = FileHandle.Instance.FileToString(Path);
                configData = JsonConvert.DeserializeObject<ConfigData>(st);
            }
        }
#endif
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void SaveData()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
         Path = Application.persistentDataPath + "/" + File_name;
#endif
        string st = JsonConvert.SerializeObject(configData);
        FileHandle.Instance.SaveFile(st, Path);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
