using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class MingQingTask : BaseTask
{
    public MingQingTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<MingQingPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<MingQingPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
