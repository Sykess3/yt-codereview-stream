using System;
using System.Collections.Generic;
using Source.Models.Balls;
using Source.Models.Factories;

namespace Source.Models.DataStructures
{
    public class BallsObjectPool 
    {
        private readonly IBallsFactory _ballsFactory;
        private readonly Dictionary<BallType, Queue<Ball>> _balls;

        public event Action<Ball> ReturnedToPool;

        public BallsObjectPool(IBallsFactory ballsFactory)
        {
            _ballsFactory = ballsFactory;
            _balls = new Dictionary<BallType, Queue<Ball>>();
        }

        public Ball Get(BallType type)
        {
            if (!_balls.ContainsKey(type)) 
                _balls.Add(type, new Queue<Ball>());
            if (_balls[type].Count == 0) 
                AddObjects(type, 1);

            var objectPoolItem = _balls[type].Dequeue();
            return objectPoolItem;
        }

        public void ReturnToPool(Ball ballToReturn)
        {
            _balls[ballToReturn.Type].Enqueue(ballToReturn);
            ReturnedToPool?.Invoke(ballToReturn);
        }

        private void AddObjects(BallType type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Ball ball = _ballsFactory.Create(type);

                _balls[type].Enqueue(ball);
            }
        }
    }
}