using System;

namespace Network.Excpetions
{
    public class InvalidUriFormatException : Exception
    {
        public string OriginUrl { get; }
        public string Endpoint { get; }

        public InvalidUriFormatException(string originUrl, string endpoint) : base(
            $"Unable to create uri using these parameters: OriginUrl: {originUrl}, Endpoint: {endpoint}")
        {
            OriginUrl = originUrl;
            Endpoint = endpoint;
        }
    }
}