﻿using Source.Models.DataStructures;
using UnityEngine;

namespace Source.Models.Randomizators
{
    public class RandomPositionGenerator : IRandomPositionGenerator
    {
        private const float PositionDifference = 1.2f;
        private readonly CircularArray<Vector3> _positionsDoNotRepeat;
        private readonly (float, float) _xBorders;
        private readonly float _depth;
        private readonly float _height;
        public RandomPositionGenerator
            ((float, float) xBorders,
            float depth,
            float height,  
            int positionsCountDoNotRepeat)
        {
            _xBorders = xBorders;
            _depth = depth;
            _height = height;
            _positionsDoNotRepeat = new CircularArray<Vector3>(positionsCountDoNotRepeat);
        }
        
        public Vector3 Generate()
        {
            var position = RandomPositionWithinWidthAndHeight();
            
            while (IsInPositionDoNotRepeat(position)) 
                position = RandomPositionWithinWidthAndHeight();

            _positionsDoNotRepeat.Add(position);
            return position;
        }

        private bool IsInPositionDoNotRepeat(Vector3 position)
        {
            foreach (var cachedPosition in _positionsDoNotRepeat)
            {
                if (ApproximatelyEquals(position, cachedPosition))
                    return true;
            }

            return false;
        }

        private bool ApproximatelyEquals(Vector3 position, Vector3 cachedPosition) =>
            cachedPosition.x + PositionDifference > position.x &&
            cachedPosition.x - PositionDifference < position.x;

        private Vector3 RandomPositionWithinWidthAndHeight()
        {
            var xRandomPosition = Random.Range(_xBorders.Item1 + 0.5f, _xBorders.Item2 - 0.5f);
            var yRandomPosition = Random.Range(MinHeight(), MaxHeight());
            return new Vector3(xRandomPosition, yRandomPosition, _depth);
        }

        private float MaxHeight() => _height + 2f;

        private float MinHeight() => _height + 0.5f;
    }
}