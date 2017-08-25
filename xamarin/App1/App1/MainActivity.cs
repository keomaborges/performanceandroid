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

        public object R { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.botao);
            button.Click += delegate { toggleGPSUpdates(button); };
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

        public void OnLocationChanged(Location location)
        {
            var sw = Stopwatch.StartNew();
            long tStart = sw.ElapsedMilliseconds;

            lon = location.Longitude.ToString();
            lat = location.Latitude.ToString();

            //Toast.makeText(getApplicationContext(), "Localização Obtida. Iniciando inserção no banco de dados...", Toast.LENGTH_LONG).show();

            BancoController crud = new BancoController(Application.Context);
            string latString = lat;
            string lonString = lon;

            for (int x = 0; x < 1000; x++)
            {
                if (!crud.insereDado(latString, lonString))
                {
                    Toast.MakeText(this, "Erro ao inserir coordenadas do BD. Iteração: " + x, ToastLength.Long).Show();
                }
            }

            long tEnd = sw.ElapsedMilliseconds;
            long tDelta = tEnd - tStart;
            double tempoDecorrido = tDelta / 1000.0;

            Toast.MakeText(this, "Dados inseridos com sucesso!", ToastLength.Long).Show();

            TextView txtTempo = FindViewById<TextView>(Resource.Id.tempo);
            Button botao = FindViewById<Button>(Resource.Id.botao);
            txtTempo.Text = tempoDecorrido.ToString();
            botao.SetText(Resource.String.resume);
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        public void toggleGPSUpdates(Button button)
        {
            new AlertDialog.Builder(this)
                .SetTitle("Aviso")
                .SetMessage("Agora tentarei obter sua localização pelo GPS do celular. Isso pode demorar alguns segundos que não serão contabilizados no tempo de performance.")
                .SetCancelable(false)
                .SetPositiveButton("Entendido. Continue", delegate {})
                .Show();

            locationManager.RemoveUpdates(this);
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1, 1, this);
            button.SetText(Resource.String.pause);
        }
    };
}

