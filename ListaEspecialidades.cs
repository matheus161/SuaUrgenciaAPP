using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SuaUrgenciaApp.Adapters;
using SuaUrgenciaApp.Model;
using static Android.Content.ClipData;

namespace SuaUrgenciaApp
{

    [Activity(Label = "SuaUrgenciaApp", Theme = "@style/Theme.AppCompat", Icon = "@drawable/Icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListaEspecialidade : Activity
    {
        SearchView sv1;
        ListView lvEspecialidades;
        List<Especialidade> listResultado;
        ProgressDialog progresso;
        ListaEspecialidadesAdapter adapter;
        ListaEspecialidadesAdapter adpt1;              

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lista);

            lvEspecialidades = FindViewById<ListView>(Resource.Id.lvEspecialidade);
            sv1 = FindViewById<SearchView>(Resource.Id.sv);
            GetListaEspecialidades();
            sv1.QueryTextChange += Sv1_QueryTextChange;
            sv1.ClearFocus();
            
            lvEspecialidades.ItemClick += LvEspecialidades_ItemClick;
             
        }

        private void Sv1_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {

            string textoPesquisado = sv1.Query;

            List<Especialidade> listaFiltrada = new List<Especialidade>();

            listaFiltrada = listResultado.Where(x => x.Nome.ToLower().StartsWith(textoPesquisado.ToLower())).ToList();//trazer a listview atualizada pela pesquisa, iniciada com a inicial da search view
                           
            adpt1 = new ListaEspecialidadesAdapter(this, listaFiltrada);
            lvEspecialidades.Adapter = adpt1;

            

           // adpt1.Filter.InvokeFilter(e.NewText);


            //  Intent intent = new Intent(this, typeof(MainActivity));
            //  intent.PutExtra("idEspecialidade", especialidade.IdEspecialidade);

            //  StartActivity(intent);

        }

        //private void Sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        //{

        //    listResultado = listResultado.Where(x => x.Nome.StartsWith(e.NewText)).ToList();
        //    adapter = new ListaEspecialidadesAdapter(this, listResultado);
        //    lvEspecialidades.Adapter = adapter;
        //    adapter.NotifyDataSetChanged();
        //}

        private void LvEspecialidades_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Especialidade especialidade = listResultado[e.Position];

            //Toast.MakeText(this, especialidade.Nome, ToastLength.Short).Show();

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("idEspecialidade", especialidade.IdEspecialidade);
            intent.PutExtra("descricaoEspecialidade", especialidade.Nome);


            StartActivity(intent);
            

        }
        

        public async void GetListaEspecialidades()
        {
            progresso = new ProgressDialog(this);
            progresso.SetTitle("Buscando...");
            progresso.SetMessage("Aguarde a busca...");
            progresso.SetCancelable(false);
            progresso.Show();


            string endereco = "http://10.139.187.82/Hospital/GetEspecialidades";

            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(endereco);
            listResultado = JsonConvert.DeserializeObject<List<Especialidade>>(json);

            adapter = new ListaEspecialidadesAdapter(this, listResultado);                                   
            
            lvEspecialidades.Adapter = adapter;
            progresso.Dismiss();
          
        }
    }
}