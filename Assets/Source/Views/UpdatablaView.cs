using System;
using UnityEngine;

namespace Source.Views
{
    public class UpdatablaView : View
    {
        public event Action<float> OnUpdate;
        private void Update() => OnUpdate?.Invoke(Time.deltaTime);
    }
}