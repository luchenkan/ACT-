using System;
using System.Collections.Generic;
using UnityEngine;

// 我叫有限状态机,主要是控制玩家和敌人AI的
public abstract class FSMController<T> : MonoBehaviour
{
    // 当前状态
    public T curState;

    // 状态的对象
    public StateBase<T> curStateObject;

    // 存放全部状态对象（简单的一个对象池,注意是状态的对象！！！）
    // 为以后的做提前拓展
    private Dictionary<T,StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();

    // 更新状态
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

        curStateObject = GetSateObject<K>(state); // 这里的k类似Player_Move
        curStateObject.OnEnter();
    }

    private StateBase<T> GetSateObject<K>(T state) where K : StateBase<T>,new()
    {
        // 查找对应的状态
        if (stateDic.ContainsKey(state))
            return stateDic[state];

        StateBase<T> newState = new K();
        newState.Init(state, this);
        stateDic.Add(state, newState);
        return newState;
    }

    protected virtual void Update()
    {
        Debug.Log($"当前状态{curStateObject}");
        if (curStateObject != null)
        {
            curStateObject.OnUpdate();
        }
    }
}