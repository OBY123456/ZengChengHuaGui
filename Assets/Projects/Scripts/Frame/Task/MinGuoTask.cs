using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class MinGuoTask : BaseTask
{
    public MinGuoTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<MinGuoPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<MinGuoPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
