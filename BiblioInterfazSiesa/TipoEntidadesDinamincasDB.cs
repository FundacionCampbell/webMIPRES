
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
  /// Manejo de TipoEntidadesDinamincas
  /// </summary>
  [DataObject(true)]
  [Description("Tipos de Entidades Dinámicas de las Facturas")]
  public class TipoEntidadesDinamincasDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public TipoEntidadesDinamincasDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public TipoEntidadesDinamincasDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en TipoEntidadesDinamincas
    /// </summary>
    /// <param name="m">Objeto TipoEntidadesDinamincas a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(TipoEntidadesDinamincas m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en TipoEntidadesDinamincas
    /// </summary>
    /// <param name="m">Objeto TipoEntidadesDinamincas a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(TipoEntidadesDinamincas m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro TipoEntidadesDinamincas
    /// </summary>
    /// <param name="m">Objeto TipoEntidadesDinamincas a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(TipoEntidadesDinamincas m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los TipoEntidadesDinamincas
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<TipoEntidadesDinamincas> GetTipoEntidadesDinamincas(string Criterio)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<TipoEntidadesDinamincas>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los TipoEntidadesDinamincas
    /// </summary>      
    /// <returns>Lista de TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<TipoEntidadesDinamincas> GetTipoEntidadesDinamincas()
    {
      return GetTipoEntidadesDinamincas( "" );
    }

    /// <summary>
    /// Lista de Todos los TipoEntidadesDinamincas
    /// </summary>
    /// <returns>Lista de TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<TipoEntidadesDinamincas> GetTipoEntidadesDinamincasActivo()
    {
      return GetTipoEntidadesDinamincasActivo( "" );
    }

    /// <summary>
    /// Lista de Todos los TipoEntidadesDinamincas Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<TipoEntidadesDinamincas> GetTipoEntidadesDinamincasActivo(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Activo);

        }
        else
        {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<TipoEntidadesDinamincas>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los TipoEntidadesDinamincas que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<TipoEntidadesDinamincas> GetTipoEntidadesDinamincasInicia(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);

      }
        var lista = ExecuteGetLista<TipoEntidadesDinamincas>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto TipoEntidadesDinamincas
    /// </summary>
    /// <param name="CodTipoEntidad">Código Entidad Dinámica</param>
    /// <returns>Objeto TipoEntidadesDinamincas</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public TipoEntidadesDinamincas GetUnTipoEntidadesDinamincas(string CodTipoEntidad)
    {
      TipoEntidadesDinamincas menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("CodTipoEntidad", CodTipoEntidad));
      menu = ExecuteGetUnLista<TipoEntidadesDinamincas>(GetInstruccionSql<TipoEntidadesDinamincas>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las TipoEntidadesDinamincas por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<TipoEntidadesDinamincas>.ToDataTable(GetTipoEntidadesDinamincas(criterio));
    }

    /// <summary>
    /// Lista de Valores de las TipoEntidadesDinamincas por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<TipoEntidadesDinamincas>.ToDataTable(GetTipoEntidadesDinamincasActivo(criterio));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoEntidad", DescripcionCampo = "Código Entidad Dinámica", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreTipoEntidad", DescripcionCampo = "Nombre Entidad Dinámica", TipoDato = "Cadena", Tamaño = "100" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Estado", DescripcionCampo = "Estado Activo Inactivo", TipoDato = "Cadena", Tamaño = "1" });
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
                  string.IsNullOrEmpty(dr["CodTipoEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreTipoEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["Estado"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            TipoEntidadesDinamincas dato = new TipoEntidadesDinamincas();
            //
            // Procesamos Código Entidad Dinámica
            //
            try
            {
              dato.CodTipoEntidad = dr["CodTipoEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Entidad Dinámica Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre Entidad Dinámica
            //
            try
            {
              dato.NombreTipoEntidad = dr["NombreTipoEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre Entidad Dinámica Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreTipoEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Estado Activo Inactivo
            //
            try
            {
              dato.Estado = dr["Estado"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Estado Activo Inactivo Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Estado"].ToString()
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
            TipoEntidadesDinamincas datoBase = GetUnTipoEntidadesDinamincas(dato.CodTipoEntidad);
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
                    Info = "CodTipoEntidad:" + dato.CodTipoEntidad
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
                    Info = "CodTipoEntidad:" + dato.CodTipoEntidad
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
                    Info = "CodTipoEntidad:" + dato.CodTipoEntidad
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


