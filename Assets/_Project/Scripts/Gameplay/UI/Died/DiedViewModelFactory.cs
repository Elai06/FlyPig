using _Project.Scripts.Gameplay.Player;
using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Died
{
    public class DiedViewModelFactory : IViewModelFactory<DiedViewModel, DiedView, PlayerScoreTracker>
    {
        private readonly PlayerScoreTracker _playerScoreTracker;
        private readonly GameManager _gameManager;
        private readonly IWindowService _windowService;

        public DiedViewModelFactory(PlayerScoreTracker playerScoreTracker, GameManager gameManager,
            IWindowService windowService)
        {
            _playerScoreTracker = playerScoreTracker;
            _gameManager = gameManager;
            _windowService = windowService;
        }

        public DiedViewModel Create(PlayerScoreTracker model, DiedView view)
        {
            return new DiedViewModel(model, view, _playerScoreTracker, _gameManager, _windowService);
        }
    }
}