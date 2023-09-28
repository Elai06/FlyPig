using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.UI.Died
{
    public class DiedView : MonoBehaviour
    {
        public event Action ReturnGame;
        public event Action ExitToMenu;

        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _return;

        private void OnEnable()
        {
            _exit.onClick.AddListener(Exit);
            _return.onClick.AddListener(Return);
        }

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(Exit);
            _return.onClick.RemoveListener(Return);
        }

        private void Exit()
        {
            ExitToMenu?.Invoke();
        }

        private void Return()
        {
            ReturnGame?.Invoke();
        }

        public void ShowScore(string score)
        {
            _score.text = score;
        }
    }
}