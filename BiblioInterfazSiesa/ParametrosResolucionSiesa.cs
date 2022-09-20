
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
  /// Parametrización de Documentos Por Resolución de Facturación
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NoResolucion")]
  [MultiEmpresa]
  public class ParametrosResolucionSiesa : BaseData 
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
    /// No. de Resolución
    /// </summary>
    [Description("No. de Resolución")]
    [DisplayName("No. de Resolución")]
    [DataObjectField(true)]
    [SearchColumn]
    public string NoResolucion { get; set; }
    [NoDataBase]
    public string NombreResolucion { get; set; }
    /// <summary>
    /// No. de Resolución Extendido
    /// </summary>
    [NoDataBase]
    public string NoResolucionExtendido
    {
      get
      {
        return NoResolucion + " - "  + NoResolucion;
      }
    }
    /// <summary>
    /// Tipo de Documento Factura
    /// </summary>
    [Description("Tipo de Documento Factura")]
    [DisplayName("Tipo de Documento Factura")]
    public string CodTipoDocumentoFactura { get; set; }
    /// <summary>
    /// Documento para Radicación de Factura
    /// </summary>
    [Description("Documento para Radicación de Factura")]
    [DisplayName("Documento para Radicación de Factura")]
    public string CodDocumentoRadicacion { get; set; }
  } // Fin Clase
} // Fin NameSpace

