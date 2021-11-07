namespace Source.Models
{
    public interface IUpdatableView : IModel
    {
        void Update(float deltaTime);
    }
}