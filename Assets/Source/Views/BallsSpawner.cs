using System;
using Source.Configs;
using Source.Infrastructure.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Views
{
    public class BallsSpawner : MonoBehaviour
    {
        private IRandomBall _randomBall;
        private LevelConfig _levelConfig;
        private float _timeToSpawn;
        private float _elapsedTime;

        public BallsSpawner Construct(IRandomBall randomBall, LevelConfig levelConfig)
        {
            _randomBall = randomBall;
            _levelConfig = levelConfig;
            
            return this;
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (!TimeForSpawnPassed())
                return;
            
            _randomBall.Get().Clicked += OnClicked;
            UpdateTimeCounter();
        }

        private void OnClicked(BallView ballView)
        {
        }

        private void UpdateTimeCounter()
        {
            _elapsedTime = -_timeToSpawn;
            _timeToSpawn = Random.Range(_levelConfig.MinTimeToSpawnInSeconds, _levelConfig.MaxTimeToSpawnInSeconds);
        }

        private bool TimeForSpawnPassed() => _elapsedTime >= _timeToSpawn;
    }
}