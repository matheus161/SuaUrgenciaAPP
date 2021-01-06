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
    class HospitalEspecialidade
    {
        public int IdHospital { get; set; }
        public int IdEspecialidade { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public bool Disponibilidade { get; set; }
        public int id { get; set; }

        public virtual Especialidade Especialidade { get; set; }
        public virtual Hospital Hospital { get; set; }
    }
}
