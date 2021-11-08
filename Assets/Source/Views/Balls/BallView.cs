using System;
using Source.Common;
using UnityEngine;

namespace Source.Views.Balls
{
    public class BallView : FixedUpdatableView, ITriggerView
    {
        [SerializeField] private OutOfCameraObserver _outOfCameraObserver;
        [SerializeField] private TriggerObserver _triggerObserver;
        public event Action<BallView> FeltOutOfBounds; 
        public event Action<BallView> Clicked;
        public event Action TriggerEntered;
        public event Action TriggerExited;
        

        protected void OnEnable()
        {
            _triggerObserver.TriggerEntered += OnTriggerObserverEntered;
            _triggerObserver.TriggerExited += OnTriggerObserverExited;
            _outOfCameraObserver.BecameInvisible += OnFeltOutOfBounds;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnTriggerObserverEntered;
            _triggerObserver.TriggerExited -= OnTriggerObserverExited;
            _outOfCameraObserver.BecameInvisible -= OnFeltOutOfBounds;
        }


        public void ChangePosition(Vector3 newPosition) => transform.position = newPosition;
        public void Click() => Clicked?.Invoke(this);

        private void OnTriggerObserverExited(Collider obj) => TriggerExited?.Invoke();

        private void OnTriggerObserverEntered(Collider obj) => TriggerEntered?.Invoke();
        private void OnFeltOutOfBounds() => FeltOutOfBounds?.Invoke(this);
    }
}