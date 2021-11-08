namespace Source.Models.Balls
{
    public interface ITriggerable
    {
        void OnTriggerEnter();
        void OnTriggerExited();
    }
}