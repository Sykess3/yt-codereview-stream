using System;
using Source.Infrastructure.Services;
using Source.Models;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;

namespace Source
{
    public class BallsInputHandler : MonoBehaviour
    {
        private IPlayerInput _input;

        public BallsInputHandler Construct(IPlayerInput input)
        {
            _input = input;
            return this;
        }

        private void Start() => _input.ClickedOnGameObject += OnClick;

        private void OnDestroy() => _input.ClickedOnGameObject -= OnClick;

        private void OnClick(GameObject obj)
        {
            if (obj.TryGetComponent(out BallView ball)) 
                ball.Click();
        }
    }
}