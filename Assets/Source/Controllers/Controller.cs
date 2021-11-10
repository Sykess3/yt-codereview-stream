using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public abstract class Controller<TModel, TView>
        where TModel : IModel
        where TView : View<TModel>
    {
        protected readonly TView View;
        protected readonly TModel Model;

        protected Controller(TView view, TModel model)
        {
            View = view;
            Model = model;
        }

        public void Initialize()
        {
            View.Created += OnCreate;
            View.Destroyed += OnDestroy;

            View.OnCreate();
        }

        private void OnCreate()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView<TModel>>(
                out var fixedUpdatableModel, out var fixedUpdatableView))
                fixedUpdatableView.OnFixedUpdate += fixedUpdatableModel.FixedUpdate;
            if (UnityCallBackFunctionsContractIsCorrect<IUpdatable, UpdatableView<TModel>>(
                out var updatableModel, out var updatableView))
                updatableView.OnUpdate += updatableModel.Update;

            Subscribe();
        }

        private void OnDestroy()
        {
            if (UnityCallBackFunctionsContractIsCorrect<IFixedUpdatable, FixedUpdatableView<TModel>>(out var model,
                out var view))
                view.OnFixedUpdate -= model.FixedUpdate;
            if (UnityCallBackFunctionsContractIsCorrect<IUpdatable, UpdatableView<TModel>>(
                out var updatableModel, out var updatableView))
                updatableView.OnUpdate -= updatableModel.Update;

            UnSubscribe();
        }

        private bool UnityCallBackFunctionsContractIsCorrect<TModel_, TView_>(out TModel_ upcastedModel,
            out TView_ upcastedView)
        {
            if (Model is TModel_ model && View is TView_ view)
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