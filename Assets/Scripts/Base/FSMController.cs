using System;
using System.Collections.Generic;
using UnityEngine;

// 我叫有限状态机,主要是控制玩家和敌人AI的
public abstract class FSMController : MonoBehaviour
{
    // 当前状态
    public abstract Enum curState { get; set; }

    // 状态的对象
    public StateBase curStateObject;

    // 存放全部状态对象（简单的一个对象池,注意是状态的对象！！！）
    // 为以后的做提前拓展
    private List<StateBase> stateList = new List<StateBase>();

    // 更新状态
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
        // 这里仅仅是查找对应的状态
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