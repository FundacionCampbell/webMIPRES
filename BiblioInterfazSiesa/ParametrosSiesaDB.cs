
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Utils;
using LiloSoft.Types.Data;
using LiloSoft.Web.ProveedorWeb;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Manejo de ParametrosSiesa
  /// </summary>
  [DataObject(true)]
  [Description("Parametros Generales de Interfaz Siesa")]
  public class ParametrosSiesaDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public ParametrosSiesaDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public ParametrosSiesaDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en ParametrosSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosSiesa a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(ParametrosSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en ParametrosSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosSiesa a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(ParametrosSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro ParametrosSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosSiesa a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(ParametrosSiesa m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los ParametrosSiesa
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<ParametrosSiesa> GetParametrosSiesa(string Criterio, String Empresa)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los ParametrosSiesa
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosSiesa> GetParametrosSiesa(String Empresa)
    {
      return GetParametrosSiesa(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosSiesa> GetParametrosSiesaActivo(String Empresa)
    {
      return GetParametrosSiesaActivo(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosSiesa Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosSiesa> GetParametrosSiesaActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Activo);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los ParametrosSiesa que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosSiesa> GetParametrosSiesaInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

      }
        var lista = ExecuteGetLista<ParametrosSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto ParametrosSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <returns>Objeto ParametrosSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public ParametrosSiesa GetUnParametrosSiesa(string Empresa)
    {
      ParametrosSiesa menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("Empresa", Empresa));
      menu = ExecuteGetUnLista<ParametrosSiesa>(GetInstruccionSql<ParametrosSiesa>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las ParametrosSiesa por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      string Empresa = "";
      var par1 = from p in parametros
                 where p.Name.ToLower()=="empresa"
                 select p;
      if (par1 != null && par1.Count() > 0)
        Empresa = par1.First().Value.ToString();
      return Tablas<ParametrosSiesa>.ToDataTable(GetParametrosSiesa(criterio ,Empresa));
    }

    /// <summary>
    /// Lista de Valores de las ParametrosSiesa por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      string Empresa = "";
      var par1 = from p in parametros
                 where p.Name.ToLower()=="empresa"
                 select p;
      if (par1 != null && par1.Count() > 0)
        Empresa = par1.First().Value.ToString();
      return Tablas<ParametrosSiesa>.ToDataTable(GetParametrosSiesaActivo(criterio ,Empresa));
    }

    #endregion

    #region Metodos de Cargue Excel
    /// <summary>
    /// Lista de MetaData para los Campos de la Tabla a Cargar
    /// </summary>
    /// <returns>Lista de MetaDataExcel de la Tabla</returns>
    public override List<MetaDataExcel> GetMetaData()
    {
      List<MetaDataExcel> lstMeta = new List<MetaDataExcel>();
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Empresa", DescripcionCampo = "Empresa de Trabajo", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCompania", DescripcionCampo = "Código Compañia S1ESA", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCentroOperacion", DescripcionCampo = "Código del Centro de Operación", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodUnidadNegocio", DescripcionCampo = "Código de Unidad Negocio S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoDocumentoFactura", DescripcionCampo = "Tipo de Documento Factura", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodClaseInternaDocFactura", DescripcionCampo = "Clase Interna Documento Factura", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "EstadoDocumentoFactura", DescripcionCampo = "Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado", TipoDato = "Cadena", Tamaño = "1" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "EstadoImpresionFactura", DescripcionCampo = "Estado Impresión Factura 0:No Impreso, 1: Impreso", TipoDato = "Cadena", Tamaño = "1" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodVendedorFactura", DescripcionCampo = "Código Vendedor de la Factura", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCondicionPagoFactura", DescripcionCampo = "Condición de Pago de la Factura", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodMoneda", DescripcionCampo = "Moneda General para Documentos", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodPais", DescripcionCampo = "Código del Pais S1ESA", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodBodegaServicios", DescripcionCampo = "Bodega para Servicios S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodListaPrecios", DescripcionCampo = "Lista de Precios S1ESA", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCuentaVentasSinFact", DescripcionCampo = "Cuenta Contable para Ventas No Facturadas", TipoDato = "Cadena", Tamaño = "30" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoDocumentoVentasSinFact", DescripcionCampo = "Tipo de Documento Ventas Sin Facturar", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoDocumentoFacturaSinFact", DescripcionCampo = "Tipo de Documento Facturado Ventas Sin Facturar", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "EstadoDocumentoVentasSinFact", DescripcionCampo = "Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado", TipoDato = "Cadena", Tamaño = "1" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "EstadoImpresionVentasSinFact", DescripcionCampo = "Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso", TipoDato = "Cadena", Tamaño = "1" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodFlujoEfectivo", DescripcionCampo = "Flujo de Efectivo del Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoDocumentoBanco", DescripcionCampo = "Tipo Documento Banco Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodNroDocumentoBanco", DescripcionCampo = "No. de  Documento Banco Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "UrlWebService", DescripcionCampo = "Dirección Url del Web Service UNOEE", TipoDato = "Cadena", Tamaño = "300" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreConexionWS", DescripcionCampo = "Nombre de Conexión del Web Service", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "UsuarioWS", DescripcionCampo = "Usuario de Conexión del Web Service", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "ClaveWS", DescripcionCampo = "Clave de Conexión Web Service", TipoDato = "Cadena", Tamaño = "50" });
        lstMeta.Add(new MetaDataExcel() { NombreCampo = "Operacion", DescripcionCampo = "Operacion I:Ingresar A:Actualizar E:Eliminar", TipoDato = "Cadena", Tamaño = "1" });
        return lstMeta;
    }

    /// <summary>
    /// Cargue de Archivo de Excel de la Información en la Tabla
    /// </summary>
    /// <param name="pNombreArchivoExcel">Archivo de Excel a Cargar</param>
    /// <returns>Lista de Errore de la Carga</returns>
    public override List<ErroresCarga> CargarDatos(string pNombreArchivoExcel)
    {
      List<ErroresCarga> lstErrores = new List<ErroresCarga>();
      DataTable dtExcel = LiloSoft.Utils.Utilidades.CargarUnEXCEL(pNombreArchivoExcel, NombreHojaExcel, true);
                  // Verificamos si hay Datos
      if (dtExcel != null && dtExcel.Rows.Count > 0)
      {
        int filas = 0;
        foreach (DataRow dr in dtExcel.Rows)
        {
          try
          {
            string Operacion = "";
            filas++;
            // Verificamos si Tiene o No Datos el Registro Completo
            if (
                  string.IsNullOrEmpty(dr["Empresa"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCompania"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCentroOperacion"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodUnidadNegocio"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoDocumentoFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodClaseInternaDocFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["EstadoDocumentoFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["EstadoImpresionFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodVendedorFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCondicionPagoFactura"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodMoneda"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodPais"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodBodegaServicios"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodListaPrecios"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCuentaVentasSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoDocumentoVentasSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoDocumentoFacturaSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["EstadoDocumentoVentasSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["EstadoImpresionVentasSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodFlujoEfectivo"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoDocumentoBanco"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodNroDocumentoBanco"].ToString()) && 
                  string.IsNullOrEmpty(dr["UrlWebService"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreConexionWS"].ToString()) && 
                  string.IsNullOrEmpty(dr["UsuarioWS"].ToString()) && 
                  string.IsNullOrEmpty(dr["ClaveWS"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            ParametrosSiesa dato = new ParametrosSiesa();
            //
            // Procesamos Empresa de Trabajo
            //
            try
            {
              dato.Empresa = dr["Empresa"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Empresa de Trabajo Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Empresa"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código Compañia S1ESA
            //
            try
            {
              dato.CodCompania = dr["CodCompania"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Compañia S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCompania"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Centro de Operación
            //
            try
            {
              dato.CodCentroOperacion = dr["CodCentroOperacion"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Centro de Operación Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCentroOperacion"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código de Unidad Negocio S1ESA
            //
            try
            {
              dato.CodUnidadNegocio = dr["CodUnidadNegocio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código de Unidad Negocio S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodUnidadNegocio"].ToString()
              });
              continue;
            }
            //
            // Procesamos Tipo de Documento Factura
            //
            try
            {
              dato.CodTipoDocumentoFactura = dr["CodTipoDocumentoFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Documento Factura Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoDocumentoFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Clase Interna Documento Factura
            //
            try
            {
              dato.CodClaseInternaDocFactura = dr["CodClaseInternaDocFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Clase Interna Documento Factura Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodClaseInternaDocFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado
            //
            try
            {
              dato.EstadoDocumentoFactura = dr["EstadoDocumentoFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Factura en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["EstadoDocumentoFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Estado Impresión Factura 0:No Impreso, 1: Impreso
            //
            try
            {
              dato.EstadoImpresionFactura = dr["EstadoImpresionFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Impresión Factura 0:No Impreso, 1: Impreso Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["EstadoImpresionFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código Vendedor de la Factura
            //
            try
            {
              dato.CodVendedorFactura = dr["CodVendedorFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Vendedor de la Factura Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodVendedorFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Condición de Pago de la Factura
            //
            try
            {
              dato.CodCondicionPagoFactura = dr["CodCondicionPagoFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Condición de Pago de la Factura Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCondicionPagoFactura"].ToString()
              });
              continue;
            }
            //
            // Procesamos Moneda General para Documentos
            //
            try
            {
              dato.CodMoneda = dr["CodMoneda"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Moneda General para Documentos Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodMoneda"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Pais S1ESA
            //
            try
            {
              dato.CodPais = dr["CodPais"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Pais S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodPais"].ToString()
              });
              continue;
            }
            //
            // Procesamos Bodega para Servicios S1ESA
            //
            try
            {
              dato.CodBodegaServicios = dr["CodBodegaServicios"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Bodega para Servicios S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodBodegaServicios"].ToString()
              });
              continue;
            }
            //
            // Procesamos Lista de Precios S1ESA
            //
            try
            {
              dato.CodListaPrecios = dr["CodListaPrecios"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Lista de Precios S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodListaPrecios"].ToString()
              });
              continue;
            }
            //
            // Procesamos Cuenta Contable para Ventas No Facturadas
            //
            try
            {
              dato.CodCuentaVentasSinFact = dr["CodCuentaVentasSinFact"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Cuenta Contable para Ventas No Facturadas Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCuentaVentasSinFact"].ToString()
              });
              continue;
            }
            //
            // Procesamos Tipo de Documento Ventas Sin Facturar
            //
            try
            {
              dato.CodTipoDocumentoVentasSinFact = dr["CodTipoDocumentoVentasSinFact"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Documento Ventas Sin Facturar Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoDocumentoVentasSinFact"].ToString()
              });
              continue;
            }
            //
            // Procesamos Tipo de Documento Facturado Ventas Sin Facturar
            //
            try
            {
              dato.CodTipoDocumentoFacturaSinFact = dr["CodTipoDocumentoFacturaSinFact"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Documento Facturado Ventas Sin Facturar Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoDocumentoFacturaSinFact"].ToString()
              });
              continue;
            }
            //
            // Procesamos Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado
            //
            try
            {
              dato.EstadoDocumentoVentasSinFact = dr["EstadoDocumentoVentasSinFact"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Documento Ventas Sin Facturar en S1ESA 1:Elaboración, 1:Aprobado, 2:Anulado Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["EstadoDocumentoVentasSinFact"].ToString()
              });
              continue;
            }
            //
            // Procesamos Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso
            //
            try
            {
              dato.EstadoImpresionVentasSinFact = dr["EstadoImpresionVentasSinFact"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Impresión Ventas Sin Factura 0:No Impreso, 1: Impreso Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["EstadoImpresionVentasSinFact"].ToString()
              });
              continue;
            }
            //
            // Procesamos Flujo de Efectivo del Item S1ESA
            //
            try
            {
              dato.CodFlujoEfectivo = dr["CodFlujoEfectivo"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Flujo de Efectivo del Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodFlujoEfectivo"].ToString()
              });
              continue;
            }
            //
            // Procesamos Tipo Documento Banco Item S1ESA
            //
            try
            {
              dato.CodTipoDocumentoBanco = dr["CodTipoDocumentoBanco"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo Documento Banco Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoDocumentoBanco"].ToString()
              });
              continue;
            }
            //
            // Procesamos No. de  Documento Banco Item S1ESA
            //
            try
            {
              dato.CodNroDocumentoBanco = dr["CodNroDocumentoBanco"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "No. de  Documento Banco Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodNroDocumentoBanco"].ToString()
              });
              continue;
            }
            //
            // Procesamos Dirección Url del Web Service UNOEE
            //
            try
            {
              dato.UrlWebService = dr["UrlWebService"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Dirección Url del Web Service UNOEE Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["UrlWebService"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre de Conexión del Web Service
            //
            try
            {
              dato.NombreConexionWS = dr["NombreConexionWS"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre de Conexión del Web Service Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreConexionWS"].ToString()
              });
              continue;
            }
            //
            // Procesamos Usuario de Conexión del Web Service
            //
            try
            {
              dato.UsuarioWS = dr["UsuarioWS"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Usuario de Conexión del Web Service Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["UsuarioWS"].ToString()
              });
              continue;
            }
            //
            // Procesamos Clave de Conexión Web Service
            //
            try
            {
              dato.ClaveWS = dr["ClaveWS"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Clave de Conexión Web Service Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["ClaveWS"].ToString()
              });
              continue;
            }

            // 
            // Procesamos la Operacion
            //
            try
            {
              Operacion = dr["Operacion"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Operación Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Operacion"].ToString()
              });
              continue;
            }
            // Buscamos el Objeto por su Llave Primaria
            ParametrosSiesa datoBase = GetUnParametrosSiesa(dato.Empresa);
            switch (Operacion.ToUpper())
            {
              case "I":
                if (datoBase != null)
                {
                  lstErrores.Add(new ErroresCarga()
                  {
                    IdError = "Valor ya Existe",
                    TextoError = "El Registro que Trata de Ingresar ya Existe",
                    FilaNo = filas,
                    Info = "Empresa:" + dato.Empresa
                  });
                  continue;
                }
                else
                {
                  Insert(dato);
                }
                break;
              case "A":
                if (datoBase == null)
                {
                  lstErrores.Add(new ErroresCarga()
                  {
                    IdError = "Valor No Existe",
                    TextoError = "El Registro que Trata de Actualizar No Existe",
                    FilaNo = filas,
                    Info = "Empresa:" + dato.Empresa
                  });
                  continue;
                }
                else
                {
                  Update(dato);
                }
                break;
              case "E":
                if (datoBase == null)
                {
                  lstErrores.Add(new ErroresCarga()
                  {
                    IdError = "Valor No Existe",
                    TextoError = "El Registro que Trata de Eliminar No Existe",
                    FilaNo = filas,
                    Info = "Empresa:" + dato.Empresa
                  });
                  continue;
                }
                else
                {
                  Delete(dato);
                }
                break;
              default:
                lstErrores.Add(new ErroresCarga()
                {
                  IdError = "Operación Es Invalida",
                  TextoError = "Valor para la Operación es Inválida",
                  FilaNo = filas,
                  Info = Operacion
                });
                continue;
            }
          }// Fin Try
          catch (Exception ex)
          {
            lstErrores.Add(new ErroresCarga()
            {
              IdError = "Error General",
              TextoError = ex.Message,
              FilaNo = filas,
              Info = ""
            });
            continue;
          }
        } // Fin de Foreach dtExcel
      } // Fin de Datos excel
      else
      {
        if (dtExcel == null)
        {
          // No Hay Datos en Excel
          lstErrores.Add(new ErroresCarga()
          {
            IdError = "Error Carga",
            TextoError = (LiloSoft.Utils.Utilidades.UltimoError!=null?LiloSoft.Utils.Utilidades.UltimoError.Message:"Error No Especificado")
          });

        }
        else
        {
          // No Hay Datos en Excel
          lstErrores.Add(new ErroresCarga()
          {
            IdError = "Sin Datos",
            TextoError = "El Archivo No Tiene Datos"
          });
        }
      }
      return lstErrores;
    }


    #endregion

  } // Fin Clase
} // Fin NameSpace


