using System.ComponentModel;
using Source.Models.Balls;
using Source.Models.DataStructures;
using UnityEngine;

namespace Source.Models.Randomizators
{
    public class RandomBallGenerator : IRandomBallGenerator
    {
        private const float RedBallChance = 1f;
        private readonly BallsObjectPool _pool;

        public RandomBallGenerator(BallsObjectPool pool)
        {
            _pool = pool;
            _pool.ReturnedToPool += OnReturnToPool;
        }

        public Ball Get()
        {
            var random = Random.Range(0f, 1f);
            if (random < RedBallChance)
            {
                var redBall = _pool.Get(BallType.Red);
                redBall.Popped += _pool.ReturnToPool;
                redBall.FeltOutOfBounds += _pool.ReturnToPool;
                return redBall;
            }

            throw new InvalidEnumArgumentException();
        }

        private void OnReturnToPool(Ball obj)
        {
            obj.Popped -= _pool.ReturnToPool;
            obj.FeltOutOfBounds -= _pool.ReturnToPool;
        }
    }
}