using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Source.Infrastructure;
using Source.Infrastructure.Services.Factories;
using Source.Views;
using UnityEngine;

namespace Source.DataStructures
{
    public class BallsViewsObjectPool 
    {
        private readonly IBallsFactory _ballsFactory;
        private readonly Dictionary<BallType, Queue<BallView>> _views;

        public event Action<BallView> ReturnedToPool;

        public BallsViewsObjectPool(IBallsFactory ballsFactory)
        {
            _ballsFactory = ballsFactory;
            _views = new Dictionary<BallType, Queue<BallView>>();
        }

        public BallView Get(BallType type)
        {
            if (!_views.ContainsKey(type)) 
                _views.Add(type, new Queue<BallView>());
            if (_views[type].Count == 0) 
                AddObjects(type, 1);

            var objectPoolItem = _views[type].Dequeue();
            objectPoolItem.gameObject.SetActive(true);
            return objectPoolItem;
        }

        public void ReturnToPool(BallView viewToReturn)
        {
            viewToReturn.gameObject.SetActive(false);
            _views[viewToReturn.Type].Enqueue(viewToReturn);
            ReturnedToPool?.Invoke(viewToReturn);
        }

        private void AddObjects(BallType type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                BallView ballView = _ballsFactory.Create(type);

                ballView.gameObject.SetActive(false);
                _views[type].Enqueue(ballView);
            }
        }
    }
}