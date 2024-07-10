namespace GV.Player
{
    using UnityEngine;
    using GV.Patterns;

    [RequireComponent(typeof(PlayerData))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerData PlayerData { get; private set; }

        private StateMachine<PlayerController> _stateMachine;
        public Idle Idle { get; private set; }
        public Sleep Sleep { get; private set; }


        private void Awake() => InitializeController();

        private void Update() => _stateMachine.CurrentState.OnUpdate(this);

        private void InitializeController()
        {
            PlayerData = GetComponent<PlayerData>();
            _stateMachine = new StateMachine<PlayerController>(this);
            InstantiateStates();
            _stateMachine.Run(Sleep);
        }

        private void InstantiateStates()
        {
            Idle = new Idle("Idle", _stateMachine);
            Sleep = new Sleep("Sleep", _stateMachine);
        }
    }
}

