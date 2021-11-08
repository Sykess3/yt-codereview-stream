using System;

namespace Source.Views.Balls
{
    public interface ITriggerView
    {
        event Action TriggerEntered;
        event Action TriggerExited;
    }
}