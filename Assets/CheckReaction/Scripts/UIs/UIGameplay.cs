using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _bestText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _holdTheBtnText;

        [Header("Colors")]
        [SerializeField] private Color _passedColor;
        [SerializeField] private Color _unPassedColor;

        [Header("Others")]
        [SerializeField] private GameObject _squareNodePrefab;
        [SerializeField] private GameObject _circleNodePrefab;
        private Image[] _nodes = new Image[5];
        [SerializeField] private Transform _nodeRoot;


        // Cached
        private float _updateTimerFrequence = 0.05f;
        private float _updateTimerFrequenceCount = 0.0f;
        private string _bestString = "";


        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
            GameLogicHandler.OnNodePasseed += LoadNodesUI;

            GameplayManager.OnPlaying += LoadBest;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
            GameLogicHandler.OnNodePasseed -= LoadNodesUI;

            GameplayManager.OnPlaying -= LoadBest;
        }


        private void Start()
        {
            CreateNodes();         
            LoadNodesUI(GameLogicHandler.Instance.NumNodePassed);
            LoadLanguague();
            LoadBest();

            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                
                Loader.Load(Loader.Scene.MenuScene);
            });
        }

        private void Update()
        {
            if (Time.time - _updateTimerFrequenceCount > _updateTimerFrequence)
            {
                _updateTimerFrequenceCount = Time.time;
                UpdateTimeUI();
            }
        }

        private void UpdateTimeUI()
        {
            _timerText.text = TimerManager.Instance.TimeToText();
        }


        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
        }

   

        private void LoadLanguague()
        {
            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "back");
            _bestString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "best");
        }   


        private void LoadBest()
        {
            if(GameManager.Instance.RecordList.Count != 0)
            {
                _bestText.text = $"{_bestString} \n{Utilities.TimeToText(GameManager.Instance.RecordList[0])}";
            }
            else
            {
                _bestText.text = $"{_bestString} \n-";
            }
        }

        private void CreateNodes()
        {
            GameObject nodePrefab;
            switch(GameManager.Instance.Node)
            {
                case GameManager.NodeType.SQUARE:
                    nodePrefab = _squareNodePrefab;
                    break;
                default:
                    nodePrefab = _circleNodePrefab;
                    break;
            }

            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i] = Instantiate(nodePrefab, _nodeRoot).GetComponent<Image>();
            }
        }
        private void LoadNodesUI(int numNodePassed)
        {
            for(int i = 0; i < _nodes.Length; i++)
            {
                if (i < numNodePassed)
                {
                    _nodes[i].color = _passedColor;
                }
                else
                {
                    _nodes[i].color = _unPassedColor;
                }
            }
        }
    }
}
