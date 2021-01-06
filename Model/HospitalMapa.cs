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
    public class HospitalMapa
    {
        public string Nome { get; set; }

        public string Especialidade { get; set; }

        public String HoraInicio { get; set; }

        public String HoraFim { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Telefone { get; set; }

        public string Telefone2 { get; set; }

    }
}