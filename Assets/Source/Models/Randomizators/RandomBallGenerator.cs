using System.ComponentModel;
using Source.Models.Balls;
using Source.Models.DataStructures;
using UnityEngine;

namespace Source.Models.Randomizators
{
    public class RandomBallGenerator : IRandomBallGenerator
    {
        private const float RedBallChance = 0.25f;
        private const float BlueBallChance = 0.5f;
        private const float OrangeBallChance = 0.75f;
        private const float PurpleBallChance = 1f;
        
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
                SubscribeToReturnPool(redBall);
                return redBall;
            }
            if(random < BlueBallChance)
            {
                var blueBall = _pool.Get(BallType.Blue);
                SubscribeToReturnPool(blueBall);
                return blueBall;
            }
            if (random < OrangeBallChance)
            {
                var orangeBall = _pool.Get(BallType.Orange);
                SubscribeToReturnPool(orangeBall);
                return orangeBall;
            }
            if (random < PurpleBallChance)
            {
                var purpleBall = _pool.Get(BallType.Purple);
                SubscribeToReturnPool(purpleBall);
                return purpleBall;
            }

            throw new InvalidEnumArgumentException();
        }

        private void SubscribeToReturnPool(Ball redBall)
        {
            redBall.Popped += _pool.ReturnToPool;
            redBall.FeltOutOfBounds += _pool.ReturnToPool;
        }

        private void OnReturnToPool(Ball obj)
        {
            obj.Popped -= _pool.ReturnToPool;
            obj.FeltOutOfBounds -= _pool.ReturnToPool;
        }
    }
}