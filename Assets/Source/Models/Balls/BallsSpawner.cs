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
        private readonly IRandomBallGenerator _randomBallGeneratorGenerator;
        private readonly IRandomPositionGenerator _randomPositionGenerator;
        private readonly IBallsSpawnerLevelConfig _levelConfig;
        private readonly Ball[] _randomBalls;
        private float _timeToSpawn;
        private float _elapsedTime;

        public BallsSpawner(
            IRandomBallGenerator randomBallGeneratorGenerator,
            IBallsSpawnerLevelConfig levelConfig,
            IRandomPositionGenerator randomPositionGenerator)
        {
            _randomBallGeneratorGenerator = randomBallGeneratorGenerator;
            _levelConfig = levelConfig;
            _randomPositionGenerator = randomPositionGenerator;
            _randomBalls = new Ball[levelConfig.BallsByOneSpawn.Max];
        }

        public void Update(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;
            if (!TimeForSpawnPassed())
                return;

            CreateRandomAmountOfBalls();
            UpdateTimeToSpawn();
        }

        private void CreateRandomAmountOfBalls()
        {
            var count = Random.Range(_levelConfig.BallsByOneSpawn.Min, _levelConfig.BallsByOneSpawn.Max + 1);
            FillRandomBallArray(count);
            InitializeBallsByPositions(count);
        }

        private void InitializeBallsByPositions(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Ball randomBall = _randomBalls[i];
                SetRandomPositionTo(randomBall);
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
            _timeToSpawn = Random.Range(_levelConfig.DelayBetweenSpawn.Min, _levelConfig.DelayBetweenSpawn.Max);
        }

        private bool TimeForSpawnPassed() => _elapsedTime >= _timeToSpawn;
    }
}