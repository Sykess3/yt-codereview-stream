using System;
using UnityEngine;

namespace Source.Common
{
    public class OutOfCameraObserver : MonoBehaviour
    {
        public event Action BecameInvisible;
        public event Action BecameVisible;
        private void OnBecameInvisible() => BecameInvisible?.Invoke();

        private void OnBecameVisible() => BecameVisible?.Invoke();
    }
}