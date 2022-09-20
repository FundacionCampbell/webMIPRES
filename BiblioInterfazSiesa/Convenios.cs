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
  /// <summary>
  /// Convenios de la Empresa 
  /// </summary>
  [ShowColumn(ColumnaMostrar = "Nombre_Convenio")]
  [MultiEmpresa]
  public class Convenios : BaseData
  {


    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [Description("Empresa de Trabajo")]
    [DisplayName("Empresa de Trabajo")]
    [DataObjectField(true)]
    public string empresa { get; set; }
    /// <summary>
    /// Código del Convenio
    /// </summary>
    [Description("Código del Convenio")]
    [DisplayName("Código del Convenio")]
    [DataObjectField(true)]
    [SearchColumn]
    public string Cod_Convenio { get; set; }
    /// <summary>
    /// Nombre del Convenio
    /// </summary>
    [Description("Nombre del Convenio")]
    [DisplayName("Nombre del Convenio")]
    [SearchColumn]
    [ReadOnlyDb]
    public string Nombre_Convenio { get; set; }
    /// <summary>
    ///  Extendido
    /// </summary>
    [NoDataBase]
    public string Nombre_ConvenioExtendido
    {
      get
      {
        return empresa + " - " + Cod_Convenio + " - " + Nombre_Convenio;
      }
    }
    /// <summary>
    /// Nit de la Entidad
    /// </summary>
    [Description("Nit de la Entidad")]
    [DisplayName("Nit de la Entidad")]
    [ReadOnlyDb]
    public string NitEntidad { get; set; }

    /// <summary>
    /// Nombre de la Entidad
    /// </summary>
    [NoDataBase(SelectFromDB = true,
      NombreTabla = "Entidades", CampoOrigen = "NitEntidad", CampoValor = "NitEntidad",
      CampoVisual = "Nombre_Entidad", NombreColumna = "Nombre_Entidad",
      ManejaEmpresa = true, ManejaSucursal = false)]
    public string Nombre_Entidad { get; set; }
    /// <summary>
    /// Fecha de Inicio de Vigencia
    /// </summary>
    [Description("Fecha de Inicio de Vigencia")]
    [DisplayName("Fecha de Inicio de Vigencia")]
    [ReadOnlyDb]
    public Fecha Fec_Ini_Vigencia { get; set; }
    /// <summary>
    /// Fecha de Final de Vigencia
    /// </summary>
    [Description("Fecha de Final de Vigencia")]
    [DisplayName("Fecha de Final de Vigencia")]
    [ReadOnlyDb]
    public Fecha Fec_Fin_Vigencia { get; set; }

    [ReadOnlyDb]
    public string Estado { get; set; }
    /// <summary>
    /// Sucursal Contable
    /// </summary>
    [Description("Sucursal Contable")]
    [DisplayName("Sucursal Contable")]
    public string SucursalContable { get; set; }
    /// <summary>
    /// Punto de Envio Contable 
    /// </summary>
    [Description("Punto de Envio Contable ")]
    [DisplayName("Punto de Envio Contable ")]
    public string CodPuntoEnvioContable { get; set; }
    /// <summary>
    /// Tipo de Cliente Contable
    /// </summary>
    [Description("Tipo de Cliente Contable")]
    [DisplayName("Tipo de Cliente Contable")]
    public string CodTipoClienteContable { get; set; }
    /// <summary>
    /// Código de Condición de Pago del Cliente
    /// </summary>
    [Description("Código de Condición de Pago del Cliente")]
    [DisplayName("Código de Condición de Pago del Cliente")]
    public string CodCondicionPagoFactura { get; set; }
  }
}
