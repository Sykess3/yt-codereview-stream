using System;
using UnityEngine;

namespace Source.Models.Balls
{
    public class Ball : IFixedUpdatable
    {
        private readonly IBallConfig _config;
        private readonly Falling _falling;
        private Vector3 _position;
        private bool _canFall;
        public int Cost => _config.Cost;

        public BallType Type => _config.Type;
        public int Damage => _config.Damage;

        private Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(_position);
            }
        }

        public event Action<Vector3> PositionChanged;

        public event Action<Ball> Popped;

        public event Action<Ball> FeltOutOfBounds;

        public event Action Initialized;


        public Ball(IBallConfig config, FallingAcceleration acceleration, Vector3 screenBottomBorder)
        {
            _config = config;
            _falling = new Falling(acceleration, _config.Velocity, screenBottomBorder);
            _falling.FeltOutOfBounds += FallOutOfBounds;
        }

        public void Initialize(Vector3 startPosition)
        {
            _canFall = true;
            Position = startPosition;
            Initialized?.Invoke();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_canFall) 
                Position = _falling.Execute(Position, fixedDeltaTime);
        }

        public void Pop()
        {
            _canFall = false;
            OnPop();
            Popped?.Invoke(this);
        }

        private void FallOutOfBounds()
        {
            _canFall = false;
            FeltOutOfBounds?.Invoke(this);
        }

        protected virtual void OnPop()
        {
        }
    }
}