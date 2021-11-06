using System.ComponentModel;
using Source.DataStructures;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services
{
    public class RandomBall : IRandomBall
    {
        private const float RedBallChance = 1f;
        private readonly BallsViewsObjectPool _pool;

        public RandomBall(BallsViewsObjectPool pool)
        {
            _pool = pool;
            _pool.ReturnedToPool += OnReturnToPool;
        }

        public BallView Get()
        {
            var random = Random.Range(0f, 1f);
            if (random < RedBallChance)
            {
                var redBall = _pool.Get(BallType.Red);
                redBall.Clicked += _pool.ReturnToPool;
                return redBall;
            }

            throw new InvalidEnumArgumentException();
        }

        private void OnReturnToPool(BallView obj) => 
            obj.Clicked -= _pool.ReturnToPool;
    }
}