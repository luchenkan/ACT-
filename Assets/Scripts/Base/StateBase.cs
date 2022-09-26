using System;
using UnityEngine;

/// <summary>
/// ״̬����
/// </summary>
public abstract class StateBase
{
    // ��ǰ״̬
    public Enum stateType;

    // ��ǰ���Ƶ�Ԫ
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
