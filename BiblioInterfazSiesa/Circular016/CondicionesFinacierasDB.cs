
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
  /// Manejo de CondicionesFinacieras
  /// </summary>
  [DataObject(true)]
  [Description("Tabla de Condiciones Financieras Circular 016 2017")]
  public class CondicionesFinacierasDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public CondicionesFinacierasDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public CondicionesFinacierasDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en CondicionesFinacieras
    /// </summary>
    /// <param name="m">Objeto CondicionesFinacieras a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(CondicionesFinacieras m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en CondicionesFinacieras
    /// </summary>
    /// <param name="m">Objeto CondicionesFinacieras a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(CondicionesFinacieras m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro CondicionesFinacieras
    /// </summary>
    /// <param name="m">Objeto CondicionesFinacieras a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(CondicionesFinacieras m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los CondicionesFinacieras
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<CondicionesFinacieras> GetCondicionesFinacieras(string Criterio)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<CondicionesFinacieras>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los CondicionesFinacieras con Criterio de Busqueda Entre Fechas
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial de Busqueda</param>
    /// <param name="FechaFinal">Fecha Final de Busqueda</param>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CondicionesFinacieras> GetCondicionesFinacieras(string Criterio, Fecha FechaInicial, Fecha FechaFinal)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Fechas);
          lstParam.AddParametro("Criterio", Criterio);

        lstParam.AddParametro("FechaInicial", FechaInicial);
        lstParam.AddParametro("FechaFinal", FechaFinal);
        var lista = ExecuteGetLista<CondicionesFinacieras>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los CondicionesFinacieras
    /// </summary>      
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CondicionesFinacieras> GetCondicionesFinacieras()
    {
      return GetCondicionesFinacieras( "" );
    }

    /// <summary>
    /// Lista de Todos los CondicionesFinacieras
    /// </summary>
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CondicionesFinacieras> GetCondicionesFinacierasActivo()
    {
      return GetCondicionesFinacierasActivo( "" );
    }

    /// <summary>
    /// Lista de Todos los CondicionesFinacieras Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CondicionesFinacieras> GetCondicionesFinacierasActivo(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Activo);

        }
        else
        {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);

        }
        var lista = ExecuteGetLista<CondicionesFinacieras>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los CondicionesFinacieras que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <returns>Lista de CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<CondicionesFinacieras> GetCondicionesFinacierasInicia(string Criterio)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Todos);

        }
        else
        {
          sqlText = GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);

      }
        var lista = ExecuteGetLista<CondicionesFinacieras>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto CondicionesFinacieras
    /// </summary>
    /// <param name="CodConcepto">Código Concepto</param>
    /// <returns>Objeto CondicionesFinacieras</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public CondicionesFinacieras GetUnCondicionesFinacieras(Entero CodConcepto)
    {
      CondicionesFinacieras menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("CodConcepto", CodConcepto));
      menu = ExecuteGetUnLista<CondicionesFinacieras>(GetInstruccionSql<CondicionesFinacieras>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las CondicionesFinacieras por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<CondicionesFinacieras>.ToDataTable(GetCondicionesFinacieras(criterio));
    }

    /// <summary>
    /// Lista de Valores de las CondicionesFinacieras por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      return Tablas<CondicionesFinacieras>.ToDataTable(GetCondicionesFinacierasActivo(criterio));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodConcepto", DescripcionCampo = "Código Concepto", TipoDato = "Entero", Tamaño = "" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "NombreConcepto", DescripcionCampo = "Descripción del Concepto", TipoDato = "Cadena", Tamaño = "500" });
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
                  string.IsNullOrEmpty(dr["CodConcepto"].ToString()) && 
                  string.IsNullOrEmpty(dr["NombreConcepto"].ToString()) && 
                  string.IsNullOrEmpty(dr["Estado"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            CondicionesFinacieras dato = new CondicionesFinacieras();
            //
            // Procesamos Código Concepto
            //
            try
            {
              dato.CodConcepto = Entero.Parse(dr["CodConcepto"].ToString());
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Concepto Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodConcepto"].ToString()
              });
              continue;
            }
            //
            // Procesamos Descripción del Concepto
            //
            try
            {
              dato.NombreConcepto = dr["NombreConcepto"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Descripción del Concepto Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["NombreConcepto"].ToString()
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
            CondicionesFinacieras datoBase = GetUnCondicionesFinacieras(dato.CodConcepto);
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
                    Info = "CodConcepto:" + dato.CodConcepto
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
                    Info = "CodConcepto:" + dato.CodConcepto
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
                    Info = "CodConcepto:" + dato.CodConcepto
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


