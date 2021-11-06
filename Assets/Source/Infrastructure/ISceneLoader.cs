using System;

namespace Source.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action OnLoaded);
    }
}