using System;
using Source.Models.Balls;
using UnityEngine;

namespace Source.Views
{
    public class FallingAccelerationView : UpdatableView<FallingAcceleration>
    {
        public event Action SpeedUpped;
        
        public void InformAboutSpeedUp()
        {
            SpeedUpped?.Invoke();
            Debug.Log("SPEED UPPED");
        }
    }
}