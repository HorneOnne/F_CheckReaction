using TMPro;
using UnityEngine;

namespace CheckReaction
{
    public class UIGameover : CustomCanvas
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _tooEarlyText;

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
            _tooEarlyText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "too early");        
        }
    }
}
