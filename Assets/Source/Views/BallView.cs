using System;
using Source.Common;
using Source.Infrastructure;
using Source.Models;
using UnityEngine;

namespace Source.Views
{
    public class BallView : FixedUpdatableView
    {
        [SerializeField] private OutOfCameraObserver _outOfCameraObserver;
        public event Action<BallView> FeltOutOfBounds; 
        public event Action<BallView> Clicked;
        

        protected void OnEnable()
        {
            _outOfCameraObserver.BecameInvisible += OnFeltOutOfBounds;
        }

        private void OnDisable()
        {
            _outOfCameraObserver.BecameInvisible -= OnFeltOutOfBounds;
        }

        public void ChangePosition(Vector3 newPosition) => transform.position = newPosition;
        public void Click() => Clicked?.Invoke(this);

        private void OnFeltOutOfBounds() => FeltOutOfBounds?.Invoke(this);
    }
}