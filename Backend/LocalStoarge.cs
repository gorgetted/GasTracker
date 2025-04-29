using GasTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GasTracker.Backend {
    class LocalStoarge {
        private static readonly string vehiclesFilePath = Path.Combine("d:/", "vehicles.json");
        private static readonly string fillFilePath = Path.Combine("d:/", "fills.json");

        public static void CreateFiles() {
            if (!File.Exists(vehiclesFilePath)) {
                File.Create(vehiclesFilePath).Close();
            }
        }

        #region VehicleFileManagement

        /// <summary>
        /// Adds a vehicle to the vehicles.json file.
        /// </summary>
        /// <param name="newVehicle">Vechile too be added</param>
        /// <returns></returns>
        /// <exception cref="NonUniqueVechile">Thrown if vechile is already in the file</exception>
        public static async Task AppendVehicleAsync(Vehicle newVehicle) {
            List<Vehicle> vehicles = await LoadVehiclesAsync();
            foreach (Vehicle v in vehicles) {
                if (v.Equals(newVehicle)) {
                    Debug.write("Vehicle already exists: " + newVehicle.ToString());
                    throw new NonUniqueVechile("Vehicle already exists: " + newVehicle.ToString());
                }
            }
            vehicles.Add(newVehicle);

            string updatedJson = JsonConvert.SerializeObject(vehicles, Formatting.Indented);
            await File.WriteAllTextAsync(vehiclesFilePath, updatedJson);
        }

        /// <summary>
        /// Loads the vehicles from the vehicles.json file.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Vehicle>> LoadVehiclesAsync() {
            try {
                var json = await File.ReadAllTextAsync(vehiclesFilePath);

                var list = JsonConvert.DeserializeObject<List<Vehicle>>(json) ?? new List<Vehicle>();

                return list;
            }
            catch (OperationCanceledException oe) {
                Debug.write(oe.ToString());
            }
            return new List<Vehicle>();
        }

        /// <summary>
        /// Gets the count of vehicles in the vehicles.json file. (Not Used Anymore)
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetVechileCountAsync() {
            List<Vehicle> vehicles = await LoadVehiclesAsync();
            return vehicles.Count;
        }

        /// <summary>
        /// Deletes a vehicle from the vehicles.json file.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static async Task DeleteVehicleAsync(Vehicle vehicle) {
            List<Vehicle> vehicles = await LoadVehiclesAsync();
            foreach (Vehicle v in vehicles) {
                if (v.Equals(vehicle)) {
                    vehicles.Remove(v);
                    break;
                }
            }
            string updatedJson = JsonConvert.SerializeObject(vehicles, Formatting.Indented);
            await File.WriteAllTextAsync(vehiclesFilePath, updatedJson);
        }

        #endregion

        #region FillFileManagement



        #endregion
    }

    public class NonUniqueVechile : Exception {
        public NonUniqueVechile(string message) : base(message) {
        }
    }
}