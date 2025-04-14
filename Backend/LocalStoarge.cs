using GasTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GasTracker.Backend {
    class LocalStoarge {
        private static readonly string vehicleFileName = Path.Combine("d:/", "vehicles.json");

        public static async Task appendVehicleAsync(Vehicle newVehicle) {
            JArray vehicleArray;

            // Load existing data or create a new JArray
            if (File.Exists(vehicleFileName)) {
                var existingJson = await File.ReadAllTextAsync(vehicleFileName);
                vehicleArray = JArray.Parse(string.IsNullOrWhiteSpace(existingJson) ? "[]" : existingJson);
            }
            else {
                vehicleArray = new JArray();
            }

            // Create JObject from vehicle and add it to the array
            JObject vehicleObject = JObject.FromObject(newVehicle);
            vehicleArray.Add(vehicleObject);

            // Write back to file
            await File.WriteAllTextAsync(vehicleFileName, vehicleArray.ToString(Formatting.Indented));
        }


        public static async Task<List<Vehicle>> loadVehiclesAsync() {
            if (File.Exists(vehicleFileName)) {
                var existingJson = await File.ReadAllTextAsync(vehicleFileName);
                if (!string.IsNullOrWhiteSpace(existingJson)) {
                    var vehicles = JArray.Parse(existingJson);
                    return vehicles.ToObject<List<Vehicle>>();
                }
            }
            return new List<Vehicle>();
        }
    }
}
