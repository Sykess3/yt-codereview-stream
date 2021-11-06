namespace Source.Infrastructure.Services
{
    public interface IConfigProvider
    {
        TType Get<TType, TIdentifier>(TIdentifier identifier, string path) where TType : class, IConfigWithIdentifier<TIdentifier>;
        TType Get<TType>(string path) where TType : class;
    }
}