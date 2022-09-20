using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using LiloSoft.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  public class DataIndicadorGeneral
  {
    public string Empresa { get; set; }
    public string NoAdmision { get; set; }
    public string NoCuenta { get; set; }
    public Fecha FechaIngreso { get; set; }
    [NoDataBase]
    public string Periodo => FechaIngreso.ToPeriodo();
    [NoDataBase]
    public string NombrePeriodo => FechaIngreso.ToNombrePeriodo();
    [NoDataBase]
    public string DiaSemana => FechaIngreso.ValorInterno.ToString("dddd").ToUpper();
    [NoDataBase]
    public int NoDiaSemana => (int)FechaIngreso.ValorInterno.DayOfWeek;
    [NoDataBase]
    public string DiaMes => FechaIngreso.ValorInterno.Day.ToString().PadLeft(2, '0');
    [NoDataBase]
    public string HoraDia => $"{HoraIngreso.Substring(0,2)}:00";
    public string HoraIngreso { get; set; }
    public Fecha FechaEgreso { get; set; }
    public string HoraEgreso { get; set; }
    public string Origen { get; set; }
    public string NombreOrigen { get; set; }
    public string OrigenEnc { get; set; }
    public string NombreOrigenEnc { get; set; }
    public string OrigenEnvio { get; set; }
    public string NombreOrigenEnvio { get; set; }
    public string OrigenEncEnvio { get; set; }
    public string NombreOrigenEncEnvio { get; set; }

    [NoDataBase]
    public string OrigenReal => OrigenEnvio.IsNullOrEmpty() ? Origen : OrigenEnvio;
    [NoDataBase]
    public string NombreOrigenReal => OrigenEnvio.IsNullOrEmpty() ? NombreOrigen : NombreOrigenEnvio;
    [NoDataBase]
    public string OrigenEncReal => OrigenEnvio.IsNullOrEmpty() ? OrigenEnc : OrigenEncEnvio;
    [NoDataBase]
    public string NombreOrigenEncReal => OrigenEnvio.IsNullOrEmpty() ? NombreOrigenEnc : NombreOrigenEncEnvio;
    public string Remitido_Ciud { get; set; }
    public string NombreRemitidoCiud { get; set; }
    public string Remitido_De { get; set; }
    public Fecha FecHoraDate { get; set; }
    public string CodServicio { get; set; }
    public string NombreServicio { get; set; }
    public string NoHistoria { get; set; }
    public string Diag_ing { get; set; }
    public string DiagIngreso { get; set; }
    public string Desc_diag_ing { get; set; }
    public string Diag_Egre { get; set; }
    public string DiagEgreso { get; set; }
    public string ConsInterno { get; set; }
    public Entero NumPacientes { get; set; }
    public string NombrePlan { get; set; }
    public Moneda ValorFactura { get; set; }

  }//fin clase
}//fin namespace
