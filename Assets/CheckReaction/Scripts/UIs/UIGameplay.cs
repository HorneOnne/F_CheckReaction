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


        private void OnEnable()
        {
            //LanguageManager.OnLanguageChanged += LoadLanguague;
            GameLogicHandler.OnNodePasseed += LoadNodesUI;
        }

        private void OnDisable()
        {
            //LanguageManager.OnLanguageChanged -= LoadLanguague;
            GameLogicHandler.OnNodePasseed -= LoadNodesUI;
        }


        private void Start()
        {
            CreateNodes();         
            LoadNodesUI(GameLogicHandler.Instance.NumNodePassed);
            LoadLanguague();

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
            //switch (LanguageManager.Instance.CurrentLanguague)
            //{
            //    default:
            //    case LanguageManager.Languague.English:
            //        _playBtnText.fontSize = 70;
            //        _settingsBtnText.fontSize = 70;
            //        _languageBtnText.fontSize = 70;
            //        break;
            //    case LanguageManager.Languague.Norwegian:
            //    case LanguageManager.Languague.Italian:
            //    case LanguageManager.Languague.German:
            //        _playBtnText.fontSize = 55;
            //        _settingsBtnText.fontSize = 55;
            //        _languageBtnText.fontSize = 55;
            //        break;
            //}


            //_playBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PLAY");
            //_settingsBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SETTINGS");
            //_languageBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "LANGUAGE");
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
