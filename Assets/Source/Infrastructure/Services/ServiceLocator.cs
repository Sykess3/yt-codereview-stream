namespace Source.Infrastructure.Services
{
    public class ServiceLocator
    {
        public void RegisterSingle<TService>(TService implementation) =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> 
        {
            public static TService ServiceInstance;
        }
    }
}