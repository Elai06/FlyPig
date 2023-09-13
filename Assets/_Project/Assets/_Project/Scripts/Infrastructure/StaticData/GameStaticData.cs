using _Project.Scripts.Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Configs/GameStaticData")]
    public class GameStaticData : ScriptableObjectInstaller
    {
        [SerializeField] private WindowsStaticData _windowsStaticData;
        public WindowData GetWindowData(WindowType windowType)
        {
            return _windowsStaticData.GetWindows().Find(x => x.WindowType == windowType);
        }
    }
}