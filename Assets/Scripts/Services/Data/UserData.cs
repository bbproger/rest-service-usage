using Newtonsoft.Json;

namespace Data
{
    public class UserData
    {
        public class GeoLocationInfo
        {
            [JsonProperty("lat")] public string Latitude { get; set; }
            [JsonProperty("lng")] public string Longitude { get; set; }
        }

        public class AddressInfo
        {
            public string Street { get; set; }
            public string City { get; set; }
            public GeoLocationInfo Geo { get; set; }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AddressInfo Address { get; set; }
    }
}