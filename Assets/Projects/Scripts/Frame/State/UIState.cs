using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using MTFrame.MTEvent;
using System;

public class UIState : BaseState
{
    //注意state一定要在get里面监听事件，没有的话就写成下面样子
    //这里一般用来监听Panel切换
    public override string[] ListenerMessageID
    {
        get
        {
            return new string[]
            {
                //事件名string类型
                MTFrame.EventType.PanelSwitch.ToString(),
            };
        }
        set { }
    }

    public override void OnListenerMessage(EventParamete parameteData)
    {

        //接收监听事件的数据，然后用swich判断做处理

        if (parameteData.EvendName == MTFrame.EventType.PanelSwitch.ToString())
        {
            PanelName panelName = parameteData.GetParameter<PanelName>()[0];
            switch (panelName)
            {
                case PanelName.WaitPanel:
                    CurrentTask.ChangeTask(new WaitTask(this));
                    break;
                case PanelName.QinHanPanel:
                    CurrentTask.ChangeTask(new QinHanTask(this));
                    break;
                case PanelName.SongYuanPanel:
                    CurrentTask.ChangeTask(new SongYuanTask(this));
                    break;
                case PanelName.MingQingPanel:
                    CurrentTask.ChangeTask(new MingQingTask(this));
                    break;
                case PanelName.MinGuoPanel:
                    CurrentTask.ChangeTask(new MinGuoTask(this));
                    break;
                case PanelName.NanChaoPanel:
                    CurrentTask.ChangeTask(new NanChaoTask(this));
                    break;
                case PanelName.SuiTangPanel:
                    CurrentTask.ChangeTask(new SuiTangTask(this));
                    break;
                case PanelName.JinDaiPanel:
                    CurrentTask.ChangeTask(new JinDaiTask(this));
                    break;
                case PanelName.KangZhanPanel:
                    CurrentTask.ChangeTask(new KangZhanTask(this));
                    break;
                case PanelName.SwitchBtnPanel:
                    CurrentTask.ChangeTask(new SwitchBtnTask(this));
                    break;
                default:
                    break;
            }
        }
    }

    public override void Enter()
    {
        base.Enter();
        CurrentTask.ChangeTask(new SwitchBtnTask(this));
        CurrentTask.ChangeTask(new WaitTask(this));
    }


    /// <summary>
    /// 切换UI Panel
    /// </summary>
    /// <param name="panelName"></param>
    public static void SwitchPanel(PanelName panelName)
    {
        EventParamete eventParamete = new EventParamete();
        eventParamete.AddParameter(panelName);
        EventManager.TriggerEvent(GenericEventEnumType.Message, MTFrame.EventType.PanelSwitch.ToString(), eventParamete);
    }
}

namespace MTFrame
{
    public enum PanelName
    {
        /// <summary>
        /// 秦汉春秋页
        /// </summary>
        QinHanPanel = 0,

        /// <summary>
        /// 近代玄风
        /// </summary>
        JinDaiPanel = 1,

        /// <summary>
        /// 南朝密码
        /// </summary>
        NanChaoPanel = 2,

        /// <summary>
        /// 隋唐沧桑
        /// </summary>
        SuiTangPanel = 3,

        /// <summary>
        /// 明清印记
        /// </summary>
        MingQingPanel = 4,

        /// <summary>
        /// 宋元气象
        /// </summary>
        SongYuanPanel = 5,

        /// <summary>
        /// 民国故事
        /// </summary>
        MinGuoPanel = 6,

        /// <summary>
        /// 抗战纪略
        /// </summary>
        KangZhanPanel = 7,

        /// <summary>
        /// 待机页
        /// </summary>
        WaitPanel,

        SwitchBtnPanel,
    }

    public enum EventType
    {
        /// <summary>
        /// 切换页面
        /// </summary>
        PanelSwitch,

        /// <summary>
        /// 转换消息传出去
        /// </summary>
        DataToPanel,
    }
}
