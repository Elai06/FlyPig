using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.UI.Pause
{
    public class PauseWindow : Window
    {
        [SerializeField] private PauseViewInitializer _viewInitializer;
        private PauseUIModel _model;

        [Inject]
        public void Construct(PauseUIModel model)
        {
            _model = model;
        }

        private void OnEnable()
        {
            _viewInitializer.Initialize(_model);
        }
    }
}