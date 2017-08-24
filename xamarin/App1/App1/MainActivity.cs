using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Provider;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager locationManager;
        private string lat, lon;
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
            locationManager = (LocationManager)GetSystemService(Context.LocationService);
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
            dialog.Show();
        }

        private bool isLocationEnabled()
        {
            return locationManager.IsProviderEnabled(LocationManager.GpsProvider) || locationManager.IsProviderEnabled(LocationManager.NetworkProvider);
        }

        public void onLocationChanged(Location location)
        {
            var sw = Stopwatch.StartNew();
            long tStart = sw.ElapsedMilliseconds;

            lon = location.Longitude.ToString();
            lat = location.Latitude.ToString();

            //Toast.makeText(getApplicationContext(), "Localização Obtida. Iniciando inserção no banco de dados...", Toast.LENGTH_LONG).show();

            BancoController crud = new BancoController(getBaseContext());
            string latString = string.valueOf(lat);
            string lonString = string.valueOf(lon);

            for (int x = 0; x < 1000; x++)
            {
                if (!crud.insereDado(latString, lonString))
                {
                    Toast.makeText(getApplicationContext(), "Erro ao inserir coordenadas do BD. Iteração: " + x, Toast.LENGTH_LONG).show();
                }
            }

            long tEnd = sw.ElapsedMilliseconds;
            long tDelta = tEnd - tStart;
            double tempoDecorrido = tDelta / 1000.0;

            Toast.makeText(getApplicationContext(), "Dados inseridos com sucesso!", Toast.LENGTH_LONG).show();

            TextView txtTempo = (TextView)findViewById(R.id.tempo);
            Button botao = (Button)findViewById(R.id.botao);
            txtTempo.setText(String.valueOf(tempoDecorrido));
            botao.setText(R.string.resume);
        }

        @Override
    public void onStatusChanged(String s, int i, Bundle bundle)
        {

        }

        @Override
    public void onProviderEnabled(String s)
        {

        }

        @Override
    public void onProviderDisabled(String s)
        {

        }
    };
}

