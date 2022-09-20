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

        public ActionResult HomeFacturacion()
        {
            userRole = (string)Session["userRole"];
            (ViewBag).Role = userRole;
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
            DataTable dtParametro = new DataTable();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            List<parametros_mipres> Resultado = new List<parametros_mipres>();

            if (CodigoEmp != "C30") {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor +";user id=jgarcia;Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "Select t.Nit,t.token,t.tokenproveedor,t.Maneja_Ws,t.Empresa,t.UrlPrescripciones,t.UrlUnaPrescripcion,t.UrlValidaToken,t.UrlEntregaAmbito,t.UrlReporteEntrega,t.TokenGenerado,t.UrlEntregasFechas,t.UrlAnularNoEntrega,t.UrlAnularReporteEntrega,t.UrlEntregaPrescripcion,";
                sql += "t.UrlReportePrescripcion,t.UrlReportesFechas,t.UrlFacturacion From parametros_mipres t ";
                sql += "Where t.Maneja_Ws = 'S' and t.Empresa = '" + CodigoEmp + "' ";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtParametro.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<parametros_mipres> lstParametro = dtParametro.AsEnumerable().Select(m => new parametros_mipres()
                {
                    Empresa = m.Field<string>("Empresa"),
                    Nit = m.Field<string>("Nit"),
                    token = m.Field<string>("token"),
                    tokenproveedor = m.Field<string>("tokenproveedor"),
                    Maneja_Ws = m.Field<string>("Maneja_Ws"),
                    UrlPrescripciones = m.Field<string>("UrlPrescripciones"),
                    UrlValidaToken = m.Field<string>("UrlValidaToken"),
                    UrlEntregaAmbito = m.Field<string>("UrlEntregaAmbito"),
                    UrlReporteEntrega = m.Field<string>("UrlReporteEntrega"),
                    UrlUnaPrescripcion = m.Field<string>("UrlUnaPrescripcion"),
                    TokenGenerado = m.Field<string>("TokenGenerado"),
                    UrlEntregasFechas = m.Field<string>("UrlEntregasFechas"),
                    UrlAnularNoEntrega = m.Field<string>("UrlAnularNoEntrega"),
                    UrlAnularReporteEntrega = m.Field<string>("UrlAnularReporteEntrega"),
                    UrlEntregaPrescripcion = m.Field<string>("UrlEntregaPrescripcion"),
                    UrlReportePrescripcion = m.Field<string>("UrlReportePrescripcion"),
                    UrlReportesFechas = m.Field<string>("UrlReportesFechas"),
                    UrlFacturacion = m.Field<string>("UrlFacturacion")
                }).ToList();
                Resultado = lstParametro.ToList();
            }
            else
            {
                Resultado = dataDb.GetParametrosMIPRES(CodigoEmp);
            }

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
            if (CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor +";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";" );
                string sql = "SELECT c.empresa, c.nocuenta, c.nohistoria as Identificacion, c.fechaingreso, CONCAT_WS(' ', p.nombre1, p.nombre2, p.apellido1, p.apellido2) As Paciente FROM Cuenta c INNER JOIN Pacientes p ON c.NoHistoria = p.NoHistoria ";
                sql += "WHERE c.Estado in ('A', 'C') And c.NoHistoria = '" + NoHistoria + "' ";
                sql += "Order by c.nocuenta Desc";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtPaciente.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<PacientesSIRAS> Resultado = new List<PacientesSIRAS>();
                foreach (DataRow dr in dtPaciente.Rows)
                {
                    PacientesSIRAS pacientesSIRA = new PacientesSIRAS()
                    {
                        Empresa = dr["Empresa"].ToString(),
                        NoCuenta = dr["NoCuenta"].ToString(),
                        Identificacion = dr["Identificacion"].ToString()
                    };
                    DateTime dateTime = Convert.ToDateTime(dr["FechaIngreso"].ToString());
                    pacientesSIRA.FechaIngreso = dateTime.Date;
                    pacientesSIRA.Paciente = dr["Paciente"].ToString();
                    Resultado.Add(pacientesSIRA);
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<PacientesSIRAS> Resultado = dataDb.GetPacienteIngreso(CodigoEmp, NoHistoria);
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
                //var result = dataDb.GetPacienteIngreso(CodigoEmp.ToString(), NoHistoria);
            }
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
        public JsonResult GetEgresosCasos(string NoCuenta, string EmpCaso)
        {
            ObtenerConexion();
            DataTable dtEgreso = new DataTable();
            List<SelectListItem> li = new List<SelectListItem>();

            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

             ObtenerConexion();
            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";" );
                string sql = "SELECT c.empresa, c.Nocuenta, a.NoAdmision, a.fechaIngreso, a.horaingreso, s.nombre AS Servicio,c.estado, a.fechaegreso, a.horaEgreso, a.estado AS estadoIng,";
                sql += "a.Estado AS EstAdm, st.nombre AS servTras, a.no_Autorizacion, a.autorizado_Por ";
                sql += "FROM Cuenta c INNER JOIN Admisiones a ON c.empresa = a.empresa AND c.nocuenta = a.nocuenta AND a.fechaegreso IS NOT NULL ";
                sql += "INNER JOIN Servicios_Clinica s ON c.empresa = s.empresa AND a.codservicio = s.Cod_Servicio " ;
                sql += "LEFT OUTER JOIN Admisiones AT ON a.empresa = at.empresa AND at.noadmision = a.noadmisionTras ";
                sql += "LEFT OUTER JOIN Servicios_Clinica st ON at.empresa = st.empresa AND at.codservicio = st.Cod_Servicio ";
                sql += "WHERE c.Empresa = '" + EmpCaso + "' AND c.Estado<>'X' And c.NoCuenta = " + Convert.ToInt32(NoCuenta) + " " ;
                sql += "Order by a.fechaIngreso DESC, a.horaingreso DESC";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtEgreso.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<EgresosCasos> Resultado = new List<EgresosCasos>();
                foreach (DataRow dr in dtEgreso.Rows)
                {
                    EgresosCasos egresosCaso = new EgresosCasos()
                    {
                        empresa = dr["empresa"].ToString(),
                        NoCuenta = dr["NoCuenta"].ToString(),
                        NoAdmision = dr["NoAdmision"].ToString()
                    };
                    DateTime dateTime = Convert.ToDateTime(dr["fechaIngreso"].ToString());
                    egresosCaso.fechaIngreso = dateTime.Date;
                    egresosCaso.horaingreso = dr["horaingreso"].ToString();
                    egresosCaso.Servicio = dr["Servicio"].ToString();
                    egresosCaso.estado = dr["estado"].ToString();
                    dateTime = Convert.ToDateTime(dr["fechaegreso"].ToString());
                    egresosCaso.fechaegreso = dateTime.Date;
                    egresosCaso.horaEgreso = dr["horaEgreso"].ToString();
                    egresosCaso.estadoIng = dr["estadoIng"].ToString();
                    egresosCaso.EstAdm = dr["EstAdm"].ToString();
                    egresosCaso.servTras = dr["servTras"].ToString();
                    egresosCaso.no_Autorizacion = dr["no_Autorizacion"].ToString();
                    egresosCaso.autorizado_Por = dr["autorizado_Por"].ToString();
                    Resultado.Add(egresosCaso);
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<EgresosCasos> Resultado = dataDb.GetEgresosCasos(CodigoEmp, Convert.ToInt32(NoCuenta));
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
                //var result = dataDb.GetEgresosCasos(CodigoEmp.ToString(), Convert.ToInt32(NoCuenta));
            }




        }

        [HttpPost]
        public JsonResult GetExtractoCasos(string NoCuenta, string EmpCaso)
        {
            ObtenerConexion();
            DataTable dtExtracto = new DataTable();
            List<SelectListItem> li = new List<SelectListItem>();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "SELECT f.nitentidad, e.nombre_entidad, e.Cod_Ent_Admin,f.ConsInterno,f.CodConvenio,f.NoFactura,cv.CodEsquemaTar, cv.TipoTopeSoat, cv.soat, cv.CopagoPorNivel, f.ValorFactura, f.Valor_Descuento,";
                sql += "f.Valor_coopago,f.valor_moderadora,CONCAT(e.nombre_entidad, '-', e.Cod_Ent_Admin) AS NomEPS ";
                sql += "FROM facturas f INNER JOIN entidades e ON f.empresa = e.empresa AND f.Nitentidad = e.nitentidad ";
                sql += "INNER JOIN convenios cv ON f.empresa = cv.empresa AND f.codconvenio = cv.cod_convenio ";
                sql += "WHERE f.empresa = '" + EmpCaso + "' AND f.nocuenta = " + Convert.ToInt32(NoCuenta) + " AND f.estado IN( 'F','A','CI','C') " ;
                sql += "Order by f.consinterno";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtExtracto.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<ConsultaResponsables> Resultado = new List<ConsultaResponsables>();
                foreach (DataRow dr in dtExtracto.Rows)
                {
                    Resultado.Add(new ConsultaResponsables()
                    {
                        NitEntidad = dr["nitentidad"].ToString(),
                        Nombre_Entidad = dr["nombre_entidad"].ToString(),
                        Cod_Ent_Admin = dr["Cod_Ent_Admin"].ToString(),
                        ConsInterno = Convert.ToDecimal(dr["ConsInterno"].ToString()),
                        CodConvenio = dr["CodConvenio"].ToString(),
                        Nofactura = dr["Nofactura"].ToString(),
                        CodEsquemaTar = dr["CodEsquemaTar"].ToString(),
                        TipoTopeSoat = dr["TipoTopeSoat"].ToString(),
                        soat = dr["soat"].ToString(),
                        CopagoPorNivel = dr["CopagoPorNivel"].ToString(),
                        ValorFactura = Convert.ToDecimal(dr["ValorFactura"].ToString()),
                        Valor_Descuento = Convert.ToDecimal(dr["Valor_Descuento"].ToString()),
                        NomEPS = dr["NomEPS"].ToString(),
                        Valor_coopago = Convert.ToDecimal(dr["Valor_coopago"].ToString()),
                        valor_moderadora = Convert.ToDecimal(dr["valor_moderadora"].ToString())
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ConsultaResponsables> Resultado = dataDb.GetResponsableMIPRES( CodigoEmp, Convert.ToInt32(NoCuenta));
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetDetalleExtractoMIPRES(string ConsInterno, string NoCuenta, string EmpCaso)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtDetalle = new DataTable();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "SELECT a.NoCuenta,a.fecha_cargo,a.ConsInternofactura,a.Codservicio AS CodigoArt, a.NombreServicio AS NombreArticulo,a.cantidad,a.precio,(a.cantidad * a.precio) AS total,";
                sql += "a.CodigoCUM AS CumArt, a.CodigoCUM, a.CodigoCUM AS CodigoCUM_Alterno ";
                sql += "FROM cuenta_detalle a ";
                sql += "WHERE a.nocuenta = " + Convert.ToInt32(NoCuenta) + " AND a.empresa = '" + EmpCaso + "' AND a.depInterno = '09' AND a.estadoorden = 'G' " ;
                sql += "AND a.ConsInternofactura = " + Convert.ToInt32(ConsInterno) + " Order by a.NombreServicio,a.CodServicio, a.fecha_cargo";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtDetalle.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<DetalleExtractoMIPRES> Resultado = new List<DetalleExtractoMIPRES>();
                foreach (DataRow dr in dtDetalle.Rows)
                {
                    Resultado.Add(new DetalleExtractoMIPRES()
                    {
                        NoCuenta = dr["NoCuenta"].ToString(),
                        fecha_cargo = dr["fecha_cargo"].ToString(),
                        ConsInternofactura = Convert.ToDecimal(dr["ConsInternofactura"].ToString()),
                        CodigoArt = dr["CodigoArt"].ToString(),
                        NombreArticulo = dr["NombreArticulo"].ToString(),
                        cantidad = Convert.ToDecimal(dr["cantidad"].ToString()),
                        precio = Convert.ToDecimal(dr["precio"].ToString()),
                        total = Convert.ToDecimal(dr["total"].ToString()),
                        CumArt = dr["CumArt"].ToString(),
                        CodigoCUM = dr["CodigoCUM"].ToString(),
                        CodigoCUM_Alterno = dr["CodigoCUM_Alterno"].ToString()
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<DetalleExtractoMIPRES> Resultado = dataDb.GetDetalleExtractoMIPRES(CodigoEmp, Convert.ToInt32(NoCuenta), Convert.ToInt32(ConsInterno));
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
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
            if (Empresa != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                MySqlCommand comm = new MySqlCommand(sql, DBConnect)
                {
                    CommandText = "Act_ParametrosMIPRES",
                    CommandType = CommandType.StoredProcedure
                };
                comm.Parameters.AddWithValue("@parEmpresa", Empresa);
                comm.Parameters["@parEmpresa"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNit", Nit);
                comm.Parameters["@parNit"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parToken", token);
                comm.Parameters["@parToken"].Direction = ParameterDirection.Input;
                try
                {
                    try
                    {
                        DBConnect.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        result = string.Concat("Error:", exception.Message);
                    }
                }
                finally
                {
                    DBConnect.Close();
                }
            }
            else
            {
                List<Parametro> param = new List<Parametro>();
                param.AddParametro("Empresa", Empresa);
                param.AddParametro("Nit", Nit);
                param.AddParametro("NoToken", token);
                if (! SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                {
                    throw  SqlDbMysql.UltimaExcepcion;
                }
            }

            result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarEntrega(string NoPrescripcion, string NoCuenta, string NoHistoria, string NoIdentificacion, string NitEntidad, string Responsable, string CodEPS, string CodigoCum, decimal NoId, decimal IdEntrega, string CantEnt, string PrecioUni, string ValEntrega, string CodigoArt)
        {
            ObtenerConexion();
            string Empresa = CodigoEmp;
            var result = string.Empty;
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var sql = "Insert_EntregaMipres";
            decimal valortot = Convert.ToInt32(CantEnt) * Convert.ToInt32(PrecioUni);

            if (Empresa != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                MySqlCommand comm = new MySqlCommand(sql, DBConnect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comm.Parameters.AddWithValue("@parEmpresa", Empresa);
                comm.Parameters["@parEmpresa"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoPrescripcion", NoPrescripcion);
                comm.Parameters["@parNoPrescripcion"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoCuenta", NoCuenta);
                comm.Parameters["@parNoCuenta"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoHistoria", NoHistoria);
                comm.Parameters["@parNoHistoria"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoIdentificacion", NoIdentificacion);
                comm.Parameters["@parNoIdentificacion"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNitEntidad", NitEntidad);
                comm.Parameters["@parNitEntidad"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parResponsable", Responsable);
                comm.Parameters["@parResponsable"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCodMinisterio", CodEPS);
                comm.Parameters["@parCodMinisterio"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCodigoCum", CodigoCum);
                comm.Parameters["@parCodigoCum"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCodigoArt", CodigoArt);
                comm.Parameters["@parCodigoArt"].Direction = ParameterDirection.Input; 
                comm.Parameters.AddWithValue("@parId", NoId);
                comm.Parameters["@parId"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parIdEntrega", IdEntrega);
                comm.Parameters["@parIdEntrega"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCantEnt", Convert.ToInt32(CantEnt));
                comm.Parameters["@parCantEnt"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parPrecio", Convert.ToInt32(PrecioUni));
                comm.Parameters["@parPrecio"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parVlrEntrega", Convert.ToInt32(valortot.ToString())); //ValEntrega
                comm.Parameters["@parVlrEntrega"].Direction = ParameterDirection.Input;
                try
                {
                    try
                    {
                        DBConnect.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        result = string.Concat("Error:", exception.Message);
                    }
                }
                finally
                {
                    DBConnect.Close();
                }
            }
            else
            {
                List<Parametro> param = new List<Parametro>();
                param.AddParametro("Empresa", Empresa);
                param.AddParametro("NoPrescripcion", NoPrescripcion);
                param.AddParametro("NoCuenta", NoCuenta);
                param.AddParametro("NoHistoria", NoHistoria);
                param.AddParametro("NoIdentificacion", NoIdentificacion);
                param.AddParametro("NitEntidad", NitEntidad);
                param.AddParametro("Responsable", Responsable);
                param.AddParametro("CodEPS", CodEPS);
                param.AddParametro("CodigoCum", CodigoCum);
                param.AddParametro("CodigoArt", CodigoArt);
                param.AddParametro("NoId", NoId);
                param.AddParametro("IdEntrega", IdEntrega);
                param.AddParametro("CantEnt", Convert.ToInt32(CantEnt));
                param.AddParametro("PrecioUni", Convert.ToInt32(PrecioUni));
                param.AddParametro("ValEntrega", Convert.ToInt32(valortot.ToString()));
                if (! SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                {
                    throw  SqlDbMysql.UltimaExcepcion;
                }
            }

            result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReporteEntrega(string NoPrescripcion, string CodigoCum, decimal IdReporteEntrega, decimal IdAnulacion, string Estado)
        {
            string Empresa = CodigoEmp;
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
            var sql = "Act_EntregaMipres";
            string result = string.Empty;
            if (Empresa != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                MySqlCommand comm = new MySqlCommand(sql, DBConnect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comm.Parameters.AddWithValue("@parEmpresa", Empresa);
                comm.Parameters["@parEmpresa"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoPrescripcion", NoPrescripcion);
                comm.Parameters["@parNoPrescripcion"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCodigoCum", CodigoCum);
                comm.Parameters["@parCodigoCum"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parIdEntrega", IdReporteEntrega);
                comm.Parameters["@parIdEntrega"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parIdAnulacion", IdAnulacion);
                comm.Parameters["@parIdAnulacion"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parEstado", Estado);
                comm.Parameters["@parEstado"].Direction = ParameterDirection.Input;
                try
                {
                    try
                    {
                        DBConnect.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        result = string.Concat("Error:", exception.Message);
                    }
                }
                finally
                {
                    DBConnect.Close();
                }
            }
            else
            {
                List<Parametro> param = new List<Parametro>();
                param.AddParametro("Empresa", Empresa);
                param.AddParametro("NoPrescripcion", NoPrescripcion);
                param.AddParametro("CodigoCum", CodigoCum);
                param.AddParametro("IdReporteEntrega", IdReporteEntrega);
                param.AddParametro("IdAnulacion", IdAnulacion);
                param.AddParametro("Estado", Estado);
                if (! SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                {
                    throw  SqlDbMysql.UltimaExcepcion;
                }
            }

            result = "OK";
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetReportePrescripciones(Fecha FechaIni, Fecha FechaFin)
        {
            ObtenerConexion();
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtReporte = new DataTable();
            var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection(string.Concat(new string[] { "server=",  IpServidor, ";user id=jgarcia; Password=lili2004;port=",  Puerto, ";database=",  BaseDatos, ";" }));
                string sql = "Select a.Empresa,a.NoPrescripcion,a.Responsable,a.NoCuenta,a.NoIdentificacion, CONCAT_WS(' ', b.nombre1, b.nombre2, b.apellido1, b.apellido2) As Paciente,a.CodigoCum,a.Id,a.IdEntrega,a.IdReporte,CONCAT('$', FORMAT(a.VlrEntrega, 2)) as Valor ,a.IdAnulacion,a.FechaProceso,a.Estado ";
                sql += "From entrega_mipres a Inner Join Pacientes b On a.NoIdentificacion = b.NoHistoria ";
                sql += "Where a.empresa ='" + CodigoEmp + "' and a.FechaProceso between " + FechaIni + " and " + FechaFin + " " ;
                sql += "Order by a.FechaProceso, a.NoPrescripcion";
                MySqlCommand comm = new MySqlCommand(sql,DBConnect);
                DBConnect.Open();
                dtReporte.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<entrega_mipres> Resultado = new List<entrega_mipres>();
                foreach (DataRow dr in dtReporte.Rows)
                {
                    entrega_mipres entregaMipre = new entrega_mipres()
                    {
                        Empresa = dr["Empresa"].ToString(),
                        NitEntidad = dr["nitentidad"].ToString(),
                        NoPrescripcion = dr["NoPrescripcion"].ToString(),
                        NoCuenta = dr["NoCuenta"].ToString(),
                        NoHistoria = dr["NoHistoria"].ToString(),
                        NoIdentificacion = dr["NoIdentificacion"].ToString(),
                        Responsable = dr["Responsable"].ToString(),
                        CodEPS = dr["CodEPS"].ToString(),
                        CodigoCum = dr["CodigoCum"].ToString(),
                        Id = Convert.ToDecimal(dr["Id"].ToString()),
                        IdEntrega = Convert.ToDecimal(dr["IdEntrega"].ToString()),
                        IdReporte = Convert.ToDecimal(dr["IdReporte"].ToString()),
                        CantEnt = Convert.ToDecimal(dr["CantEnt"].ToString()),
                        PrecioUni = Convert.ToDecimal(dr["PrecioUni"].ToString()),
                        VlrEntrega = Convert.ToDecimal(dr["VlrEntrega"].ToString())
                    };
                    DateTime dateTime = Convert.ToDateTime(dr["FechaProceso"].ToString());
                    entregaMipre.FechaProceso = dateTime.Date;
                    entregaMipre.Estado = dr["Estado"].ToString();
                    entregaMipre.Paciente = dr["Paciente"].ToString();
                    entregaMipre.Valor = dr["Valor"].ToString();
                    entregaMipre.IdAnulacion = Convert.ToDecimal(dr["IdAnulacion"].ToString());
                    Resultado.Add(entregaMipre);
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<entrega_mipres> Resultado = dataDb.GetReportePrescripciones(CodigoEmp, FechaIni, FechaFin);
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetEntregaMipres(string NoPrescripcion, string IdEntrega)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtEntrega = new DataTable();
            ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB( SqlDbMysql);

            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "SELECT NoPrescripcion, NoCuenta, NoIdentificacion, CodigoCum, Id, IdEntrega, IdReporte, CantEnt, PrecioUni,IdAnulacion, VlrEntrega, FechaProceso, Estado ";
                sql += "From entrega_mipres Where empresa = '" + CodigoEmp + "' and ";
                sql += "NoPrescripcion = '" + NoPrescripcion + "' and Id = " + IdEntrega + "" ;
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtEntrega.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<entrega_mipres> Resultado = new List<entrega_mipres>();
                foreach (DataRow dr in dtEntrega.Rows)
                {
                    entrega_mipres entregaMipre = new entrega_mipres()
                    {
                        Empresa = dr["Empresa"].ToString(),
                        NitEntidad = dr["nitentidad"].ToString(),
                        NoPrescripcion = dr["NoPrescripcion"].ToString(),
                        NoCuenta = dr["NoCuenta"].ToString(),
                        NoHistoria = dr["NoHistoria"].ToString(),
                        NoIdentificacion = dr["NoIdentificacion"].ToString(),
                        Responsable = dr["Responsable"].ToString(),
                        CodEPS = dr["CodEPS"].ToString(),
                        CodigoCum = dr["CodigoCum"].ToString(),
                        Id = Convert.ToDecimal(dr["Id"].ToString()),
                        IdEntrega = Convert.ToDecimal(dr["IdEntrega"].ToString()),
                        IdReporte = Convert.ToDecimal(dr["IdReporte"].ToString()),
                        CantEnt = Convert.ToDecimal(dr["CantEnt"].ToString()),
                        PrecioUni = Convert.ToDecimal(dr["PrecioUni"].ToString()),
                        VlrEntrega = Convert.ToDecimal(dr["VlrEntrega"].ToString())
                    };
                    DateTime dateTime = Convert.ToDateTime(dr["FechaProceso"].ToString());
                    entregaMipre.FechaProceso = dateTime.Date;
                    entregaMipre.Estado = dr["Estado"].ToString();
                    entregaMipre.Paciente = dr["Paciente"].ToString();
                    entregaMipre.Valor = dr["Valor"].ToString();
                    entregaMipre.IdAnulacion = Convert.ToDecimal(dr["IdAnulacion"].ToString());
                    Resultado.Add(entregaMipre);
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<entrega_mipres> Resultado = dataDb.GetEntregaMipres( CodigoEmp, NoPrescripcion, IdEntrega);
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
        }

        //---- Facturacion

        [HttpPost]
        public ActionResult GetCuentaMIPRES(string NoIdentificacion)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtCuenta = new DataTable();
            ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB( SqlDbMysql);

            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "SELECT c.NoCuenta  FROM Cuenta c INNER JOIN Pacientes p ON c.NoHistoria = p.NoHistoria ";
                sql += "WHERE c.Empresa = '" +  CodigoEmp + "' AND c.Estado in ('A', 'C') And c.NoHistoria = '" + NoIdentificacion + "' ";
                sql += "Order by c.nocuenta Desc";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtCuenta.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<PacientesSIRAS> Resultado = new List<PacientesSIRAS>();
                foreach (DataRow dr in dtCuenta.Rows)
                {
                    Resultado.Add(new PacientesSIRAS()
                    {
                        NoCuenta = dr["NoCuenta"].ToString()
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<PacientesSIRAS> Resultado = dataDb.GetCuentaMIPRES( CodigoEmp, NoIdentificacion);
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult lovEPS(string NoCuenta)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtEPS = new DataTable();
             ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB( SqlDbMysql);

            if (CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "Select DISTINCT f.nitentidad, CONCAT(e.nombre_entidad, '-', IFNULL(e.Cod_Ent_Admin,'')) as NomEPS From facturas f inner join entidades e on f.empresa = e.empresa and f.Nitentidad = e.nitentidad ";
                sql += "inner join convenios cv on f.empresa = cv.empresa and f.codconvenio = cv.cod_convenio ";
                sql += "Where f.empresa ='" + CodigoEmp + "' and f.nocuenta = " + Convert.ToInt32(NoCuenta) + " and f.estado in('F', 'A', 'CI', 'C') " ;
                sql += "Order By f.consinterno";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtEPS.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<ConsultaResponsables> Resultado = new List<ConsultaResponsables>();
                foreach (DataRow dr in dtEPS.Rows)
                {
                    Resultado.Add(new ConsultaResponsables()
                    {
                        NitEntidad = dr["nitentidad"].ToString(),
                        NomEPS = dr["NomEPS"].ToString()
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ConsultaResponsables> result = dataDb.GetEpsMIPRES( CodigoEmp, Convert.ToInt32(NoCuenta));
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult lovFactEPS(string NoCuenta, string NitEntidad)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtFactEPS = new DataTable();
             ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB( SqlDbMysql);

            if ( CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "Select f.NoFactura,f.ConsInterno,f.CodConvenio From facturas f inner join entidades e on f.empresa = e.empresa and f.Nitentidad = e.nitentidad ";
                sql += "Where f.empresa ='" +  CodigoEmp + "' and f.nocuenta = " + Convert.ToInt32(NoCuenta) + " and f.estado in('F', 'A', 'CI', 'C') " ;
                sql += "and f.nitentidad = '" + NitEntidad + "' And f.NoFactura is not null Order By f.NoFactura ";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtFactEPS.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<ConsultaResponsables> Resultado = new List<ConsultaResponsables>();
                foreach (DataRow dr in dtFactEPS.Rows)
                {
                    Resultado.Add(new ConsultaResponsables()
                    {
                        Nofactura = dr["Nofactura"].ToString(),
                        ConsInterno = Convert.ToDecimal(dr["ConsInterno"].ToString()),
                        CodConvenio = dr["CodConvenio"].ToString()
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ConsultaResponsables> result = dataDb.GetFactResponsables( CodigoEmp, Convert.ToInt32(NoCuenta), NitEntidad);
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetConseIntFact(string NoFact)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtFactEPS = new DataTable();
            ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            if (CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "Select f.ConsInterno From facturas f Where f.empresa = '"+ CodigoEmp +"' and f.NoFactura='" +NoFact+"' Order By f.NoFactura ";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtFactEPS.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<ConsultaResponsables> Resultado = new List<ConsultaResponsables>();
                foreach (DataRow dr in dtFactEPS.Rows)
                {
                    Resultado.Add(new ConsultaResponsables()
                    {
                        ConsInterno = Convert.ToDecimal(dr["ConsInterno"].ToString())
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ConsultaResponsables> result = dataDb.GetConseIntFact(CodigoEmp, NoFact);
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
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
        public ActionResult RegistraFacturacion(string NoPrescripcion, decimal IdEntrega, string NoFactura, string CodEPS, decimal CantEnt, decimal ValorUni, decimal ID_F, decimal IdFacturacion, decimal ConsIntFac)
        {
            ObtenerConexion();
            string Empresa =  CodigoEmp;
            string sql = "Act_FacturaMipres";
            string result = string.Empty;
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            if (Empresa != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                MySqlCommand comm = new MySqlCommand(sql, DBConnect)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comm.Parameters.AddWithValue("@parEmpresa", Empresa);
                comm.Parameters["@parEmpresa"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoPrescripcion", NoPrescripcion);
                comm.Parameters["@parNoPrescripcion"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parIdEntrega", IdEntrega);
                comm.Parameters["@parIdEntrega"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parNoFactura", NoFactura);
                comm.Parameters["@parNoFactura"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCodEPS", CodEPS);
                comm.Parameters["@parCodEPS"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parCantEnt", CantEnt);
                comm.Parameters["@parCantEnt"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parValorUni", ValorUni);
                comm.Parameters["@parValorUni"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parID_F", ID_F);
                comm.Parameters["@parID_F"].Direction = ParameterDirection.Input;
                comm.Parameters.AddWithValue("@parConsIntFac", ConsIntFac);
                comm.Parameters["@parConsIntFac"].Direction = ParameterDirection.Input;
                try
                {
                    try
                    {
                        DBConnect.Open();
                        comm.ExecuteNonQuery();
                        result = "SI";
                    }
                    catch (Exception exception)
                    {
                        result = string.Concat("Error:", exception.Message);
                    }
                }
                finally
                {
                    DBConnect.Close();
                }
            }
            else
            {
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
                param.AddParametro("ConsIntFac", ConsIntFac);
                result = "SI";
                if (! SqlDbMysql.EjecutarComando(sql, true, param.ToArray()))
                {
                    result = "NO";
                    throw  SqlDbMysql.UltimaExcepcion;
                }
            }

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidarEntrega(string NoPrescripcion)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            DataTable dtPrescripcion = new DataTable();
            ObtenerConexion();
            ConsultasEsculapioDB dataDb = new ConsultasEsculapioDB(SqlDbMysql);

            if (CodigoEmp != "C30")
            {
                MySqlConnection DBConnect = new MySqlConnection("server=" + IpServidor + ";user id=jgarcia; Password=lili2004;port=" + Puerto + ";database=" + BaseDatos + ";");
                string sql = "Select f.NoPrescripcion From entrega_mipres f Where f.empresa = '" + CodigoEmp + "' and f.NoPrescripcion ='" + NoPrescripcion + "' ";
                MySqlCommand comm = new MySqlCommand(sql, DBConnect);
                DBConnect.Open();
                dtPrescripcion.Load(comm.ExecuteReader());
                DBConnect.Close();
                List<entrega_mipres> Resultado = new List<entrega_mipres>();
                foreach (DataRow dr in dtPrescripcion.Rows)
                {
                    Resultado.Add(new entrega_mipres()
                    {
                        NoPrescripcion = dr["NoPrescripcion"].ToString()
                    });
                }
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<entrega_mipres> Resultado = dataDb.GetValPrescripcion(CodigoEmp, NoPrescripcion);
                return Json(JsonConvert.SerializeObject(Resultado), JsonRequestBehavior.AllowGet);
            }
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