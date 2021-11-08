using System;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;
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
            View.Created += OnCreated;
            View.Destroyed += OnDestroy;
            
            View.OnCreate();
        }

        private void OnCreated()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView>(
                out var fixedUpdatableModel, out var fixedUpdatableView))
                fixedUpdatableView.OnFixedUpdate += fixedUpdatableModel.FixedUpdate;
            if (UnityCallBackFunctionsContractIsCorrect<IUpdatable, UpdatableView>(
                out var updatableModel, out var updatableView))
                updatableView.OnUpdate += updatableModel.Update;
            if (UnityCallBackFunctionsContractIsCorrect<ITriggerable, ITriggerView>
                (out var triggerableModel, out var triggerView))
            {
                triggerView.TriggerEntered += triggerableModel.OnTriggerEnter;
                triggerView.TriggerExited += triggerableModel.OnTriggerExited;
            }

            Subscribe();
        }

        private void OnDestroy()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView>(out var model, out var view))
                view.OnFixedUpdate -= model.FixedUpdate;
            if (UnityCallBackFunctionsContractIsCorrect<IUpdatable, UpdatableView>(
                out var updatableModel, out var updatableView))
                updatableView.OnUpdate -= updatableModel.Update;
            if (UnityCallBackFunctionsContractIsCorrect<ITriggerable, ITriggerView>
                (out var triggerableModel, out var triggerView))
            {
                triggerView.TriggerEntered -= triggerableModel.OnTriggerEnter;
                triggerView.TriggerExited -= triggerableModel.OnTriggerExited;
            }
            
            UnSubscribe();
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

        protected abstract void UnSubscribe();

        protected abstract void Subscribe();
    }
}