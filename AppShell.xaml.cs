using GasTracker.Views;

namespace GasTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewVehicle), typeof(NewVehicle));
        }
    }
}
