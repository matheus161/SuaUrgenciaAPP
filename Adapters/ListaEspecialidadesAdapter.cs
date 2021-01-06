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
using Java.Lang;
using SuaUrgenciaApp.Model;

namespace SuaUrgenciaApp.Adapters
{
    public class ListaEspecialidadesAdapter : BaseAdapter
    {
        List<Especialidade> listaEspecialidades;
        Activity activity;

        public ListaEspecialidadesAdapter(Activity activity, List<Especialidade> listaEspecialidades)
        {
            this.activity = activity;
            this.listaEspecialidades = listaEspecialidades;
        }

        public override int Count => listaEspecialidades.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listaEspecialidades[position].IdEspecialidade;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if(convertView==null)
            {
                view = activity.LayoutInflater.Inflate(Resource.Layout.Item_Especialidade, parent, false);
            }

            TextView tvEspecialidade = view.FindViewById<TextView>(Resource.Id.tvEspecialidade);
            TextView tvDescricao = view.FindViewById<TextView>(Resource.Id.tvDescricao);

            tvEspecialidade.Text = listaEspecialidades[position].Nome;
            tvDescricao.Text = listaEspecialidades[position].Descricao;

            return view;

        }
    }
}