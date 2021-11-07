using Source.Configs;
using Source.Models.Balls;
using UnityEngine;

namespace Source.Models
{
    public class BallsSpawner : IUpdatableView
    {
        private readonly IRandomBall _randomBall;
        private readonly LevelConfig _levelConfig;
        private float _timeToSpawn;
        private float _elapsedTime;

        public BallsSpawner(IRandomBall randomBall, LevelConfig levelConfig)
        {
            _randomBall = randomBall;
            _levelConfig = levelConfig;
        }
        public void Update(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;
            if (!TimeForSpawnPassed())
                return;

            var ball = DropRandomBall();
            ball.Initialize(new Vector3(0, 2, 0));
            UpdateTimeCounter();
        }
        private Ball DropRandomBall() => _randomBall.Get();


        private void UpdateTimeCounter()
        {
            _elapsedTime = -_timeToSpawn;
            _timeToSpawn = Random.Range(_levelConfig.MinTimeToSpawnInSeconds, _levelConfig.MaxTimeToSpawnInSeconds);
        }

        private bool TimeForSpawnPassed() => _elapsedTime >= _timeToSpawn;
    }
}