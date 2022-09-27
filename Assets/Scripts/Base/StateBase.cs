using System;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase<T>
{
    // 当前状态
    public T stateType;

    public virtual void Init(T state,FSMController<T> controller)
    {
        this.stateType = state;
    }

    public abstract void OnEnter();

    public abstract void OnLeave();

    public abstract void OnUpdate();
}
