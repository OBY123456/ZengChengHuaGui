using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class NanChaoTask : BaseTask
{
    public NanChaoTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<NanChaoPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<NanChaoPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
