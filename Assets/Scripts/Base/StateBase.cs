using System;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase<T>
{
    // 当前状态
    public T stateType;

    public void Init(T state,FSMController<T> controller)
    {
        stateType = state;
        OnInit(state, controller);
    }

    protected virtual void OnInit(T state, FSMController<T> controller) { }

    public abstract void OnEnter();

    public abstract void OnLeave();

    public abstract void OnUpdate();
}
