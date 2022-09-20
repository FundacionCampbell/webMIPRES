
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
  /// Manejo de ParametrosItemsSiesa
  /// </summary>
  [DataObject(true)]
  [Description("Parametrización de Items de las Facturas")]
  public class ParametrosItemsSiesaDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public ParametrosItemsSiesaDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public ParametrosItemsSiesaDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en ParametrosItemsSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosItemsSiesa a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(ParametrosItemsSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en ParametrosItemsSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosItemsSiesa a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(ParametrosItemsSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro ParametrosItemsSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosItemsSiesa a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(ParametrosItemsSiesa m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los ParametrosItemsSiesa
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<ParametrosItemsSiesa> GetParametrosItemsSiesa(string Criterio, String Empresa)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosItemsSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los ParametrosItemsSiesa
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosItemsSiesa> GetParametrosItemsSiesa(String Empresa)
    {
      return GetParametrosItemsSiesa(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosItemsSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosItemsSiesa> GetParametrosItemsSiesaActivo(String Empresa)
    {
      return GetParametrosItemsSiesaActivo(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosItemsSiesa Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosItemsSiesa> GetParametrosItemsSiesaActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Activo);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosItemsSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los ParametrosItemsSiesa que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosItemsSiesa> GetParametrosItemsSiesaInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

      }
        var lista = ExecuteGetLista<ParametrosItemsSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto ParametrosItemsSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="CodServicio">Servicio de la Institucion</param>
    /// <param name="CodDependencia">Dependencia de la Institucion</param>
    /// <returns>Objeto ParametrosItemsSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public ParametrosItemsSiesa GetUnParametrosItemsSiesa(string Empresa,string CodServicio,string CodDependencia)
    {
      ParametrosItemsSiesa menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("Empresa", Empresa));
      pars.Add(new Parametro("CodServicio", CodServicio));
      pars.Add(new Parametro("CodDependencia", CodDependencia));
      menu = ExecuteGetUnLista<ParametrosItemsSiesa>(GetInstruccionSql<ParametrosItemsSiesa>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las ParametrosItemsSiesa por Criterio
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
      return Tablas<ParametrosItemsSiesa>.ToDataTable(GetParametrosItemsSiesa(criterio ,Empresa));
    }

    /// <summary>
    /// Lista de Valores de las ParametrosItemsSiesa por Criterio Activos
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
      return Tablas<ParametrosItemsSiesa>.ToDataTable(GetParametrosItemsSiesaActivo(criterio ,Empresa));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodServicio", DescripcionCampo = "Servicio de la Institucion", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodDependencia", DescripcionCampo = "Dependencia de la Institucion", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreServicio", DescripcionCampo = "Nombre del Servicio de la Clinica", TipoDato = "Cadena", Tamaño = "100" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreDependencia", DescripcionCampo = "Nombre Dependencia de la Clinica", TipoDato = "Cadena", Tamaño = "100" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodItem", DescripcionCampo = "Código del Item de S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "ReferenciaItem", DescripcionCampo = "Referencia Item S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodConcepto", DescripcionCampo = "Código Concepto Item S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodMotivo", DescripcionCampo = "Código del Motivo Item S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCentroCosto", DescripcionCampo = "Centro de Costos Item S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodUnidadItem", DescripcionCampo = "Unidad Item S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodUnidadNegocio", DescripcionCampo = "Código de la Unidad de Negocio", TipoDato = "Cadena", Tamaño = "30" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCuentaVentasSinFact", DescripcionCampo = "Cuenta Contable para Ventas No Facturadas", TipoDato = "Cadena", Tamaño = "30" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodFlujoEfectivo", DescripcionCampo = "Flujo de Efectivo del Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoDocumentoBanco", DescripcionCampo = "Tipo de Documento Banco Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodNroDocumentoBanco", DescripcionCampo = "No. de Documento Banco Item S1ESA", TipoDato = "Cadena", Tamaño = "20" });
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
                  string.IsNullOrEmpty(dr["CodServicio"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodDependencia"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreServicio"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreDependencia"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodItem"].ToString()) && 
                  string.IsNullOrEmpty(dr["ReferenciaItem"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodConcepto"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodMotivo"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCentroCosto"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodUnidadItem"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodUnidadNegocio"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodCuentaVentasSinFact"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodFlujoEfectivo"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoDocumentoBanco"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodNroDocumentoBanco"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            ParametrosItemsSiesa dato = new ParametrosItemsSiesa();
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
            // Procesamos Servicio de la Institucion
            //
            try
            {
              dato.CodServicio = dr["CodServicio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Servicio de la Institucion Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodServicio"].ToString()
              });
              continue;
            }
            //
            // Procesamos Dependencia de la Institucion
            //
            try
            {
              dato.CodDependencia = dr["CodDependencia"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Dependencia de la Institucion Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodDependencia"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre del Servicio de la Clinica
            //
            try
            {
              dato.NombreServicio = dr["NombreServicio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre del Servicio de la Clinica Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreServicio"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre Dependencia de la Clinica
            //
            try
            {
              dato.NombreDependencia = dr["NombreDependencia"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre Dependencia de la Clinica Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreDependencia"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Item de S1ESA
            //
            try
            {
              dato.CodItem = dr["CodItem"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Item de S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodItem"].ToString()
              });
              continue;
            }
            //
            // Procesamos Referencia Item S1ESA
            //
            try
            {
              dato.ReferenciaItem = dr["ReferenciaItem"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Referencia Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["ReferenciaItem"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código Concepto Item S1ESA
            //
            try
            {
              dato.CodConcepto = dr["CodConcepto"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Concepto Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodConcepto"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Motivo Item S1ESA
            //
            try
            {
              dato.CodMotivo = dr["CodMotivo"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Motivo Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodMotivo"].ToString()
              });
              continue;
            }
            //
            // Procesamos Centro de Costos Item S1ESA
            //
            try
            {
              dato.CodCentroCosto = dr["CodCentroCosto"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Centro de Costos Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCentroCosto"].ToString()
              });
              continue;
            }
            //
            // Procesamos Unidad Item S1ESA
            //
            try
            {
              dato.CodUnidadItem = dr["CodUnidadItem"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Unidad Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodUnidadItem"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código de la Unidad de Negocio
            //
            try
            {
              dato.CodUnidadNegocio = dr["CodUnidadNegocio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código de la Unidad de Negocio Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodUnidadNegocio"].ToString()
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
            // Procesamos Tipo de Documento Banco Item S1ESA
            //
            try
            {
              dato.CodTipoDocumentoBanco = dr["CodTipoDocumentoBanco"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Documento Banco Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoDocumentoBanco"].ToString()
              });
              continue;
            }
            //
            // Procesamos No. de Documento Banco Item S1ESA
            //
            try
            {
              dato.CodNroDocumentoBanco = dr["CodNroDocumentoBanco"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "No. de Documento Banco Item S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodNroDocumentoBanco"].ToString()
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
            ParametrosItemsSiesa datoBase = GetUnParametrosItemsSiesa(dato.Empresa,dato.CodServicio,dato.CodDependencia);
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodServicio:" + dato.CodServicio + "," + "CodDependencia:" + dato.CodDependencia
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodServicio:" + dato.CodServicio + "," + "CodDependencia:" + dato.CodDependencia
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodServicio:" + dato.CodServicio + "," + "CodDependencia:" + dato.CodDependencia
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


