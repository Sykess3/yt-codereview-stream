using System.ComponentModel;
using Source.Models.Balls;
using UnityEngine;

namespace Source.Models
{
    public class RandomBall : IRandomBall
    {
        private const float RedBallChance = 1f;
        private readonly BallsObjectPool _pool;

        public RandomBall(BallsObjectPool pool)
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