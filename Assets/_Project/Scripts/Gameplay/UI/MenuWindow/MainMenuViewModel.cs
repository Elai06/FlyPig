using System.Threading.Tasks;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.PersistenceProgress;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.MenuWindow
{
    public class MainMenuViewModel : ViewModelBase<MainMenuUIModel, MainMenuView>
    {
        private readonly IWindowService _windowService;
        private readonly PlayerScoreTracker _playerScoreTracker;
        private readonly IProgressService _progressService;

        public MainMenuViewModel(MainMenuUIModel model, MainMenuView view, IWindowService windowService,
            PlayerScoreTracker playerScoreTracker, IProgressService progressService)
            : base(model, view)
        {
            _windowService = windowService;
            _playerScoreTracker = playerScoreTracker;
            _progressService = progressService;
        }

        public override Task Show()
        {
            View.SetScore(0.ToString());
            View.SetHighScore(_progressService.PlayerProgress.ScoreProgress.ScoreRecordValue.ToString());
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            base.Subscribe();

            View.ClickPauseButton += OnClickPause;
            _playerScoreTracker.ScoreChanged += ScoreChanged;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            View.ClickPauseButton -= OnClickPause;
            _playerScoreTracker.ScoreChanged -= ScoreChanged;
        }

        private void ScoreChanged(int score)
        {
            View.SetScore(score.ToString());
            View.SetHighScore(_progressService.PlayerProgress.ScoreProgress.ScoreRecordValue.ToString());
        }

        private void OnClickPause()
        {
            _windowService.Open(WindowType.Pause);
        }
    }
}