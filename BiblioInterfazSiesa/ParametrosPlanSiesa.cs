
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LiloSoft.Types.Data;
using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Parametros para Cuentas Por Plan
  /// </summary>
  [ShowColumn(ColumnaMostrar = "CodPlan")]
  [MultiEmpresa]
  public class ParametrosPlanSiesa : BaseData 
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [Description("Empresa de Trabajo")]
    [DisplayName("Empresa de Trabajo")]
    [DataObjectField(true)]
    [SearchColumn]
    public string Empresa { get; set; }
    /// <summary>
    /// Código del Plan
    /// </summary>
    [Description("Código del Plan")]
    [DisplayName("Código del Plan")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodPlan { get; set; }
    /// <summary>
    /// Nombre del Plan
    /// </summary>
    [NoDataBase]
    public string NombrePlan { get; set; }
    /// <summary>
    /// Código del Plan Extendido
    /// </summary>
    [NoDataBase]
    public string CodPlanExtendido
    {
      get
      {
        return CodPlan + " - "  + CodPlan;
      }
    }
    /// <summary>
    /// Cuenta Contable Emisión
    /// </summary>
    [Description("Cuenta Contable Emisión")]
    [DisplayName("Cuenta Contable Emisión")]
    public string CuentaEmision { get; set; }
    /// <summary>
    /// Cuenta de Radicación
    /// </summary>
    [Description("Cuenta de Radicación")]
    [DisplayName("Cuenta de Radicación")]
    public string CuentaRadicacion { get; set; }

    /// <summary>
    /// Incluir este plan en la Intefaz de S1ESA
    /// </summary>
    [NoDataBase]
    [Description("Incluir este plan en la Intefaz de S1ESA")]
    [DisplayName("Incluir este plan en la Intefaz de S1ESA")]
    public bool IncluirInterfazSiesa { get; set; }
    /// <summary>
    /// Utilizar el Nit de la Factura o el Predeterminado del Sistema
    /// </summary>
    [NoDataBase]
    [Description("Utilizar el Nit de la Factura o el Predeterminado del Sistema")]
    [DisplayName("Utilizar el Nit de la Factura o el Predeterminado del Sistema")]
    public bool UtilizarNitFactura { get; set; }

  } // Fin Clase
} // Fin NameSpace

