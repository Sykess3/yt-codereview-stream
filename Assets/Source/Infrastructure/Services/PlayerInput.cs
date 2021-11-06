using System;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private Camera _mainCamera;
        public event Action<GameObject> ClickedOnGameObject;

        public PlayerInput Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
            return this;
        }

        private void Update()
        {
            if (ClickedOnClickableGameObject(out var hit))
            {
                var clickedObject = hit.transform.parent.gameObject;
                
                ClickedOnGameObject?.Invoke(clickedObject);
            }
        }

        private bool ClickedOnClickableGameObject(out RaycastHit hit)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                hit = new RaycastHit();
                return false;
            }
            Ray inputRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var raycast = Physics.Raycast(inputRay, out hit, Constants.ClickableMask);
            
            return raycast;     
        }
    }
}