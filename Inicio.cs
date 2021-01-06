using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SuaUrgenciaApp
{
    [Activity(Label = "SuaUrgenciaApp", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class Inicio : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(Inicio).Name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Log.Debug(TAG, "Inicio.OnCreate");

        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); 
            Log.Debug(TAG, "Startup work is finished - starting ListaEspecialidade.");
            StartActivity(new Intent(Application.Context, typeof(ListaEspecialidade)));
        }

    }
}