using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorGeolocation.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _client;

        public GeocodingService(HttpClient client)
        {
            _client = client;
        }

        public async Task<GeocodingResults> FindAsync(string address)
        {
            var reqUrl = $@"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={YOU_API_KEY}";

            var response = await _client.GetAsync(reqUrl);

            return JsonConvert.DeserializeObject<GeocodingResults>(await response.Content.ReadAsStringAsync());
        }
    }


    public partial class GeocodingResults
    {
        [JsonProperty("results")]
        public GeocodingResult[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class GeocodingResult
    {
        [JsonProperty("address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public partial class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("bounds")]
        public GeometryBounds Bounds { get; set; }

        [JsonProperty("location")]
        public GeometryLocation Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public GeometryBounds Viewport { get; set; }

        public partial class GeometryBounds
        {
            [JsonProperty("northeast")]
            public GeometryLocation Northeast { get; set; }

            [JsonProperty("southwest")]
            public GeometryLocation Southwest { get; set; }
        }


        public partial class GeometryLocation
        {
            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lng")]
            public double Lng { get; set; }
        }
    }
}
