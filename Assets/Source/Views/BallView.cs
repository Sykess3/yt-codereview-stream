using System;
using Source.Infrastructure;
using Source.Models;
using UnityEngine;

namespace Source.Views
{
    public class BallView : FixedUpdatableView
    {
        public event Action<BallView> Clicked;
        public BallType Type { get; private set; }

        public BallView Construct(BallType type)
        {
            Type = type;
            return this;
        }

        public void Click()
        {
            Clicked?.Invoke(this);
        }

        public void ChangePosition(Vector3 newPosition) => transform.position = newPosition;
    }
}