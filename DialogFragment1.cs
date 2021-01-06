using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SuaUrgenciaApp.Model;
using Android.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Gms.Maps.Model;
using Android.Util;

namespace SuaUrgenciaApp
{
    public class DialogFragment1 : DialogFragment
    {
        public static HospitalMapa hp;
        TextView txtDistancia;
        TextView txtTempo;
        public static LatLng posicaoAtual;

        public static DialogFragment1 NewInstance(Bundle bundle, HospitalMapa hospital, LatLng posicao)
        {
            DialogFragment1 fragment = new DialogFragment1();
            {
                fragment.Arguments = bundle;              

            }

            hp = hospital;
            posicaoAtual = posicao;



            return fragment;

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            View view = inflater.Inflate(Resource.Layout.Detalhes, container, false);
            TextView nomeHospital = view.FindViewById<TextView>(Resource.Id.textNomeHospital);
            TextView horaInicio = view.FindViewById<TextView>(Resource.Id.showinicio);
            TextView horaFim = view.FindViewById<TextView>(Resource.Id.showfim);
            ImageView telefone = view.FindViewById<ImageView>(Resource.Id.telefone);
            ImageView btnChegar = view.FindViewById<ImageView>(Resource.Id.btnMarcar);
            txtDistancia = view.FindViewById<TextView>(Resource.Id.txtdistancia);
            txtTempo = view.FindViewById<TextView>(Resource.Id.showtempo);
            

            nomeHospital.Text = hp.Nome;
            horaInicio.Text = hp.HoraInicio;
            horaFim.Text = hp.HoraFim;
            //telefone.Click += Telefone_Click;

            Button btnFechar = view.FindViewById<Button>(Resource.Id.btnFechar);
            btnFechar.Click += delegate
            
            {
                Dismiss();
            };

            btnChegar.Click += delegate
            {
               // Android.Net.Uri gmmIntentUri = Android.Net.Uri.Parse("google.navigation:q=Taronga+Zoo,+Sydney+Australia");
                Android.Net.Uri gmmIntentUri = Android.Net.Uri.Parse($"google.navigation:q={hp.Latitude.ToString().Replace(",",".")},{hp.Longitude.ToString().Replace(",", ".")}");


                Intent mapIntent = new Intent(Intent.ActionView, gmmIntentUri);    
                mapIntent.SetPackage("com.google.android.apps.maps");
                StartActivity(mapIntent);
            };

            //telefone.Click += (s, e) =>
            //{
            //    var numero = Android.Net.Uri.Parse("tel" + hp.Telefone);
            //    Intent intentLigacao = new Intent(Intent.ActionCall, numero);
            //    StartActivity(intentLigacao);
            //};

            telefone.Click += delegate
            {
                var numero = Android.Net.Uri.Parse("tel:" + hp.Telefone);
                Intent intentLigacao = new Intent(Intent.ActionCall, numero);
                Activity.StartActivity(intentLigacao);

            };


            // void Telefone_Click(object sender, EventArgs e)
            //{
            //    var intent = new Intent(Intent.ActionCall);
            //    intent.SetData(Uri.Parse("tel:" + hp.Telefone));
            //    StartActivity(intent);
            //}
            CalcularTempoDistancia();


            return view;
        }

        public async void CalcularTempoDistancia()
        {
            string latitudeOrigem = posicaoAtual.Latitude.ToString().Replace(",", ".");
            string longitudeOrigem = posicaoAtual.Longitude.ToString().Replace(",", "."); ;
            string latitudeDestino = hp.Latitude.ToString().Replace(",", ".");
            string longitudeDestino = hp.Longitude.ToString().Replace(",", ".");
            string chaveApi = "AIzaSyDkeh_hMufoElsXDD2l4cdgiUMHLrYymcM";

            string endereco = $"https://maps.googleapis.com/maps/api/directions/json?origin={latitudeOrigem},{longitudeOrigem}&destination={latitudeDestino},{longitudeDestino}&SENSOR=FALSE&key={chaveApi}";

            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(endereco);
            
            RootObject objetoCompleto = JsonConvert.DeserializeObject<RootObject>(json);

            double distanciaRetornada = objetoCompleto.routes.Sum(r => r.legs.Sum(l => l.distance.value));
            double duracaoRetornada = objetoCompleto.routes.Sum(r => r.legs.Sum(l => l.duration.value));

            Log.Debug("distancia", distanciaRetornada.ToString("n1"));
            Log.Debug("tempo", duracaoRetornada.ToString());

            string distanciaFormatada = "";
            string tempoFormatado = "";
            
            if(distanciaRetornada>=1000)
            {
                distanciaFormatada = (distanciaRetornada / 1000) + " km";
            } else
            {
                distanciaFormatada = distanciaRetornada + " m";
            }

            int horas = (int)duracaoRetornada / 3600;
            int resto = (int)duracaoRetornada % 3600;
            int minutos = resto / 60;

            if (horas <= 0)
                tempoFormatado = minutos + "min";
            else
                tempoFormatado = $"{horas}h {minutos}min";


            txtDistancia.Text = distanciaFormatada;
            txtTempo.Text = tempoFormatado;

        }

       
        }
}