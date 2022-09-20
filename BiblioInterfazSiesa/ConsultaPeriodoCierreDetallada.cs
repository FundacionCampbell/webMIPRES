using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Consulta del Cierre del Periodo Detallada
  /// </summary>
  public class ConsultaPeriodoCierreDetallada
  {
    /// <summary>
    /// Nit de la Entidad
    /// </summary>
    public string NitEntidad { get; set; }
    /// <summary>
    /// Nombre de la Entidad
    /// </summary>
    public string NombreEntidad { get; set; }
    /// <summary>
    /// Sucursal Contable del Cliente
    /// </summary>
    public string SucursalContable { get; set; }
    /// <summary>
    /// Código del Plan
    /// </summary>
    public string CodPlan { get; set; }
    /// <summary>
    /// Nombre del Plan
    /// </summary>
    public string NombrePlan { get; set; }
    /// <summary>
    /// Código del Servicio
    /// </summary>
    public string CodServicio { get; set; }
    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string NombreServicio { get; set; }
    /// <summary>
    /// Código de la Dependencia
    /// </summary>
    public string CodDependencia { get; set; }
    /// <summary>
    /// Nombre de la Dependencia
    /// </summary>
    public string NombreDependencia { get; set; }
    /// <summary>
    /// Valor de la Venta
    /// </summary>
    public Moneda ValorVenta { get; set; } 
  }
}
