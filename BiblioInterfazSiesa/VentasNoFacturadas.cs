using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Ventas No Facturadas
  /// </summary>
  public class VentasNoFacturadas
  {
    public string Empresa { get; set; }
    public string PeriodoCierre { get; set; }
    public string NoCuenta { get; set; }
    public string NoFactura { get; set; }
    public string NoResolucionFact { get; set; }
    [BooleanEquivalent("S", "N")]
    public bool UtilizarNitFactura { get; set; }

    public string NitEntidad { get; set; }
    public string NombreEntidad { get; set; }
    public string SucursalContable { get; set; }
    public string CodConvenio { get; set; }
    public string NombreConvenio { get;set; }
    public Fecha FechaVencimiento { get; set; }
    public string CodPlan { get; set; }
    public string NombrePlan { get; set; }
    public Entero ConsInterno { get; set; }
    public string CodServicio { get; set; }
    public string NombreServicio { get; set; }
    public string CodDependencia { get; set; }
    public string NombreDependencia { get; set; }
    public Moneda ValorVenta { get; set; }
  }//fin clase
}//fin namespace
