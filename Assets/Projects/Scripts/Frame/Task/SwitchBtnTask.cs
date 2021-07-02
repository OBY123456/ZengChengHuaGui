using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class SwitchBtnTask : BaseTask
{
    public SwitchBtnTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<SwitchBtnPanel>(WindowTypeEnum.Screen,UIPanelStateEnum.Open);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<SwitchBtnPanel>(WindowTypeEnum.Screen, UIPanelStateEnum.Hide);
    }
}
