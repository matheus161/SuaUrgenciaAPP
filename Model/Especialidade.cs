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

namespace SuaUrgenciaApp.Model
{
    public class Especialidade
    {

        public int IdEspecialidade { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
       

        public override string ToString()//Retornar a descrição depois
        {
            return Nome +""+ Descricao;
        }
        
    }
}