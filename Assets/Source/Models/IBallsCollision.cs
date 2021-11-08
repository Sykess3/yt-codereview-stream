using System;
using System.Collections.Generic;
using Source.Models.Balls;

namespace Source.Models
{
    public interface IBallsCollision : IModel
    {
        public void Chek(IEnumerable<Ball> balls, Action<Ball> onCollisionDetected);
    }
}