using System;
using Source.Infrastructure.Services;
using Source.Models.Level;

namespace Source.Models
{
    public class Score : IModel
    {
        private readonly IGoalConfig _goalConfig;
        private readonly PlayerProgress _progress;
        private int _currentScore;
        public event Action<int> Updated;
        public event Action GoalReached;

        public Score(IGoalConfig goalConfig, PlayerProgress progress)
        {
            _goalConfig = goalConfig;
            _progress = progress;
        }

        public void IncreaseBy(int amount)
        {
            _currentScore += amount;
            _progress.CurrentScore = _currentScore;
            if (_currentScore >= _goalConfig.Goal) 
                GoalReached?.Invoke();
            
            Updated?.Invoke(_currentScore);
        }
    }
}