using _Project.Scripts.Gameplay.Obstacles;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
      
        private bool _isStart;

        public void StartPlay()
        {
            _isStart = true;
        }

        public void Pause()
        {
            _isStart = false;
        }

        public void Reset()
        {
            _obstaclesSpawner.Reset();
        }
    }
}