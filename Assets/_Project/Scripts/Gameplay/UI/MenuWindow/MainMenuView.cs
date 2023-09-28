using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.UI.MenuWindow
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action ClickPauseButton;

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highscore;
        [SerializeField] private Button _pauseButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(ClickPause);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(ClickPause);
        }

        public void SetScore(string score)
        {
            _scoreText.text = score;
        }
        
        public void SetHighScore(string score)
        {
            _highscore.text = score;
        }

        private void ClickPause()
        {
            ClickPauseButton?.Invoke();
        }
    }
}