namespace GasTracker.Models {
    class Vehicle {
        public static int VehicleCount { get; set; } = 0;
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }

        public Vehicle() {
            Id = VehicleCount + 1;
            Make = "Honda";
            Model = "Civic";
            Year = 2005;
            Mileage = 1000;
            VehicleCount++;
        }

        public override string ToString() {
            return $"{Year} {Make} {Model}";
        }
    }
}
