using UnityEngine;

namespace Source.Models.Randomizators
{
    public interface IRandomPositionGenerator
    {
        Vector3 Generate();
        void Initialize(int positionsDoNotRepeatAmount);
    }
}