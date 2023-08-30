using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class UIFigures : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _squareFigureBtn;
        [SerializeField] private Button _circleFigureBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _headingText;

        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _unselectColor;

        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
        }

        private void Start()
        {
            LoadLanguague();
            LoadFigureNodeUI();

            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });

            _squareFigureBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                GameManager.Instance.Node = GameManager.NodeType.SQUARE;
                LoadFigureNodeUI();
            });

            _circleFigureBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                GameManager.Instance.Node = GameManager.NodeType.CIRCLE;
                LoadFigureNodeUI();
            });

        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _squareFigureBtn.onClick.RemoveAllListeners();
            _circleFigureBtn.onClick.RemoveAllListeners();
        }



        private void LoadLanguague()
        {
            //switch (LanguageManager.Instance.CurrentLanguague)
            //{
            //    default:
            //    case LanguageManager.Languague.Eng:
            //        _playBtnText.fontSize = 70;
            //        _settingsBtnText.fontSize = 70;
            //        _languageBtnText.fontSize = 70;
            //        break;
            //    case LanguageManager.Languague.Rus:
            //        _playBtnText.fontSize = 55;
            //        _settingsBtnText.fontSize = 55;
            //        _languageBtnText.fontSize = 55;
            //        break;
            //}


            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "back");
            _headingText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "FIGURES");
        }

        private void LoadFigureNodeUI()
        {
            switch(GameManager.Instance.Node)
            {
                case GameManager.NodeType.SQUARE:
                    _squareFigureBtn.image.color = _selectColor;
                    _circleFigureBtn.image.color = _unselectColor;
                    break;
                default:
                    _squareFigureBtn.image.color = _unselectColor;
                    _circleFigureBtn.image.color = _selectColor;
                    break;
            }
        }
    }
}
