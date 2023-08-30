using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class AchievementSlot : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private TextMeshProUGUI _fasterText;
        [SerializeField] private TextMeshProUGUI _timeText;

        [Header("Others")]
        [SerializeField] private Sprite _highlightSprite;
        [SerializeField] private Sprite _unhighlightSprite;
        [SerializeField] private TMP_FontAsset _highlightFont;
        [SerializeField] private TMP_FontAsset _unhighlightFont;

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
        }

        private void LoadLanguague()
        {
            _fasterText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "FASTER");
        }

        public void SetTimeText(string text)
        {
            _timeText.text = text;
        }

        public void SetHighlightState()
        {
            _slotImage.sprite = _highlightSprite;

            _fasterText.font = _highlightFont;
            _timeText.font = _highlightFont;
        }

        public void SetUnHighlightState()
        {
            _slotImage.sprite = _unhighlightSprite;

            _fasterText.font = _unhighlightFont;
            _timeText.font = _unhighlightFont;
        }
    }
}
