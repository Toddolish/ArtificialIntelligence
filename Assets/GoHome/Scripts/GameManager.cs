using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Xml.Serialization;
using System.IO;

namespace GoHome
{
    [Serializable]
    public class GameData
    {
        public int score;
    }

    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        private void Awake()
        {
            Instance = this;
            // dataPath = "C:\Users\Documents\Project\Assets"
            fullPath = Application.dataPath + "/GoHome/Data/" + fileName;
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        #endregion
        public int currentScore = 0;
        public int currentLevel = 0;
        public bool isGameRunning = true;
        public Transform levelContainer;

        [Header("UI")]
        public Text ScoreText;

        [Header("Game Saves")]
        public String fileName = "GameData";
        
        public Level[] levels;
        private string fullPath;

        
        private void Save()
        {
            // Create a serializer of type GameData
            var serializer = new XmlSerializer(typeof(GameData));
            //using (var stream = new FileStream(fullPath))
            {

            }
        }

        private void Load()
        {
            
        }
        
        private void Start()
        {
            // Populate levels array with levels in game
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            setLevel(currentLevel);
        }

        // Disable all levels except the levelIndex
        private void setLevel(int LevelIndex)
        {
            // Loop through all levels
            for (int i = 0; i < levels.Length; i++)
            {
                GameObject level = levels[i].gameObject;
                level.SetActive(false); // Disable level
                // Is current index (i) the same as levelIndex?
                if(i == LevelIndex)
                {
                    // Enable that level instead
                    level.SetActive(true);
                }
            }
        }

        public void GameOver()
        {
            // Stop game from running
            isGameRunning = false;
        }

        public void AddScore(int scoreToAdd)
        {
            currentScore += scoreToAdd;
            ScoreText.text = "Score: " + currentScore;
        }

        public void NextLevel()
        {
            // Increase current level
            currentLevel++;
            // If currentLevel exceeds level length
            if (currentLevel >= levels.Length)
            {
                // GameOver!
            }
            // else
            else
            {
                // Update current level
                setLevel(currentLevel);
            }
        }
    }
}
