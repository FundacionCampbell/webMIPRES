using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
    [MultiEmpresa]
    class historia_ordenes_medicas : BaseData
    {
        [DataObjectField(true)]
        public string Empresa { get; set; }
        public string NoHistoria { get; set; }
        public decimal NoFolio { get; set; }
        [DataObjectField(true)]
        public decimal NoOrdenMedica { get; set; }
        [DataObjectField(true)]
        public decimal NoCuenta { get; set; }
        [DataObjectField(true)]
        public decimal NoAdmision { get; set; }
        public string CodServicio { get; set; }
        public string TipoOrdenMedica { get; set; }
        public string CodDependencia { get; set; }
        public Fecha FechaOrden { get; set; }
        public string HoraOrden { get; set; }
        public string CedulaMedico { get; set; }
        public string UsuarioGrabacion { get; set; }
        public string Estado { get; set; }
        public Fecha FechaRealGrabacion { get; set; }
        public Fecha HoraRealGrabacion { get; set; }
        public decimal NoOrdenServicio { get; set; }
        public string CodEsquema { get; set; }
        public string UsuarioAct { get; set; }
        public Fecha FechaAct { get; set; }
        public string HoraAct { get; set; }
        public string Realizado { get; set; }
        public string TipoMedicamento { get; set; }
        public string DuracionMezcla { get; set; }
        public Int32 TiempoMezcla { get; set; }
        public string Prioridad { get; set; }
        public string Recomendaciones { get; set; }
        public decimal InfusionMezcla { get; set; }
        public string EstadoAplicacion { get; set; }
        public string PrimeraDosisDifiere { get; set; }
        public decimal PrimeraDosis { get; set; }
        public string TextoMezcla { get; set; }
        public string CuidadoFacturable { get; set; }
        public Fecha FechaSuspencion { get; set; }
        public string HoraSuspencion { get; set; }
        public string UsuarioSuspencion { get; set; }
        public string NoRadicado { get; set; }
        public string OrdenAmbulatoria { get; set; }
        public string Justificacion { get; set; }
        public string OrdenAutomatica { get; set; }
        public string AdicionarEpicrisis { get; set; }
        public string MedicoHuella { get; set; }
        public string DosisRespuesta { get; set; }
        public string PorRazonNecesaria { get; set; }
        public decimal CantidadDiluyente { get; set; }
        public string UnidadDiluyente { get; set; }
        public string Interconsulta { get; set; }
        public string Interpretacion { get; set; }
        public string OrdenSalida { get; set; }
    }
}
