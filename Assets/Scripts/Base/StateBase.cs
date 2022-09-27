using System;
using UnityEngine;

/// <summary>
/// ״̬����
/// </summary>
public abstract class StateBase<T>
{
    // ��ǰ״̬
    public T stateType;

    public virtual void Init(T state,FSMController<T> controller)
    {
        this.stateType = state;
    }

    public abstract void OnEnter();

    public abstract void OnLeave();

    public abstract void OnUpdate();
}
