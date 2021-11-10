using System;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;

namespace Source.Controllers
{
    public class BallController : Controller<Ball, BallView>
    {
        private readonly PlayerHealth _playerHealth;
        
        public BallController(BallView view, Ball model, PlayerHealth playerHealth) : base(view, model)
        {
            _playerHealth = playerHealth;
        }

        protected override void Subscribe()
        {
            View.Clicked += OnClick;
            Model.FeltOutOfBounds += OnFeltOutOfBounds;
            Model.PositionChanged += View.ChangePosition;
            Model.Initialized += OnModelInitialize;
        }

        protected override void UnSubscribe()
        {
            View.Clicked -= OnClick;
            Model.PositionChanged -= View.ChangePosition;
            Model.FeltOutOfBounds -= OnFeltOutOfBounds;
            Model.Initialized -= OnModelInitialize;
        }

        private void OnModelInitialize()
        {
            View.GraphicModel.SetActive(true);
            Physics.SyncTransforms();
        }

        private void OnFeltOutOfBounds(Ball ball)
        {
            _playerHealth.TakeDamage(Model.Damage);
            View.GraphicModel.SetActive(false);
        }

        private void OnClick(BallView ballView)
        {
            Model.Pop();
            View.GraphicModel.SetActive(false);
        }
    }
}