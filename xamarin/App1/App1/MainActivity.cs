using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Provider;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        LocationManager locationManager;
        double lat, lon;
        private static string DATABASE_NAME = "KeomaPerformance.db";
        private static int DATABASE_VERSION = 1;
        private static string COORD_TABLE_NAME = "coordenadas";
        private static string COORD_COLUMN_LAT = "lat";
        private static string COORD_COLUMN_LON = "lon";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            locationManager = (LocationManager) GetSystemService(Context.LocationService);
        }

        private void showAlert()
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            dialog
                .SetTitle("Enable Location")
                .SetMessage("Seu GPS está desativado.\nFavor habilitá-lo para utilizar esse app.")
                .SetPositiveButton("Location Settings", (senderAlert, args) =>
                {
                    Intent myIntent = new Intent(Settings.ActionLocationSourceSettings);
                    StartActivity(myIntent);
                });
            dialog.show();
        }

    AlertDialog.Builder alert = new AlertDialog.Builder(this);
alert.SetTitle ("Confirm delete");
alert.SetMessage ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
alert.SetPositiveButton ("Delete", (senderAlert, args) => {
	Toast.MakeText(this ,"Deleted!" , ToastLength.Short).Show();
});

alert.SetNegativeButton ("Cancel", (senderAlert, args) => {
	Toast.MakeText(this ,"Cancelled!" , ToastLength.Short).Show();
});

Dialog dialog = alert.Create();
dialog.Show();
    }
}

