using System;
using _Project.Scripts.Gameplay.Player;
using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.UI.Died
{
    public class DiedWindow : Window
    {
        [SerializeField] private DiedViewInitializer _diedViewInitializer;

        private PlayerScoreTracker _playerScoreTracker;

        [Inject]
        public void Construct(PlayerScoreTracker playerScoreTracker)
        {
            _playerScoreTracker = playerScoreTracker;
        }

        private void OnEnable()
        {
            _diedViewInitializer.Initialize(_playerScoreTracker);
        }
    }
}