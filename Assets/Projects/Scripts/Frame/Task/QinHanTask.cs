using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class QinHanTask : BaseTask
{
    public QinHanTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<QinHanPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<QinHanPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
