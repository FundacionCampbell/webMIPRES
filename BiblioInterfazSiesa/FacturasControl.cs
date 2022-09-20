using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Representa las Facturas de un Control de Cargue
  /// </summary>
  public class FacturasControl
  {
    public string NoFactura { get; set; }
    public string NoCuenta { get; set; }
    public string NitEntidad { get; set; }
    public string CodConvenio { get; set; }
    public Fecha Fecha_Emision { get; set; }
    public Fecha Fecha_Radicacion { get; set; }
  }
}
