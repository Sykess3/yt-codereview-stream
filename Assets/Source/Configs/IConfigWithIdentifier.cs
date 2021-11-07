namespace Source.Configs
{
    public interface IConfigWithIdentifier<T>
    {
        public T Identifier { get; }
    }
}