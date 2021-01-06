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
    class Hospital
    {
        

        public int IdHospital { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public decimal Latidude { get; set; }
        public decimal Longitude { get; set; }
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }

    }
}