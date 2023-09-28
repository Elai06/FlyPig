using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.UI.Died;
using _Project.Scripts.Gameplay.UI.MenuWindow;
using _Project.Scripts.Gameplay.UI.Pause;
using _Project.Scripts.Infrastructure.PersistenceProgress;
using _Project.Scripts.Infrastructure.SaveLoads;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StaticData;
using Infrastructure.SaveLoads;
using Infrastructure.SceneManagement;
using Infrastructure.UnityBehaviours;
using Infrastructure.Windows;
using SirGames.Scripts.Infrastructure.StateMachine;
using SirGames.Scripts.Infrastructure.Windows;
using SirGames.Scripts.Windows;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineService _coroutineService;
        [SerializeField] private LayersContainer _layersContainer;
        [SerializeField] private GameStaticData _gameStaticData;
        [SerializeField] private GameManager _gameManager;
        [FormerlySerializedAs("_score")] [SerializeField] private PlayerScoreTracker _playerScoreTracker;

        public override void InstallBindings()
        {
            BindViewModelFactory();
            BindGameStates();
            BindInfrastructureServices();
            BindModels();
        }

        private void BindViewModelFactory()
        {
            Container.BindInterfacesAndSelfTo<MainMenuViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiedViewModelFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ExitState>().AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IProgressService>().To<PlayerProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();

            Container.Bind<LayersContainer>().FromInstance(_layersContainer).AsSingle();
            Container.Bind<GameStaticData>().FromInstance(_gameStaticData).AsSingle();
            Container.Bind<ICoroutineService>().FromInstance(_coroutineService).AsSingle();
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<PauseUIModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuUIModel>().AsSingle();
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<PlayerScoreTracker>().FromInstance(_playerScoreTracker).AsSingle();
        }
    }
}