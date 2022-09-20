using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz
{
  public class CierreFacturasPeriodo
  {
    [DataObjectField(true)]
    public string Empresa { get; set; } //varchar(6) NOT NULLEmpresa de Trabajo
    [DataObjectField(true)]
    public string PeriodoCierre { get; set; } // varchar(6) NOT NULLPeriodo de Cierre
    public Fecha FechaInicial { get; set; }//date NULLFecha Inicial de Cierre
    public Fecha FechaFinal { get; set; } // date NULLFecha Final del Cierre
    public string Usuario { get; set; } // varchar(50) NULLUsuario que Realiza el Cierre
    public Fecha FechaCierre { get; set; } // date NULLFecha Real del Cierre
    public string HoraCierre { get; set; } // varchar(5) NULLHora real del Cierre
    public Moneda TotalVentas { get; set; } // decimal(18,2) NULLTotal Ventas del Cierre
    public string Contabilizado { get; set; } // enum('N','S') NULLPeriodo Contabilizado
    public string UsuarioContabiliza { get; set; } // varchar(50) NULLUsuario que Contabiliza
    public Fecha FechaContabiliza { get; set; } // date NULLFEcha de Contabilización
    public string HoraContabiliza { get; set; } // 

    [NoDataBase]
    public string NombrePeriodo
    {
      get
      {
        var nom = "(Selecione Periodo)";
        if (!PeriodoCierre.IsNullOrEmpty()) nom = PeriodoCierre;
        return nom;
      }
    }
  }//fin clase
}//fin namespace
