
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
  /// Parametros Generales de Interfaz Siesa
  /// </summary>
  [ShowColumn(ColumnaMostrar = "Empresa")]
  [MultiEmpresa]
  public class ParametrosSiesa : BaseData 
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
    /// Código Compañia S1ESA
    /// </summary>
    [Description("Código Compañia S1ESA")]
    [DisplayName("Código Compañia S1ESA")]
    public string CodCompania { get; set; }
    /// <summary>
    /// Código del Centro de Operación
    /// </summary>
    [Description("Código del Centro de Operación")]
    [DisplayName("Código del Centro de Operación")]
    public string CodCentroOperacion { get; set; }
    /// <summary>
    /// Código de Unidad Negocio S1ESA
    /// </summary>
    [Description("Código de Unidad Negocio S1ESA")]
    [DisplayName("Código de Unidad Negocio S1ESA")]
    public string CodUnidadNegocio { get; set; }
    /// <summary>
    /// Tipo de Documento Factura
    /// </summary>
    [Description("Tipo de Documento Factura")]
    [DisplayName("Tipo de Documento Factura")]
    public string CodTipoDocumentoFactura { get; set; }
    /// <summary>
    /// Clase Interna Documento Factura
    /// </summary>
    [Description("Clase Interna Documento Factura")]
    [DisplayName("Clase Interna Documento Factura")]
    public string CodClaseInternaDocFactura { get; set; }
    /// <summary>
    /// Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado
    /// </summary>
    [Description("Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado")]
    [DisplayName("Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado")]
    public string EstadoDocumentoFactura { get; set; }
    /// <summary>
    /// Estado Impresión Factura 0:No Impreso, 1: Impreso
    /// </summary>
    [Description("Estado Impresión Factura 0:No Impreso, 1: Impreso")]
    [DisplayName("Estado Impresión Factura 0:No Impreso, 1: Impreso")]
    public string EstadoImpresionFactura { get; set; }
    /// <summary>
    /// Código Vendedor de la Factura
    /// </summary>
    [Description("Código Vendedor de la Factura")]
    [DisplayName("Código Vendedor de la Factura")]
    public string CodVendedorFactura { get; set; }
    /// <summary>
    /// Condición de Pago de la Factura
    /// </summary>
    [Description("Condición de Pago de la Factura")]
    [DisplayName("Condición de Pago de la Factura")]
    public string CodCondicionPagoFactura { get; set; }
    /// <summary>
    /// Moneda General para Documentos
    /// </summary>
    [Description("Moneda General para Documentos")]
    [DisplayName("Moneda General para Documentos")]
    public string CodMoneda { get; set; }
    /// <summary>
    /// Código del Pais S1ESA
    /// </summary>
    [Description("Código del Pais S1ESA")]
    [DisplayName("Código del Pais S1ESA")]
    public string CodPais { get; set; }

    /// <summary>
    /// Tercero para Documentos
    /// </summary>
    [Description("Tercero para Documentos")]
    [DisplayName("Tercero para Documentos")]
    public string TerceroDocumento { get; set; }
    /// <summary>
    /// Bodega para Servicios S1ESA
    /// </summary>
    [Description("Bodega para Servicios S1ESA")]
    [DisplayName("Bodega para Servicios S1ESA")]
    public string CodBodegaServicios { get; set; }
    /// <summary>
    /// Lista de Precios S1ESA
    /// </summary>
    [Description("Lista de Precios S1ESA")]
    [DisplayName("Lista de Precios S1ESA")]
    public string CodListaPrecios { get; set; }
    /// <summary>
    /// Cuenta Contable para Ventas No Facturadas
    /// </summary>
    [Description("Cuenta Contable para Ventas No Facturadas")]
    [DisplayName("Cuenta Contable para Ventas No Facturadas")]
    public string CodCuentaVentasSinFact { get; set; }
    /// <summary>
    /// Tipo de Documento Ventas Sin Facturar
    /// </summary>
    [Description("Tipo de Documento Ventas Sin Facturar")]
    [DisplayName("Tipo de Documento Ventas Sin Facturar")]
    public string CodTipoDocumentoVentasSinFact { get; set; }
    /// <summary>
    /// Tipo de Documento Radicación Facturas
    /// </summary>
    [Description("Tipo de Documento Radicación Facturas")]
    [DisplayName("Tipo de Documento Radicación Facturas")]
    public string CodTipoDocumentoRadicacion { get; set; }
    /// <summary>
    /// Tipo de Documento Facturado Ventas Sin Facturar
    /// </summary>
    [Description("Tipo de Documento Facturado Ventas Sin Facturar")]
    [DisplayName("Tipo de Documento Facturado Ventas Sin Facturar")]
    public string CodTipoDocumentoFacturaSinFact { get; set; }
    /// <summary>
    /// Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado
    /// </summary>
    [Description("Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado")]
    [DisplayName("Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado")]
    public string EstadoDocumentoVentasSinFact { get; set; }
    /// <summary>
    /// Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso
    /// </summary>
    [Description("Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso")]
    [DisplayName("Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso")]
    public string EstadoImpresionVentasSinFact { get; set; }
    /// <summary>
    /// Flujo de Efectivo del Item S1ESA
    /// </summary>
    [Description("Flujo de Efectivo del Item S1ESA")]
    [DisplayName("Flujo de Efectivo del Item S1ESA")]
    public string CodFlujoEfectivo { get; set; }
    /// <summary>
    /// Tipo Documento Banco Item S1ESA
    /// </summary>
    [Description("Tipo Documento Banco Item S1ESA")]
    [DisplayName("Tipo Documento Banco Item S1ESA")]
    public string CodTipoDocumentoBanco { get; set; }
    /// <summary>
    /// No. de  Documento Banco Item S1ESA
    /// </summary>
    [Description("No. de  Documento Banco Item S1ESA")]
    [DisplayName("No. de  Documento Banco Item S1ESA")]
    public string CodNroDocumentoBanco { get; set; }
    /// <summary>
    /// Dirección Url del Web Service UNOEE
    /// </summary>
    [Description("Dirección Url del Web Service UNOEE")]
    [DisplayName("Dirección Url del Web Service UNOEE")]
    public string UrlWebService { get; set; }
    /// <summary>
    /// Nombre de Conexión del Web Service
    /// </summary>
    [Description("Nombre de Conexión del Web Service")]
    [DisplayName("Nombre de Conexión del Web Service")]
    public string NombreConexionWS { get; set; }
    /// <summary>
    /// Usuario de Conexión del Web Service
    /// </summary>
    [Description("Usuario de Conexión del Web Service")]
    [DisplayName("Usuario de Conexión del Web Service")]
    public string UsuarioWS { get; set; }
    /// <summary>
    /// Clave de Conexión Web Service
    /// </summary>
    [Description("Clave de Conexión Web Service")]
    [DisplayName("Clave de Conexión Web Service")]
    public string ClaveWS { get; set; }

    /// <summary>
    /// Medio de Pago Facturas
    /// </summary>
    [Description("Medio de Pago Facturas")]
    [DisplayName("Medio de Pago Facturas")]
    public string CodMedioPago { get; set; }

    /// <summary>
    /// Banco para Caja
    /// </summary>
    [Description("Banco para Caja")]
    [DisplayName("Banco para Caja")]
    public string CodBancoCaja { get; set; }
    /// <summary>
    /// Código del Evento de Radicación
    /// </summary>
    [Description("Código Evento de Radicación")]
    [DisplayName("Código Evento de Radicación")]
    public string CodEventoRadicacion { get; set; }
    /// <summary>
    /// Tipo Documento Glosas Nota Crédito
    /// </summary>
    [Description("Tipo Documento Glosas Nota Crédito")]
    [DisplayName("Tipo Documento Glosas Nota Crédito")]
    public string CodTipoDocumGlosaNotaCredito { get; set; }
    /// <summary>
    /// Tipo Documento Glosas Aceptaciones
    /// </summary>
    [Description("Tipo Documento Glosas Aceptaciones")]
    [DisplayName("Tipo Documento Glosas Aceptaciones")]
    public string CodTipoDocumGlosaAceptaciones { get; set; }

  } // Fin Clase
} // Fin NameSpace

