using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class UIAchievements : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _headingText;

        [Header("Others")]
        [SerializeField] private AchievementSlot _achievementSlotPrefab;
        [SerializeField] private Transform _achievementRoot;

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
            LoadAchievements();

            _backBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });
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
            _headingText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "ACHIEVEMENTS");
        }

        private void LoadAchievements()
        {
            foreach (Transform child in _achievementRoot.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < GameManager.Instance.RecordList.Count; i++)
            {
                AchievementSlot slot = Instantiate(_achievementSlotPrefab, _achievementRoot);
                slot.SetTimeText(Utilities.TimeToText(GameManager.Instance.RecordList[i]));

                if (i == 0)
                    slot.SetHighlightState();
                else
                {
                    slot.SetUnHighlightState();
                }
            }
        }
    }
}
