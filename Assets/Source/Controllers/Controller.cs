using System;
using Source.Models;
using Source.Views;
using Unity.Collections.LowLevel.Unsafe;

namespace Source.Controllers
{
    public abstract class Controller 
    {
        protected readonly View View;
        protected readonly IModel Model;

        protected Controller(View view, IModel model)
        {
            View = view;
            Model = model;
        }

        public void Initialize()
        {
            View.Enabled += OnEnable;
            View.Disabled += OnDisable;
        }

        private void OnEnable()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView>(out var model, out var view))
                view.OnFixedUpdate += model.FixedUpdate;

            Subscribe();
        }

        private bool UnityCallBackFunctionsContractIsCorrect< TModel, TView>(out TModel upcastedModel, out TView upcastedView)
        {
            if (Model is TModel model && View is TView view)
            {
                upcastedModel = model;
                upcastedView = view;
                return true;
            }
            
            upcastedModel = default;
            upcastedView = default;
            
            return false;
        }

        private void OnDisable()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView>(out var model, out var view))
                view.OnFixedUpdate -= model.FixedUpdate;
            
            UnSubscribe();
        }

        protected abstract void UnSubscribe();

        protected abstract void Subscribe();
    }
}