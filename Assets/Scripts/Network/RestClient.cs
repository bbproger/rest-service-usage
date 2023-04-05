using System.Threading;
using Cysharp.Threading.Tasks;
using Network.Configuration;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Network
{
    public interface IRestClient
    {
        UniTask<byte[]> GetAsync(string endpoint, CancellationToken cancellationToken = default);

        UniTask<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
            where TResponse : class;

        UniTask<TResponse> PostAsync<TResponse>(string endpoint, object payload,
            CancellationToken cancellationToken = default) where TResponse : class;

        UniTask<byte[]> GetExplicitAsync(string url, CancellationToken cancellationToken = default);
        UniTask<Texture2D> GetExplicitTextureAsync(string url, CancellationToken cancellationToken = default);
    }

    public class RestClient : IRestClient
    {
        private RestClientConfig _restClientConfig;

        [Inject]
        private void Inject(IRestClientConfigFactory restClientConfigFactory)
        {
            _restClientConfig = restClientConfigFactory.Create();
        }

        public UniTask<byte[]> GetAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            Request request = new(_restClientConfig, UnityWebRequest.kHttpVerbGET, endpoint);
            return request.SendAsync(cancellationToken);
        }

        public UniTask<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            Request<TResponse> request = new(_restClientConfig, UnityWebRequest.kHttpVerbGET, endpoint);
            return request.SendAsync(cancellationToken);
        }

        public UniTask<TResponse> PostAsync<TResponse>(string endpoint, object payload,
            CancellationToken cancellationToken = default) where TResponse : class
        {
            Request<TResponse> request = new(_restClientConfig, UnityWebRequest.kHttpVerbPOST, endpoint);
            request.SetPayload(payload);
            return request.SendAsync(cancellationToken);
        }

        public UniTask<byte[]> GetExplicitAsync(string url, CancellationToken cancellationToken = default)
        {
            Request request = new Request(new RestClientConfig(url, null), UnityWebRequest.kHttpVerbGET, null);
            return request.SendAsync(cancellationToken);
        }
        
        public UniTask<Texture2D> GetExplicitTextureAsync(string url, CancellationToken cancellationToken = default)
        {
            RequestTexture request = new RequestTexture(new RestClientConfig(url, null), UnityWebRequest.kHttpVerbGET, null);
            return request.SendAsync(cancellationToken);
        }
    }
}