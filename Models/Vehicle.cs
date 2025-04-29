using System.Diagnostics;

namespace GasTracker.Models {
    public class Vehicle {
        /// <summary>
        /// Unique identifier for the vehicle made with Guid from prefences and vehicle data
        /// </summary>
        public string Id { get; set; } = "";
        public string Make { get; set; } = "Maker";
        public string Model { get; set; } = "Model";
        public int Year { get; set; } = 2000;
        public int Mileage { get; set; } = 0;

        /// <summary>
        /// Default constructor for Vehicle (Only for serialization)
        /// </summary>
        public Vehicle() {
            // Default constructor
            // only for serialization
        }

        /// <summary>
        /// Constructor for Vehicle
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="mileage"></param>
        public Vehicle(string make, string model, int year, int mileage) {
            this.Id = $"{MauiProgram.deviceId}-{make}-{model}-{year}";
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Mileage = mileage;

            Debug.write("Vehicle created: " + this.ToString());
        }

        /// <summary>
        /// Returns a string representation of the vehicle
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{Year} {Make} {Model}";
        }

        /// <summary>
        /// Checks if two vehicles are equal using the Id property
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(Vehicle other) {
            if(Id == other.Id) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if two vehicles are equal using the Id property
        /// </summary>
        /// <param name="vehicle1"></param>
        /// <param name="vehicle2"></param>
        /// <returns></returns>
        public static bool Equals(Vehicle vehicle1, Vehicle vehicle2) {
            if (vehicle1.Id == vehicle2.Id) {
                return true;
            }
            return false;
        }
    }
}
