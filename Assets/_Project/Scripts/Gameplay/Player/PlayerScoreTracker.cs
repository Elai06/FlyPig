using System;
using _Project.Scripts.Infrastructure.PersistenceProgress;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerScoreTracker : MonoBehaviour
    {
        public event Action<int> ScoreChanged;

        private const int OBSTACLE_SCORE_POINT_LAYER = 8;

        private GameManager _gameManager;
        private IProgressService _playerProgress;

        private int _score;

        [Inject]
        private void Construct(GameManager gameManager, IProgressService playerProgress)
        {
            _gameManager = gameManager;
            _playerProgress = playerProgress;
        }

        private void OnEnable()
        {
            _gameManager.Reset += OnResetGame;
        }

        private void OnDisable()
        {
            _gameManager.Reset -= OnResetGame;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == OBSTACLE_SCORE_POINT_LAYER)
            {
                SetScore(1);
            }
        }
        
        private void OnResetGame()
        {
            SetScore(-_score);
        }

        private void SetHighScore()
        {
            var progress = _playerProgress.PlayerProgress.ScoreProgress;
            if (progress.ScoreRecordValue < _score)
            {
                progress.ScoreRecordValue = _score;
            }
        }

        private void SetScore(int value)
        {
            _score += value;
            SetHighScore();
            ScoreChanged?.Invoke(_score);
        }

        public int GetScore()
        {
           return _score;
        }
    }
}