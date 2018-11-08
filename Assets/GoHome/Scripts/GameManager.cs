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
            // GoHome\Data\GameSame.xml"
            fullPath = Application.dataPath + "/GoHome/Data/" + fileName + ".xml";
            // Check if file exists
            if (File.Exists(fullPath))
            {
                // Load the file and contents
                Load();
            }
        }
        private void OnDestroy()
        {
            Instance = null;
            // Save data on destroy
           //  Save();
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
        private GameData data = new GameData();

        
        private void Save()
        {
            // Set data's score to current
            data.score = currentScore;

            // Create a serializer of type GameData
            var serializer = new XmlSerializer(typeof(GameData));
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        private void Load()
        {
            var serializer = new XmlSerializer(typeof(GameData));
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                data = serializer.Deserialize(stream) as GameData;
            }
        }
        
        private void Start()
        {
            currentScore = data.score;
            ScoreText.text = "Score: " + currentScore;

            // Populate levels array with levels in game
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            SetLevel(currentLevel);
        }

        // Disable all levels except the levelIndex
        private void SetLevel(int LevelIndex)
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
                SetLevel(currentLevel);
            }
        }
    }
}
