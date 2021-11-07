using System;
using UnityEngine;

namespace Source.Models.Balls
{
    public abstract class Ball : IFixedUpdatable
    {
        private readonly IBallConfig _config;
        private Vector3 _position;
        private bool _isPopped;
        private bool _isOutOfBounds;

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

        protected Ball(IBallConfig config)
        {
            _config = config;
        }

        public void Initialize(Vector3 startPosition)
        {
            _isPopped = false;
            _isOutOfBounds = false;
            Position = startPosition;
            Initialized?.Invoke();
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_isPopped || _isOutOfBounds)
                return;

            Fall(fixedDeltaTime);
        }

        public void Pop()
        {
            _isPopped = true;
            OnPop();
            Popped?.Invoke(this);
        }

        public void FallOutOfBounds()
        {
            _isOutOfBounds = true;
            FeltOutOfBounds?.Invoke(this);
        }

        protected virtual void OnPop()
        {
        }

        private void Fall(float fixedDeltaTime)
        {
            Position += _config.Velocity * fixedDeltaTime;
        }

        public void IncreaseSpeedBy(float number)
        {
        }

        public void DecreaseSpeedBy(float number)
        {
        }
    }
}