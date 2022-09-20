
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

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  /// <summary>
  /// Manejo de PeriodosReporte
  /// </summary>
  [DataObject(true)]
  [Description("Periodos Reporte de Información")]
  public class PeriodosReporteDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public PeriodosReporteDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public PeriodosReporteDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en PeriodosReporte
    /// </summary>
    /// <param name="m">Objeto PeriodosReporte a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(PeriodosReporte m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en PeriodosReporte
    /// </summary>
    /// <param name="m">Objeto PeriodosReporte a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(PeriodosReporte m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro PeriodosReporte
    /// </summary>
    /// <param name="m">Objeto PeriodosReporte a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(PeriodosReporte m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los PeriodosReporte
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<PeriodosReporte> GetPeriodosReporte(string Criterio)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<PeriodosReporte>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los PeriodosReporte con Criterio de Busqueda Entre Fechas
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial de Busqueda</param>
    /// <param name="FechaFinal">Fecha Final de Busqueda</param>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<PeriodosReporte> GetPeriodosReporte(string Criterio, Fecha FechaInicial, Fecha FechaFinal)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Fechas);
          lstParam.AddParametro("Criterio", Criterio);

        lstParam.AddParametro("FechaInicial", FechaInicial);
        lstParam.AddParametro("FechaFinal", FechaFinal);
        var lista = ExecuteGetLista<PeriodosReporte>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los PeriodosReporte
    /// </summary>      
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<PeriodosReporte> GetPeriodosReporte()
    {
      return GetPeriodosReporte( "" );
    }

    /// <summary>
    /// Lista de Todos los PeriodosReporte
    /// </summary>
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<PeriodosReporte> GetPeriodosReporteActivo()
    {
      return GetPeriodosReporteActivo( "" );
    }

    /// <summary>
    /// Lista de Todos los PeriodosReporte Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<PeriodosReporte> GetPeriodosReporteActivo(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Activo);

        }
        else
        {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<PeriodosReporte>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los PeriodosReporte que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<PeriodosReporte> GetPeriodosReporteInicia(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);

      }
        var lista = ExecuteGetLista<PeriodosReporte>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto PeriodosReporte
    /// </summary>
    /// <param name="CodPeriodoReporte">Código de Periodo de Reporte</param>
    /// <returns>Objeto PeriodosReporte</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public PeriodosReporte GetUnPeriodosReporte(string CodPeriodoReporte)
    {
      PeriodosReporte menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("CodPeriodoReporte", CodPeriodoReporte));
      menu = ExecuteGetUnLista<PeriodosReporte>(GetInstruccionSql<PeriodosReporte>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las PeriodosReporte por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<PeriodosReporte>.ToDataTable(GetPeriodosReporte(criterio));
    }

    /// <summary>
    /// Lista de Valores de las PeriodosReporte por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<PeriodosReporte>.ToDataTable(GetPeriodosReporteActivo(criterio));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodPeriodoReporte", DescripcionCampo = "Código de Periodo de Reporte", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombrePeriodoReporte", DescripcionCampo = "Nombre Periodo de Reporte", TipoDato = "Cadena", Tamaño = "100" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "MesInicial", DescripcionCampo = "Mes Inicial de Reporte", TipoDato = "Cadena", Tamaño = "2" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "MesFinal", DescripcionCampo = "Mes Final del Reporte", TipoDato = "Cadena", Tamaño = "2" });
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
                  string.IsNullOrEmpty(dr["CodPeriodoReporte"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombrePeriodoReporte"].ToString()) && 
                  string.IsNullOrEmpty(dr["MesInicial"].ToString()) && 
                  string.IsNullOrEmpty(dr["MesFinal"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            PeriodosReporte dato = new PeriodosReporte();
            //
            // Procesamos Código de Periodo de Reporte
            //
            try
            {
              dato.CodPeriodoReporte = dr["CodPeriodoReporte"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código de Periodo de Reporte Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodPeriodoReporte"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre Periodo de Reporte
            //
            try
            {
              dato.NombrePeriodoReporte = dr["NombrePeriodoReporte"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre Periodo de Reporte Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombrePeriodoReporte"].ToString()
              });
              continue;
            }
            //
            // Procesamos Mes Inicial de Reporte
            //
            try
            {
              dato.MesInicial = dr["MesInicial"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Mes Inicial de Reporte Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["MesInicial"].ToString()
              });
              continue;
            }
            //
            // Procesamos Mes Final del Reporte
            //
            try
            {
              dato.MesFinal = dr["MesFinal"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Mes Final del Reporte Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["MesFinal"].ToString()
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
            PeriodosReporte datoBase = GetUnPeriodosReporte(dato.CodPeriodoReporte);
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
                    Info = "CodPeriodoReporte:" + dato.CodPeriodoReporte
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
                    Info = "CodPeriodoReporte:" + dato.CodPeriodoReporte
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
                    Info = "CodPeriodoReporte:" + dato.CodPeriodoReporte
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


