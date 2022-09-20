
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
  /// Manejo de CiudadesConexion
  /// </summary>
  [DataObject(true)]
  [Description("Conexión de Ciudades")]
  public class CiudadesConexionDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public CiudadesConexionDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public CiudadesConexionDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en CiudadesConexion
    /// </summary>
    /// <param name="m">Objeto CiudadesConexion a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(CiudadesConexion m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en CiudadesConexion
    /// </summary>
    /// <param name="m">Objeto CiudadesConexion a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(CiudadesConexion m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro CiudadesConexion
    /// </summary>
    /// <param name="m">Objeto CiudadesConexion a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(CiudadesConexion m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los CiudadesConexion
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<CiudadesConexion> GetCiudadesConexion(string Criterio)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<CiudadesConexion>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los CiudadesConexion con Criterio de Busqueda Entre Fechas
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial de Busqueda</param>
    /// <param name="FechaFinal">Fecha Final de Busqueda</param>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CiudadesConexion> GetCiudadesConexion(string Criterio, Fecha FechaInicial, Fecha FechaFinal)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Fechas);
          lstParam.AddParametro("Criterio", Criterio);

        lstParam.AddParametro("FechaInicial", FechaInicial);
        lstParam.AddParametro("FechaFinal", FechaFinal);
        var lista = ExecuteGetLista<CiudadesConexion>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los CiudadesConexion
    /// </summary>      
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CiudadesConexion> GetCiudadesConexion()
    {
      return GetCiudadesConexion( "" );
    }

    /// <summary>
    /// Lista de Todos los CiudadesConexion
    /// </summary>
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CiudadesConexion> GetCiudadesConexionActivo()
    {
      return GetCiudadesConexionActivo( "" );
    }

    /// <summary>
    /// Lista de Todos los CiudadesConexion Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CiudadesConexion> GetCiudadesConexionActivo(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Activo);

        }
        else
        {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<CiudadesConexion>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los CiudadesConexion que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CiudadesConexion> GetCiudadesConexionInicia(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);

      }
        var lista = ExecuteGetLista<CiudadesConexion>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto CiudadesConexion
    /// </summary>
    /// <param name="CiudadServer">Ciudad del Servidor</param>
    /// <returns>Objeto CiudadesConexion</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public CiudadesConexion GetUnCiudadesConexion(string CiudadServer)
    {
      CiudadesConexion menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("CiudadServer", CiudadServer));
      menu = ExecuteGetUnLista<CiudadesConexion>(GetInstruccionSql<CiudadesConexion>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metódos Sustituidos
    /// <summary>
    /// Lista de Valores de las CiudadesConexion por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<CiudadesConexion>.ToDataTable(GetCiudadesConexion(criterio));
    }

    /// <summary>
    /// Lista de Valores de las CiudadesConexion por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<CiudadesConexion>.ToDataTable(GetCiudadesConexionActivo(criterio));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CiudadServer", DescripcionCampo = "Ciudad del Servidor", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreCiudadServer", DescripcionCampo = "Nombre Ciudad del Servidor", TipoDato = "Cadena", Tamaño = "100" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "EmpresaConexion", DescripcionCampo = "Empresa para Conexion", TipoDato = "Cadena", Tamaño = "6" });
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
                  string.IsNullOrEmpty(dr["CiudadServer"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreCiudadServer"].ToString()) && 
                  string.IsNullOrEmpty(dr["EmpresaConexion"].ToString()) && 
                  string.IsNullOrEmpty(dr["Estado"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            CiudadesConexion dato = new CiudadesConexion();
            //
            // Procesamos Ciudad del Servidor
            //
            try
            {
              dato.CiudadServer = dr["CiudadServer"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Ciudad del Servidor Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CiudadServer"].ToString()
              });
              continue;
            }
            //
            // Procesamos Nombre Ciudad del Servidor
            //
            try
            {
              dato.NombreCiudadServer = dr["NombreCiudadServer"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Nombre Ciudad del Servidor Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreCiudadServer"].ToString()
              });
              continue;
            }
            //
            // Procesamos Empresa para Conexion
            //
            try
            {
              dato.EmpresaConexion = dr["EmpresaConexion"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Empresa para Conexion Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["EmpresaConexion"].ToString()
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
            CiudadesConexion datoBase = GetUnCiudadesConexion(dato.CiudadServer);
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
                    Info = "CiudadServer:" + dato.CiudadServer
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
                    Info = "CiudadServer:" + dato.CiudadServer
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
                    Info = "CiudadServer:" + dato.CiudadServer
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


