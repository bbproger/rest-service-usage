using System;
using System.Linq;
using Network.Extra;
using UnityEngine;

namespace Network.Configuration
{
    public interface IRestClientConfigFactory
    {
        RestClientConfig Create();
    }

    [CreateAssetMenu(fileName = "Rest Client Config Factory",
        menuName = "Networking/Rest Client/Config/Rest Client Config Factory")]
    public class RestClientConfigFactory : ScriptableObject, IRestClientConfigFactory
    {
        [SerializeField] private RestClientConfigScriptableObject[] restClientConfigs;
        [SerializeField] private RestClientEnvironment currentEnvironment;

        public RestClientConfig Create()
        {
            RestClientConfigScriptableObject restClientConfig =
                restClientConfigs.SingleOrDefault(config => config.Contains(currentEnvironment));
            if (restClientConfig == null)
                throw new Exception($"Rest Client Config not found for environment: {currentEnvironment}");

            return new RestClientConfig(restClientConfig.OriginUrl, restClientConfig.GlobalHeaders);
        }
    }
}