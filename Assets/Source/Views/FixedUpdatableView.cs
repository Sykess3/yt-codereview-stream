using System;
using UnityEngine;

namespace Source.Views
{
    public class FixedUpdatableView : View
    {
        public event Action<float> OnFixedUpdate;
        private void FixedUpdate() => OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
}