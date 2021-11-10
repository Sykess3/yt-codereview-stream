using System;
using Source.Models;
using UnityEngine;

namespace Source.Views
{
    public abstract class UpdatableView<TModel> : View<TModel> where TModel : IModel
    {
        public event Action<float> OnUpdate;
        private void Update() => OnUpdate?.Invoke(Time.deltaTime);
    }
}