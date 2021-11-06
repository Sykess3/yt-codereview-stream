using Source.DataStructures;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.AssetManagement;
using Source.Infrastructure.Services.Factories;

namespace Source.Infrastructure.Root
{
    public class SingleServicesRegistration
    {
        private readonly ServiceLocator _services;
        private readonly ILevelLoader _levelLoader;

        public SingleServicesRegistration(ServiceLocator services, ILevelLoader levelLoader)
        {
            _services = services;
            _levelLoader = levelLoader;
        }

        public void Execute()
        {
            _services.RegisterSingle(_levelLoader);
            _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());

            _services.RegisterSingle<IConfigProvider>(new ConfigProvider());
            _services.RegisterSingle<IBallsFactory>(new BallsFactory(
                _services.Single<IConfigProvider>()));

            _services.RegisterSingle<IRandomBall>(new RandomBall(
                new BallsViewsObjectPool(_services.Single<IBallsFactory>())));
            
            _services.RegisterSingle<IGameObjectsFactory>(new GameObjectsFactory(
                _services));
        }
    }
}