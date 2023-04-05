using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Network.Extra
{
    public interface IConvertor
    {
        byte[] SerializeBinary(object value);
        string Serialize(object value);
        T Deserialize<T>(string json);
        T Deserialize<T>(byte[] data);
    }

    public class Convertor : IConvertor
    {
        private readonly Encoding _encoding;
        private readonly JsonSerializerSettings _settings;

        public Convertor(Encoding encoding)
        {
            _encoding = encoding;
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            _settings.Converters.Add(new StringEnumConverter());
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        public byte[] SerializeBinary(object value)
        {
            string json = Serialize(value);
            return _encoding.GetBytes(json);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public T Deserialize<T>(byte[] data)
        {
            string json = _encoding.GetString(data);
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}