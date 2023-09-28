using Infrastructure.Windows;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Pause
{
    public class PauseViewModelFactory : IViewModelFactory<PauseViewModel, PauseView, PauseUIModel>
    {
        private readonly IWindowService _windowService;
        private readonly GameManager _gameManager;

        public PauseViewModelFactory(IWindowService windowService, GameManager gameManager)
        {
            _windowService = windowService;
            _gameManager = gameManager;
        }

        public PauseViewModel Create(PauseUIModel model, PauseView view) =>
            new(model, view, _windowService, _gameManager);
    }
}