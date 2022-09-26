using System;
using UnityEngine;

public enum PlayerState
{
    Player_Move,
    Player_Attack,
}

public class Player_Controller : FSMController
{
    private PlayerState state;
    public override Enum curState { get => state; set => state = (PlayerState)value; }

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
        UpdateState(PlayerState.Player_Move, true);
    }
}
