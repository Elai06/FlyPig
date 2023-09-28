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

        public bool IsPlay { get; private set; }
        public bool IsPause { get; private set; }

        public bool IsCanPlay => IsPlay && !IsPause;

        private void OnEnable()
        {
            _playerManager.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerManager.Died -= OnDied;
        }

        private bool IsUIClick()
        {
#if UNITY_EDITOR
            return EventSystem.current.IsPointerOverGameObject();
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                var isTouchingUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                return isTouchingUI;
            }

            return false;
#endif
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsUIClick() && !IsPause) return;
                StartPlay();
            }
        }

        private void Update()
        {
            if (!IsCanPlay) return;
            _obstaclesSpawner.ObstacleUpdate();
            _playerMovement.MovementUpdate();
        }

        private void OnDied()
        {
            SwitchPlay(false);
            _windowService.Open(WindowType.Died);
            _playerMovement.ResetSpeed();
            Died?.Invoke();
        }

        public void StartPlay()
        {
            SwitchPlay(true);
        }

        public void Pause(bool isPause)
        {
            IsPause = isPause;
        }

        private void SwitchPlay(bool isPlay)
        {
            IsPlay = isPlay;
        }

        public void ResetGame()
        {
            SwitchPlay(false);
            Pause(false);
            _obstaclesSpawner.Reset();
            Reset?.Invoke();
        }
    }
}