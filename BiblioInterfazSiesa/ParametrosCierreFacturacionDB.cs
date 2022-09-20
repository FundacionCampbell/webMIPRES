using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  public class ParametrosCierreFacturacionDB:BaseDB
  {
    #region Constructores
    public ParametrosCierreFacturacionDB()
    {
      ComportamientoIndividualComandos = true;
      TipoComandoIndividual = TipoComando.InstruccionSQL;
    }

    public ParametrosCierreFacturacionDB(DB pSQLDb)
      : this()
    {
      SqlDb = pSQLDb;
    }

    #endregion



    public ParametrosCierreFacturacion GetUnParametrosCierreFacturacion(string Empresa)
    {
      var sql = GetSelect<ParametrosCierreFacturacion>();
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      return ExecuteGetUnLista<ParametrosCierreFacturacion>(sql, false, lstPars.ToArray());
    }

  }
}
