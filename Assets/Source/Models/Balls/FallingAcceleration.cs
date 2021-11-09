using System;
using UnityEngine;

namespace Source.Models.Balls
{
    public class FallingAcceleration : IUpdatable
    {
        private readonly IFallingAccelerationLevelConfig _levelConfig;
        private float _timePassedFromPreviousAcceleration;
        private Vector3 _acceleration;

        public event Action SpeedIncreased;

        public FallingAcceleration(IFallingAccelerationLevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public Vector3 Get() => - _acceleration;

        public void Update(float deltaTime)
        {
            _timePassedFromPreviousAcceleration += deltaTime;

            if (_timePassedFromPreviousAcceleration < _levelConfig.TimeToSpeedUp)
                return;
            
            IncreaseAcceleration();
        }

        private void IncreaseAcceleration()
        {
            _acceleration += new Vector3(0, _levelConfig.SpeedIncreaseAmountAfterSpeedUp, 0);
            _timePassedFromPreviousAcceleration = 0;
            SpeedIncreased?.Invoke();
        }
    }
}