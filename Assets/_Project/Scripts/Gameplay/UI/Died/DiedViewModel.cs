using System.Threading.Tasks;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Died
{
    public class DiedViewModel : ViewModelBase<PlayerScoreTracker, DiedView>
    {
        private PlayerScoreTracker _playerScoreTracker;
        private GameManager _gameManager;
        private IWindowService _windowService;

        public DiedViewModel(PlayerScoreTracker model, DiedView view, PlayerScoreTracker playerScoreTracker,
            GameManager gameManager, IWindowService windowService) : base(model, view)
        {
            _playerScoreTracker = playerScoreTracker;
            _gameManager = gameManager;
            _windowService = windowService;
        }

        public override Task Show()
        {
            View.ShowScore(_playerScoreTracker.GetScore().ToString());
            _gameManager.Pause(true);
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            base.Subscribe();

            View.ExitToMenu += ExitToMenu;
            View.ReturnGame += ExitToMenu;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            View.ExitToMenu -= ExitToMenu;
            View.ReturnGame -= ExitToMenu;
        }

        private void ExitToMenu()
        {
            _windowService.Close(WindowType.Died);
            _gameManager.ResetGame();
        }
    }
}