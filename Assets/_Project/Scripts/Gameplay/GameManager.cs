using System;
using _Project.Scripts.Gameplay.Obstacles;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public event Action Reset;
        public event Action Died;

        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
        [SerializeField] private PlayerMovement _playerMovement;

        private IWindowService _windowService;

        [Inject]
        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public bool IsStart { get; private set; }

        private void OnEnable()
        {
            _playerManager.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerManager.Died -= OnDied;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                StartPlay();
            }
        }

        private void Update()
        {
            if (!IsStart) return;
            _obstaclesSpawner.ObstacleUpdate();
            _playerMovement.MovementUpdate();
        }

        private void OnDied()
        {
            Pause();
            _windowService.Open(WindowType.Died);
            _playerMovement.ResetSpeed();
            Died?.Invoke();
        }

        public void StartPlay()
        {
            IsStart = true;
        }

        public void Pause()
        {
            IsStart = false;
        }

        public void ResetGame()
        {
            Pause();
            _obstaclesSpawner.Reset();
            Reset?.Invoke();
        }
    }
}