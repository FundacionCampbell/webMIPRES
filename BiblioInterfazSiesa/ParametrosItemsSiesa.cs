
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
  /// Parametrización de Items de las Facturas
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NombreServicio")]
  [MultiEmpresa]
  public class ParametrosItemsSiesa : BaseData 
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [Description("Empresa de Trabajo")]
    [DisplayName("Empresa de Trabajo")]
    [DataObjectField(true)]
    public string Empresa { get; set; }
    /// <summary>
    /// Servicio de la Institucion
    /// </summary>
    [Description("Servicio de la Institucion")]
    [DisplayName("Servicio de la Institucion")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodServicio { get; set; }
    /// <summary>
    /// Dependencia de la Institucion
    /// </summary>
    [Description("Dependencia de la Institucion")]
    [DisplayName("Dependencia de la Institucion")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodDependencia { get; set; }
    /// <summary>
    /// Nombre del Servicio de la Clinica
    /// </summary>
    [Description("Nombre del Servicio de la Clinica")]
    [DisplayName("Nombre del Servicio de la Clinica")]
    [SearchColumn]
    public string NombreServicio { get; set; }
    /// <summary>
    /// Nombre del Servicio de la Clinica Extendido
    /// </summary>
    [NoDataBase]
    public string NombreServicioExtendido
    {
      get
      {
        return CodServicio + " - " + CodDependencia + " - "  + NombreServicio;
      }
    }
    /// <summary>
    /// Nombre Dependencia de la Clinica
    /// </summary>
    [Description("Nombre Dependencia de la Clinica")]
    [DisplayName("Nombre Dependencia de la Clinica")]
    [SearchColumn]
    public string NombreDependencia { get; set; }
    /// <summary>
    /// Código del Item de S1ESA
    /// </summary>
    [Description("Código del Item de S1ESA")]
    [DisplayName("Código del Item de S1ESA")]
    public string CodItem { get; set; }
    /// <summary>
    /// Referencia Item S1ESA
    /// </summary>
    [Description("Referencia Item S1ESA")]
    [DisplayName("Referencia Item S1ESA")]
    public string ReferenciaItem { get; set; }
    /// <summary>
    /// Código Concepto Item S1ESA
    /// </summary>
    [Description("Código Concepto Item S1ESA")]
    [DisplayName("Código Concepto Item S1ESA")]
    public string CodConcepto { get; set; }
    /// <summary>
    /// Código del Motivo Item S1ESA
    /// </summary>
    [Description("Código del Motivo Item S1ESA")]
    [DisplayName("Código del Motivo Item S1ESA")]
    public string CodMotivo { get; set; }
    /// <summary>
    /// Centro de Costos Item S1ESA
    /// </summary>
    [Description("Centro de Costos Item S1ESA")]
    [DisplayName("Centro de Costos Item S1ESA")]
    public string CodCentroCosto { get; set; }
    /// <summary>
    /// Unidad Item S1ESA
    /// </summary>
    [Description("Unidad Item S1ESA")]
    [DisplayName("Unidad Item S1ESA")]
    public string CodUnidadItem { get; set; }
    /// <summary>
    /// Código de la Unidad de Negocio
    /// </summary>
    [Description("Código de la Unidad de Negocio")]
    [DisplayName("Código de la Unidad de Negocio")]
    public string CodUnidadNegocio { get; set; }
    /// <summary>
    /// Cuenta Contable para Ventas No Facturadas (DB)
    /// </summary>
    [Description("Cuenta Contable para Ventas No Facturadas (DB)")]
    [DisplayName("Cuenta Contable para Ventas No Facturadas")]
    public string CodCuentaVentasSinFact { get; set; }
    /// <summary>
    /// Cuenta Puenta de Ventas No Facturadas (CR)
    /// </summary>
    [Description("Cuenta Puenta de Ventas No Facturadas (CR)")]
    [DisplayName("Cuenta Puenta de Ventas No Facturadas (CR)")]
    public string CodCuentaPuenteVentasSinfact { get; set; }
    /// <summary>
    /// Cuenta de Reversión de Venta No Facturada (DB)
    /// </summary>
    [Description("Cuenta de Reversión de Venta No Facturada (DB)")]
    [DisplayName("Cuenta de Reversión de Venta No Facturada (DB)")]
    public string CodCuentaReversaVentaNoFacturada { get; set; }
    /// <summary>
    /// Flujo de Efectivo del Item S1ESA
    /// </summary>
    [Description("Flujo de Efectivo del Item S1ESA")]
    [DisplayName("Flujo de Efectivo del Item S1ESA")]
    public string CodFlujoEfectivo { get; set; }
    /// <summary>
    /// Tipo de Documento Banco Item S1ESA
    /// </summary>
    [Description("Tipo de Documento Banco Item S1ESA")]
    [DisplayName("Tipo de Documento Banco Item S1ESA")]
    public string CodTipoDocumentoBanco { get; set; }
    /// <summary>
    /// No. de Documento Banco Item S1ESA
    /// </summary>
    [Description("No. de Documento Banco Item S1ESA")]
    [DisplayName("No. de Documento Banco Item S1ESA")]
    public string CodNroDocumentoBanco { get; set; }
    /// <summary>
    /// Código de Cuenta para Ventas No Facturadas Años Anteriores
    /// </summary>
    [Description("Código de Cuenta para Ventas No Facturadas Años Anteriores")]
    [DisplayName("Código de Cuenta para Ventas No Facturadas Años Anteriores")]
    public string CodCuentaNoFacturadaAnteriores { get; set; }
    /// <summary>
    /// Código de Cuenta Ingresos Mes Actual
    /// </summary>
    [Description("Código de Cuenta Ingresos Mes Actual")]
    [DisplayName("Código de Cuenta Ingresos Mes Actual")]
    public string CodCuentaIngresosMesActual { get; set; }
    /// <summary>
    /// Código de Cuenta Ingresos Mes Anterior
    /// </summary>
    [Description("Código de Cuenta Ingresos Mes Anterior")]
    [DisplayName("Código de Cuenta Ingresos Mes Anterior")]
    public string CodCuentaIngresosMesAnterior { get; set; }
    /// <summary>
    /// Código de Cuentas para Devoluciones (Glosas) (DB)
    /// </summary>
    [Description("Código de Cuentas para Devoluciones (Glosas) (DB)")]
    [DisplayName("Código de Cuentas para Devoluciones (Glosas) (DB)")]
    public string CodCuentaDevolucionesGlosas { get; set; }

  } // Fin Clase
} // Fin NameSpace

