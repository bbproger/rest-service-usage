using Network.Configuration;
using UnityEngine;
using Zenject;

namespace Network.Di
{
    public class NetworkInstaller : MonoInstaller<NetworkInstaller>
    {
        [SerializeField] private string restClientConfigFactoryLocation;

        public override void InstallBindings()
        {
            Container
                .Bind<IRestClientConfigFactory>()
                .To<RestClientConfigFactory>()
                .FromScriptableObjectResource(restClientConfigFactoryLocation)
                .AsSingle()
                .Lazy();

            Container
                .Bind<IRestClient>()
                .To<RestClient>()
                .AsSingle()
                .Lazy();
        }
    }
}