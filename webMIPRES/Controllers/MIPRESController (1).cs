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
using System.Net.Http;
using System.Text;
using System.IO;
using webMIPRES.Models;
using System.Web.Script.Serialization;
using LiloSoft.DataBase.ConectaDB;
using LiloSoft.DataBase;

namespace webMIPRES.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MIPRESController : BaseInterfazController
    {
        public string NomEmpresa;
        public string CodigoEmp;
        public string userRole;
        public string IpServidor;
        public string Puerto;
        public string BaseDatos;
        public string Usuario;
        public string Clave;

        // GET: MIPRES
        public ActionResult Index()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string model)
        {
            return View();
        }

        public ActionResult IndexPrescripcion()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;

            return View();
        }

        [HttpPost]
        public ActionResult IndexPrescripcion(string model)
        {
            return View();
        }

        public ActionResult IndexEntrega()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;

            return View();
        }

        [HttpPost]
        public ActionResult IndexEntrega(string model)
        {
            return View();
        }

        public ActionResult IndexReporte()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;

            return View();
        }

        [HttpPost]
        public ActionResult IndexReporte(string model)
        {
            return View();
        }

        public ActionResult ValidaToken()
        {
            Session["TokenGenerado"] = string.Empty;
            Session["NitEmpresa"] = string.Empty;
            Session["TokemEmp"] = string.Empty;

            return View();
        }

        [HttpPost]
        public ActionResult ValidaToken(ValidaTokenModel model, FormCollection formreg)
        {
            Session["TokenGenerado"] = Request.Form["hfTokenGenerado"].ToString();
            Session["NitEmpresa"] = Request.Form["hfNit"].ToString();
            Session["TokemEmp"] = Request.Form["hfTokenEmp"].ToString();
            return View();
        }

        public ActionResult IndexFacturacion()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;

            return View();
        }

        [HttpPost]
        public ActionResult IndexFacturacion(string model)
        {
            return View();
        }

        private DB ObtenerConexion()
        {
            CodigoEmp = (string)Session["CodEmpresa"];
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

        public ActionResult InformeMIPRES()
        {
            return View();
        }

        // Visuliazacion en PDF de Instructivo de Eventos Aderso
        public ActionResult ViewPDF()
        {
            ViewBag.scripCall = "AbrePdf();";
            return View();
        }

        [HttpPost]
        public ActionResult ViewPDF(string verpdf)
        {
            ViewBag.scripCall = "AbrePdf();";
            return View();
        }

        public ActionResult ViewFacpdf()
        {
            ViewBag.scripCall = "AbrePdf();";
            return View();
        }

        [HttpPost]
        public ActionResult ViewFacpdf(string verpdf)
        {
            ViewBag.scripCall = "AbrePdf();";
            return View();
        }


        public ActionResult Consultas()
        {
            return View();
        }

        public ActionResult GetPrescripcionesFecha()
        {
            return View();
        }

        public ActionResult GetNumeroPrescripcion()
        {
            //ViewBag.Token = Session["TokenGenerado"].ToString();
            return View();
        }

        public ActionResult GetDatosCasos(MedicamentoModel model)
        {
            //model.TokenGenerado = Session["TokenGenerado"].ToString();
            return PartialView("GetDatosCasos", model);
        }

        public ActionResult GetEntregasFechas()
        {
            return View();
        }

        public ActionResult GetPrescripcionEntrega()
        {
            return View();
        }

        public ActionResult GetReportesFechas()
        {
            return View();
        }

        public ActionResult GetPrescripcionReporte()
        {
            return View();
        }


        // Conexion Web Services
        [HttpPost]
        public ActionResult GetParametros()
        {
            ObtenerConexion();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var Resultado = dataDb.GetParametrosMIPRES(CodigoEmp);

            return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGenerarToken(string Uri, string nit, string token)
        {
            object result = string.Empty;
            try
            {
                string UrlPage = Uri;
                UrlPage += nit + "/" + token;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(UrlPage);
                request.Method = "GET";
                string test = string.Empty;
                result = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject(test);
                    reader.Close();
                    dataStream.Close();

                    Session["TokenGenerado"] = test;
                }
            }
            catch (WebException ex)
            {
                //string text = "ERROR: " + ex.Message.ToString();
                string exMessage = ex.Message;

                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        exMessage = responseReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject(exMessage);
                    }
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPrescripciones(string Uri, string nit, Fecha fecha, string token, string tipo)
        {
            object result = string.Empty;
            try
            {
                string UrlPage = Uri;
                string dateFormatted = fecha.ToString("yyyy-MM-dd");
                UrlPage += nit + "/" + dateFormatted + "/" + token;

                if (tipo == "E")
                {
                    UrlPage = Uri + "/" + nit + "/" + token + "/" + dateFormatted;
                }


                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(UrlPage);
                request.Method = "GET";
                string test = string.Empty;
                result = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject(test);
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (WebException ex)
            {
                //string text = "ERROR: " + ex.Message.ToString();
                string exMessage = ex.Message;

                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        exMessage = responseReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject(exMessage);
                    }
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnaPrescripcion(string Uri, string nit, string numero, string token)
        {
            object result = string.Empty;
            try
            {
                string UrlPage = Uri;
                UrlPage += nit + "/" + token + "/" + numero;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(UrlPage);
                request.Method = "GET";
                string test = string.Empty;
                result = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject(test);
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (WebException ex)
            {
                //string text = "ERROR: " + ex.Message.ToString();
                string exMessage = ex.Message;

                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        exMessage = responseReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject(exMessage);
                    }
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PutEntregaAmbito(string Uri, string nit, string token, string Datos)
        {
            object result = string.Empty;

            try
            {
                string UrlPage = Uri;
                UrlPage += nit + "/" + token;
                var test = string.Empty;
                string respuesta = string.Empty;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(UrlPage);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var datJSON = JsonConvert.DeserializeObject(Datos);
                    string json = JsonConvert.SerializeObject(datJSON);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseText = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject(responseText);
                }
            }
            catch (WebException ex)
            {
                //string text = "ERROR: " + ex.Message.ToString();
                string exMessage = ex.Message;

                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        exMessage = responseReader.ReadToEnd();
                        //string respuesta = "ERROR:" + exMessage;
                        result = JsonConvert.DeserializeObject(exMessage);
                    }
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PutAnularEntrega(string Uri, string nit, string token, string IdEntrega)
        {
            object result = string.Empty;
            try
            {
                string UrlPage = Uri;
                UrlPage += nit + "/" + token + "/" + IdEntrega + "/";

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(UrlPage);
                request.Method = "PUT";
                request.ContentLength = 0;
                string test = string.Empty;
                result = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject(test);
                    reader.Close();
                    dataStream.Close();

                }
            }
            catch (WebException ex)
            {
                //string text = "ERROR: " + ex.Message.ToString();
                string exMessage = ex.Message;

                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        exMessage = responseReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject(exMessage);
                    }
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        // Metodos Esculapio

        [HttpPost]
        public JsonResult GetPacienteIngreso(string NoHistoria)
        {
            ObtenerConexion();
            DataTable dtPaciente = new DataTable();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetPacienteIngreso(CodigoEmp.ToString(), NoHistoria);

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPacienteDistrito(string NoHistoria)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetPacienteDistrito(NoHistoria); //CodigoEmp.tostring();

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEgresosCasos(string NoCuenta)
        {
            ObtenerConexion();
            DataTable dtEgreso = new DataTable();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetEgresosCasos(CodigoEmp.ToString(), Convert.ToInt32(NoCuenta));

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetExtractoCasos(string NoCuenta)
        {
            ObtenerConexion();
            DataTable dtExtracto = new DataTable();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetResponsableMIPRES(CodigoEmp.ToString(), Convert.ToInt32(NoCuenta));

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDetalleExtractoMIPRES(string ConsInterno, string NoCuenta)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtDetalle = new DataTable();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetDetalleExtractoMIPRES(this.CodigoEmp, Convert.ToInt32(NoCuenta), Convert.ToInt32(ConsInterno));
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetValidaEntrega(string NoPrescripcion, string CodigoCum, string tipo)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var result = dataDb.GetValidaEntrega(CodigoEmp.ToString(), NoPrescripcion, CodigoCum, tipo);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult RegistrarTokenGen(string Nit, string token)
        {
            ObtenerConexion();
            string Empresa = CodigoEmp;
            var result = string.Empty;

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            var sql = "Act_ParametrosMIPRES";
            var param = new List<Parametro>();
            param.AddParametro("Empresa", Empresa.ToString());
            param.AddParametro("NoToken", token);
            param.AddParametro("Nit", Nit);

            if (!SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                throw SqlDbMysql.UltimaExcepcion;

            result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarEntrega(string NoPrescripcion, string NoCuenta, string NoHistoria, string NoIdentificacion, string NitEntidad, string Responsable, string CodEPS, string CodigoCum, decimal NoId, decimal IdEntrega, string CantEnt, string PrecioUni, string ValEntrega)
        {
            ObtenerConexion();
            string Empresa = CodigoEmp;
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            var sql = "Insert_EntregaMipres";
            var param = new List<Parametro>();
            param.AddParametro("Empresa", Empresa);
            param.AddParametro("NoPrescripcion", NoPrescripcion);
            param.AddParametro("NoCuenta", NoCuenta);
            param.AddParametro("NoHistoria", NoHistoria);
            param.AddParametro("NoIdentificacion", NoIdentificacion);
            param.AddParametro("NitEntidad", NitEntidad);
            param.AddParametro("Responsable", Responsable);
            param.AddParametro("CodEPS", CodEPS);
            param.AddParametro("CodigoCum", CodigoCum);
            param.AddParametro("NoId", NoId);
            param.AddParametro("IdEntrega", IdEntrega);
            param.AddParametro("CantEnt", Convert.ToInt32(CantEnt));
            param.AddParametro("PrecioUni", Convert.ToInt32(PrecioUni));
            param.AddParametro("ValEntrega", Convert.ToInt32(ValEntrega));

            if (!SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                throw SqlDbMysql.UltimaExcepcion;

            var result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReporteEntrega(string NoPrescripcion, string CodigoCum, decimal IdReporteEntrega, decimal IdAnulacion, string Estado)
        {
            string Empresa = CodigoEmp;
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            var sql = "Act_EntregaMipres";
            var param = new List<Parametro>();
            param.AddParametro("Empresa", Empresa.ToString());
            param.AddParametro("NoPrescripcion", NoPrescripcion);
            param.AddParametro("CodigoCum", CodigoCum);
            param.AddParametro("IdReporteEntrega", IdReporteEntrega);
            param.AddParametro("IdAnulacion", IdAnulacion);
            param.AddParametro("Estado", Estado);
            if (!SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                throw SqlDbMysql.UltimaExcepcion;

            var result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetReportePrescripciones(Fecha FechaIni, Fecha FechaFin)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtReporte = new DataTable();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            List<entrega_mipres> result = dataDb.GetReportePrescripciones(CodigoEmp.ToString(), FechaIni, FechaFin);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEntregaMipres(string NoPrescripcion, string IdEntrega)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtEntrega = new DataTable();
            ObtenerConexion();

            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(this.SqlDbMysql);
            List<entrega_mipres> result = dataDb.GetEntregaMipres(this.CodigoEmp, NoPrescripcion, IdEntrega);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        //---- Facturacion

        [HttpPost]
        public ActionResult GetCuentaMIPRES(string NoIdentificacion)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtCuenta = new DataTable();
            ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            List<PacientesSIRAS> result = dataDb.GetCuentaMIPRES(CodigoEmp, NoIdentificacion);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult lovEPS(string NoCuenta)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtEPS = new DataTable();
            this.ObtenerConexion();

            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            List<ConsultaResponsables> result = dataDb.GetEpsMIPRES(this.CodigoEmp, Convert.ToInt32(NoCuenta));
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult lovFactEPS(string NoCuenta, string NitEntidad)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtFactEPS = new DataTable();
            this.ObtenerConexion();

            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            List<ConsultaResponsables> result = dataDb.GetFactResponsables(this.CodigoEmp, Convert.ToInt32(NoCuenta), NitEntidad);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult PutFacturacion(string Uri, string nit, string token, string Datos)
        {
            object result = string.Empty;
            try
            {
                string UrlPage = string.Concat(Uri, nit, "/", token);
                string test = string.Empty;
                string respuesta = string.Empty;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(UrlPage);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Datos)));
                }
                using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
                }
            }
            catch (WebException webException)
            {
                WebException ex = webException;
                string exMessage = ex.Message;
                if (ex.Response != null)
                {
                    using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        result = JsonConvert.DeserializeObject(responseReader.ReadToEnd());
                    }
                }
            }
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegistraFacturacion(string NoPrescripcion, decimal IdEntrega, string NoFactura, string CodEPS, decimal CantEnt, decimal ValorUni, decimal ID_F, decimal IdFacturacion)
        {
            ObtenerConexion();
            string Empresa = this.CodigoEmp;
            string sql = "Act_FacturaMipres";
            string result = string.Empty;

            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            List<Parametro> param = new List<Parametro>();
            param.AddParametro("Empresa", Empresa);
            param.AddParametro("NoPrescripcion", NoPrescripcion);
            param.AddParametro("IdEntrega", IdEntrega);
            param.AddParametro("NoFactura", NoFactura);
            param.AddParametro("CodEPS", CodEPS);
            param.AddParametro("CantEnt", CantEnt);
            param.AddParametro("ValorUni", ValorUni);
            param.AddParametro("ID_F", ID_F);
            param.AddParametro("IdFacturacion", IdFacturacion);
            if (!this.SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
            {
                throw this.SqlDbMysql.UltimaExcepcion;
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
    




        //--

        public ActionResult Pendientes()
        {
            return View();
        }

        public ActionResult Entregadas()
        {
            return View();
        }




    }
}