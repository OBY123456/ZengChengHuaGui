using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class SuiTangTask : BaseTask
{
    public SuiTangTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<SuiTangPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<SuiTangPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
