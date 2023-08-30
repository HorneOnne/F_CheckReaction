using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CheckReaction
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event System.Action OnScoreUp;


        public enum NodeType
        {
            SQUARE,
            CIRCLE,
        }


        #region Properties
        public NodeType Node { get; set; } = NodeType.SQUARE; 
        public List<float> RecordList { get; private set; } = new List<float>();
        #endregion

        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }


        public void SetRecord(float score)
        {
            RecordList = AddAndSort(RecordList, score, 7);
        }

        private List<float> AddAndSort(List<float> originalList, float newValue, int maxLength)
        {
            originalList.Add(newValue);
            originalList.Sort((a, b) => a.CompareTo(b));

            if (originalList.Count > maxLength)
            {
                originalList.RemoveAt(maxLength);
            }

            return originalList;
        }


    }
}
