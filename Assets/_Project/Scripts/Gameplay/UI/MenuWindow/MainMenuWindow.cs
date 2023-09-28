using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.UI.MenuWindow
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private MainMenuViewInitializer _viewInitializer;

        private MainMenuUIModel _mainMenuUIModel;

        [Inject]
        public void Conscruct(MainMenuUIModel mainMenuUIModel)
        {
            _mainMenuUIModel = mainMenuUIModel;
        }

        private void OnEnable()
        {
            _viewInitializer.Initialize(_mainMenuUIModel);
        }
    }
}