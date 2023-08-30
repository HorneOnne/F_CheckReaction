using UnityEngine;

namespace CheckReaction
{
    public class GameLogicHandler : MonoBehaviour
    {
        public static GameLogicHandler Instance { get; private set; }
        public static event System.Action<int> OnNodePasseed;
    
        public const int NUMBER_OF_NODES = 5;
        private bool _isStartPress = false;

        // Timer
        private float _timer;
        private float _timeEachStage;
        private float _minTimeEachStage = 1.0f;
        private float _maxTimeEachStage = 1.3f;

        #region Properties
        public int NumNodePassed { get; private set; } = 0;
        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            ButtonLogic.OnMousePressBtn += () =>
            {
                _isStartPress = true;
            };

            ButtonLogic.OnMouseUpBtn += OnMouseUpBtnState;
            ButtonLogic.OnReset += OnNewRoundReset;
            GameplayManager.OnWin += OnWinReset;
        }


        private void OnDisable()
        {
            ButtonLogic.OnMousePressBtn -= () =>
            {
                _isStartPress = true;
            };

            ButtonLogic.OnMouseUpBtn -= OnMouseUpBtnState;
            ButtonLogic.OnReset -= OnNewRoundReset;
            GameplayManager.OnWin -= OnWinReset;
        }

        private void Start()
        {
            _timeEachStage = Random.Range(_minTimeEachStage, _maxTimeEachStage);
        }

        private void Update()
        {
            if(_isStartPress && GameplayManager.Instance.CurrentState == GameplayManager.GameState.PLAYING)
            {
                if(Time.time - _timer > _timeEachStage)
                {
                    _timer = Time.time;
                    _timeEachStage = Random.Range(_minTimeEachStage, _maxTimeEachStage);

                    NumNodePassed++;
                    OnNodePasseed?.Invoke(NumNodePassed);
                    
                    if(NumNodePassed == NUMBER_OF_NODES + 1)
                    {
                        GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.STARTTIMER);
                    }
                }
            }
        }


        private void OnMouseUpBtnState()
        {
            _isStartPress = false;
            bool isGameover = CheckGameOver();
            if(isGameover)
            {
                Debug.Log("Game over");
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
            }
            else
            {
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
            }
        }
        private bool CheckGameOver()
        {
            return NumNodePassed <= NUMBER_OF_NODES;
        }


        private void OnNewRoundReset()
        {
            NumNodePassed = 0;
            OnNodePasseed?.Invoke(NumNodePassed);
            _timeEachStage = Random.Range(_minTimeEachStage, _maxTimeEachStage);
            _timer = Time.time;
        }

        private void OnWinReset()
        {
            NumNodePassed = 0;
            OnNodePasseed?.Invoke(NumNodePassed);
        }
    }

}
