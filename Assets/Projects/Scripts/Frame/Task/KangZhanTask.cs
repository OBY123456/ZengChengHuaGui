using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class KangZhanTask : BaseTask
{
    public KangZhanTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<KangZhanPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<KangZhanPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
