using GasTracker.Backend;
using GasTracker.Models;

namespace GasTracker.Views;

public partial class NewVehicle : ContentPage {
    public NewVehicle() {
        InitializeComponent();
        loadTable();
    }

    public void loadTable() {
        // Load the vehicles from local storage
       List<Vehicle> list = new List<Vehicle>();
        Task<List<Vehicle>> loadVehiclesTask = LocalStoarge.loadVehiclesAsync();
        loadVehiclesTask.ContinueWith(task => {
            list = task.Result;
            // Update the UI on the main thread
            MainThread.BeginInvokeOnMainThread(() => {
                // Assuming you have a ListView or similar to display the vehicles
                cvVehicles.ItemsSource = list;
            });
        });
    }

    private void btnAdd_Clicked(object sender, EventArgs e) {
        Vehicle vehicle = new Vehicle() {
            Make = entMake.Text,
            Model = entModel.Text,
            Year = int.Parse(entYear.Text),
            Mileage = int.Parse(entMileage.Text)
        };
        Task saveVehicleTask = LocalStoarge.appendVehicleAsync(vehicle);
        Debug.write(LocalStoarge.loadVehiclesAsync().ToString());
        entMake.Text = "";
        entModel.Text = "";
        entYear.Text = "";
        entMileage.Text = "";
        loadTable();
    }
}