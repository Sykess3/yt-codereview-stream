using System.Collections;
using System.Collections.Generic;
using Source.Configs;
using Source.Models.Randomizators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Models.Balls
{
    public class BallsSpawner : IUpdatable
    {
        public const int MaxBallsByOneSpawn = 4;
        private readonly IRandomBallGenerator _randomBallGeneratorGenerator;
        private readonly IRandomPositionGenerator _randomPositionGenerator;
        private readonly IBallsCollision _collision;
        private readonly LevelConfig _levelConfig;
        private readonly Ball[] _randomBalls;
        private float _timeToSpawn;
        private float _elapsedTime;

        public BallsSpawner(
            IRandomBallGenerator randomBallGeneratorGenerator,
            LevelConfig levelConfig,
            IRandomPositionGenerator randomPositionGenerator,
            IBallsCollision collision)
        {
            _randomBallGeneratorGenerator = randomBallGeneratorGenerator;
            _levelConfig = levelConfig;
            _randomPositionGenerator = randomPositionGenerator;
            _collision = collision;
            _randomBalls = new Ball[MaxBallsByOneSpawn];
        }

        public void Update(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;
            if (!TimeForSpawnPassed())
                return;

            IEnumerable<Ball> initializedBalls = CreateRandomAmountOfBalls();
            _collision.Chek(initializedBalls, onCollisionDetected: SetRandomPositionTo);
            UpdateTimeToSpawn();
        }

        private IEnumerable<Ball> CreateRandomAmountOfBalls()
        {
            var count = Random.Range(1, MaxBallsByOneSpawn + 1);
            FillRandomBallArray(count);
            return InitializeBallsByPositions(count);
        }

        private IEnumerable<Ball> InitializeBallsByPositions(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Ball randomBall = _randomBalls[i];
                SetRandomPositionTo(randomBall);
                yield return randomBall;
            }
        }


        private void SetRandomPositionTo(Ball randomBall)
        {
            Vector3 ballPosition = _randomPositionGenerator.Generate();
            randomBall.Initialize(ballPosition);
        }

        private void FillRandomBallArray(int count)
        {
            int index = 0;
            while (index < count)
            {
                _randomBalls[index] = DropRandomBall();
                index++;
            }
        }


        private Ball DropRandomBall() => _randomBallGeneratorGenerator.Get();


        private void UpdateTimeToSpawn()
        {
            _elapsedTime = -_timeToSpawn;
            _timeToSpawn = Random.Range(_levelConfig.MinTimeToSpawnInSeconds, _levelConfig.MaxTimeToSpawnInSeconds);
        }

        private bool TimeForSpawnPassed() => _elapsedTime >= _timeToSpawn;
    }
}