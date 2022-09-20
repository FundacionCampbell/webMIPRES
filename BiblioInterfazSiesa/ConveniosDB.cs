
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
  /// Manejo de Convenios
  /// </summary>
  [DataObject(true)]
  [Description("")]
  public class ConveniosDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public ConveniosDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public ConveniosDB(DB db)
      : this()
    {
      SqlDb = db;
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en Convenios
    /// </summary>
    /// <param name="m">Objeto Convenios a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public void Insert(Convenios m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Insert), EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en Convenios
    /// </summary>
    /// <param name="m">Objeto Convenios a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update, true)]
    public void Update(Convenios m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Update), EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro Convenios
    /// </summary>
    /// <param name="m">Objeto Convenios a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public void Delete(Convenios m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Delete), EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

  #endregion

        #region Metodos de Listas
        /// <summary>
        /// Lista de Todos los Convenios
        /// </summary>
        /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
        /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
        /// <returns>Lista de Convenios</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
    public List<Convenios> GetConvenios(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Todos);
        lstParam.AddParametro("Empresa", Empresa);

      }
      else
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Criterio);
        lstParam.AddParametro("Criterio", Criterio);
        lstParam.AddParametro("Empresa", Empresa);

      }
      var lista = ExecuteGetLista<Convenios>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
      return lista;
    }

    /// <summary>
    /// Lista de Todos los Convenios
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de Convenios</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public List<Convenios> GetConvenios(String Empresa)
    {
      return GetConvenios("", Empresa);
    }

    /// <summary>
    /// Lista de Todos los Convenios
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de Convenios</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public List<Convenios> GetConveniosActivo(String Empresa)
    {
      return GetConveniosActivo("", Empresa);
    }

    /// <summary>
    /// Lista de Todos los Convenios Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de Convenios</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public List<Convenios> GetConveniosActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Activo);
        lstParam.AddParametro("Empresa", Empresa);

      }
      else
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.CriterioActivos);
        lstParam.AddParametro("Criterio", Criterio);
        lstParam.AddParametro("Empresa", Empresa);

      }
      var lista = ExecuteGetLista<Convenios>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
      return lista;
    }


    /// <summary>
    /// Lista de Todos los Convenios que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de Convenios</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public List<Convenios> GetConveniosInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Todos);
        lstParam.AddParametro("Empresa", Empresa);

      }
      else
      {
        sqlText = GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.Inicia);
        lstParam.AddParametro("Criterio", Criterio);
        lstParam.AddParametro("Empresa", Empresa);

      }
      var lista = ExecuteGetLista<Convenios>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
      return lista;
    }


    /// <summary>
    /// Busca un objeto Convenios
    /// </summary>
    /// <param name="empresa"></param>
    /// <param name="Cod_Convenio"></param>
    /// <returns>Objeto Convenios</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public Convenios GetUnConvenios(string empresa, string Cod_Convenio)
    {
      Convenios menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("empresa", empresa));
      pars.Add(new Parametro("Cod_Convenio", Cod_Convenio));
      menu = ExecuteGetUnLista<Convenios>(GetInstruccionSql<Convenios>(TiposProcedimientosAlmacenados.SoloUno), EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las Convenios por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      string Empresa = "";
      var par1 = from p in parametros
                 where p.Name.ToLower() == "empresa"
                 select p;
      if (par1 != null && par1.Count() > 0)
        Empresa = par1.First().Value.ToString();
      return Tablas<Convenios>.ToDataTable(GetConvenios(criterio, Empresa));
    }

    /// <summary>
    /// Lista de Valores de las Convenios por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      string Empresa = "";
      var par1 = from p in parametros
                 where p.Name.ToLower() == "empresa"
                 select p;
      if (par1 != null && par1.Count() > 0)
        Empresa = par1.First().Value.ToString();
      return Tablas<Convenios>.ToDataTable(GetConveniosActivo(criterio, Empresa));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "empresa", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "4" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Cod_Convenio", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Nombre_Convenio", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "60" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NitEntidad", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "14" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Fec_Ini_Vigencia", DescripcionCampo = "", TipoDato = "Fecha", Tamaño = "" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Fec_Fin_Vigencia", DescripcionCampo = "", TipoDato = "Fecha", Tamaño = "" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Estado", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "1" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "SucursalContable", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "5" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodPuntoEnvioContable", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoClienteContable", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodCondicionPagoFactura", DescripcionCampo = "", TipoDato = "Cadena", Tamaño = "10" });
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
                  string.IsNullOrEmpty(dr["empresa"].ToString()) &&
                  string.IsNullOrEmpty(dr["Cod_Convenio"].ToString()) &&
                  string.IsNullOrEmpty(dr["Nombre_Convenio"].ToString()) &&
                  string.IsNullOrEmpty(dr["NitEntidad"].ToString()) &&
                  string.IsNullOrEmpty(dr["Fec_Ini_Vigencia"].ToString()) &&
                  string.IsNullOrEmpty(dr["Fec_Fin_Vigencia"].ToString()) &&
                  string.IsNullOrEmpty(dr["Estado"].ToString()) &&
                  string.IsNullOrEmpty(dr["SucursalContable"].ToString()) &&
                  string.IsNullOrEmpty(dr["CodPuntoEnvioContable"].ToString()) &&
                  string.IsNullOrEmpty(dr["CodTipoClienteContable"].ToString()) &&
                  string.IsNullOrEmpty(dr["CodCondicionPagoFactura"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            Convenios dato = new Convenios();
            //
            // Procesamos 
            //
            try
            {
              dato.empresa = dr["empresa"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Empresa de Trabajo Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["empresa"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.Cod_Convenio = dr["Cod_Convenio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Convenio Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Cod_Convenio"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.Nombre_Convenio = dr["Nombre_Convenio"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre del Convenio Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Nombre_Convenio"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.NitEntidad = dr["NitEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nit Entidad Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NitEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.Fec_Ini_Vigencia = Fecha.Parse(dr["Fec_Ini_Vigencia"].ToString());
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Fecha de Inicio Vigencia Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Fec_Ini_Vigencia"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.Fec_Fin_Vigencia = Fecha.Parse(dr["Fec_Fin_Vigencia"].ToString());
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Fecha Final de Vigencia Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Fec_Fin_Vigencia"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.Estado = dr["Estado"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Estado"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.SucursalContable = dr["SucursalContable"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Sucursal Contable Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["SucursalContable"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.CodPuntoEnvioContable = dr["CodPuntoEnvioContable"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Punto de Envio Contable Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodPuntoEnvioContable"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.CodTipoClienteContable = dr["CodTipoClienteContable"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Cliente Contable Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoClienteContable"].ToString()
              });
              continue;
            }
            //
            // Procesamos 
            //
            try
            {
              dato.CodCondicionPagoFactura = dr["CodCondicionPagoFactura"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código de Condición de Pago del Cliente Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodCondicionPagoFactura"].ToString()
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
            Convenios datoBase = GetUnConvenios(dato.empresa, dato.Cod_Convenio);
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
                    Info = "empresa:" + dato.empresa + "," + "Cod_Convenio:" + dato.Cod_Convenio
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
                    Info = "empresa:" + dato.empresa + "," + "Cod_Convenio:" + dato.Cod_Convenio
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
                    Info = "empresa:" + dato.empresa + "," + "Cod_Convenio:" + dato.Cod_Convenio
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
            TextoError = (LiloSoft.Utils.Utilidades.UltimoError != null ? LiloSoft.Utils.Utilidades.UltimoError.Message : "Error No Especificado")
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

