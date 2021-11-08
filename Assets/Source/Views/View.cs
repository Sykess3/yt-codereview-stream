using System;
using UnityEngine;

namespace Source.Views
{
    public abstract class View : MonoBehaviour
    {
        public event Action Destroyed;
        public event Action Created;
        
        public void OnCreate() => Created?.Invoke();

        private void OnDestroy() => Destroyed?.Invoke();
    }
}