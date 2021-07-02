using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class JinDaiTask : BaseTask
{
    public JinDaiTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<JinDaiPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<JinDaiPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
