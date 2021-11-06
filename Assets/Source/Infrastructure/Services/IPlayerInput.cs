using System;
using UnityEngine;

namespace Source.Infrastructure.Services
{
    public interface IPlayerInput
    {
        event Action<GameObject> ClickedOnGameObject;
    }
}