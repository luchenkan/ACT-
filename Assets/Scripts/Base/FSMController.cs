using System;
using System.Collections.Generic;
using UnityEngine;

// �ҽ�����״̬��,��Ҫ�ǿ�����Һ͵���AI��
public abstract class FSMController : MonoBehaviour
{
    // ��ǰ״̬
    public abstract Enum curState { get; set; }

    // ״̬�Ķ���
    public StateBase curStateObject;

    // ���ȫ��״̬���󣨼򵥵�һ�������,ע����״̬�Ķ��󣡣�����
    // Ϊ�Ժ������ǰ��չ
    private List<StateBase> stateList = new List<StateBase>();

    // ����״̬
    public void UpdateState(Enum state, bool needRefresh)
    {
        if (state == curState || !needRefresh)
        {
            return;
        }

        if (curStateObject != null)
        {
            return;
        }

        curStateObject = GetSateObject(state);
        curStateObject.OnEnter();
    }

    private StateBase GetSateObject(Enum state)
    {
        // ��������ǲ��Ҷ�Ӧ��״̬
        for (int i = 0; i < stateList.Count; ++i)
        {
            if (stateList[i].stateType == state)
            {
                return stateList[i];
            }
        }
        StateBase newState = Activator.CreateInstance(Type.GetType(state.ToString())) as StateBase;
        newState.Init(state, this);
        stateList.Add(newState);
        return newState;
    }

    protected virtual void Update()
    {
        if (curStateObject != null)
        {
            curStateObject.OnUpdate();
        }
    }
}