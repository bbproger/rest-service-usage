using System;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Network.Configuration;
using Network.Excpetions;
using Network.Extra;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class Request
    {
        private readonly RestClientConfig _clientConfig;
        private readonly string _method;
        private readonly string _endpoint;

        private Header[] _headers;

        public Request(RestClientConfig clientConfig, string method, string endpoint)
        {
            _clientConfig = clientConfig;
            _method = method;
            _endpoint = endpoint;
        }

        public void SetAdditionalHeaders(params Header[] headers)
        {
            _headers = headers;
        }

        protected virtual UnityWebRequest BuildRequest()
        {
            Uri requestUri = BuildRequestUri();
            UnityWebRequest request = new(requestUri, _method);
            foreach (Header header in _clientConfig.GlobalHeaders?.Concat(_headers ?? Enumerable.Empty<Header>()) ??
                                      (_headers ?? Enumerable.Empty<Header>()))
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        }

        public async UniTask<byte[]> SendAsync(CancellationToken cancellationToken = default)
        {
            UnityWebRequest request = null;
            try
            {
                request = BuildRequest();
                await request.SendWebRequest().WithCancellation(cancellationToken);
                return request.downloadHandler.data;
            }
            finally
            {
                request?.Dispose();
            }
        }

        private Uri BuildRequestUri()
        {
            if (string.IsNullOrEmpty(_endpoint))
            {
                return new Uri(_clientConfig.OriginUrl);
            }

            string url = _clientConfig.OriginUrl + _endpoint;
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                throw new InvalidUriFormatException(_clientConfig.OriginUrl, _endpoint);

            return uri;
        }
    }

    public class Request<TResponse> : Request where TResponse : class
    {
        private object _payload;
        protected IConvertor Convertor { get; private set; }

        public Request(RestClientConfig clientConfig, string method, string endpoint) : base(clientConfig, method,
            endpoint)
        {
            Convertor = new Convertor(new UTF8Encoding());
        }

        public void SetPayload(object payload)
        {
            _payload = payload;
        }

        protected override UnityWebRequest BuildRequest()
        {
            UnityWebRequest request = base.BuildRequest();
            if (_payload == null) return request;

            byte[] data = Convertor.SerializeBinary(_payload);
            request.uploadHandler = new UploadHandlerRaw(data);
            return request;
        }

        public new async UniTask<TResponse> SendAsync(CancellationToken cancellationToken = default)
        {
            byte[] data = await base.SendAsync(cancellationToken);
            if (data == null) throw new Exception("Invalid response");

            TResponse response = Convertor.Deserialize<TResponse>(data);
            return response;
        }
    }
    public class RequestTexture : Request
    {
        public RequestTexture(RestClientConfig clientConfig, string method, string endpoint) : base(clientConfig,
            method,
            endpoint)
        {
        }

        protected override UnityWebRequest BuildRequest()
        {
            UnityWebRequest request = base.BuildRequest();
            request.downloadHandler = new DownloadHandlerTexture(true);
            return request;
        }

        public new async UniTask<Texture2D> SendAsync(CancellationToken cancellationToken = default)
        {
            byte[] data = await base.SendAsync(cancellationToken);
            Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture2D.LoadImage(data, false);
            return texture2D;
        }
    }
}