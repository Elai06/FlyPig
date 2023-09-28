using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.PersistenceProgress;
using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.MenuWindow
{
    public class MainMenuViewModelFactory : IViewModelFactory<MainMenuViewModel, MainMenuView, MainMenuUIModel>
    {
        private IWindowService _windowService;
        private PlayerScoreTracker _playerScoreTracker;
        private IProgressService _progressService;

        public MainMenuViewModelFactory(IWindowService windowService, PlayerScoreTracker playerScoreTracker,
            IProgressService progressService)
        {
            _windowService = windowService;
            _playerScoreTracker = playerScoreTracker;
            _progressService = progressService;
        }

        public MainMenuViewModel Create(MainMenuUIModel model, MainMenuView view) =>
            new(model, view, _windowService, _playerScoreTracker, _progressService);
    }
}