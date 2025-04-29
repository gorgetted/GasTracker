using GasTracker.Backend;
using GasTracker.Models;

namespace GasTracker.Views;

public partial class Fills : ContentPage {
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    private Location _location;

    public Fills() {
        InitializeComponent();
        btnRefreshLocation_Clicked(this, null);
        loadPicker();
    }

    public async Task GetCurrentLocation() {
        try {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
                _location = location;
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex) {
            // Unable to get location
        }
        finally {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest() {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    private async void btnRefreshLocation_Clicked(object sender, EventArgs e) {
        await GetCurrentLocation();
        GasStation gasStation = await GooglePlaces.GetNearestGasStationAsync(_location.Latitude, _location.Longitude);
        lblLocation.Text = gasStation.name + " on " + gasStation.address.ToString();
        //lblLocation.Text = _location.Latitude + "," + _location.Longitude;
    }

    private void btnAdd_Clicked(object sender, EventArgs e) {

    }

    private async void btnNewVechile_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(GasTracker.Views.NewVehicle));
    }

    private async void loadPicker() {
        List<Vehicle> loadedVehicles = await Backend.LocalStoarge.LoadVehiclesAsync();
        foreach( var vehicle in loadedVehicles) {
            pikVechile.Items.Add(vehicle.ToString());
        }
    }
}