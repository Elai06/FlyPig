using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> _obstacles;
        [SerializeField] private int _startObstacles = 1;
        [SerializeField] private float _interval;
        [SerializeField] private float _speed;


        private Dictionary<int, Obstacle> _obstaclesSpawned = new();

        public void Start()
        {
            SpawnObstacles();
        }

        public void ObstacleUpdate()
        {
            MoveObstacles();
            TrackingObstacles();
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
            foreach (var obstacle in _obstaclesSpawned.Values
                         .Where(obstacle => obstacle.gameObject.activeSelf))
            {
                obstacle.gameObject.transform.position += Vector3.left * (_speed * Time.deltaTime);
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

            CreateObstacle();
        }
    }
}