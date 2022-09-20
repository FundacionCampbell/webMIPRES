
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
  /// Manejo de SiesaEquivalenciasNits
  /// </summary>
  [DataObject(true)]
  [Description("Equivalencia de Nits para S1ESA")]
  public class SiesaEquivalenciasNitsDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public SiesaEquivalenciasNitsDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public SiesaEquivalenciasNitsDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en SiesaEquivalenciasNits
    /// </summary>
    /// <param name="m">Objeto SiesaEquivalenciasNits a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(SiesaEquivalenciasNits m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en SiesaEquivalenciasNits
    /// </summary>
    /// <param name="m">Objeto SiesaEquivalenciasNits a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(SiesaEquivalenciasNits m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro SiesaEquivalenciasNits
    /// </summary>
    /// <param name="m">Objeto SiesaEquivalenciasNits a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(SiesaEquivalenciasNits m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNits(string Criterio, String Empresa)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<SiesaEquivalenciasNits>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits con Criterio de Busqueda Entre Fechas
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial de Busqueda</param>
    /// <param name="FechaFinal">Fecha Final de Busqueda</param>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNits(string Criterio, String Empresa, Fecha FechaInicial, Fecha FechaFinal)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Fechas);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        lstParam.AddParametro("FechaInicial", FechaInicial);
        lstParam.AddParametro("FechaFinal", FechaFinal);
        var lista = ExecuteGetLista<SiesaEquivalenciasNits>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNits(String Empresa)
    {
      return GetSiesaEquivalenciasNits(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNitsActivo(String Empresa)
    {
      return GetSiesaEquivalenciasNitsActivo(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNitsActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Activo);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<SiesaEquivalenciasNits>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los SiesaEquivalenciasNits que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<SiesaEquivalenciasNits> GetSiesaEquivalenciasNitsInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

      }
        var lista = ExecuteGetLista<SiesaEquivalenciasNits>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto SiesaEquivalenciasNits
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="NitOriginal">Nit Original</param>
    /// <returns>Objeto SiesaEquivalenciasNits</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public SiesaEquivalenciasNits GetUnSiesaEquivalenciasNits(string Empresa,string NitOriginal)
    {
      SiesaEquivalenciasNits menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("Empresa", Empresa));
      pars.Add(new Parametro("NitOriginal", NitOriginal));
      menu = ExecuteGetUnLista<SiesaEquivalenciasNits>(GetInstruccionSql<SiesaEquivalenciasNits>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metódos Sustituidos
    /// <summary>
    /// Lista de Valores de las SiesaEquivalenciasNits por Criterio
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
      return Tablas<SiesaEquivalenciasNits>.ToDataTable(GetSiesaEquivalenciasNits(criterio ,Empresa));
    }

    /// <summary>
    /// Lista de Valores de las SiesaEquivalenciasNits por Criterio Activos
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
      return Tablas<SiesaEquivalenciasNits>.ToDataTable(GetSiesaEquivalenciasNitsActivo(criterio ,Empresa));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NitOriginal", DescripcionCampo = "Nit Original", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NitReemplazo", DescripcionCampo = "Nit Reemplazar", TipoDato = "Cadena", Tamaño = "20" });
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
                  string.IsNullOrEmpty(dr["NitOriginal"].ToString()) && 
                  string.IsNullOrEmpty(dr["NitReemplazo"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            SiesaEquivalenciasNits dato = new SiesaEquivalenciasNits();
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
            // Procesamos Nit Original
            //
            try
            {
              dato.NitOriginal = dr["NitOriginal"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nit Original Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NitOriginal"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nit Reemplazar
            //
            try
            {
              dato.NitReemplazo = dr["NitReemplazo"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nit Reemplazar Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NitReemplazo"].ToString()
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
            SiesaEquivalenciasNits datoBase = GetUnSiesaEquivalenciasNits(dato.Empresa,dato.NitOriginal);
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
                    Info = "Empresa:" + dato.Empresa + "," + "NitOriginal:" + dato.NitOriginal
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
                    Info = "Empresa:" + dato.Empresa + "," + "NitOriginal:" + dato.NitOriginal
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
                    Info = "Empresa:" + dato.Empresa + "," + "NitOriginal:" + dato.NitOriginal
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


