using System;
using System.Collections.Generic;
using Source.Models.Balls;
using Source.Views;

namespace Source.Models
{
    public class BallsCollision : IFixedUpdatable, IBallsCollision
    {
        private readonly List<Ball> _cachedBalls;
        private Action<Ball> _setRandomPositionTo;

        public BallsCollision()
        {
            _cachedBalls = new List<Ball>();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (Ball ball in _cachedBalls)
                if (ball.IsInsideOtherBall)
                    _setRandomPositionTo(ball);
        }

        public void Chek(IEnumerable<Ball> balls, Action<Ball> onCollisionDetected)
        {
            _setRandomPositionTo = onCollisionDetected;

            AddBallsToArrayIfTheyAreNotAlreadyIn(balls);
        }

        private void AddBallsToArrayIfTheyAreNotAlreadyIn(IEnumerable<Ball> balls)
        {
            foreach (var ball in balls)
                if (!_cachedBalls.Contains(ball))
                    _cachedBalls.Add(ball);
        }
    }
}