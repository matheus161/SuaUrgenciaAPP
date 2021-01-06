using Android.App;
using System.Linq;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Gms.Maps;
using SuaUrgenciaApp.Model;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Gms.Maps.Model;
using Android.Content;
using Android.Util;
using Android.Locations;
using Android.Runtime;
using Android.Content.PM;

//using Cocosw.BottomSheetActions;

namespace SuaUrgenciaApp
{
    [Activity(Label = "Hospitais próximos", Theme = "@style/Theme.AppCompat", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback, ILocationListener
    {
        GoogleMap map;
        ProgressDialog progresso;
        List<HospitalMapa> listHospital;
        LatLng posicaoAtual;
        LocationManager locationManager;


        public void OnMapReady(GoogleMap googleMap)
        {
            
            map = googleMap;

            map.MarkerClick += Map_MarkerClick;
            map.MapType = GoogleMap.MapTypeHybrid;            

        }

        private void Map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            List<HospitalMapa> hospitaisMapa = listHospital;
            Marker usuarioclicado = e.Marker;//pega o usuario clicado no mapa

            HospitalMapa hospital = hospitaisMapa.FirstOrDefault(x => x.Latitude == usuarioclicado.Position.Latitude && x.Longitude == usuarioclicado.Position.Longitude);
          
            //Log.Debug("posicao", hospital.Nome);

            //if(usuarioclicado.Title != "voce")
            //{
            //    HttpClient client = new HttpClient();
            //    int idEspecialidade = Intent.GetIntExtra("idEspecialidade", 0);
            //    string endereco = "http://10.139.187.90/Hospital/GetHospitalByEspecialidade/" + idEspecialidade;
                
            //    string json = await client.GetStringAsync(endereco);
            //    HospitalEspecialidade hospitalEspecialidade = JsonConvert.DeserializeObject<HospitalEspecialidade>(json);

            //    Hospital hospitalSelecionado = JsonConvert.DeserializeObject<Hospital>(json);
            //    HospitalEspecialidade hosp = 




            //}




            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }
            ft.AddToBackStack(null);
            
            DialogFragment1 newFragment = DialogFragment1.NewInstance(null,hospital, posicaoAtual);
            newFragment.Show(ft, "dialog");


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            //lvEspecialidades = FindViewById<ListView>(Resource.Id.lvEspecialidade);


            //lvEspecialidades.ItemClick += LvEspecialidades_ItemClick;
            
            SupportMapFragment supportMapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            supportMapFragment.GetMapAsync(this);            

            GetListaEspecialidades();

            locationManager = (LocationManager)GetSystemService(LocationService);
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 24000, 100, this);

        }

        public async void GetListaEspecialidades()
        {
            progresso = new ProgressDialog(this);
            progresso.SetTitle("Buscando...");
            progresso.SetMessage("Aguarde a busca...");
            progresso.SetCancelable(false);
            progresso.Show();

            int idEspecialidade = Intent.GetIntExtra("idEspecialidade", 0);
            string descricaoEspecialidade = Intent.GetStringExtra("descricaoEspecialidade");

            SupportActionBar.Title = descricaoEspecialidade;


            string endereco = "http://10.139.187.82/Hospital/GetHospitalByEspecialidade/" + idEspecialidade;

            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(endereco);
            

            listHospital = JsonConvert.DeserializeObject<List<HospitalMapa>>(json);
            
            
            ConfigurarMarcadores();
            



            progresso.Dismiss();
        }

        public void ConfigurarMarcadores()
        {

            foreach (var item in listHospital)
            {
                LatLng posicao = new LatLng(item.Latitude, item.Longitude);
                //MarkerOptions marker = new MarkerOptions();
                Marker make = map.AddMarker(new MarkerOptions().SetPosition(posicao).SetTitle("Hospital: "+item.Nome)
                    .SetSnippet(item.Especialidade + ":" +" Hora início: "+ item.HoraInicio +" " +
                    "Hora fim: "+ item.HoraFim));
                

                //map.AddMarker(marker);
              //  make.ShowInfoWindow();               

            }



        }

        public void OnLocationChanged(Location location)
        {
            posicaoAtual = new LatLng(location.Latitude, location.Longitude);
            
            if(posicaoAtual!=null)
            {
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(posicaoAtual, 10);                
                map.AnimateCamera(camera);
            }          
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

        //public void OnClick(IDialogInterface dialog, int which)
        //{

        //}
    }
}

