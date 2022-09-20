using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Utils;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  public class EntidadesDB:BaseDB
  {
    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public EntidadesDB() : base() {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
    }
    /// <summary>
    /// Constructor con Objeto de Acceso a Datos
    /// </summary>
    /// <param name="pSqlDb"></param>
    public EntidadesDB(DB pSqlDb) : this()
    {
      this.SqlDb = pSqlDb;
    }
    #endregion

    #region Métodos de Acceso a Datos
    public List<Entidades> GetEntidades(string Criterio, string Empresa)
    {
      var lstParam = new List<Parametro>();
      lstParam.AddParametro("Empresa", Empresa);
      var sqlText = "";
      if (string.IsNullOrEmpty(Criterio))
      {
        sqlText = GetInstruccionSql<Entidades>(TiposProcedimientosAlmacenados.Activo);
      }
      else
      {
        sqlText = GetInstruccionSql<Entidades>(TiposProcedimientosAlmacenados.CriterioActivos);
        lstParam.AddParametro("Criterio", Criterio);

      }
      var lista = ExecuteGetLista<Entidades>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
      return lista;

    }
    #endregion

    #region Listas de Valores
    /// <summary>
    /// Lista de Valores de las EstadosTrazabilidad por Criterio
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetLista(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {
      EmpresaConeccion emp = null;
      var empresa = parametros.ValorParametro<string>("Empresa");
      var SqlDbMysql = ConsultaInstitucionDB.ObtenerBaseMySqL(empresa, SqlDb, ref emp);
      var EmpresaEsculapio = emp.EmpresaEsculapio;
      var dataDb  = new EntidadesDB(SqlDbMysql);
      return Tablas<Entidades>.ToDataTable(dataDb.GetEntidades(criterio, EmpresaEsculapio));
    }

    /// <summary>
    /// Lista de Valores de las EstadosTrazabilidad por Criterio Activos
    /// </summary>
    /// <param name="criterio">Criterio de Busqueda</param>
    /// <param name="parametros">Lista de Parametros</param>
    /// <returns></returns>
    public override DataTable GetListaActiva(string criterio, List<LiloSoft.Web.ProveedorWeb.LOVParams> parametros)
    {

      return GetLista(criterio, parametros);
    }

    #endregion

  }//fin clase
}//fin namespace
