using System;
using System.Collections.Generic;
using UnityEngine;

// �ҽ�����״̬��,��Ҫ�ǿ�����Һ͵���AI��
public abstract class FSMController<T> : MonoBehaviour
{
    // ��ǰ״̬
    public T curState;

    // ״̬�Ķ���
    public StateBase<T> curStateObject;

    // ���ȫ��״̬���󣨼򵥵�һ�������,ע����״̬�Ķ��󣡣�����
    // Ϊ�Ժ������ǰ��չ
    private Dictionary<T,StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();

    // ����״̬
    public void UpdateState<K>(T state, bool needRefresh = true) where K : StateBase<T>, new()
    {
        if (state.Equals(curState) || !needRefresh)
        {
            return;
        }

        if (curStateObject != null)
        {
            curStateObject.OnLeave();
        }

        curStateObject = GetSateObject<K>(state); // �����k����Player_Move
        curStateObject.OnEnter();
    }

    private StateBase<T> GetSateObject<K>(T state) where K : StateBase<T>,new()
    {
        // ���Ҷ�Ӧ��״̬
        if (stateDic.ContainsKey(state))
            return stateDic[state];

        StateBase<T> newState = new K();
        newState.Init(state, this);
        stateDic.Add(state, newState);
        return newState;
    }

    protected virtual void Update()
    {
        Debug.Log($"��ǰ״̬{curStateObject}");
        if (curStateObject != null)
        {
            curStateObject.OnUpdate();
        }
    }
}