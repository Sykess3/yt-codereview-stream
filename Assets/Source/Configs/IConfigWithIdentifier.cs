namespace Source.Infrastructure.Services
{
    public interface IConfigWithIdentifier<T>
    {
        public T Identifier { get; }
    }
}