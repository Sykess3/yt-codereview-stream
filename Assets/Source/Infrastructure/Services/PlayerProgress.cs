using System;

namespace Source.Infrastructure.Services
{
    [Serializable]
    public class PlayerProgress
    {
        public int MaxScore;
        public int CurrentScore;

        public PlayerProgress(int maxScore)
        {
            MaxScore = maxScore;
        }
    }
}