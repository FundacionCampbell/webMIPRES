
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
  /// Manejo de ParametrosPlanSiesa
  /// </summary>
  [DataObject(true)]
  [Description("Parametros para Cuentas Por Plan")]
  public class ParametrosPlanSiesaDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public ParametrosPlanSiesaDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public ParametrosPlanSiesaDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en ParametrosPlanSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosPlanSiesa a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(ParametrosPlanSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en ParametrosPlanSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosPlanSiesa a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(ParametrosPlanSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro ParametrosPlanSiesa
    /// </summary>
    /// <param name="m">Objeto ParametrosPlanSiesa a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(ParametrosPlanSiesa m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los ParametrosPlanSiesa
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<ParametrosPlanSiesa> GetParametrosPlanSiesa(string Criterio, String Empresa)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosPlanSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los ParametrosPlanSiesa
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosPlanSiesa> GetParametrosPlanSiesa(String Empresa)
    {
      return GetParametrosPlanSiesa(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosPlanSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosPlanSiesa> GetParametrosPlanSiesaActivo(String Empresa)
    {
      return GetParametrosPlanSiesaActivo(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los ParametrosPlanSiesa Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosPlanSiesa> GetParametrosPlanSiesaActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Activo);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<ParametrosPlanSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los ParametrosPlanSiesa que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<ParametrosPlanSiesa> GetParametrosPlanSiesaInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

      }
        var lista = ExecuteGetLista<ParametrosPlanSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto ParametrosPlanSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="CodPlan">Código del Plan</param>
    /// <returns>Objeto ParametrosPlanSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public ParametrosPlanSiesa GetUnParametrosPlanSiesa(string Empresa,string CodPlan)
    {
      ParametrosPlanSiesa menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("Empresa", Empresa));
      pars.Add(new Parametro("CodPlan", CodPlan));
      menu = ExecuteGetUnLista<ParametrosPlanSiesa>(GetInstruccionSql<ParametrosPlanSiesa>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las ParametrosPlanSiesa por Criterio
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
      return Tablas<ParametrosPlanSiesa>.ToDataTable(GetParametrosPlanSiesa(criterio ,Empresa));
    }

    /// <summary>
    /// Lista de Valores de las ParametrosPlanSiesa por Criterio Activos
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
      return Tablas<ParametrosPlanSiesa>.ToDataTable(GetParametrosPlanSiesaActivo(criterio ,Empresa));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodPlan", DescripcionCampo = "Código del Plan", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CuentaEmision", DescripcionCampo = "Cuenta Contable Emisión", TipoDato = "Cadena", Tamaño = "30" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CuentaRadicacion", DescripcionCampo = "Cuenta de Radicación", TipoDato = "Cadena", Tamaño = "30" });
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
                  string.IsNullOrEmpty(dr["CodPlan"].ToString()) && 
                  string.IsNullOrEmpty(dr["CuentaEmision"].ToString()) && 
                  string.IsNullOrEmpty(dr["CuentaRadicacion"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            ParametrosPlanSiesa dato = new ParametrosPlanSiesa();
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
            // Procesamos Código del Plan
            //
            try
            {
              dato.CodPlan = dr["CodPlan"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Plan Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodPlan"].ToString()
              });
              continue;
            }
            //
            // Procesamos Cuenta Contable Emisión
            //
            try
            {
              dato.CuentaEmision = dr["CuentaEmision"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Cuenta Contable Emisión Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CuentaEmision"].ToString()
              });
              continue;
            }
            //
            // Procesamos Cuenta de Radicación
            //
            try
            {
              dato.CuentaRadicacion = dr["CuentaRadicacion"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Cuenta de Radicación Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CuentaRadicacion"].ToString()
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
            ParametrosPlanSiesa datoBase = GetUnParametrosPlanSiesa(dato.Empresa,dato.CodPlan);
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodPlan:" + dato.CodPlan
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodPlan:" + dato.CodPlan
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodPlan:" + dato.CodPlan
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


