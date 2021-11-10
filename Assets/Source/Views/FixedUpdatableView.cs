using System;
using Source.Models;
using UnityEngine;

namespace Source.Views
{
    public abstract class FixedUpdatableView<TModel> : View<TModel> where TModel : IModel
    {
        public event Action<float> OnFixedUpdate;
        private void FixedUpdate() => OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
}