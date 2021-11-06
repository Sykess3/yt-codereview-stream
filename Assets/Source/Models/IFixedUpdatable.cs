namespace Source.Models
{
    public interface IFixedUpdatable : IModel
    {
        void FixedUpdate(float fixedDeltaTime);
    }
}