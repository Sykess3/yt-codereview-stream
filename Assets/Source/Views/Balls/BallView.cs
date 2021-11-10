using System;
using Source.Common;
using Source.Models.Balls;
using UnityEngine;

namespace Source.Views.Balls
{
    public class BallView : FixedUpdatableView<Ball>
    {
        [SerializeField] private MeshRenderer _graphicModel;
        public event Action<BallView> Clicked;
        public GameObject GraphicModel => _graphicModel.gameObject;

        public void ChangePosition(Vector3 newPosition) => transform.position = newPosition;
        public void Click() => Clicked?.Invoke(this);
    }
}