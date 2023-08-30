using UnityEngine;

namespace CheckReaction
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Instance { get; private set; }

        private float timer = 0f;
        // Cached
        private GameplayManager _gameplayManager;
        private bool _startTimer = false;

        #region Properties
        public float Time { get { return timer; } }
        #endregion


        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GameplayManager.OnStartTimer += () =>
            {
                _startTimer = true;
            };

            ButtonLogic.OnReset += ResetTimer;
        }

        private void OnDisable()
        {
            GameplayManager.OnStartTimer -= () =>
            {
                _startTimer = true;
            };

            ButtonLogic.OnReset -= ResetTimer;
        }

        private void Start()
        {
            _gameplayManager = GameplayManager.Instance;
        }

        private void Update()
        {

            if (_gameplayManager.CurrentState == GameplayManager.GameState.STARTTIMER && _startTimer)
            {
                timer += UnityEngine.Time.deltaTime;
            }
        }

        public string TimeToText()
        {
            int minutes = Mathf.FloorToInt(timer);
            int seconds = Mathf.RoundToInt((timer - minutes) * 60);

            return $"{minutes:D1},{seconds:D3}";
        }

        public string TimeToText(float value)
        {
            int minutes = Mathf.FloorToInt(value);
            int seconds = Mathf.RoundToInt((value - minutes) * 60);

            return $"{minutes:D1},{seconds:D3}";
        }

        private void ResetTimer()
        {
            timer = 0.0f;
        }
    }
}
