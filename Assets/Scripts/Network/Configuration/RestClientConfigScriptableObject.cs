using Network.Extra;
using UnityEngine;

namespace Network.Configuration
{
    [CreateAssetMenu(fileName = "Rest Client Config", menuName = "Networking/Rest Client/Config/Rest Client Config")]
    public class RestClientConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private RestClientEnvironment environment;
        [SerializeField] private string originUrl;
        [SerializeField] private Header[] globalHeaders;

        public string OriginUrl => originUrl;

        public Header[] GlobalHeaders => globalHeaders;

        public bool Contains(RestClientEnvironment environment)
        {
            return this.environment == environment;
        }
    }
}