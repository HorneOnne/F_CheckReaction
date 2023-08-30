using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace CheckReaction
{
    public class ButtonLogic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static event System.Action OnMousePressBtn;
        public static event System.Action OnMouseUpBtn;
        public static event System.Action OnReset;

        [Header("References")]
        [SerializeField] private Image _btnImage;
        [SerializeField] private TextMeshProUGUI _btnText;
        [Space(5)]
        [SerializeField] private Color _idleBtnColor;
        [SerializeField] private Color _pressedBtnColor;
        [Space(5)]
        [SerializeField] private TMP_FontAsset _idleFont;
        [SerializeField] private TMP_FontAsset _pressedFont;


        private const string IDLE_TEXT = "HOLD THE BUTTON";
        private const string PRESSED_TEXT = "release the \nbutton \nwhen the \nlights go \nout";


        private void Start()
        {
            LoadIdleBtn();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.WIN ||
                GameplayManager.Instance.CurrentState == GameplayManager.GameState.GAMEOVER)
            {
                OnReset?.Invoke();
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
            }

            if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.PLAYING)
            {
                OnMousePressBtn?.Invoke();
                LoadPressedBtn();
            }

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnMouseUpBtn?.Invoke();
            LoadIdleBtn();
        }

        private void LoadIdleBtn()
        {
            _btnImage.color = _idleBtnColor;
            _btnText.fontSize = 60;
            _btnText.font = _idleFont;
            _btnText.text = IDLE_TEXT;
        }

        private void LoadPressedBtn()
        {
            _btnImage.color = _pressedBtnColor;
            _btnText.fontSize = 40;
            _btnText.font = _pressedFont;
            _btnText.text = PRESSED_TEXT;
        }
    }

}
