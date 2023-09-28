using System.Threading.Tasks;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Pause
{
    public class PauseViewModel : ViewModelBase<PauseUIModel, PauseView >
    {
        private readonly IWindowService _windowService;
        private readonly GameManager _gameManager;
        
        public PauseViewModel(PauseUIModel model, PauseView view, IWindowService windowService, GameManager gameManager) : base(model, view)
        {
            _windowService = windowService;
            _gameManager = gameManager;
        }

        public override Task Show()
        {
            _gameManager.Pause();
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            base.Subscribe();

            View.ReturnClick += Return;
            View.ExitClick += Exit;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            
            View.ReturnClick -= Return;
            View.ExitClick -= Exit;
        }

        private void Exit()
        {
            _gameManager.ResetGame();
            _windowService.Close(WindowType.Pause);
        }

        private void Return()
        {
            _windowService.Close(WindowType.Pause);
        }
    }
}