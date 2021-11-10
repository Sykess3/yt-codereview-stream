using System;
using Source.Models;
using Source.Models.Balls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Views
{
    public class PlayerHealthView : View<PlayerHealth>
    {
        [SerializeField] private Image _image;

        protected override void OnStart() => UpdateHPBar();

        public void UpdateHPBar()
        {
            float currentHpCoefficient = Model.CurrentHp / (float)Model.MaxHP;
            _image.fillAmount = currentHpCoefficient;
        }
    }
}