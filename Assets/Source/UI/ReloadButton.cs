using System;
using Source.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class ReloadButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private void Start() => 
            _button.onClick.AddListener(LevelLoader.Reload);
    }
}