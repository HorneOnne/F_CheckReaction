using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace CheckReaction
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance { get; private set; }
        public static System.Action OnLanguageChanged;

        public TMP_FontAsset NormalFont;
        public TMP_FontAsset RusFont;

        private Dictionary<string, WordDict> dict = new Dictionary<string, WordDict>()
        {
            {"english", new WordDict("english", "английский")},
            {"russian", new WordDict("russian", "русский")},
            {"PLAY", new WordDict("PLAY", "играть")},
            {"FIGURES", new WordDict("FIGURES", "фигуры")},
            {"SETTINGS", new WordDict("SETTINGS", "настройки")},
            {"ACHIEVEMENTS", new WordDict("ACHIEVEMENTS", "достижения")},
            {"language", new WordDict("language", "язык")},
            {"back", new WordDict("back", "назад")},
            {"sound", new WordDict("sound", "звук")},
            {"FASTER", new WordDict("FASTER", "быстрее")},
            {"HOLD THE \nBUTTON", new WordDict("HOLD THE \nBUTTON", "зажми \nкнопку")},
            {"too early", new WordDict("too early", "слишком рано")},
            {"best", new WordDict("best", "лучший")},
            {"release the \nbutton when \nthe lights go \nout", 
                new WordDict("release the \nbutton when \nthe lights go \nout",
                            "отпусти \nкнопку, \nкогда огни \nпогаснут")},

        };


        public enum Languague
        {
            Eng,
            Rus
        }

        public Languague CurrentLanguague;


        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }


        public void ChangeLanguague(Languague languague)
        {
            this.CurrentLanguague = languague;
            OnLanguageChanged?.Invoke();
        }

        public string GetWord(Languague type, string word)
        {
            if (dict.ContainsKey(word))
            {
                return dict[word].GetWord(type);
            }
            return "";
        }
    }

    public class WordDict
    {
        public string Eng;
        public string Rus;

        public WordDict(string eng,  string rus)
        {
            Eng = eng;
            Rus = rus;
        }

        public string GetWord(LanguageManager.Languague language)
        {
            switch (language)
            {
                default:
                case LanguageManager.Languague.Eng:
                    return Eng;
                case LanguageManager.Languague.Rus:
                    return Rus;
            }
        }
    }
}
