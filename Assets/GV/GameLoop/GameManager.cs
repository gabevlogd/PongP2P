namespace GV.GameLoop
{
    using UnityEngine;
    using GV.Patterns;

    public class GameManager : Singleton<GameManager>
    {
        protected StateMachine<GameManager> _stateMachine;
        public Play Play { get; private set; }
        public GameOver GameOver { get; private set; }
        public Pause Pause { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            InitializeManager();
        }

        private void Update() => _stateMachine.CurrentState.OnUpdate(this);

        private void InitializeManager()
        {
            _stateMachine = new StateMachine<GameManager>(this);
            InstantiateStates();
            _stateMachine.Run(Play);
        }

        private void InstantiateStates()
        {
            Play = new Play("Play", _stateMachine);
            Pause = new Pause("Pause", _stateMachine);
            GameOver = new GameOver("GameOver", _stateMachine);
        }
    }
}

