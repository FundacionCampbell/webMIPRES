using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiloSoft.Utils;
using LiloSoft.Types.Data;

namespace LiloSoft.Siesa.Interfaz
{
    public class ConsultaInstitucionDB : BaseDB
    {
        #region Contructores
        /// <summary>
        /// Constructor por Defecto
        /// </summary>
        public ConsultaInstitucionDB()
        {
            ComportamientoIndividualComandos = true;
            TipoComandoIndividual = TipoComando.InstruccionSQL;
        }

        /// <summary>
        /// Constructo con Objeto de Acceso a Datos
        /// </summary>
        /// <param name="pSqlDb"></param>
        public ConsultaInstitucionDB(DB pSqlDb) : this()
        {
            SqlDb = pSqlDb;
        }
        #endregion

        /// <summary>
        /// Dependencias Activas de la Institución
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <returns>Lista de Dependencias</returns>
        public List<Dependencias> GetDependencias(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<Dependencias>(false));
            sql.AppendLine(@" Where Empresa = :Empresa and Estado = 'A' and
    ( Servicios = 'S'  Or
      ( Servicios = 'N' and CodDependencia = (Select DependenciaExterna 
                     From Parametros_Facturacion Where Empresa = :Empresa ) 
      ) 
    )    
Order by 2");
            return ExecuteGetLista<Dependencias>(sql.ToString(), false, lstParams.ToArray());
        }

        /// <summary>
        /// Servicios Activos de la Institución
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <returns>Lista de Servicios_Clinica</returns>
        public List<Servicios_Clinica> GetServiciosClinica(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<Servicios_Clinica>(false));
            sql.AppendLine(" Where Empresa = :Empresa and Estado = 'A' Order by 2");
            return ExecuteGetLista<Servicios_Clinica>(sql.ToString(), false, lstParams.ToArray());
        }

        public object GetUnCasoEsculapio(string empresa, object noCuenta)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Origenes de la Institución
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <returns>Lista de Origen</returns>
        public List<Origen> GetOrigenesClinica(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<Origen>(false));
            sql.AppendLine(" Where Empresa = :Empresa and Estado = 'A' Order by 2");
            return ExecuteGetLista<Origen>(sql.ToString(), false, lstParams.ToArray());
        }

        public Entidades GetUnEntidades(string Empresa, string NitEntidad)
        {
            var sql = GetSelect<Entidades>();
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NitEntidad", NitEntidad);
            return ExecuteGetUnLista<Entidades>(sql, false, lstPars.ToArray());
        }

        public EmpresaEsculapioIps GetUnEmpresaEsculapioIps(string Empresa)
        {
            var sql = "GetEmpresaEsculapio";//GetSelect<EmpresaEsculapioIps>();
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetUnLista<EmpresaEsculapioIps>(sql, true, lstPars.ToArray());
        }

        /// <summary>
        /// Obtener las Empresas del Usuario
        /// </summary>
        /// <param name="Usuario">Nombre del Usuario</param>
        /// <returns>Lista de Empresas</returns>
        public List<EmpresaEsculapioIps> GetEmpresaEsculapioUsuario(string Usuario)
        {
            var sql = "GetUsuariosEmpresa";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Usuario", Usuario);
            return ExecuteGetLista<EmpresaEsculapioIps>(sql, true, lstPars.ToArray());
        }

        public List<EmpresasWeb> Get_EmpresasWeb()
        {
            string sql = "Get_EmpresasWeb";
            List<Parametro> lstPars = new List<Parametro>();
            return base.ExecuteGetLista<EmpresasWeb>(sql, true, null);
        }

        public List<EmpresasWeb> Get_UnaEmpresasWeb(string codEmpresa)
        {
            string sql = string.Concat("Select a.Empresa, b.EmpresaEsculapio, a.NombreEmpresa, b.Servidor,b.Puerto,b.BaseDatos ", "From Empresas a Inner Join EmpresaConeccion b On a.empresa = b.Empresa ");
            sql = string.Concat(sql, "Where b.Empresa = :codEmpresa and a.Estado = 'A'");
            List<Parametro> lstParams = new List<Parametro>();
            lstParams.AddParametro("codEmpresa", codEmpresa);
            return base.ExecuteGetLista<EmpresasWeb>(sql, false, lstParams.ToArray());
        }

        public Convenios GetUnConvenio(string Empresa, string CodConvenio)
        {
            var sql = GetSelect<Convenios>();
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Cod_Convenio", CodConvenio);
            return ExecuteGetUnLista<Convenios>(sql, false, lstPars.ToArray());
        }

        public bool ExisteFactura(string Empresa, string NoFactura)
        {
            var resp = true;
            var sql = "Select NoFactura From Facturas Where Empresa = :Empresa and NoFactura = :NoFactura";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoFactura", NoFactura);
            var dt = SqlDb.EjecutarConsulta(sql, false, lstPars.ToArray());
            if (dt == null && SqlDb.UltimaExcepcion != null) throw SqlDb.UltimaExcepcion;
            if (dt != null && dt.Rows.Count == 0)
                resp = false;
            return resp;
        }

        public List<DetalleFacturas> GetInterfazFacturas(
             string Empresa, // Empresa 
       string TipoFechas, // Tipo de Fechas E:Emisión R:Radicación
       string TipoFactura, // Facturas a Traer T: Todas las Facturas U:Una Factura 
                           // N: De un Responsable, C:Convenio,  A:Anuladas
       string ProcesarMarcadas, // Procesar Marcadas S:Sí o N:No
       Fecha FechaInicial, // -- Fecha Inicial
       Fecha FechaFinal, // Fecha Final
       string NoFactura, // -- No. de Factura a Buscar
       string NitEntidad, // -- Nit de la Entidad a Buscar	
       string CodConvenio //-- Código del Convenio a Buscar
           )
        {
            var sql = "GetFacturasInterfaz";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("TipoFechas", TipoFechas);
            lstPars.AddParametro("TipoFactura", TipoFactura);
            lstPars.AddParametro("ProcesarMarcadas", ProcesarMarcadas);
            lstPars.AddParametro("FechaInicial", FechaInicial);
            lstPars.AddParametro("FechaFinal", FechaFinal);
            lstPars.AddParametro("NoFactura", NoFactura);
            lstPars.AddParametro("NitEntidad", NitEntidad);
            lstPars.AddParametro("CodConvenio", CodConvenio);
            return ExecuteGetLista<DetalleFacturas>(sql, true, lstPars.ToArray());
        }

        /// <summary>
        /// Detalle de Glosas para Interfaz con S1ESA
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="FechaInicial">Fecha Inicial</param>
        /// <param name="FechaFinal">Fecha Final</param>
        /// <returns></returns>
        public List<GlosasDetalle> GetInterfazGlosas(string Empresa, Fecha FechaInicial, Fecha FechaFinal)
        {
            var sql = "GetGlosasInterfaz";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("FechaInicial", FechaInicial);
            lstPars.AddParametro("FechaFinal", FechaFinal);
            return ExecuteGetLista<GlosasDetalle>(sql, true, lstPars.ToArray());

        }

        public CuentaEsculapio GetUnCasoEsculapio(string Empresa, string NoCuenta)
        {
            var sql = "GetunCaso";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetUnLista<CuentaEsculapio>(sql, true, lstPars.ToArray());
        }

        public List<AdmisionesCorreos> GetCorreosAdmisiones(string Empresa)
        {
            var sqlBld = new StringBuilder(GetSelectAll<AdmisionesCorreos>())
              .AppendWhere("Empresa = :Empresa ");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<AdmisionesCorreos>(sql, false, lstPars.ToArray());
        }

        public AdmisionesCorreos GetUnCorreosAdmisiones(string Empresa, string Correo)
        {
            var sqlBld = new StringBuilder(GetSelectAll<AdmisionesCorreos>())
              .AppendWhere("Empresa = :Empresa and Correo = :Correo ");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Correo", Correo);
            var sql = sqlBld.ToString();
            return ExecuteGetUnLista<AdmisionesCorreos>(sql, false, lstPars.ToArray());
        }

        #region Métodos Estaticos
        /// <summary>
        /// Obtener Conexion a Base de Datos MySQL de las Instituciones
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="pSqlDb">Objeto de Acceso a Datos Local</param>
        /// <returns>Objeto de Acceso a Datos MySql</returns>
        public static DB ObtenerBaseMySqL(string Empresa, DB pSqlDb, ref EmpresaConeccion emp)
        {
            var empDb = new EmpresaConeccionDB(pSqlDb);
            var dbMySQl = DB.NuevaDB(DataBase.ManejadorBaseDatos.MySql);
            emp = empDb.GetUnEmpresaConeccion(Empresa);
            if (emp == null)
                throw new Exception("No Hay Parametros de Conexión Definidos para la Empresa {0}".Formato(Empresa));
            dbMySQl.Servidor = emp.Servidor;
            dbMySQl.Puerto = emp.Puerto;
            dbMySQl.BaseDatos = emp.BaseDatos;
            dbMySQl.Usuario = emp.Usuario;
            dbMySQl.Clave = emp.Clave;
            if (!dbMySQl.AbrirConexion())
                throw dbMySQl.UltimaExcepcion;
            return dbMySQl;
        }

        /// <summary>
        /// Buscar Periodo de Cierre
        /// </summary>
        /// <param name="Empresa"></param>
        /// <param name="Periodo"></param>
        /// <returns></returns>
        public CierreFacturasPeriodo GetCierrePeriodoFacturacion(string Empresa, string Periodo)
        {
            return GetUn<CierreFacturasPeriodo>(new CierreFacturasPeriodo
            {
                Empresa = Empresa,
                PeriodoCierre = Periodo
            });
        }

        /// <summary>
        /// Control de Cargue de Consecutivos para Interfaz de S1ESA
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="MotivoCargue">Mótivo del Cargue</param>
        /// <param name="Usuario">Usuario que realiza el cargue</param>
        /// <returns>Objeto ControlCargueSiesa</returns>
        public ControlCargueSiesa GetControlCargueSiesa(string Empresa, string MotivoCargue, string Usuario)
        {
            var sql = "GetControlCargueSiesa";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("MotivoCargue", MotivoCargue);
            lstPars.AddParametro("Usuario", Usuario);
            return ExecuteGetUnLista<ControlCargueSiesa>(sql, true, lstPars.ToArray());
        }

        /// <summary>
        /// Obtener los Controles de Cargue Entre Fechas
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="FechaInicial">Fecha Inicial</param>
        /// <param name="FechaFinal">Fecha Final</param>
        /// <returns>Lista de Control de Cargues</returns>
        public List<ControlCargueSiesa> GetControlCargueSiesaFechas(string Empresa, Fecha FechaInicial, Fecha FechaFinal)
        {
            var sql = new StringBuilder();
            sql.AppendLine(GetSelectAll<ControlCargueSiesa>(false));
            sql.AppendLine(" WHERE Fecha between :FechaInicial and :FechaFinal");
            sql.AppendLine(" ORDER by IdControlCargue ");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("FechaInicial", FechaInicial);
            lstPars.AddParametro("FechaFinal", FechaFinal);
            return ExecuteGetLista<ControlCargueSiesa>(sql.ToString(), false, lstPars.ToArray());
        }

        public List<FacturasControl> GetFacturasControlCargue(string Empresa, EnteroLargo IdControlCargue)
        {
            var control = GetUn<ControlCargueSiesa>(new ControlCargueSiesa { Empresa = Empresa, IdControlCargue = IdControlCargue });
            if (control != null)
            {
                var sql = @"
SELECT NoFactura,NoCuenta, NitEntidad,CodConvenio, Fecha_Emision, Fecha_Radicacion
FROM facturas
WHERE empresa = :Empresa and {0} = :IdControlCargue
Order by Fecha_Emision, NoFactura
".Formato((control.MotivoCargue.ToUpper() == "FACTURACION" ? "IdControlCargueFacturacion" : "IdControlCargueRadicacion"));
                var lstPars = new List<Parametro>();
                lstPars.AddParametro("Empresa", Empresa);
                lstPars.AddParametro("IdControlCargue", IdControlCargue);
                return ExecuteGetLista<FacturasControl>(sql, false, lstPars.ToArray());
            }
            else throw new Exception("El Control de Facturacion No. {0} de la Empresa {1} No Existe".Formato(IdControlCargue, Empresa));
        }

        public void DesmarcarFacturasControlCargue(string Empresa, EnteroLargo IdControlCargue)
        {
            var control = GetUn<ControlCargueSiesa>(new ControlCargueSiesa { Empresa = Empresa, IdControlCargue = IdControlCargue });
            if (control != null)
            {
                var sql = @"
Update facturas
Set {1} = 'N'
WHERE empresa = :Empresa and {0} = :IdControlCargue
Order by Fecha_Emision, NoFactura
".Formato((control.MotivoCargue.ToUpper() == "FACTURACION" ? "IdControlCargueFacturacion" : "IdControlCargueRadicacion"),
         (control.MotivoCargue.ToUpper() == "FACTURACION" ? "ProcesoContable" : "ProcesoContableRadicado"));
                var lstPars = new List<Parametro>();
                lstPars.AddParametro("Empresa", Empresa);
                lstPars.AddParametro("IdControlCargue", IdControlCargue);
                if (!SqlDb.EjecutarComando(sql, false, lstPars.ToArray()))
                    throw SqlDb.UltimaExcepcion;
            }
            else throw new Exception("El Control de Facturacion No. {0} de la Empresa {1} No Existe".Formato(IdControlCargue, Empresa));
        }

        /// <summary>
        /// Procesar el Cierre de Facturación
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="Usuario">Usuairo que Realiza el Cierre</param>
        /// <returns></returns>
        public CierreFacturacionResultado CierreFacturacion(string Empresa, string Usuario)
        {
            var sql = "CierreFacturacion";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("Usuario", Usuario);
            return ExecuteGetUnLista<CierreFacturacionResultado>(sql, true, pars.ToArray());
        }

        /// <summary>
        /// Procesar el Cierre de Facturación
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="Usuario">Usuairo que Realiza el Cierre</param>
        /// <returns></returns>
        public CierreFacturacionResultado CierreFacturacionEspecial(string Empresa, string Usuario)
        {
            var sql = "CierreFacturaEspecial";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("Usuario", Usuario);
            return ExecuteGetUnLista<CierreFacturacionResultado>(sql, true, pars.ToArray());
        }

        /// <summary>
        /// Cancelar el Cierre de Facturación de Un Periodo
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="PeriodoCierre">Periodo a Cancelar Cierre</param>
        /// <param name="Usuario">Usuario que Realiza la Cancelación</param>
        public void CancelarCierreFacturacion(string Empresa, string PeriodoCierre, string Usuario)
        {
            var sql = "CancelarCierrePeriodo";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("PeriodoCierre", PeriodoCierre);
            pars.AddParametro("Usuario", Usuario);
            if (!SqlDb.EjecutarComando(sql, true, pars.ToArray()))
                throw SqlDb.UltimaExcepcion;
        }
        /// <summary>
        /// Lista de Cierres No Contabilizados
        /// </summary>
        /// <param name="Empresa">Empres de Trabajo</param>
        /// <returns>Cierres no Contabilizados</returns>
        public List<CierreFacturasPeriodo> GetCierreFacturasNoContabilizados(string Empresa)
        {
            var sql = GetSelectAll<CierreFacturasPeriodo>(false)
               + " Where Empresa = :Empresa and Contabilizado = 'N' ";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<CierreFacturasPeriodo>(sql, false, pars.ToArray());
        }

        /// <summary>
        /// Resumen de VEntas No Facturadas de un Periodo de Cierre
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="PeriodoContabilizar">Periodo a Contabilizar</param>
        /// <returns></returns>
        public List<ResumenVentasNoFacturadas> GetVentasNoFacturadas(string Empresa, string PeriodoContabilizar)
        {
            var sql = "GetVentasNoFacturadas";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("PeriodoContabilizar", PeriodoContabilizar);
            return ExecuteGetLista<ResumenVentasNoFacturadas>(sql, true, pars.ToArray());
        }

        public List<Planes_Tarifarios> GetPlanesTarifarios(string Empresa)
        {
            var sql = GetSelectAll<Planes_Tarifarios>(false) + " Where Empresa = :Empresa Order by NombrePlan";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<Planes_Tarifarios>(sql, false, pars.ToArray());
        }

        public Planes_Tarifarios GetUnPlanesTarifario(string Empresa, string CodPlan)
        {
            var sql = GetSelectAll<Planes_Tarifarios>(false)
              + " Where Empresa = :Empresa and CodPlan = :CodPlan ";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("CodPlan", CodPlan);
            return ExecuteGetUnLista<Planes_Tarifarios>(sql, false, pars.ToArray());
        }

        public void UpdatePlanTarifario(Planes_Tarifarios plan)
        {
            var Sql = "UpdatePlanTarifarioSiesa";
            List<Parametro> lst = new List<Parametro>();
            lst.AddParametro("Empresa", plan.Empresa);
            lst.AddParametro("CodPlan", plan.CodPlan);
            lst.AddParametro("IncluirInterfazSiesa", (plan.IncluirInterfazSiesa ? "S" : "N"));
            lst.AddParametro("UtilizarNitFactura", (plan.UtilizarNitFactura ? "S" : "N"));
            if (!SqlDb.EjecutarComando(Sql, true, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }

        }

        public List<Resolucion_Facturacion> GetResolucionesActivas(string Empresa)
        {
            var sql = GetSelectAll<Resolucion_Facturacion>(false) + " Where Empresa = :Empresa and Estado = 'A' Order by NoResolucion";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<Resolucion_Facturacion>(sql, false, pars.ToArray());
        }

        public List<Resolucion_Facturacion> GetResolucionesTodas(string Empresa)
        {
            var sql = GetSelectAll<Resolucion_Facturacion>(false) + " Where Empresa = :Empresa Order by NoResolucion";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<Resolucion_Facturacion>(sql, false, pars.ToArray());
        }

        public List<VentasNoFacturadas> GetReversaVentasNoFacturads(String Empresa, Fecha FechaInicial, Fecha FechaFinal, bool ProcesarMarcadas)
        {
            var sql = "GetReversaVentasNoFacturadas";
            var pars = new List<Parametro>();
            pars.AddParametro("Empresa", Empresa);
            pars.AddParametro("FechaInicial", FechaInicial);
            pars.AddParametro("FechaFinal", FechaFinal);
            pars.AddParametro("ProcesarMarcadas", (ProcesarMarcadas ? "S" : "N"));
            return ExecuteGetLista<VentasNoFacturadas>(sql, true, pars.ToArray());
        }

        /// <summary>
        /// Marcar Factura de la interdfaz
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="NoCuenta">No. de Cuenta</param>
        /// <param name="ConsInterno">Cons Interno Factura</param>
        /// <param name="Usuario">Usuario que realiza el proceso</param>
        /// <param name="IdControlCargue">Id. Control de Cargue para Facturacion</param>
        public void MarcarFactura(string Empresa, Entero NoCuenta, Entero ConsInterno, string Usuario, EnteroLargo IdControlCargue)
        {
            var sql = @"
Update Facturas
Set ProcesoContable = 'S',
    FechaProcesoContable = current_date(),
    HoraProcesoContable = SUBSTRING(CURRENT_TIME(),1,5),
    UsuarioProcesoContable = :Usuario,
    IdControlCargueFacturacion = :IdControlCargue
Where Empresa = :Empresa and
      NoCuenta = :NoCuenta and
      ConsInterno  = :ConsInterno
";
            var lpars = new List<Parametro>();
            lpars.AddParametro("Usuario", Usuario);
            lpars.AddParametro("Empresa", Empresa);
            lpars.AddParametro("NoCuenta", NoCuenta);
            lpars.AddParametro("ConsInterno", ConsInterno);
            lpars.AddParametro("IdControlCargue", IdControlCargue);
            if (!SqlDb.EjecutarComando(sql, false, lpars.ToArray()))
                throw SqlDb.UltimaExcepcion;
        }

        /// <summary>
        /// Marcar Factura de la interdfaz
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="NoCuenta">No. de Cuenta</param>
        /// <param name="ConsInterno">Cons Interno Factura</param>
        /// <param name="Usuario">Usuario que realiza el proceso</param>
        /// <param name="IdControlCargue">Id. Control de Cargue para Radicación</param>
        public void MarcarFacturaRadicado(string Empresa, Entero NoCuenta, Entero ConsInterno, string Usuario, EnteroLargo IdControlCargue)
        {
            var sql = @"
Update Facturas 
Set ProcesoContableRadicado = 'S',
    FechaProcesoContableRadicado = current_date(),
    HoraProcesoContableRadicado = SUBSTRING(CURRENT_TIME(),1,5),
    UsuarioProcesoContableRadicado = :Usuario,
    IdControlCargueRadicacion = :IdControlCargue
Where Empresa = :Empresa and
      NoCuenta = :NoCuenta and
      ConsInterno  = :ConsInterno
";
            var lpars = new List<Parametro>();
            lpars.AddParametro("Usuario", Usuario);
            lpars.AddParametro("Empresa", Empresa);
            lpars.AddParametro("NoCuenta", NoCuenta);
            lpars.AddParametro("ConsInterno", ConsInterno);
            lpars.AddParametro("IdControlCargue", IdControlCargue);
            if (!SqlDb.EjecutarComando(sql, false, lpars.ToArray()))
                throw SqlDb.UltimaExcepcion;
        }

        /// <summary>
        /// Marcar Factura de Reversión de Ventas No Facturadas
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="NoCuenta">No. de Cuenta</param>
        /// <param name="ConsInterno">Cons Interno Factura</param>
        /// <param name="Usuario">Usuario que realiza el proceso</param>
        public void MarcarFacturaReversaVentaNoFacturada(string Empresa, Entero NoCuenta, Entero ConsInterno, string Usuario)
        {
            var sql = @"
Update CierreFacturas 
Set Contabilizado = 'S',
    FechaContabiliza = current_date(),
    HoraContabiliza = SUBSTRING(CURRENT_TIME(),1,5),
    UsuarioContabiliza = :Usuario
Where Empresa = :Empresa and
      NoCuenta = :NoCuenta and
      ConsInternoFactura  = :ConsInterno
";
            var lpars = new List<Parametro>();
            lpars.AddParametro("Usuario", Usuario);
            lpars.AddParametro("Empresa", Empresa);
            lpars.AddParametro("NoCuenta", NoCuenta);
            lpars.AddParametro("ConsInterno", ConsInterno);
            if (!SqlDb.EjecutarComando(sql, false, lpars.ToArray()))
                throw SqlDb.UltimaExcepcion;
        }
        #endregion

        #region Consultas de Cierre de Facturación
        /// <summary>
        /// Traer la Lista de Periodos Cerrados
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <returns>Lista de los Periodos Cerrados</returns>
        public List<CierreFacturasPeriodo> GetPeriodosCerrados(string Empresa)
        {
            var sql = new StringBuilder(GetSelectAll<CierreFacturasPeriodo>(false));
            sql.AppendLine(" WHERE Empresa = :Empresa");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<CierreFacturasPeriodo>(sql.ToString(), false, lstPars.ToArray());
        }

        /// <summary>
        /// Consulta del Periodo Cerrado por Servicio y Dependencia
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="PeriodoCierre">Periodo del Cierre</param>
        /// <returns>Lista de ConsultaPeriodoCierre con los Datos del Cierre</returns>
        public List<ConsultaPeriodoCierre> GetConsultaCierre(string Empresa, string PeriodoCierre)
        {
            var sql = "GetConsultaCierre";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("PeriodoCierre", PeriodoCierre);
            return ExecuteGetLista<ConsultaPeriodoCierre>(sql, true, lstPars.ToArray());
        }



        /// <summary>
        /// Consulta del Periodo Cerrado por Tercero, Plan, Servicio y Dependencia
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="PeriodoCierre">Periodo del Cierre</param>
        /// <returns>Lista de ConsultaPeriodoCierre con los Datos del Cierre</returns>
        public List<ConsultaPeriodoCierreDetallada> GetConsultaCierreDetalle(string Empresa, string PeriodoCierre)
        {
            var sql = "GetConsultaCierreDetalle";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("PeriodoCierre", PeriodoCierre);
            return ExecuteGetLista<ConsultaPeriodoCierreDetallada>(sql, true, lstPars.ToArray());
        }
        /// <summary>
        /// Consulta del Periodo Cerrado por Tercero, Plan, Servicio y Dependencia
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="PeriodoCierre">Periodo del Cierre</param>
        /// <returns>Lista de ConsultaPeriodoCierre con los Datos del Cierre</returns>
        public List<ConsultaPeriodoCierreCasos> GetConsultaCierreCasos(string Empresa, string PeriodoCierre)
        {
            var sql = "GetConsultaCierreCasos";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("PeriodoCierre", PeriodoCierre);
            return ExecuteGetLista<ConsultaPeriodoCierreCasos>(sql, true, lstPars.ToArray());
        }

        public static object ObtenerBaseMySqL(string empresa, DB sqlDb, ref EmpresaConeccion empresaConeccion, object emp)
        {
            throw new NotImplementedException();
        }
        #endregion

    }//fin clase
}//fin namespace
