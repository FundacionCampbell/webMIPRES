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
    class historia_ordenes_medicas_detalle : BaseData
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
        [DataObjectField(true)]
        public decimal ConsecutivoDetalle { get; set; }
        public string CodMedicamento { get; set; }
        public string NombreMedicamento { get; set; }
        public decimal Dosis { get; set; }
        public string Frecuencia { get; set; }
        public string CodViaAplicacion { get; set; }
        public string DescripcionCuidado { get; set; }
        public string ObservacionCuidado { get; set; }
        public string CodDependencia { get; set; }
        public string CodServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Aplicacion { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public decimal Horas { get; set; }
        public decimal Dias { get; set; }
        public decimal CantidadAplicada { get; set; }
        public string TipoMedicamento { get; set; }
        public string EstadoAplicacion { get; set; }
        public string TipoTratamiento { get; set; }
        public string codCuidado { get; set; }
        public string CuidadoFacturable { get; set; }
        public string IntervaloHoras { get; set; }
        public string PrimeraAplicacion { get; set; }
        public Fecha FechaPrimeraAplicacion { get; set; }
        public string HoraPrimeraAplicacion { get; set; }
        public Fecha FechaSuspencion { get; set; }
        public string HoraSuspencion { get; set; }
        public string UsuarioSuspencion { get; set; }
        public string UsuarioGraba { get; set; }
        public Fecha FechaGraba { get; set; }
        public string HoraGraba { get; set; }
        public string POS { get; set; }
        public string SinEquivalencia { get; set; }
        public string EstadoDetalle { get; set; }
        public decimal CantidadEntregada { get; set; }
        public string UsuarioAutoriza { get; set; }
        public string DiasHora {get;set;}
        public Fecha FechaUltimaAplicacion { get; set; }
        public string HoraUltimaAplicacion { get; set; }
        public Int32 NumeroTotalAplicaciones { get; set; }
        public string Lateralidad { get; set; }

    }
}
