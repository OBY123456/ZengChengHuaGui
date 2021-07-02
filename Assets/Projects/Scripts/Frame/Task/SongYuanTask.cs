using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class SongYuanTask : BaseTask
{
    public SongYuanTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<SongYuanPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<SongYuanPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
