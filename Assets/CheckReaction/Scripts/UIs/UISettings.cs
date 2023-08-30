using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class UISettings : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _englishBtn;
        [SerializeField] private Button _russianBtn;


        [Header("Sliders")]
        [SerializeField] private Slider _soundSlider;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _englishBtnText;
        [SerializeField] private TextMeshProUGUI _russianBtnText;

        [SerializeField] private TextMeshProUGUI _settingsText;
        [SerializeField] private TextMeshProUGUI _languageText;
        [SerializeField] private TextMeshProUGUI _soundText;


        [Header("Others")]
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _deactiveSprite;
        [SerializeField] private TMP_FontAsset _activeFont;
        [SerializeField] private TMP_FontAsset _deactiveFont;


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
            _soundSlider.value = SoundManager.Instance.SFXVolume;


            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);           
            });

            _englishBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                LanguageManager.Instance.ChangeLanguague(LanguageManager.Languague.Eng);
            });

            _russianBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                LanguageManager.Instance.ChangeLanguague(LanguageManager.Languague.Rus);
            });

            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _englishBtn.onClick.RemoveAllListeners();
            _russianBtn.onClick.RemoveAllListeners();
            _soundSlider.onValueChanged.RemoveAllListeners();
        }

        private void OnSoundSliderChanged(float value)
        {
            SoundManager.Instance.SFXVolume = value;
            SoundManager.Instance.BackgroundVolume = value;
            SoundManager.Instance.UpdateBackgroundVolume();
        }

       
        private void LoadLanguague()
        {
            switch (LanguageManager.Instance.CurrentLanguague)
            {
                default:
                case LanguageManager.Languague.Eng:
                    _englishBtn.image.sprite = _activeSprite;
                    _russianBtn.image.sprite = _deactiveSprite;

                    _englishBtnText.font = _activeFont;
                    _russianBtnText.font = _deactiveFont;
                    break;
                case LanguageManager.Languague.Rus:
                    _englishBtn.image.sprite = _deactiveSprite;
                    _russianBtn.image.sprite = _activeSprite;

                    _englishBtnText.font = _deactiveFont;
                    _russianBtnText.font = _activeFont;
                    break;
            }


            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "back");
            _englishBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "english");
            _russianBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "russian");
            _settingsText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SETTINGS");
            _languageText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "language");
            _soundText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "sound");
        }
    }
}
