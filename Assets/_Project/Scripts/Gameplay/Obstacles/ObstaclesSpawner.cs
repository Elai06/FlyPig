using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Player;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerScoreTracker _playerScoreTracker;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private List<Obstacle> _obstacles;
        [SerializeField] private int _startObstacles = 1;
        [SerializeField] private float _interval;
        [SerializeField] private float _beginningSpeed = 1;
        [SerializeField] private float _speedMultiplier = 1.05f;

        private float _currentSpeed;

        private readonly Dictionary<int, Obstacle> _obstaclesSpawned = new();

        public float Speed => _currentSpeed;

        public void Start()
        {
            SpawnObstacles();
            _currentSpeed = _beginningSpeed;
        }

        private void OnEnable()
        {
            _playerScoreTracker.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _playerScoreTracker.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            if (score % 3 == 0)
            {
                _currentSpeed *= _speedMultiplier;
                
                Debug.Log($"Speed : {_currentSpeed}");
            }
        }

        public void Update()
        {
            if (_gameManager.IsCanPlay)
            {
                MoveObstacles();
                TrackingObstacles();
            }
        }

        private void TrackingObstacles()
        {
            for (int i = 0; i < _obstaclesSpawned.Count; i++)
            {
                var obstacle = _obstaclesSpawned.Values.ToList()[i];
                if (obstacle.gameObject.activeSelf && obstacle.EndPoint.position.x < -1.75f)
                {
                    obstacle.gameObject.SetActive(false);
                    CreateObstacle();
                }
            }
        }

        private void MoveObstacles()
        {
            for (int i = 0; i < _obstaclesSpawned.Count; i++)
            {
                var obstacle = _obstaclesSpawned.Values.ToList()[i];
                if (obstacle.gameObject.activeSelf)
                {
                    obstacle.gameObject.transform.Translate(-_currentSpeed * Time.deltaTime, 0,0);
                }
            }
        }

        private void SpawnObstacles()
        {
            for (int i = 0; i < _startObstacles; i++)
            {
                CreateObstacle();
            }
        }

        private void CreateObstacle()
        {
            var obstacleIndex = GetRandomObstacleIndex();

            while (!IsAvailableObstacle(obstacleIndex))
            {
                obstacleIndex = GetRandomObstacleIndex();
            }

            if (_obstaclesSpawned.TryGetValue(obstacleIndex, out var obstacle))
            {
                obstacle.gameObject.SetActive(true);
                obstacle.transform.localPosition = Vector3.zero;
            }
            else
            {
                obstacle = Instantiate(_obstacles[obstacleIndex], transform);
                _obstaclesSpawned.Add(obstacleIndex, obstacle);
            }
        }

        private int GetRandomObstacleIndex()
        {
            var index = Random.Range(0, _obstacles.Count);
            return index;
        }

        private bool IsAvailableObstacle(int index)
        {
            if (_obstaclesSpawned.TryGetValue(index, out var obstacle))
            {
                return !obstacle.gameObject.activeSelf;
            }

            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < _obstaclesSpawned.Count; i++)
            {
                var obstacle = _obstaclesSpawned.Values.ToList()[i];
                obstacle.gameObject.SetActive(false);
            }

            _currentSpeed = _beginningSpeed;
            CreateObstacle();
        }
    }
}