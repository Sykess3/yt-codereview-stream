using System;
using UnityEditorInternal;
using UnityEngine;

namespace Source.Models
{
    public class PlayerHealth : IModel
    {
        public int CurrentHp { get; private set; }
        public readonly int MaxHP;

        public event Action DamageTaken;
        public event Action OnGameOver;
        public PlayerHealth(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHp = maxHP;
        }
        public void TakeDamage(int amount)
        {
            CurrentHp -= amount;
            Debug.Log(CurrentHp);
            if (CurrentHp <= 0) 
                OnGameOver?.Invoke();
            
            DamageTaken?.Invoke();
        }
    }
}