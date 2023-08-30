using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _figuresBtn;
        [SerializeField] private Button _settingsBtn;
        [SerializeField] private Button _achievementsBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _playBtnText;
        [SerializeField] private TextMeshProUGUI _figuresBtnText;
        [SerializeField] private TextMeshProUGUI _settingsBtnText;
        [SerializeField] private TextMeshProUGUI _achievementsBtnText;



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

            _playBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.Load(Loader.Scene.GameplayScene);
            });

            _figuresBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayFiguresMenu(true);
            });

            _settingsBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplaySettingsMenu(true);

            });

            _achievementsBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayAchievementsMenu(true);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _figuresBtn.onClick.RemoveAllListeners();
            _settingsBtn.onClick.RemoveAllListeners();
            _achievementsBtn.onClick.RemoveAllListeners();
        }

      

        private void LoadLanguague()
        {
            switch (LanguageManager.Instance.CurrentLanguague)
            {
                default:
                case LanguageManager.Languague.Eng:
                    _achievementsBtnText.fontSize = 27;
                    break;
                case LanguageManager.Languague.Rus:
                    _achievementsBtnText.fontSize = 35;
                    break;
            }


            _playBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PLAY");
            _figuresBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "FIGURES");
            _settingsBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SETTINGS");
            _achievementsBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "ACHIEVEMENTS");
        }
    }
}
