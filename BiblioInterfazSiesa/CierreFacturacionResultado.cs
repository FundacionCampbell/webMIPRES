using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Resultado del Cierre de Facturación
  /// </summary>
  public class CierreFacturacionResultado
  {
    public Fecha FechaInicial { get; set; }
    public Fecha FechaFinal { get; set; }
    public string PeriodoCierre { get; set; }
    public Moneda TotalVenta { get; set; }

  }//fin clase
}//fin namespace
