using System;
using UnityEngine;

namespace Source.Models
{
    public abstract class Ball : IFixedUpdatable
    {
        private readonly IBallConfig _config;
        private Vector3 _velocity;
        private Vector3 _position;

        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(_position);
            }
        }

        public event Action<Vector3> PositionChanged;

        public Ball(IBallConfig config)
        {
            _config = config;
            _velocity = new Vector3(0, -_config.StartSpeed, 0);
        }


        public void FixedUpdate(float fixedDeltaTime) => Fall(fixedDeltaTime);

        private void Fall(float fixedDeltaTime)
        {
            Position += _velocity * fixedDeltaTime;
        }

        public void Pop()
        {
            OnPop();
        }

        protected virtual void OnPop()
        {
        }

        public void IncreaseSpeedBy(float number)
        {
            _velocity.y += number;
        }

        public void DecreaseSpeedBy(float number)
        {
            _velocity.y -= number;

            if (_velocity.y < 0)
                _velocity.y = 0;
        }
    }
}