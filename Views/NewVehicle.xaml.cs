using GasTracker.Backend;
using GasTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GasTracker.Views;

public partial class NewVehicle : ContentPage {
    public ICommand DeleteVehicleCommand { get; }
    public ObservableCollection<Vehicle> Vehicles { get; } = new ObservableCollection<Vehicle>();


    public NewVehicle() {
        InitializeComponent();
        BindingContext = this;
        DeleteVehicleCommand = new Command<Vehicle>(OnDeleteVehicle);

    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await loadTable();
        List<Vehicle> allVehicles = await LocalStoarge.LoadVehiclesAsync();
    }

    public async Task loadTable() {
        Vehicles.Clear();
        var loadedVehicles = await LocalStoarge.LoadVehiclesAsync();
        foreach (var vehicle in loadedVehicles) {
            Vehicles.Add(vehicle);
        }
    }

    private async void btnAdd_Clicked(object sender, EventArgs e) {
        Vehicle vehicle = new Vehicle(
            entMake.Text,
            entModel.Text,
            int.Parse(entYear.Text),
            int.Parse(entMileage.Text));

        try {
            await LocalStoarge.AppendVehicleAsync(vehicle);
        }
        catch (NonUniqueVechile nu) {
            await DisplayAlert("Vechile Already Exists", "", "OK");
            Debug.write(nu.ToString());
            return;
        }
        entMake.Text = "";
        entModel.Text = "";
        entYear.Text = "";
        entMileage.Text = "";

        await loadTable();
    }

    private async void OnDeleteVehicle(Vehicle vehicle) {
        if (vehicle == null)
            return;

        bool confirm = await DisplayAlert("Confirm?", $"Delete {vehicle.ToString()}?", "Yes", "No");

        if (confirm) {
            await LocalStoarge.DeleteVehicleAsync(vehicle);
            Vehicles.Remove(vehicle);
        }
    }

}