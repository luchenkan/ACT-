using System;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase
{
    // 当前状态
    public Enum stateType;

    // 当前控制单元
    public FSMController controller;

    public virtual void Init(Enum state, FSMController controller)
    {
        this.stateType = state;
        this.controller = controller;
    }

    public abstract void OnEnter();

    public abstract void OnLeave();

    public abstract void OnUpdate();
}
