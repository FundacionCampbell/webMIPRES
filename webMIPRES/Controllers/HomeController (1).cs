using LiloSoft.Types.Data;
using LiloSoft.Utils;
using LiloSoft.Web.Mvc.Controllers;
using LiloSoft.Web.Mvc.Models;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiloSoft.Siesa.Interfaz.Controllers;
using LiloSoft.Siesa.Interfaz;
using Newtonsoft.Json;
using LiloSoft.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Data;
using webMIPRES.Models;
using SIS.EsculapioWeb.HistoriaClinica.Models.PruebaEsculapio;
using LiloSoft.DataBase;
using LiloSoft.DataBase.ConectaDB;

namespace webMIPRES.Controllers
{
    public class HomeController : BaseInterfazController
    {
        public string NomEmpresa;

        public string userId;
        public string IpServidor;
        public string Puerto;
        public string BaseDatos;
        public string Usuario;
        public string Clave;

        public ActionResult Index()
        {
            var model = new EsculapioDetCuentaInputModel();
            model.Empresa = Empresa;

            //-- Obtener Empresa por Ip de Conexion  ==  IPAddressClient; //Request.UserHostAddress; // 

            string userId = (string)Session["userID"];
            if (string.IsNullOrEmpty(userId))
            {
                Session["CodEmpresa"] = "";
                Session["IpConexion"] = "";
                Session["IpPuerto"] = "";
                Session["BaseDatos"] = "";
                Session["userconexion"] = "";
                Session["passconexion"] = "";
                Session["userRole"] = "";
                Session["UserName"] = "";
                Session["NameCompany"] = "";
                Get_EmpresasWeb();
            }

            if (userId == null)
            {
                return RedirectToAction("LogIn");
            }
            else
            {
                return View("Index");
            }

        }


        //.... LOGIN
        #region  LOGIN USUARIOS
        // Login en view partial
        public ActionResult LogIn()
        {

            var cnxEmpresa = Session["CodEmpresa"]; //System.Web.HttpContext.Current.Session["Empresa"];
            //ViewBag.CodEmpresa = cnxEmpresa;
            ViewBag.MessageError = "";
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(UsuariosModel user, FormCollection formcollection)
        {
            string CodigoEmp = Request.Form["hfCodigoEmp"].ToString();
            Get_UnaEmpresasWeb(CodigoEmp);
            user.usuario = Request.Form["usuario"].ToString();
            user.PassWordUsu = Request.Form["PassWordUsu"].ToString();
            try
            {
                DB dbValida = ObtenerConexion();
                if (!dbValida.AbrirConexion())
                {
                     ViewBag.MessageError = "Invalido Usuario y/o Password";
                     return View("LogIn", user);
                }
                else if (ValidaUsuario(user.usuario, user.PassWordUsu))
                {
                    Session["userID"] = user.usuario;
                    return View("Index");
                }
                else
                {
                    ViewBag.MessageError = "Usuario no tiene perfil para este modulo";
                    return View("LogIn", user);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool ValidaUsuario(string usuario, string passusu)
        {
            bool bool_resp = false;
            string CodigoEmpresa = Session["CodEmpresa"].ToString();
            DataTable dtUsuario = new DataTable();
            string IdUsuario = string.Empty;
            string IdRole = string.Empty;
            string NomUsua = string.Empty;
            ObtenerConexion();
            if (CodigoEmpresa != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=" + usuario + "; Password=" +  passusu + ";port=" + Puerto + ";database=" + BaseDatos + ";" );
                string sql = "SELECT a.usuario,a.Role,b.Nombre FROM usuarios_mipres a Inner Join usuarios b On a.usuario=b.usuario ";
                MySqlCommand comm = new MySqlCommand(sql + "WHERE a.usuario='" + usuario + "' And a.Empresa='" + CodigoEmpresa + "' AND a.Estado='A'" , DBConnect);
                DBConnect.Open();
                dtUsuario.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<ConsultaUsuariosAdv> lstUsuario = dtUsuario.AsEnumerable().Select(m => new ConsultaUsuariosAdv()
                 {
                    Role = m.Field<string>("Role"),
                    Nombre = m.Field<string>("Nombre"),
                    Usuario = m.Field<string>("Usuario")
                }).ToList();

                foreach (ConsultaUsuariosAdv item in lstUsuario)
                {
                    IdUsuario = item.Usuario;
                    IdRole = item.Role;
                    NomUsua = item.Nombre;
                }
            }
            else
            {
                foreach (ConsultaUsuariosAdv item in (new ConsultasEsculapioDB(SqlDbMysql).GetUsuarioMIPRES(CodigoEmpresa, usuario)))
                {
                    IdUsuario = item.Usuario;
                    IdRole = item.Role;
                    NomUsua = item.Nombre;
                }
            }
            if (IdUsuario != "")
            {
                Session["userRole"] = IdRole;
                Session["UserName"] = NomUsua;
                bool_resp = true;
            }
            return bool_resp;
        }

        public ActionResult LogOut()
        {
            string userId = (string)Session["userID"];
            Session.Abandon();
            Session["userID"] = null;
            return RedirectToAction("Index", "Home");
        }

        ///
        #endregion
        //---------------------------------------
        //// Captura Empresa de Conexion
        [HttpPost]
        public ActionResult Get_EmpresasWeb()
        {
            ConsultaInstitucionDB dataDb = new ConsultaInstitucionDB(SqlDb);
            List<SelectListItem> li = new List<SelectListItem>();
            List<EmpresasWeb> result = dataDb.Get_EmpresasWeb();
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        private void Get_UnaEmpresasWeb(string CodEmpresa)
        {
            ConsultaInstitucionDB dataDb = new ConsultaInstitucionDB(SqlDb);
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (EmpresasWeb item in dataDb.Get_UnaEmpresasWeb(CodEmpresa))
            {
               ViewBag.EsculapioEMP = item.EmpresaEsculapio;
               ViewBag.ipConn = item.Servidor;
              ViewBag.ipport = item.Puerto;
               ViewBag.ipBd = item.BaseDatos;
                Session["NameCompany"] = item.NombreEmpresa;
            }
            Session["CodEmpresa"] = ViewBag.EsculapioEMP;
            Session["IpConexion"] = Request.Form["hfIpConexion"].ToString();
            Session["BaseDatos"] = Request.Form["hfBaseDB"].ToString();
            Session["IpPuerto"] = Request.Form["hfPuerto"].ToString();
            Session["userconexion"] = Request.Form["usuario"].ToString();
            Session["passconexion"] = Request.Form["PassWordUsu"].ToString();
            ObtenerConexion();
        }

        private DB ObtenerConexion()
        {
            IpServidor = (string)Session["IpConexion"];
            Puerto = (string)Session["IpPuerto"];
            BaseDatos = (string)Session["BaseDatos"];
            Usuario = (string)Session["userconexion"];
            Clave = (string)Session["passconexion"];
            SqlDbMysql.Servidor = IpServidor;
            SqlDbMysql.Puerto = Puerto;
            SqlDbMysql.BaseDatos = BaseDatos;
            SqlDbMysql.Usuario = Usuario;
            SqlDbMysql.Clave = Clave;
            DB db = DB.NuevaDB(ManejadorBaseDatos.MySql);
            db.Servidor = SqlDbMysql.Servidor;
            db.BaseDatos = SqlDbMysql.BaseDatos;
            db.Puerto = SqlDbMysql.Puerto;
            db.Usuario = SqlDbMysql.Usuario;
            db.Clave = Clave;
            return db;
        }

        // --------
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}