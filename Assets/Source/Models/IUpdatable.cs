namespace Source.Models
{
    public interface IUpdatable : IModel
    {
        void Update(float deltaTime);
    }
}