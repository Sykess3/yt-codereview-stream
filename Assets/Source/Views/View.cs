using System;
using Source.Models;
using UnityEngine;

namespace Source.Views
{
    public abstract class View<TModel> : MonoBehaviour where TModel : IModel
    {
        public TModel Model { get; private set; }

        public event Action Destroyed;
        public event Action Created;

        public void Construct(TModel model)
        {
            Model = model;
        }

        private void Start()
        {
            if (Model == null)
                throw new NullReferenceException($"You must construct view of model {typeof(TModel).FullName}");
            
            OnStart();
        }

        protected virtual void OnStart() { }

        public void OnCreate() => Created?.Invoke();

        private void OnDestroy() => Destroyed?.Invoke();
        
        
    }
}