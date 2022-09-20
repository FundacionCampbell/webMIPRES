using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webMIPRES.Models
{
    public class EntregaAmbitoModel
    {
        public string NoPrescripcion { get; set; }
        public string TipoTec { get; set; }
        public Int32 ConTec { get; set; }
        public string TipoIDPaciente { get; set; }
        public string NoIDPaciente { get; set; }
        public Int32 NoEntrega { get; set; }
        public string CodSerTecEntregado { get; set; }
        public string CantTotEntregada { get; set; }
        public Int32 EntTotal { get; set; }
        public Int32 CausaNoEntrega { get; set; }
        public string FecEntrega { get; set; }
        public string NoLote { get; set; }
    }
}