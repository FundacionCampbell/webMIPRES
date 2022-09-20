
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
  /// Manejo de EntidadesDinamicasSiesa
  /// </summary>
  [DataObject(true)]
  [Description("Parametrización Entidades Dinámicas")]
  public class EntidadesDinamicasSiesaDB : LiloSoft.Web.ProveedorWeb.BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public EntidadesDinamicasSiesaDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Base de Datos
    /// </summary>
    /// <param name="db">Manejador de Bases de Datos</param>
    public EntidadesDinamicasSiesaDB(DB db)
      : this()
    {
      SqlDb = db; 
    }
    #endregion


    #region Metodos de Acceso a Datos
    /// <summary>
    /// Insertar un Registro en EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="m">Objeto EntidadesDinamicasSiesa a Insertar</param>
    [DataObjectMethod(DataObjectMethodType.Insert,true)]
    public void Insert(EntidadesDinamicasSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if(!SqlDb.EjecutarComando(GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Insert),EsProcedimientoAlmacenado,lst.ToArray()))
      {
      throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Actualizar un Registro en EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="m">Objeto EntidadesDinamicasSiesa a Actualizar</param>
    [DataObjectMethod(DataObjectMethodType.Update,true)]
    public void Update(EntidadesDinamicasSiesa m)
    {
      List<Parametro> lst = GetParametros(m);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Update),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }

    /// <summary>
    /// Eliminar un Registro EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="m">Objeto EntidadesDinamicasSiesa a Eliminar</param>
    [DataObjectMethod(DataObjectMethodType.Delete,true)]    
    public void Delete(EntidadesDinamicasSiesa m)
    {
      List<Parametro> lst = GetParametros(m, true);
      if (!SqlDb.EjecutarComando(GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Delete),EsProcedimientoAlmacenado, lst.ToArray()))
      {
        throw new Exception(SqlDb.UltimoError);
      }
    }


    #endregion

    #region Metodos de Listas
    /// <summary>
    /// Lista de Todos los EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,true)]
    public List<EntidadesDinamicasSiesa> GetEntidadesDinamicasSiesa(string Criterio, String Empresa)
    {
        var lstParam = new List<Parametro>();
        var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Criterio);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<EntidadesDinamicasSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }

    /// <summary>
    /// Lista de Todos los EntidadesDinamicasSiesa
    /// </summary>      
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<EntidadesDinamicasSiesa> GetEntidadesDinamicasSiesa(String Empresa)
    {
      return GetEntidadesDinamicasSiesa(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<EntidadesDinamicasSiesa> GetEntidadesDinamicasSiesaActivo(String Empresa)
    {
      return GetEntidadesDinamicasSiesaActivo(  "", Empresa);
    }

    /// <summary>
    /// Lista de Todos los EntidadesDinamicasSiesa Activos
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<EntidadesDinamicasSiesa> GetEntidadesDinamicasSiesaActivo(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Activo);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.CriterioActivos);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

        }
        var lista = ExecuteGetLista<EntidadesDinamicasSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Lista de Todos los EntidadesDinamicasSiesa que Inician por un Criterio
    /// </summary>
    /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
    /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
    /// <returns>Lista de EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select,false)]
    public List<EntidadesDinamicasSiesa> GetEntidadesDinamicasSiesaInicia(string Criterio, String Empresa)
    {
      var lstParam = new List<Parametro>();
      var sqlText = "";
        if (string.IsNullOrEmpty(Criterio))
        {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Todos);
          lstParam.AddParametro("Empresa", Empresa);

        }
        else
        {
          sqlText = GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.Inicia);
          lstParam.AddParametro("Criterio", Criterio);
          lstParam.AddParametro("Empresa", Empresa);

      }
        var lista = ExecuteGetLista<EntidadesDinamicasSiesa>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
        return lista;
    }


    /// <summary>
    /// Busca un objeto EntidadesDinamicasSiesa
    /// </summary>
    /// <param name="Empresa">Empres de Trabajo</param>
    /// <param name="CodTipoEntidad">Código Tipo de Entidad Dinámica</param>
    /// <returns>Objeto EntidadesDinamicasSiesa</returns>
    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public EntidadesDinamicasSiesa GetUnEntidadesDinamicasSiesa(string Empresa,string CodTipoEntidad)
    {
      EntidadesDinamicasSiesa menu = null;
      List<Parametro> pars = new List<Parametro>();
      pars.Add(new Parametro("Empresa", Empresa));
      pars.Add(new Parametro("CodTipoEntidad", CodTipoEntidad));
      menu = ExecuteGetUnLista<EntidadesDinamicasSiesa>(GetInstruccionSql<EntidadesDinamicasSiesa>(TiposProcedimientosAlmacenados.SoloUno),EsProcedimientoAlmacenado, pars.ToArray());
      return menu;
    }

    #endregion

    #region Metodos Sustituidos
    /// <summary>
    /// Lista de Valores de las EntidadesDinamicasSiesa por Criterio
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
      return Tablas<EntidadesDinamicasSiesa>.ToDataTable(GetEntidadesDinamicasSiesa(criterio ,Empresa));
    }

    /// <summary>
    /// Lista de Valores de las EntidadesDinamicasSiesa por Criterio Activos
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
      return Tablas<EntidadesDinamicasSiesa>.ToDataTable(GetEntidadesDinamicasSiesaActivo(criterio ,Empresa));
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
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "Empresa", DescripcionCampo = "Empres de Trabajo", TipoDato = "Cadena", Tamaño = "6" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoEntidad", DescripcionCampo = "Código Tipo de Entidad Dinámica", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodClaseDocumento", DescripcionCampo = "Clase de Documento S1ESA", TipoDato = "Cadena", Tamaño = "10" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodGrupoEntidad", DescripcionCampo = "Código del Grupo de Entidad S1ESA", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodEntidad", DescripcionCampo = "Código de la Entidad", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodAtributo", DescripcionCampo = "Código del Atributo Entidad S1ESA", TipoDato = "Cadena", Tamaño = "50" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodMaestroEntidades", DescripcionCampo = "Código del Maestro Entidades", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodMaestroDetalleEntidad", DescripcionCampo = "Código del Maestro Detalle Entidades S1ESA", TipoDato = "Cadena", Tamaño = "20" });
      lstMeta.Add(new MetaDataExcel() { NombreCampo = "CodTipoEntidadSiesa", DescripcionCampo = "Tipo de Entidad S1ESA", TipoDato = "Cadena", Tamaño = "20" });
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
                  string.IsNullOrEmpty(dr["CodTipoEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodClaseDocumento"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodGrupoEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodAtributo"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodMaestroEntidades"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodMaestroDetalleEntidad"].ToString()) && 
                  string.IsNullOrEmpty(dr["CodTipoEntidadSiesa"].ToString()))
            {
              break; // Salimos del Ciclo si el Registro esta Vacio Todo
            }
            // Empezamos a Crear el Objeto que Necesitamos
            EntidadesDinamicasSiesa dato = new EntidadesDinamicasSiesa();
            //
            // Procesamos Empres de Trabajo
            //
            try
            {
              dato.Empresa = dr["Empresa"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Empres de Trabajo Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["Empresa"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código Tipo de Entidad Dinámica
            //
            try
            {
              dato.CodTipoEntidad = dr["CodTipoEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código Tipo de Entidad Dinámica Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Clase de Documento S1ESA
            //
            try
            {
              dato.CodClaseDocumento = dr["CodClaseDocumento"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Clase de Documento S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodClaseDocumento"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Grupo de Entidad S1ESA
            //
            try
            {
              dato.CodGrupoEntidad = dr["CodGrupoEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Grupo de Entidad S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodGrupoEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código de la Entidad
            //
            try
            {
              dato.CodEntidad = dr["CodEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código de la Entidad Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Atributo Entidad S1ESA
            //
            try
            {
              dato.CodAtributo = dr["CodAtributo"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Atributo Entidad S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodAtributo"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Maestro Entidades
            //
            try
            {
              dato.CodMaestroEntidades = dr["CodMaestroEntidades"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Maestro Entidades Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodMaestroEntidades"].ToString()
              });
              continue;
            }
            //
            // Procesamos Código del Maestro Detalle Entidades S1ESA
            //
            try
            {
              dato.CodMaestroDetalleEntidad = dr["CodMaestroDetalleEntidad"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Código del Maestro Detalle Entidades S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodMaestroDetalleEntidad"].ToString()
              });
              continue;
            }
            //
            // Procesamos Tipo de Entidad S1ESA
            //
            try
            {
              dato.CodTipoEntidadSiesa = dr["CodTipoEntidadSiesa"].ToString();
            }
            catch (Exception ex)
            {
              lstErrores.Add(new ErroresCarga()
              {
                IdError = "Tipo de Entidad S1ESA Errado",
                TextoError = ex.Message,
                FilaNo = filas,
                Info = dr["CodTipoEntidadSiesa"].ToString()
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
            EntidadesDinamicasSiesa datoBase = GetUnEntidadesDinamicasSiesa(dato.Empresa,dato.CodTipoEntidad);
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodTipoEntidad:" + dato.CodTipoEntidad
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodTipoEntidad:" + dato.CodTipoEntidad
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
                    Info = "Empresa:" + dato.Empresa + "," + "CodTipoEntidad:" + dato.CodTipoEntidad
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


