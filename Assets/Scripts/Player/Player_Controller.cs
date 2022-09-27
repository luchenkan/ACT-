using System;
using UnityEngine;

public enum PlayerState
{
    None,
    Player_Move,
    Player_Attack,
}

public class Player_Controller : FSMController<PlayerState>
{
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }

    public CharacterController characterController { get; private set; }

    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        characterController = GetComponent<CharacterController>();

        // 初始化移动状态
        UpdateState<Player_Move>(PlayerState.Player_Move, true);
    }

    public bool CheckAttack()
    {
        return input.GetAttackKey();
    }
}
