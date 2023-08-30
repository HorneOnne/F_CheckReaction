using UnityEngine;

namespace CheckReaction
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu UIMainMenu;
        public UIFigures UIFigures;
        public UISettings UISettings;
        public UIAchievements UIAchievements;
 


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayFiguresMenu(false);
            DisplaySettingsMenu(false);
            DisplayAchievementsMenu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            UIMainMenu.DisplayCanvas(isActive);
        }

        public void DisplayFiguresMenu(bool isActive)
        {
            UIFigures.DisplayCanvas(isActive);
        }

        public void DisplaySettingsMenu(bool isActive)
        {
            UISettings.DisplayCanvas(isActive);
        }

        public void DisplayAchievementsMenu(bool isActive)
        {
            UIAchievements.DisplayCanvas(isActive);
        }
    }
}
