using System;
using _Project.Scripts.Gameplay.Progress;

namespace _Project.Scripts.Infrastructure.PersistenceProgress
{
    [Serializable]
    public class PlayerProgress
    {
        public ScoreProgress ScoreProgress;

        public PlayerProgress()
        {
            ScoreProgress = new ScoreProgress();
        }
    }
}