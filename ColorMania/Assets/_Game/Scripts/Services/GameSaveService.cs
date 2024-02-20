using Interfaces;
using UnityEngine;

namespace Services
{
    public class GameSaveService : ILevelSaveService
    {
        private const string _currentLevel_Key = nameof(_currentLevel_Key);

        private int currentLevel
        {
            get => PlayerPrefs.GetInt("_currentLevel_Key", 0);
            set => PlayerPrefs.SetInt("_currentLevel_Key", value);
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public void SetLevel(int level)
        {
            currentLevel = level;
        }
    }
}