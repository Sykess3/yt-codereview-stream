using System;
using UnityEngine;

namespace Source.Views
{
    public class View : MonoBehaviour
    {
        public event Action Disabled;
        public event Action Enabled;

        // Костыль, проблема в том что Unity вызывает OnEnable сразу после создания
        // а подписка на OnEnable модели в контроллере происходит как раз после создания
        private void Start() => Enabled?.Invoke();

        private void OnDisable() => Disabled?.Invoke();

        private void OnEnable() => Enabled?.Invoke();
    }
}