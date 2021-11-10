using System;
using UnityEngine;

namespace Source.Models.Balls
{
    public class Falling 
    {
        private readonly FallingAcceleration _fallingAcceleration;
        private readonly Vector3 _startVelocity;
        private readonly Vector3 _screenBottomBorder;
        public event Action FeltOutOfBounds;

        public Falling(FallingAcceleration fallingAcceleration, Vector3 startVelocity, Vector3 screenBottomBorder)
        {
            _fallingAcceleration = fallingAcceleration;
            _startVelocity = startVelocity;
            _screenBottomBorder = screenBottomBorder;
        }

        public Vector3 Execute(Vector3 currentPosition, float fixedDeltaTime)
        {
            currentPosition += (_startVelocity + _fallingAcceleration.Get()) * fixedDeltaTime;
            if (currentPosition.y < _screenBottomBorder.y) 
                FeltOutOfBounds?.Invoke();
            return currentPosition;
        }
    }
}