using Network.Extra;

namespace Network.Configuration
{
    public struct RestClientConfig
    {
        public string OriginUrl { get; }
        public Header[] GlobalHeaders { get; }

        public RestClientConfig(string originUrl, Header[] globalHeaders)
        {
            OriginUrl = originUrl;
            GlobalHeaders = globalHeaders;
        }
    }
}