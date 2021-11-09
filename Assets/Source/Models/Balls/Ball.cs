using System;
using UnityEngine;

namespace Source.Models.Balls
{
    public abstract class Ball : IFixedUpdatable
    {
        private readonly IBallConfig _config;
        private readonly FallingAcceleration _fallingAcceleration;
        private Vector3 _position;

        public BallType Type => _config.Type;
        public event Action<Vector3> PositionChanged;
        public event Action<Ball> Popped;
        public event Action<Ball> FeltOutOfBounds;
        public event Action Initialized;

        private Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(_position);
            }
        }

        protected Ball(IBallConfig config, FallingAcceleration fallingAcceleration)
        {
            _config = config;
            _fallingAcceleration = fallingAcceleration;
        }

        public void Initialize(Vector3 startPosition)
        {
            Position = startPosition;
            Initialized?.Invoke();
        }
        
        public void FixedUpdate(float fixedDeltaTime) => Fall(fixedDeltaTime);

        public void Pop()
        {
            OnPop();
            Popped?.Invoke(this);
        }
        public void FallOutOfBounds() => FeltOutOfBounds?.Invoke(this);

        protected virtual void OnPop()
        {
        }

        private void Fall(float fixedDeltaTime)
        {
            Position += CurrentVelocity() * fixedDeltaTime;
        }

        private Vector3 CurrentVelocity() => _config.Velocity + _fallingAcceleration.Get();
    }
}