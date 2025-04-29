using Newtonsoft.Json.Linq;

namespace GasTracker.Backend {

    internal class GooglePlaces {

        public static async Task<GasStation> GetNearestGasStationAsync(double latitude, double longitude) {
            var apiKey = MauiProgram.GOOGLE_API_KEY;
            var radius = 5000; // in meters (example: 1000m = 1km)
            var type = "gas_station";

            // Nearby Search endpoint
            var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?" +
                        $"location={latitude},{longitude}" +
                        $"&rankby=distance" +
                        $"&type={type}" +
                        $"&key={apiKey}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var json = JObject.Parse("{ \"root\": " + response + "}")["root"];  // quick hack to parse


            var firstResult = json["results"]?.FirstOrDefault();
            var placeName = firstResult?["name"]?.ToString();
            var placeAddress = firstResult?["vicinity"]?.ToString();

            GasStation gasStation = new GasStation() {
                name = placeName ?? "",
                address = splitAddress(placeAddress)
            };
#if DEBUG
            Debug.write(placeAddress);
#endif
            return gasStation;
        }

        public static Address splitAddress(string address) {
            Address addy = new Address();

            string houseNum = address.Substring(0, address.IndexOf(" "));
            address = address.Substring(address.IndexOf(" ") + 1);

            string street = address.Substring(0, address.IndexOf(","));
            address = address.Substring(address.IndexOf(",") + 1);

            string city = address;

            addy.street = street.Trim();
            addy.city = city.Trim();
            addy.houseNumber = houseNum.Trim();

            return addy;
        }
    }



    struct GasStation {
        public string name;
        public Address address;
    }

    struct Address {
        public string houseNumber;
        public string street;
        public string city;
        public string fullAddress;

        public Address() {
            houseNumber = "1234";
            street = "Street";
            city = "City";
            fullAddress = "1234 Street, City";
        }

        public override string ToString() {
            return $"{street} in {city}";
        }
    }
}
