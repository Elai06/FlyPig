using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.UI.Pause
{
    public class PauseView : MonoBehaviour
    {
        public event Action ReturnClick;
        public event Action ExitClick;

        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _returnButton;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Exit);
            _returnButton.onClick.AddListener(Return);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveAllListeners();
        }

        private void Return()
        {
            ReturnClick?.Invoke();
        }

        private void Exit()
        {
            ExitClick?.Invoke();
        }
    }
}