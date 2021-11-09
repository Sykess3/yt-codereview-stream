using System;
using UnityEngine;

namespace Source.Views
{
    public class FallingAccelerationView : UpdatableView
    {
        public event Action SpeedUpped;
        
        public void InformAboutSpeedUp()
        {
            SpeedUpped?.Invoke();
            Debug.Log("SPEED UPPED");
        }
    }
}