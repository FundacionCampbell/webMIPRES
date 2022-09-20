using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz.Controllers
{
  public class BaseInterfazController : BaseController
  {
    protected string EmpresaEsculapio { get; set; }
    protected DB SqlDbMysql = null;
    protected ConsultaInstitucionDB dataMainDb = null;
    protected Exception UltimoError = null;
    private bool DatosMySql()
    {
      var resp = true;
      try
      {
        EmpresaConeccion emp = null;
        SqlDbMysql = ConsultaInstitucionDB.ObtenerBaseMySqL(Empresa, SqlDb, ref emp);
        EmpresaEsculapio = emp.EmpresaEsculapio;
        dataMainDb = new ConsultaInstitucionDB(SqlDbMysql);
      }
      catch (Exception ex)
      {
        UltimoError = ex;
        resp = false;
        //RedirectToAction("ErrorConexion", "ErrorServidor", new { Error = ex });
      }
      return resp;
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      base.OnActionExecuting(filterContext);
      if (Empresa.IsNullOrEmpty())
        filterContext.Result = RedirectToAction("Seleccionar", "Empresas");
      else
      {
        if (!DatosMySql())
          filterContext.Result = //View("ErrorConexion", UltimoError);
        RedirectToAction("ErrorConexion", "Home", new { Message = UltimoError.MessageExtent() });
      }
    }

    protected bool SaveMysql(Action salvar)
    {
      var resp = true;
      try
      {
        if (!SqlDbMysql.AbrirConexion()) throw SqlDb.UltimaExcepcion;
        SqlDbMysql.IniciarTransaccion();
        salvar();
        SqlDbMysql.FinalizarTransaccion();
      }
      catch (Exception ex)
      {
        if (SqlDbMysql.IsTransaccionEnCurso) SqlDbMysql.CancelarTransaccion();
        ModelState.AddModelError("", ex.MessageExtent());
        resp = false;
      }
      return resp;
    }

  }//fin clase
}//fin  namespace