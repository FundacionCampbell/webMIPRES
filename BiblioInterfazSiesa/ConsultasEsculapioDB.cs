using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz
{
    public class ConsultasEsculapioDB : BaseDB
    {

        #region Contructores
        /// <summary>
        /// Constructor por Defecto
        /// </summary>
        public ConsultasEsculapioDB()
        {
            ComportamientoIndividualComandos = true;
            TipoComandoIndividual = TipoComando.InstruccionSQL;
        }

        /// <summary>
        /// Constructo con Objeto de Acceso a Datos
        /// </summary>
        /// <param name="pSqlDb"></param>
        public ConsultasEsculapioDB(DB pSqlDb) : this()
        {
            SqlDb = pSqlDb;
        }
        #endregion

        #region Metodos GET Consultas
        public DatosCasoBasico GetCasoBasico(string Empresa, Entero NoCuenta)
        {
            var sqlBld = new StringBuilder(@"
     Select c.NoCuenta, c.NoHistoria, Concat_ws(' ',p.Nombre1,p.nombre2, p.apellido1, p.Apellido2) NombrePaciente, 
       c.FechaIngreso, c.HoraIngreso, c.Estado, c.ConsInternoFact,p.sexo, p.Edad, p.Medida_Edad
from Cuenta c inner join Pacientes p
  on c.NoHistoria = p.NoHistoria
Where c.Empresa = :Empresa and
      c.NoCuenta = :NoCuenta
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            var sql = sqlBld.ToString();
            return ExecuteGetUnLista<DatosCasoBasico>(sql, false, lstPars.ToArray());
        }

        public List<HistoriasClinicasEmpresas> GetHistoriasPacientes(string CiudadServer, string NoHistoria)
        {
            var sqlBld = new StringBuilder(@"
SELECT :CiudadServer CiudadServer, a.Empresa,
       (SELECT CONCAT_WS(' ',e.Nombre_Empresa,e.NombreComercial) FROM Empresa e WHERE e.Empresa = a.Empresa) NombreEmpresa,
        a.Acompañante Acompanante, h.NoHistoria, 
        h.empresa, h.NoFolio, h.NoCuenta, h.NoAdmision, 
        h.Cod_Servicio, s.Nombre NombreServicio, s.CodDependencia, 
        h.CedulaMedico, a.FechaIngreso, a.HoraIngreso, 
        h.usuario, h.estado, s.Urgencias, s.consultaExterna, s.RequiereMotivoIngreso, 
        a.cod_parent, a.FechaEgreso, a.HoraEgreso  
 FROM historiaclinica h INNER JOIN Servicios_Clinica s ON 
      (h.empresa = s.empresa AND h.cod_servicio = s.cod_servicio) 
      INNER JOIN Admisiones a ON 
      (h.empresa = a.empresa AND h.noadmision = a.noAdmision AND a.estado <> 'X') 
 WHERE  h.NoHistoria = :NoHistoria
 ORDER BY a.FechaIngreso DESC, a.HoraIngreso DESC
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("CiudadServer", CiudadServer);
            lstPars.AddParametro("NoHistoria", NoHistoria);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<HistoriasClinicasEmpresas>(sql, false, lstPars.ToArray());
        }

        public List<ResponsablesCaso> GetResponsablesCaso(string Empresa, Entero NoCuenta)
        {
            var sqlBld = new StringBuilder(@"
SELECT f.ConsInterno, f.NitEntidad,
  (SELECT e.Nombre_Entidad FROM Entidades e
     WHERE e.Empresa = f.Empresa AND e.NitEntidad = f.NitEntidad) NombreEntidad,
   f.CodConvenio, c.Nombre_Convenio
FROM Facturas f INNER JOIN Convenios c
 ON f.Empresa = c.Empresa AND
    f.CodConvenio = c.Cod_Convenio
WHERE f.Empresa = :Empresa and
      f.NoCuenta = :NoCuenta
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<ResponsablesCaso>(sql, false, lstPars.ToArray());
        }

        /// <summary>
        /// Detalle de los Servicios Facturados de un Responsable
        /// </summary>
        /// <param name="Empresa">Empresa de Trabajo</param>
        /// <param name="NoCuenta">No. de Caso del Paciente</param>
        /// <param name="ConsInternoFactura">Consecutivo Interno de la Factura</param>
        /// <returns>Detalle de los Servicios Facturados</returns>
        public List<DetalleCuenta> GetDetalleServiciosFacturados(string Empresa, Entero NoCuenta, Entero ConsInternoFactura)
        {
            var sqlBld = new StringBuilder(@"
   SELECT d.fecha_cargo, horacargo,d.cod_servicio_origen,
  (SELECT nombre FROM Servicios_Clinica s
    WHERE s.Empresa = d.Empresa AND
          s.Cod_Servicio = d.cod_servicio_origen ) NombreServicioOrigen,
   d.depinterno CodDependencia,
   d.NoOrdenServicio, d.CodServicio, d.NombreServicio, d.Cantidad, d.Precio ValorServicio,
   d.Porcentaje, d.CodClase, d.Usuario_Grabacion,d.fecha_real_cargo,d.Hora_real_cargo, d.ValorIva,
   d.CostoServicio, d.CedulaMedico,
   (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
     FROM Medicos m WHERE m.Empresa = d.Empresa AND m.cedula = d.CedulaMedico ) NombreMedico,
     d.CodEspecialidad,
     (SELECT Descripcion FROM Especialidades e
       WHERE e.Cod_Especialidad = d.CodEspecialidad ) NombreEspecialidad
FROM Cuenta_Detalle d
WHERE d.Empresa = :Empresa AND
      d.NoCuenta =  :NoCuenta AND
      d.ConsInternoFactura = :ConsInternoFactura AND
      d.EstadoOrden <> 'X'
ORDER BY 1,2     
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("ConsInternoFactura", ConsInternoFactura);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<DetalleCuenta>(sql, false, lstPars.ToArray());
        }

        public DatosAdmision GetCuentaAdmision(string Empresa, string NoCuenta, string NoAdmision)
        {
            var sqlBld = new StringBuilder(@"
SELECT a.NoCuenta,a.NoAdmision,a.FechaIngreso, a.HoraIngreso, a.Estado, a.FechaEgreso, a.HoraEgreso, 
       a.CodServicio, s.Nombre NombreServicio,a.Habitacion, a.Pendiente_Hab,
       a.CodMedico,
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
     FROM Medicos m WHERE m.Empresa = a.Empresa AND m.cedula = a.CodMedico ) NombreMedico,
       a.Reingreso, a.Salida
       -- ,a.*
FROM Admisiones a INNER JOIN Servicios_Clinica s ON 
      (a.empresa = s.empresa AND a.codservicio = s.cod_servicio) 
WHERE a.Empresa = :Empresa AND
      a.NoCuenta = :NoCuenta AND
      a.NoAdmision = :NoAdmision
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);

            var sql = sqlBld.ToString();
            return ExecuteGetUnLista<DatosAdmision>(sql, false, lstPars.ToArray());
        }

        public DatosAdmision GetCuentaAdmision(string Empresa, string NoCuenta)
        {
            var sqlBld = new StringBuilder(@"
    SELECT a.NoCuenta,a.NoAdmision, a.FechaIngreso, a.HoraIngreso, a.Estado, a.FechaEgreso, a.HoraEgreso,
       a.CodServicio, s.Nombre NombreServicio, a.Habitacion, a.Pendiente_Hab,
       a.CodMedico,
       (SELECT CONCAT_WS(' ', nombre1, nombre2, apellido1, apellido2)
     FROM Medicos m WHERE m.Empresa = a.Empresa AND m.cedula = a.CodMedico ) NombreMedico,
       a.Reingreso, a.Salida
       -- ,a.*
FROM Admisiones a INNER JOIN Servicios_Clinica s ON
      (a.empresa = s.empresa AND a.codservicio = s.cod_servicio)
     INNER JOIN Cuenta c
     ON c.Empresa = a.Empresa AND
        c.NoCuenta = a.NoCuenta AND
        c.NoAdm_Inicial = a.NoAdmision
WHERE a.Empresa = :Empresa AND
      a.NoCuenta = :NoCuenta
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            var sql = sqlBld.ToString();
            return ExecuteGetUnLista<DatosAdmision>(sql, false, lstPars.ToArray());
        }

        public DatosBasicosHistoria GetDatosBasicoHistoria(String Empresa, string NoCuenta, string NoAdmision)
        {
            var sqlBld = new StringBuilder(@"
SELECT h.NoCuenta, h.NoAdmision, h.FechaAtencion, h.HoraAtencion, h.MotivoConsulta, h.EnfermedadActual,
       h.FrecCardiaca, h.FrecRespitatoria, h.PresionArterial, h.TEmperatura, h.Peso,
              h.CedulaMedico,
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
     FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaMedico ) NombreMedico, c.Diagnostico
     -- ,h.*
FROM HistoriaClinica h INNER JOIN Cuenta c
     ON c.Empresa = h.Empresa AND
        c.NoCuenta = h.NoCuenta
WHERE h.Empresa = :Empresa AND
      h.NoCuenta = :NoCuenta AND
      h.NoAdmision = :NoAdmision
 ");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            var sql = sqlBld.ToString();
            return ExecuteGetUnLista<DatosBasicosHistoria>(sql, false, lstPars.ToArray());
        }

        public List<OrdenesMedicamentos> GetOrdenesMedicamentos(string Empresa, string NoCuenta, string NoAdmision, string CiudadServer)
        {
            var sqlBld = new StringBuilder(@"
 SELECT :CiudadServer CiudadServer,h.Empresa, h.NoCuenta,h.NoAdmision, h.NoOrdenMedica, h.FechaOrden, h.HoraOrden, h.CedulaMedico, h.ordensalida, 
         CONCAT_WS(' ',m.nombre1,m.nombre2,m.apellido1,apellido2) NombreMedico, 
         h.UsuarioGrabacion, h.estado estadoM, 
         IF(h.estado='A','Ordenado', IF(h.estado='F','Cargado','Anulado')) estado, 
         h.EstadoAplicacion, h.noRadicado, IF(h.OrdenSalida='N','INTERNA', 'SALIDA') TipoOrden,  
         CONCAT_WS(' ', h.FechaRealGrabacion, h.horaRealGrabacion) FechaReal 
 FROM Historia_Ordenes_Medicas h INNER JOIN Medicos m ON h.Empresa = m.Empresa AND h.CedulaMedico = m.Cedula  
WHERE h.Empresa = :Empresa AND
      h.NoCuenta = :NoCuenta AND
      h.NoAdmision = :NoAdmision AND
       h.TipoOrdenMedica = 'M' AND 
       h.TipoMedicamento = 'N' AND  
       h.estado <> 'X' 
 ORDER BY h.FechaOrden DESC, h.HoraOrden DESC, h.NoOrdenMedica DESC
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("CiudadServer", CiudadServer);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<OrdenesMedicamentos>(sql, false, lstPars.ToArray());
        }

        public List<OrdenMedicamentoDetalle> GetOrdenMedicamentoDetalle(string Empresa, string NoCuenta, string NoAdmision, string NoOrdenMedica)
        {
            var sql = @"
 SELECT NoOrdenMedica, ConsecutivoDetalle, CodMedicamento, NombreMedicamento, CONCAT_WS(' - ', Frecuencia,observacioncuidado) Frecuencia, 
         h.EstadoAplicacion, IF(h.EstadoAplicacion='A','En Tratamiento',
         IF(h.EstadoAplicacion='C','Cumplido','Suspendido')) NomEstadoAplicacion 
 FROM historia_ordenes_medicas_detalle h 
 WHERE h.Empresa       = :Empresa AND h.tipomedicamento ='M' AND 
       h.NoCuenta      = :NoCuenta AND 
       h.NoAdmision    = :NoAdmision AND 
       h.NoOrdenMedica = :NoOrdenMedica
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("NoOrdenMedica", NoOrdenMedica);
            return ExecuteGetLista<OrdenMedicamentoDetalle>(sql, false, lstPars.ToArray());
        }

        public List<OrdenesParaclinicos> GetOrdenesParaclinicos(string Empresa, string NoCuenta, string NoAdmision, string CiudadServer, bool MostrarAnuladas = false)
        {
            var sql = @"
SELECT :CiudadServer CiudadServer, h.Empresa, h.NoCuenta, h.NoAdmision,
       h.NoFolio, h.NoOrdenMedica , h.FechaOrden, h.HoraOrden, h.CedulaMedico, 
       CONCAT_WS(' ',m.nombre1,m.nombre2,m.apellido1,apellido2) NombreMedico, 
       h.UsuarioGrabacion,  hd.CodDependencia,  h.CodDependencia DptoEnc, d.NomDependecia, 
       hd.noordenservicio, h.CodEsquema, h.usuariograbacion,  
       hd.estado estadoM,IF(h.estado='A','Ordenado',  IF(h.estado='F','Cargado',IF(h.estado='X','Anulado',IF(hd.Resultado='S','Resultado','')))) estado, 
       h.noradicado, CONCAT_WS(' ', h.FechaRealGrabacion, h.horaRealGrabacion) FechaReal, justificacion 
FROM Historia_Ordenes_Medicas h 
     INNER JOIN Medicos m ON h.Empresa = m.Empresa AND h.CedulaMedico = m.Cedula  
     INNER JOIN historia_ordenes_medicas_dpto hd ON 
           h.empresa = hd.empresa AND h.nocuenta = hd.nocuenta AND 
           h.noadmision = hd.noadmision AND h.noordenMedica = hd.noordenmedica 
     INNER JOIN Dependencias d ON hd.Empresa = d.Empresa AND 
           hd.CodDependencia = d.CodDependencia
WHERE h.Empresa = :Empresa AND 
      h.NoCuenta = :NoCuenta AND 
      h.NoAdmision = :NoAdmision AND 
      h.TipoOrdenMedica = 'P' " +
          (MostrarAnuladas ? "" : "  and h.estado <> 'X' ") + @"
ORDER BY h.FechaOrden DESC, h.HoraOrden DESC, h.NoOrdenMedica DESC
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("CiudadServer", CiudadServer);
            return ExecuteGetLista<OrdenesParaclinicos>(sql, false, lstPars.ToArray());

        }

        public List<OrdenParaClinicoDetalle> GetOrdenParaclinicoDetalle(string Empresa, string NoCuenta, string NoAdmision, string CodDependencia, string NoOrdenMedica)
        {
            var sql = @"
 SELECT NoOrdenMedica, ConsecutivoDetalle, CodServicio, NombreServicio, 
        h.ObservacionCuidado Observaciones 
 FROM historia_ordenes_medicas_detalle h 
 WHERE h.Empresa       = :Empresa AND h.tipomedicamento ='M' AND 
       h.NoCuenta      = :NoCuenta AND 
       h.NoAdmision    = :NoAdmision AND 
       h.CodDependencia = :CodDependencia and
       h.NoOrdenMedica = :NoOrdenMedica
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("CodDependencia", CodDependencia);
            lstPars.AddParametro("NoOrdenMedica", NoOrdenMedica);
            return ExecuteGetLista<OrdenParaClinicoDetalle>(sql, false, lstPars.ToArray());
        }

        public List<OrdenCuidadoPaciente> GetOrdenesCuidadoPacientes(string Empresa, string NoCuenta, string NoAdmision, string CiudadServer)
        {
            var sqlBld = new StringBuilder(@"
 SELECT :CiudadServer CiudadServer,h.Empresa, h.NoCuenta,h.NoAdmision, 
        h.NoOrdenMedica, h.FechaOrden, h.HoraOrden, h.CedulaMedico, 
       CONCAT_WS(' ', m.nombre1, m.nombre2, m.apellido1, m.apellido2) NombreMedico, 
       h.UsuarioGrabacion, h.Estado, h.NoRadicado 
FROM Historia_Ordenes_Medicas h INNER JOIN Medicos m ON 
     h.Empresa = m.Empresa AND h.CedulaMedico = m.Cedula 
WHERE h.Empresa = :Empresa AND
      h.NoCuenta = :NoCuenta AND
      h.NoAdmision = :NoAdmision AND 
      h.TipoOrdenMedica = 'C' AND 
      h.estado <> 'X' AND h.ordenautomatica ='N'  
ORDER BY h.FechaOrden DESC, h.HoraOrden DESC, h.NoOrdenMedica DESC
");
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("CiudadServer", CiudadServer);
            var sql = sqlBld.ToString();
            return ExecuteGetLista<OrdenCuidadoPaciente>(sql, false, lstPars.ToArray());
        }

        public List<OrdenCiuidadPacienteDetalle> GetOrdenCuidadoPacienteDetalle(string Empresa, string NoCuenta, string NoAdmision, string NoOrdenMedica)
        {
            var sql = @"
SELECT h.DescripcionCuidado, H.ObservacionCuidado /* NoOrdenMedica, ConsecutivoDetalle,CodMedicamento, NombreMedicamento, Frecuencia, 
        h.EstadoAplicacion,if(h.EstadoAplicacion='A','En Tratamiento',
        if(h.EstadoAplicacion='C','Cumplido','Suspendido')) NomEstadoAplicacion */ 
FROM historia_ordenes_medicas_detalle h 
 WHERE h.Empresa       = :Empresa AND 
       h.NoCuenta      = :NoCuenta AND 
       h.NoAdmision    = :NoAdmision AND 
       h.NoOrdenMedica = :NoOrdenMedica
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("NoOrdenMedica", NoOrdenMedica);
            return ExecuteGetLista<OrdenCiuidadPacienteDetalle>(sql, false, lstPars.ToArray());
        }

        public List<EvolucionMedica> GetEvolucionesMedicas(string Empresa, string NoCuenta, string NoAdmision, string CiudadServer)
        {
            var sql = @"
SELECT :CiudadServer CiudadServer,h.Empresa, h.nocuenta , h.NoAdmision, h.NoNotaClinica, h.FechaNotaClinica, 
       h.HoraNotaClinica, h.CedulaMedicoNota, 
       CONCAT_WS(' ',m.nombre1,m.nombre2,m.apellido1,apellido2) NombreMedico, 
       h.UsuarioGrabacion, h.UsuarioGrabacion usuario,h.TituloResumenNota,
       h.TextoNotaClinica, IFNULL(h.diagnostico,'') diagnostico , h.estado, e.Descripcion Especialidad,  
       h.codServicio Servicio, h.justificacion, 
       h.AdicionarEpicrisis, h.ResultadosExamenes, FechaRealGrabacion fechagraba, HoraRealGrabacion horagraba, 
       sc.nombre nombreServicio 
FROM Historia_Notas_Clinica h INNER JOIN  Medicos m ON  h.Empresa = m.Empresa AND 
      h.CedulaMedicoNota = m.Cedula  
     INNER JOIN  Especialidades e ON m.Cod_Especialidad = e.Cod_Especialidad 
     INNER JOIN  admisiones a ON h.empresa = a.empresa AND h.nocuenta = a.nocuenta AND h.noadmision = a.noadmision AND a.estado NOT IN ('X') 
     INNER JOIN Servicios_clinica sc ON h.empresa = sc.empresa AND h.codservicio = sc.cod_servicio 
WHERE h.Empresa = :Empresa AND     
      h.nocuenta = :NoCuenta AND      
      h.NoAdmision = :NoAdmision AND  
      h.TipoNotaClinica = 'M' AND h.TextoNotaClinica is not null AND
      h.estado <> 'I' ORDER BY h.FechaNotaClinica DESC, h.NoNotaClinica 
";
            //, h.HoraNotaClinica DESC
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("CiudadServer", CiudadServer);
            return ExecuteGetLista<EvolucionMedica>(sql, false, lstPars.ToArray());
        }

        public EvolucionMedica GetDetalleEvolucionMedica(string Empresa, string NoCuenta, string NoAdmision, string NoNotaClinica)
        {
            var sql = @"
SELECT h.nocuenta , h.NoAdmision, h.NoNotaClinica, h.FechaNotaClinica, 
       h.HoraNotaClinica, h.CedulaMedicoNota, 
       CONCAT_WS(' ',m.nombre1,m.nombre2,m.apellido1,apellido2) NombreMedico, 
       h.UsuarioGrabacion, h.UsuarioGrabacion usuario,h.TituloResumenNota,
       h.TextoNotaClinica, IFNULL(h.diagnostico,'') diagnostico , h.estado, e.Descripcion Especialidad,  
       h.codServicio Servicio, h.justificacion, 
       h.AdicionarEpicrisis, h.ResultadosExamenes, FechaRealGrabacion fechagraba, HoraRealGrabacion horagraba, 
       sc.nombre nombreServicio 
FROM Historia_Notas_Clinica h INNER JOIN  Medicos m ON  h.Empresa = m.Empresa AND 
      h.CedulaMedicoNota = m.Cedula  
     INNER JOIN  Especialidades e ON m.Cod_Especialidad = e.Cod_Especialidad 
     INNER JOIN  admisiones a ON h.empresa = a.empresa AND h.nocuenta = a.nocuenta AND h.noadmision = a.noadmision AND a.estado NOT IN ('X') 
     INNER JOIN Servicios_clinica sc ON h.empresa = sc.empresa AND h.codservicio = sc.cod_servicio 
WHERE h.Empresa = :Empresa AND     
      h.nocuenta = :NoCuenta AND      
      h.NoAdmision = :NoAdmision AND  
      h.NoNotaClinica = :NoNotaClinica AND
      h.TipoNotaClinica = 'M' AND 
      h.estado <> 'I' 
ORDER BY h.FechaNotaClinica DESC, h.HoraNotaClinica DESC
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("NoNotaClinica", NoNotaClinica);
            return ExecuteGetUnLista<EvolucionMedica>(sql, false, lstPars.ToArray());
        }

        public InformeQuirurgico GetInformeQuirurgico(string Empresa, string NoAdmision)
        {
            var sql = @"
SELECT NoHistoria, NoFolio, NoEpicrisis, CodServicio, NoCuenta, NoAdmision, FechaInicio, HoraInicio,  
      FechaFinal, HoraFinal, 
      CedulaMedico, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaMedico ) NombreCedulaMedico,     
      CedulaAyudante, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaAyudante ) NombreCedulaAyudante,      
      CedulaAnestesiologo,
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaAnestesiologo ) NombreCedulaAnestesiologo,       
      DiagnosticoPrequirurgico, 
      DiagnosticoPre1, DiagnosticoPre2, DiagnosticoPre3, DiagnosticoPre4, DiagnosticoPre5, 
      Hallazgos, ProcedimientosRealizados, DiagnosticoPos1, DiagnosticoPos2, DiagnosticoPos3, 
      DiagnosticoPos4, DiagnosticoPos5, JustificacionProc, DiagnosticoPostQuirurgico, 
      DescripcionProc, UsuarioGraba, FechaGraba, HoraGraba, UsuarioAct, FechaAct, HoraAct, ConsCir, 
      CodConvenio, ConsInternoFactura, Consecutivo, Conducta, OrdenesMedicas, TieneFoto, 
      CedulaMedicoOpera, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaMedicoOpera) NombreCedulaMedicoOpera,       
      DrenesMechas, MaterialOsteosintesis, tipoanestesia, Osteosintesis 
FROM Historia_InformeQx h
WHERE h.empresa = :Empresa AND 
      h.noadmision = :NoAdmision
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            return ExecuteGetUnLista<InformeQuirurgico>(sql, false, lstPars.ToArray());

        }

        public List<ConsultaCasos> GetConsultaCasos(string Empresa, string Criterio)
        {
            var sql = @"
SELECT c.nocuenta, c.nohistoria, c.FechaIngreso, CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) NombrePaciente
FROM Cuenta c INNER JOIN Pacientes p
  ON c.NoHistoria = p.NoHistoria 
WHERE c.Empresa = :Empresa AND 
      c.Estado <> 'X'  AND
      ( 
         c.NoCuenta LIKE CONCAT('%',:Criterio,'%') OR
         c.NoHistoria LIKE CONCAT('%',:Criterio,'%') OR
        CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) LIKE CONCAT('%',:Criterio,'%')
      )         
Order by c.FechaIngreso Desc, c.NoCuenta
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return ExecuteGetLista<ConsultaCasos>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaPacienteEventoAdverso> GetPacienteEventoAdverso(string Empresa, string Criterio)
        {
            var sql = @"
SELECT c.NoCuenta as NoCaso, p.nodocumento as NoIdentificacion,CONCAT_WS(' ',p.nombre1,p.nombre2) As NombrePaciente , CONCAT_WS(' ',p.apellido1, p.apellido2) As ApellidoPaciente
 FROM Cuenta c INNER JOIN Pacientes p
  ON c.NoHistoria = p.NoHistoria 
WHERE c.Empresa = :Empresa AND 
      c.Estado <> 'X'  AND
      ( 
         c.NoCuenta LIKE CONCAT('%',:Criterio,'%') OR
        CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) LIKE CONCAT(:Criterio,'%')
      )         
Order by NombrePaciente
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return ExecuteGetLista<ConsultaPacienteEventoAdverso>(sql, false, lstPars.ToArray());
        }

        public List<PacientesSIRAS> GetPacienteIngreso(string Empresa, string Criterio)
        {
            var sql = @"
SELECT c.empresa, c.nocuenta, c.nohistoria as Identificacion, c.fechaingreso, CONCAT_WS(' ',p.nombre1,p.nombre2,p.apellido1, p.apellido2) As Paciente
 FROM Cuenta c INNER JOIN Pacientes p
  ON c.NoHistoria = p.NoHistoria 
 WHERE c.Empresa = :Empresa AND c.Estado in ('A','C') And c.NoHistoria = :Criterio
 Order by c.nocuenta Desc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return ExecuteGetLista<PacientesSIRAS>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaEventosAdversos> GetEventosAdversos(string Empresa, Fecha FechaIni, Fecha FechaFin)
        {
            var sql = @"
 SELECT  a.Fecha, a.Hora, a.Servicio, a.nocuenta AS nocaso, a.NombrePaciente, 
         a.ApellidoPaciente, a.Sucesos, a.Causas,' ' as Accion
   FROM evento_adverso a
  WHERE a.empresa = :Empresa AND (a.Fecha BETWEEN :FechaIni AND :FechaFin)
   ORDER BY a.Fecha;	
";
            //(Fecha BETWEEN :FechaIni AND :FechaFin)
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("FechaIni", FechaIni);
            lstPars.AddParametro("FechaFin", FechaFin);
            //sql = sqlBld.ToString();
            return ExecuteGetLista<ConsultaEventosAdversos>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaUsuariosAdv> GetUsuariosAdv(string Empresa, string usuario, string password)
        {
            var sql = @"
 SELECT  a.Usuario, a.PassWordUsu, a.Nombre
   FROM usuarios a
  WHERE a.usuario = :usuario And a.PassWordUsu=:password
";
            //(Fecha BETWEEN :FechaIni AND :FechaFin)
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("usuario", usuario);
            lstPars.AddParametro("password", password);

            //sql = sqlBld.ToString();
            return ExecuteGetLista<ConsultaUsuariosAdv>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaUsuariosAdv> GetUsuarioRondaMed(string Empresa, string usuario)
        {
            var sql = @"SELECT a.usuario,a.cedula FROM usuarios a WHERE a.usuario=:usuario AND (a.cedula IN(SELECT b.cedula FROM medicos b WHERE b.empresa=:Empresa AND b.Estado='A') OR 
                        a.usuario IN(SELECT c.usuario FROM historia_usuario_digitador c WHERE c.Empresa=:Empresa AND c.Estado='A'))";

            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("usuario", usuario);

            //sql = sqlBld.ToString();
            return ExecuteGetLista<ConsultaUsuariosAdv>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaUsuariosAdv> GetUsuarioSIRAS(string Empresa, string usuario)
        {
            var sql = @"SELECT a.usuario FROM usuarios_siras a WHERE a.usuario=:usuario And a.Empresa=:Empresa AND a.Estado='A'";

            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("usuario", usuario);

            //sql = sqlBld.ToString();
            return ExecuteGetLista<ConsultaUsuariosAdv>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaUsuariosAdv> GetUsuarioMIPRES(string Empresa, string usuario)
        {
            var sql = @"SELECT a.usuario,a.role,b.Nombre FROM usuarios_mipres a Inner Join Usuarios b On a.usuario=b.usuario WHERE a.usuario=:usuario And a.Empresa=:Empresa AND a.Estado='A'";

            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("usuario", usuario);

            //sql = sqlBld.ToString();
            return ExecuteGetLista<ConsultaUsuariosAdv>(sql, false, lstPars.ToArray());
        }
        
        public List<ConsultaIPEmpresas> GetIpEmpresas(string Empresa)
        {
            var sql = @"
SELECT Empresa, IpConexion, Descripcion, NombreComercial as Ubicacion
 FROM Ipconexion_Empresas
WHERE Empresa = :Empresa 
 Order by IpConexion
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaIPEmpresas>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaIPEmpresas> GetCodigoEmpresa(string IpConexion) //
        {
            var sql = @"
SELECT Empresa FROM Ipconexion_Empresas
WHERE IpConexion Like CONCAT('%',:IpConexion,'%') Order by IpConexion ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("IpConexion", IpConexion);
            return ExecuteGetLista<ConsultaIPEmpresas>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaEmpresa> GetEmpresasInv(string Empresa)
        {
            var sql = @"
        SELECT Empresa,Nombre_Empresa,EmpresaInv FROM Empresa WHERE empresa=:Empresa And Estado='A';
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaEmpresa>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaRondaMed> GetNoRondaMed(string Empresa, Fecha FechaRonda)
        {
            //
            var sql = @"
SELECT Id_RondaMed,Fecha,Hora FROM Ronda_Med_Enc
    WHERE Empresa = :Empresa and Fecha=CURDATE() and Estado='A' 
 Order by Id_RondaMed
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("FechaRonda", FechaRonda);
            return ExecuteGetLista<ConsultaRondaMed>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaPacientesRonda> GetPacientesRonda(string Empresa, string Criterio)
        {
            var sql = @"SELECT c.nocuenta as nocaso, c.nohistoria, d.NoFolio, d.NoAdmision, d.Cod_Servicio, e.Nombre as NombreServicio,  CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) NombrePaciente, 
                        IFNULL(p.NoDocumento,'N/A') as NoDocumento FROM Cuenta c INNER JOIN Pacientes p  ON c.NoHistoria = p.NoHistoria 
     INNER JOIN historiaclinica d On c.NoCuenta=d.NoCuenta and d.Estado<>'C' and c.Empresa=d.Empresa and (d.fechaatencion is not null)
     INNER JOIN servicios_clinica e On d.Cod_Servicio=e.Cod_Servicio and d.Empresa=e.Empresa
    WHERE c.Empresa = :Empresa AND c.Estado <> 'X'  AND
      ( 
         c.NoCuenta LIKE CONCAT(:Criterio,'%') OR
         c.NoHistoria LIKE CONCAT(:Criterio,'%') OR
        CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) LIKE CONCAT(:Criterio,'%')
      )         
Order by c.NoCuenta desc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return ExecuteGetLista<ConsultaPacientesRonda>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaPacientesRonda> GetUnPacientesRonda(string Empresa, Int32 NoCuenta)
        {
            var sql = @"SELECT c.nocuenta as nocaso, c.nohistoria, d.NoFolio, d.NoAdmision, d.Cod_Servicio, e.Nombre as NombreServicio,  CONCAT_WS(' ',p.nombre1,p.nombre2, p.apellido1, p.apellido2) NombrePaciente, 
                        IFNULL(p.NoDocumento,'N/A') as NoDocumento FROM Cuenta c INNER JOIN Pacientes p  ON c.NoHistoria = p.NoHistoria 
     INNER JOIN historiaclinica d On c.NoCuenta=d.NoCuenta and d.Estado<>'C' and c.Empresa=d.Empresa and (d.fechaatencion is not null)
     INNER JOIN servicios_clinica e On d.Cod_Servicio=e.Cod_Servicio and d.Empresa=e.Empresa
    WHERE c.Empresa = :Empresa AND c.Estado <> 'X'  AND c.NoCuenta = :NoCuenta         
Order by c.NoCuenta desc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<ConsultaPacientesRonda>(sql, false, lstPars.ToArray());
        }

        public List<CasoSIRAS> GetCasoSIRAS(string Empresa, string Criterio)
        {
            var sql = @"SELECT DISTINCT cta.Empresa, cta.NoCuenta NoCaso, cta.nohistoria, CONCAT_WS(' ',p.nombre1, p.nombre2, p.Apellido1, p.Apellido2) Paciente,
		   s.RadicadoEnvio,	
		   IF(ISNULL(s.nocuenta),'X','OK') Reporte, s.observacion, s.DatosEnviados
	FROM Cuenta cta
	  INNER JOIN pacientes p  ON p.NoHistoria = cta.nohistoria  
	  LEFT OUTER JOIN Informe_Siras s ON cta.empresa = s.Empresa AND cta.nocuenta = s.NoCuenta  AND s.estado ='A'
	WHERE NOT (cta.Estado ='X') AND cta.Empresa=:Empresa AND cta.NoCuenta=:Criterio
	ORDER BY cta.nocuenta 
 ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return ExecuteGetLista<CasoSIRAS>(sql, false, lstPars.ToArray());
        }

        public List<CasoSIRAS> GetPlanillaSIRAS(string Empresa)
        {
            var sql = @"SELECT cta.NoCuenta, cta.NoHistoria, CONCAT_WS(' ',p.nombre1, p.nombre2, p.Apellido1, p.Apellido2) Paciente,
		   cta.NoRadicado, CONCAT_WS(' ',cta.FechaSIRAS,cta.HoraSIRAS) Fecha, 
          CASE WHEN cta.Estado = 'A' THEN 'AUTORIZADO' 
               WHEN cta.Estado = 'R' THEN 'REPORTADO'     
          END AS Estado	
	FROM planilla_enc_siras cta
	  INNER JOIN pacientes p ON p.NoHistoria = cta.nohistoria  
	WHERE cta.Estado in('R','A') AND cta.Empresa=:Empresa
	ORDER BY cta.FechaSIRAS 
 ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<CasoSIRAS>(sql, false, lstPars.ToArray());
        }

        public List<PacientesSIRAS> GetCasosPendienteSIRAS(string Empresa)
        {
            var sql = "Get_CasosPendientesSIRAS";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<PacientesSIRAS>(sql, true, lstPars.ToArray());
        }

        public List<requeridos_modulos> GetCamposRequeridos(string Modulo)
        {
            var sql = @"SELECT CampoReq, Alfanumerico, Panel From Requeridos_modulos Where estado='A' and Modulo=:Modulo
                         And Requerido='S' Order by Panel";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Modulo", Modulo);
            return ExecuteGetLista<requeridos_modulos>(sql, false, lstPars.ToArray());
        }

        public List<requeridos_modulos> GetCamposNoRequeridos(string Modulo)
        {
            var sql = @"SELECT CampoReq, Alfanumerico, Panel From Requeridos_modulos Where estado='A' and Modulo=:Modulo
                        And Requerido='N' Order by Panel";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Modulo", Modulo);
            return ExecuteGetLista<requeridos_modulos>(sql, false, lstPars.ToArray());
        }

        public List<Especialidades> GetEspecialidades(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<Especialidades>(false));
            sql.AppendLine(" Where Estado='A' and Especialidad='S' ORDER BY Descripcion");
            return ExecuteGetLista<Especialidades>(sql.ToString(), false, lstParams.ToArray());
        }

        public List<tipos_gestion_siras> GetTiposGestion(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<tipos_gestion_siras>(false));
            sql.AppendLine(" Where Estado='A' ORDER BY Descripcion");
            return ExecuteGetLista<tipos_gestion_siras>(sql.ToString(), false, lstParams.ToArray());
        }

        public List<Causas_Siras> GetCausasSIRAS(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<Causas_Siras>(false));
            sql.AppendLine(" Where Estado='A' ORDER BY Descripcion");
            return ExecuteGetLista<Causas_Siras>(sql.ToString(), false, lstParams.ToArray());
        }

        public List<parametros_siras> GetParametrosSIRAS(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<parametros_siras>(false));
            sql.AppendLine(" Where Maneja_Ws='S' and Empresa=:Empresa");
            return ExecuteGetLista<parametros_siras>(sql.ToString(), false, lstParams.ToArray());
        }

        public List<parametros_mipres> GetParametrosMIPRES(string Empresa)
        {
            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);
            var sql = new StringBuilder(GetSelectAll<parametros_mipres>(false));
            sql.AppendLine(" Where Maneja_Ws='S' and Empresa=:Empresa");
            return ExecuteGetLista<parametros_mipres>(sql.ToString(), false, lstParams.ToArray());
        }

        public List<MaestroArticulos> GetMaestroArticulo(string Empresa, string EmpresaInv, string Criterio)
        {
            var sql = @"
 SELECT  a.CodArticulo, a.NombreArticulo, a.Pos, a.Empresa, a.PrincipioActivo
   FROM maestroarticulos a
  WHERE a.empresa = :EmpresaInv And TipoArticulo='M' And (
    a.CodArticulo LIKE CONCAT('%',:Criterio,'%') Or
    a.NombreArticulo LIKE CONCAT('%',:Criterio,'%'))  
    Order by a.NombreArticulo
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("EmpresaInv", EmpresaInv);
            lstPars.AddParametro("Criterio", Criterio);

            //sql = sqlBld.ToString();
            return ExecuteGetLista<MaestroArticulos>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaReconmendaciones> GetReconmendaciones(string Empresa, string Especialidad)
        {
            var sql = @"
 SELECT  a.Cod_Reconmendacion,a.Descripcion as Reconmendacion
   FROM reconmendaciones_Especialidad a
  WHERE a.Estado='A' and a.Tipo='R' and a.Cod_Especialidad=:Especialidad
   ORDER BY a.Descripcion;	
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Especialidad", Especialidad);

            return ExecuteGetLista<ConsultaReconmendaciones>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaCuidados> GetCuidadosClinicos(string Empresa)
        {
            var sql = @"
     SELECT a.CodigoCuidadoEnc,a.DescripcionCuidadoEnc,b.CodigoCuidado,b.DescripcionCuidado
        FROM historia_cuidadosenf_enc a INNER JOIN historia_cuidados_enf b ON a.CodigoCuidadoEnc=b.CodigoCuidadoEnc 
    Where a.CodigoCuidadoEnc not in('9','6') 
    ORDER BY a.CodigoCuidadoEnc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaCuidados>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaDietas> GetDietas(string Empresa)
        {
            var sql = @"
     SELECT a.CodigoCuidadoEnc,a.DescripcionCuidadoEnc,b.CodigoCuidado,b.DescripcionCuidado
        FROM historia_cuidadosenf_enc a INNER JOIN historia_cuidados_enf b ON a.CodigoCuidadoEnc=b.CodigoCuidadoEnc 
    Where a.CodigoCuidadoEnc='6' 
    ORDER BY a.CodigoCuidadoEnc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaDietas>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaTiemposRM> GetTiemposRM(string Empresa, string Codigo)
        {
            var sql = @"
 SELECT  a.Cod_Tiempo,a.Duracion
   FROM ronda_med_Tiempos a
  WHERE a.Cod_Tiempo=:Codigo 
   ORDER BY a.Duracion;	
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Codigo", Codigo);

            return ExecuteGetLista<ConsultaTiemposRM>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaUnidadMedicamentos> GetUnidadesMedicamento(string Empresa, string Codigo)
        {
            var sql = @"
SELECT u.Unidad, m.EquivalenciaConsumo 
 FROM Historia_Unidad u INNER JOIN Historia_Unidad_Medicamentos m ON  u.unidad  = m.unidadhistoria 
WHERE u.Estado  = 'A' AND m.empresa = :Empresa AND m.CodArticulo = :Codigo;
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Codigo", Codigo);

            return ExecuteGetLista<ConsultaUnidadMedicamentos>(sql, false, lstPars.ToArray());
        }

        public List<Ronda_Med_ProcedimientoQx> GetProcedQxRM(string Empresa)
        {

            var lstParams = new List<Parametro>();
            lstParams.AddParametro("Empresa", Empresa);

            var sql = new StringBuilder(GetSelectAll<Ronda_Med_ProcedimientoQx>(false));
            sql.AppendLine(" Where Estado='A' ORDER BY Descripcion");
            return ExecuteGetLista<Ronda_Med_ProcedimientoQx>(sql.ToString(), false, lstParams.ToArray());

        }

        public List<ConsultaViasAplicaMed> GetViaAplRM(string Empresa)
        {
            var sql = @"
 SELECT  a.CodViaAplicacion,a.NombreViaAplicacion
   FROM vias_aplicacion_med a
    Where a.Estado='A' 
   ORDER BY a.NombreViaAplicacion;	
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaViasAplicaMed>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaMedicos> GetMedicos(string Empresa, string Especialidad)
        {
            var sql = @"
SELECT  b.Cedula,concat_ws(' ',b.nombre1,b.nombre2,b.apellido1,b.apellido2) AS Medico, b.Cod_Especialidad
   FROM Medicos b
    INNER JOIN especialidades c ON b.Cod_Especialidad=c.Cod_Especialidad AND c.Cod_Especialidad=:Especialidad AND c.Estado='A'
    WHERE b.Empresa=:Empresa AND b.Estado='A' AND b.nombre1<>' '   
  ORDER BY b.Nombre1;	
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Especialidad", Especialidad);
            return ExecuteGetLista<ConsultaMedicos>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaDependencias> GetDependencias(string Empresa)
        {
            var sql = @"
     SELECT CodDependencia,NomDependecia FROM dependencias 
             WHERE empresa=:Empresa AND servicios='S' AND estado='A' 
               AND ParaclinicoServicio='S' Order by NomDependecia;	
        ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaDependencias>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaDependencias> GetOtDependencias(string Empresa, string NombreDep)
        {
            var sql = @"
     SELECT CodDependencia,NomDependecia FROM dependencias 
             WHERE empresa=:Empresa AND NomDependecia=:NombreDep AND estado='A' 
             Order by NomDependecia;	
        ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NombreDep", NombreDep);
            return ExecuteGetLista<ConsultaDependencias>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaHabitacion> GetHabitacionPaciente(string Empresa, Int32 NoCuenta)
        {
            var sql = @"
     SELECT a.Habitacion as CodHab, c.NomHab From ruta_habitacion a 
         INNER JOIN habitacion c ON a.empresa = c.empresa AND a.habitacion = c.codhab
             WHERE a.empresa=:Empresa AND a.NoCuenta=:NoCuenta and 
                a.fechaingreso=(SELECT MAX(b.fechaingreso) FROM ruta_habitacion b WHERE b.empresa=:Empresa AND b.nocuenta=:NoCuenta)
             Order by c.NomHab;	
        ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<ConsultaHabitacion>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaHabitacion> GetPabellones(string Empresa)
        {
            var sql = @"SELECT a.CodPab,a.Nombre From Pabellones a WHERE a.empresa=:Empresa 
             Order by a.CodPab;	
        ";
            //AND a.Estado='A'
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaHabitacion>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaKardex> GetPacientesKardex(string Empresa, string CualesHabitaciones, string CodPabellon, string NitEntidad)
        {

            var sql = "ConsOcupacionHabitacion";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("CualesHabitaciones", CualesHabitaciones);
            lstPars.AddParametro("CodPabellon", CodPabellon);
            lstPars.AddParametro("NitEntidad", NitEntidad);
            return ExecuteGetLista<ConsultaKardex>(sql, true, lstPars.ToArray());

        }

        public List<ConsultaResponsables> GetResponsableMIPRES(string Empresa, Int32 NoCuenta)
        {
            var sql = @"Select f.nitentidad, e.nombre_entidad, f.ConsInterno,f.CodConvenio,
            cv.CodEsquemaTar, cv.TipoTopeSoat, cv.soat, cv.CopagoPorNivel, f.ValorFactura, f.Valor_Descuento
         From facturas f inner join entidades e on f.empresa = e.empresa and  f.Nitentidad = e.nitentidad 
      inner join convenios cv on f.empresa = cv.empresa and  f.codconvenio  = cv.cod_convenio 
       Where f.empresa = :Empresa and f.nocuenta = :NoCuenta and f.estado in( 'F','A','CI','C') 
       Order By f.consinterno ";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<ConsultaResponsables>(sql, false, lstPars.ToArray());
        }

        public List<EsquemasTarifarios> GetEsquemasTarifario(string Empresa)
        {
            var sql = @"SELECT CodEsquema FROM parametros_historia WHERE empresa=:Empresa;";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<EsquemasTarifarios>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaServicios> GetParaClinicos(string Empresa, string Esquema, string CodDependencia)
        {
            var sql = @"
        SELECT e.CodServicio,e.NomServicio,d.NomDependecia,d.CodDependencia,e.CodEsquema
         FROM Esquema_Tarifario_Servicios e INNER JOIN Dependencias d 
            ON e.empresa = d.Empresa AND e.codDependencia = d.codDependencia AND
            d.estado ='A' and d.CodDependencia=:CodDependencia 
            WHERE e.empresa  = :Empresa AND e.codEsquema = :Esquema  AND e.estado = 'A' AND 
          EXISTS ( SELECT 'x' FROM Clase_Servicio c WHERE c.Empresa = e.Empresa 
                  AND c.Codigo = e.claServicio  AND c.Paraclinicos = 'S' ) Order by e.NomServicio
        
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Esquema", Esquema);
            lstPars.AddParametro("CodDependencia", CodDependencia);

            return ExecuteGetLista<ConsultaServicios>(sql, false, lstPars.ToArray());
        }

        public List<HistoriaClinica> GetConsecutivoOrden(string Empresa, decimal NoCuenta, decimal NoAdmision)
        {
            var sql = "GetConsecutivoOrdenesMedicas";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            return ExecuteGetLista<HistoriaClinica>(sql, true, lstPars.ToArray());

        }

        public List<conse_hist_sol_cirugia> GetConsSolicitudCirugia(string Empresa, string TipoServ)
        {
            var sql = "GetConsecutivoSolCirugias";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("TipoServ", TipoServ);
            return ExecuteGetLista<conse_hist_sol_cirugia>(sql, true, lstPars.ToArray());

        }

        public List<ConsultaKardex> GetDiagnosticoAct(string Empresa, Int32 NoCuenta)
        {
            var sql = "GetDiagnosticoActualizado";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<ConsultaKardex>(sql, true, lstPars.ToArray());

        }

        public List<EvolucionMedica> GetNoNotaClinica(string Empresa, decimal NoCuenta, string NoHistoria, decimal NoAdmision, decimal NoFolio, Fecha Fecha)
        {
            var sql = @"
	SELECT max(NoNotaClinica) as NoNotaClinica FROM historia_notas_clinica
	WHERE empresa  =  :Empresa AND NoCuenta =  :NoCuenta AND NoHistoria = :NoHistoria 
	AND NoAdmision =  :NoAdmision AND NoFolio = :NoFolio AND FechaNotaClinica = :Fecha;
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoHistoria", NoHistoria);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            lstPars.AddParametro("NoFolio", NoFolio);
            lstPars.AddParametro("Fecha", Fecha);
            return ExecuteGetLista<EvolucionMedica>(sql, false, lstPars.ToArray());
        }

        public List<OrdenCiuidadPacienteDetalle> GetOrdenCuidadosKardex(string Empresa, string NoCuenta, string NoAdmision)
        {
            var sql = @"
 SELECT h.Empresa, h.NoCuenta,h.NoAdmision, 
        h.NoOrdenMedica, h.FechaOrden, h.HoraOrden, h.CedulaMedico, 
       CONCAT_WS(' ', m.nombre1, m.nombre2, m.apellido1, m.apellido2) NombreMedico, 
       h.UsuarioGrabacion, h.Estado, h.NoRadicado,e.DescripcionCuidado, e.ObservacionCuidado
FROM Historia_Ordenes_Medicas h 
INNER JOIN historia_ordenes_medicas_detalle e On h.empresa=e.empresa and h.NoCuenta=e.NoCuenta And
           h.NoAdmision    = e.NoAdmision AND 
           h.NoOrdenMedica = e.NoOrdenMedica AND e.DescripcionCuidado is not null
INNER JOIN Medicos m ON 
     h.Empresa = m.Empresa AND h.CedulaMedico = m.Cedula 
WHERE h.Empresa = :Empresa AND
      h.NoCuenta = :NoCuenta AND
      h.NoAdmision = :NoAdmision AND 
      h.TipoOrdenMedica = 'C' AND 
      h.estado <> 'X' AND h.ordenautomatica ='N'  
ORDER BY h.NoOrdenMedica DESC
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NoAdmision", NoAdmision);
            return ExecuteGetLista<OrdenCiuidadPacienteDetalle>(sql, false, lstPars.ToArray());
        }

        public List<EvolucionMedica> GetLaboratoriosKardex(string Empresa, string NoCuenta) //, string NoAdmision
        {
            var sql = @"
SELECT h.Empresa, h.nocuenta , h.NoAdmision, h.NoNotaClinica, h.FechaNotaClinica, 
       h.HoraNotaClinica, h.CedulaMedicoNota, 
       CONCAT_WS(' ',m.nombre1,m.nombre2,m.apellido1,apellido2) NombreMedico, 
       h.UsuarioGrabacion, h.UsuarioGrabacion usuario,h.TituloResumenNota,
       h.TextoNotaClinica, IFNULL(h.diagnostico,'') diagnostico , h.estado, e.Descripcion Especialidad,  
       h.codServicio Servicio, h.justificacion, 
       h.AdicionarEpicrisis, h.ResultadosExamenes, FechaRealGrabacion fechagraba, HoraRealGrabacion horagraba, 
       sc.nombre nombreServicio 
FROM Historia_Notas_Clinica h INNER JOIN  Medicos m ON  h.Empresa = m.Empresa AND 
      h.CedulaMedicoNota = m.Cedula  
     INNER JOIN  Especialidades e ON m.Cod_Especialidad = e.Cod_Especialidad 
     INNER JOIN  admisiones a ON h.empresa = a.empresa AND h.nocuenta = a.nocuenta AND h.noadmision = a.noadmision AND a.estado NOT IN ('X') 
     INNER JOIN Servicios_clinica sc ON h.empresa = sc.empresa AND h.codservicio = sc.cod_servicio 
WHERE h.Empresa = :Empresa AND     
      h.nocuenta = :NoCuenta AND      
      h.TipoNotaClinica = 'M' AND 
      h.estado <> 'I' And
      h.ResultadosExamenes is not null
ORDER BY h.NoNotaClinica DESC
";
            // h.NoAdmision = :NoAdmision AND  
            //, h.HoraNotaClinica DESC
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            //lstPars.AddParametro("NoAdmision", NoAdmision);

            return ExecuteGetLista<EvolucionMedica>(sql, false, lstPars.ToArray());
        }

        public List<InformeQuirurgico> GetInformeQxKardex(string Empresa, decimal NoCuenta)
        {
            var sql = @"
SELECT NoHistoria, NoFolio, NoEpicrisis, CodServicio, NoCuenta, NoAdmision, FechaInicio, HoraInicio,  
      FechaFinal, HoraFinal, 
      CedulaMedico, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaMedico ) NombreCedulaMedico,     
      CedulaAyudante, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaAyudante ) NombreCedulaAyudante,      
      CedulaAnestesiologo,
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaAnestesiologo ) NombreCedulaAnestesiologo,       
      DiagnosticoPrequirurgico, 
      DiagnosticoPre1, DiagnosticoPre2, DiagnosticoPre3, DiagnosticoPre4, DiagnosticoPre5, 
      Hallazgos, ProcedimientosRealizados, DiagnosticoPos1, DiagnosticoPos2, DiagnosticoPos3, 
      DiagnosticoPos4, DiagnosticoPos5, JustificacionProc, DiagnosticoPostQuirurgico, 
      DescripcionProc, UsuarioGraba, FechaGraba, HoraGraba, UsuarioAct, FechaAct, HoraAct, ConsCir, 
      CodConvenio, ConsInternoFactura, Consecutivo, Conducta, OrdenesMedicas, TieneFoto, 
      CedulaMedicoOpera, 
       (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
        FROM Medicos m WHERE m.Empresa = h.Empresa AND m.cedula = h.CedulaMedicoOpera) NombreCedulaMedicoOpera,       
      DrenesMechas, MaterialOsteosintesis, tipoanestesia, Osteosintesis 
FROM Historia_InformeQx h
WHERE h.empresa = :Empresa AND 
      h.NoCuenta = :NoCuenta Order by h.Consecutivo desc
";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<InformeQuirurgico>(sql, false, lstPars.ToArray());

        }

        public List<ConsultaKardex> GetCultivosPDF(string Empresa)
        {
            var sql = @"SELECT a.CodDigitalizacion, b.Ruta FROM parametros_historia a
 INNER JOIN centrodigitalizacion b ON a.Empresa=b.Empresa AND a.CodDigitalizacion=b.CodCentro
 WHERE a.empresa=:Empresa";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            return ExecuteGetLista<ConsultaKardex>(sql, false, lstPars.ToArray());
        }

        public List<planilla_det_siras> GetGestionCuenta(string Empresa, string NoCuenta)
        {
            var sql = @"SELECT a.FechaGestion, a.HoraGestion, a.TipoGestion, b.Descripcion, a.CodCausa, c.Descripcion as Causas
                        FROM planilla_det_siras a
                        Inner Join tipos_gestion_siras b On a.TipoGestion=b.CodigoGestion And b.Estado='A'
                        Inner Join Causas_siras c On a.CodCausa=c.CodCausa and c.Estado='A'
                        WHERE a.empresa=:Empresa and a.NoCuenta=:NoCuenta;";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<planilla_det_siras>(sql, false, lstPars.ToArray());
        }

        public List<datosaccidente> GetInformacionAccidente(string Empresa, decimal NoCuenta, string usuario)
        {
            var sql = "Cons_Reporte_Siras";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("usuario", usuario);
            return ExecuteGetLista<datosaccidente>(sql, true, lstPars.ToArray());

        }

        public List<PacientesMIPRES> GetPacienteDistrito(string NoHistoria) //string Empresa
        {
            var sql = @"SELECT p.empresa, p.nohistoria as Identificacion, p.Departamento, p.Municipio
              FROM Pacientes p WHERE  p.NoHistoria = :NoHistoria";
            var lstPars = new List<Parametro>();
            //lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoHistoria", NoHistoria);
            return ExecuteGetLista<PacientesMIPRES>(sql, false, lstPars.ToArray());
        }

        public List<EgresosCasos> GetEgresosCasos(string Empresa, decimal NoCuenta)
        {
            var sql = "GetEgresosCasos";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return ExecuteGetLista<EgresosCasos>(sql, true, lstPars.ToArray());
        }

        public List<DetalleExtractoMIPRES> GetDetalleExtractoMIPRES(string Empresa, decimal NoCuenta, decimal ConsInterno)
        {
            var sql = "GetDetalleExtractoMIPRES";
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("ConsInterno", ConsInterno);

            return ExecuteGetLista<DetalleExtractoMIPRES>(sql, true, lstPars.ToArray());
        }

        public List<entrega_mipres> GetValidaEntrega(string Empresa, string NoPrescripcion, string CodigoCum, string tipo)
        {

            var sql = @"SELECT IFNULL(NoPrescripcion,''),NoIdentificacion, CodigoCum, Id, IdEntrega , IdReporte,
            IdAnulacion,VlrEntrega,FechaProceso,Estado 
            From entrega_mipres Where empresa=:Empresa and NoPrescripcion=:NoPrescripcion and CodigoCum=:CodigoCum";
            if (tipo == "1")
            {
                sql = @"SELECT IFNULL(NoPrescripcion,''),NoIdentificacion, CodigoCum, Id, IdEntrega , IdReporte,
            IdAnulacion,VlrEntrega,FechaProceso,Estado 
              From entrega_mipres Where empresa=:Empresa and NoPrescripcion=:NoPrescripcion";
            }
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoPrescripcion", NoPrescripcion);
            if (tipo == "2")
            {
                lstPars.AddParametro("CodigoCum", CodigoCum);
            }

            return ExecuteGetLista<entrega_mipres>(sql, false, lstPars.ToArray());

        }

        public List<PacientesSIRAS> GetCuentaMIPRES(string Empresa, string Criterio)
        {
            string sql = "SELECT c.NoCuenta FROM Cuenta c INNER JOIN Pacientes p ON c.NoHistoria = p.NoHistoria WHERE c.Empresa = :Empresa AND c.Estado in ('A','C') And c.NoHistoria = :Criterio Order by c.nocuenta Desc";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("Criterio", Criterio);
            return base.ExecuteGetLista<PacientesSIRAS>(sql, false, lstPars.ToArray());
        }

        public List<entrega_mipres> GetEntregaMipres(string Empresa, string NoPrescripcion, string IdEntrega)
        {
            string sql = "SELECT NoPrescripcion, NoCuenta, NoIdentificacion, CodigoCum, Id, IdEntrega , IdReporte, CantEnt,PrecioUni, IdAnulacion,VlrEntrega,FechaProceso,Estado From entrega_mipres Where empresa=:Empresa and NoPrescripcion=:NoPrescripcion and Id=:IdEntrega ";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoPrescripcion", NoPrescripcion);
            lstPars.AddParametro("IdEntrega", IdEntrega);
            return base.ExecuteGetLista<entrega_mipres>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaResponsables> GetEpsMIPRES(string Empresa, int NoCuenta)
        {
            string sql = "Select DISTINCT f.nitentidad, CONCAT(e.nom" +
                "bre_entidad,'-',IFNULL(e.Cod_Ent_Admin,'')) as NomEPS From facturas f inner join entidades e on f.empresa = e.empresa and  f.Nitentidad = e.nitentidad inner join convenios cv on f.empresa = cv.empresa and  f.codconvenio  = cv.cod_convenio Where f.empresa = :Empresa and f.nocuenta = :NoCuenta and f.estado in( 'F','A','CI','C') Order By f.consinterno ";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            return base.ExecuteGetLista<ConsultaResponsables>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaResponsables> GetFactResponsables(string Empresa, int NoCuenta, string NitEntidad)
        {
            string sql = "Select f.NoFactura,f.ConsInterno,f.CodConvenio From facturas f inner join entidades e on f.empresa = e.empresa and  f.Nitentidad = e.nitentidad Where f.empresa = :Empresa and f.nocuenta = :NoCuenta and f.nitentidad = :NitEntidad and f.estado in( 'F','A','CI','C') And f.NoFactura is not null Order By f.NoFactura ";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoCuenta", NoCuenta);
            lstPars.AddParametro("NitEntidad", NitEntidad);
            return base.ExecuteGetLista<ConsultaResponsables>(sql, false, lstPars.ToArray());
        }

        public List<ConsultaResponsables> GetConseIntFact(string Empresa, string NoFact)
        {
            string sql = "Select f.ConsInterno From facturas f Where f.empresa = :Empresa and f.NoFactura=:NoFact Order By f.NoFactura ";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoFact", NoFact);
            return base.ExecuteGetLista<ConsultaResponsables>(sql, false, lstPars.ToArray());
        }

        public List<entrega_mipres> GetValPrescripcion(string Empresa, string NoPrescripcion)
        {
            string sql = "Select f.NoPrescripcion From entrega_mipres f Where f.empresa=:Empresa and f.NoPrescripcion = :NoPrescripcion";
            List<Parametro> lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("NoPrescripcion", NoPrescripcion);
            return base.ExecuteGetLista<entrega_mipres>(sql, false, lstPars.ToArray());
        }
  
        public Entero GetConnectionId()
        {
            var sql = "SELECT CONNECTION_ID() Id";
            var id = SqlDb.EjecutarEscalar<Entero>(sql, false, null);
            return id;
        }

        public List<entrega_mipres> GetReportePrescripciones(string Empresa, Fecha FechaIni, Fecha FechaFin)
        {
            var sql = @"Select a.Empresa,a.NoPrescripcion,a.Responsable,a.NoCuenta,a.NoIdentificacion, CONCAT_WS(' ',b.nombre1,b.nombre2,b.apellido1,b.apellido2) As Paciente,
                      a.CodigoCum,a.Id,a.IdEntrega,a.IdReporte,CONCAT('$', FORMAT(a.VlrEntrega, 2)) as Valor ,a.IdAnulacion,a.FechaProceso,a.Estado 
                       From entrega_mipres a Inner Join Pacientes b On a.NoIdentificacion = b.NoHistoria 
                 Where a.empresa=:Empresa and a.FechaProceso between :FechaIni and :FechaFin
            Order by a.FechaProceso, a.NoPrescripcion";
            
            var lstPars = new List<Parametro>();
            lstPars.AddParametro("Empresa", Empresa);
            lstPars.AddParametro("FechaIni", FechaIni);
            lstPars.AddParametro("FechaFin", FechaFin);
            //sql = sqlBld.ToString();
            return ExecuteGetLista<entrega_mipres>(sql, false, lstPars.ToArray());
        }

        //--
        public override DataTable GetListaActiva(string criterio, List<LOVParams> parametros)
        {
            var empresa = parametros.ValorParametro("Empresa").ToString();
            var empConexDb = new EmpresaConeccionDB(SqlDb);
            EmpresaConeccion empConex = null;
            var SqlDbMySQl = ConsultaInstitucionDB.ObtenerBaseMySqL(empresa, SqlDb, ref empConex);
            var dataDb = new ConsultasEsculapioDB(SqlDbMySQl);

            return Tablas<ConsultaCasos>.ToDataTable(dataDb.GetConsultaCasos(empConex.EmpresaEsculapio, criterio));
        }

        #endregion

        #region Metodos CRUD


        #endregion

        #region Tablero de Cirugia
        /// <summary>
        /// Lista de 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        //public List<DataTableroCirugia> GetTableroCirugia(string Empresa)
        //{
        //    var sql = "GetTableroCirugiaUrgencia";
        //    var lstPars = new List<Parametro>();
        //    lstPars.AddParametro("Empresa", Empresa);
        //    return ExecuteGetLista<DataTableroCirugia>(sql, true, lstPars.ToArray());
        //}

        //public List<DataTableroCasosPendientes> GetTableroCasosPendiente(string Empresa, Fecha FechaInicial, string HoraInicial, Fecha FechaFinal, string HoraFinal, string Servicio)
        //{
        //    var sql = "GetCasosPendienteAtencion";
        //    var lstPars = new List<Parametro>();
        //    lstPars.AddParametro("Empresa", Empresa);
        //    lstPars.AddParametro("FechaInicial", FechaInicial);
        //    lstPars.AddParametro("HoraInicial", HoraInicial);
        //    lstPars.AddParametro("FechaFinal", FechaFinal);
        //    lstPars.AddParametro("HoraFinal", HoraFinal);
        //    lstPars.AddParametro("Servicio", Servicio);
        //    return ExecuteGetLista<DataTableroCasosPendientes>(sql, true, lstPars.ToArray());
        //}

        #endregion

    }//fin clase

    //***********************************
    //-- Modelos
    //***********************************

    #region Clases Modelos BD

    public class DatosCasoBasico
    {
        [DisplayName("No.Caso")]
        public Entero NoCuenta { get; set; }
        [DisplayName("No.Historia")]
        public string NoHistoria { get; set; }
        [DisplayName("Nombre del Paciente")]
        public string NombrePaciente { get; set; }
        [DisplayName("Fec.Ingreso")]
        public Fecha FechaIngreso { get; set; }
        [DisplayName("Hor.Ingreso")]
        public string HoraIngreso { get; set; }
        [DisplayName("Estado Cuenta ")]
        public string Estado { get; set; }

        public Entero ConsInternoFact { get; set; }
        public string sexo { get; set; }
        [NoDataBase]
        [DisplayName("Sexo")]
        public string NombreSexo
        {
            get
            {
                var nom = "";
                switch (sexo)
                {
                    case "M":
                        nom = "Masculino";
                        break;
                    case "F":
                        nom = "Femenino";
                        break;
                }
                return nom;
            }
        }
        public Entero Edad { get; set; }
        [NoDataBase]
        [DisplayName("Edad ")]
        public string NombreEdad
        {
            get
            {
                var data = "";
                if (Edad != null)
                {
                    var medida =
                      "";
                    var edad = Edad.ValorInterno;
                    var plural = true;
                    if (edad == 1)
                        plural = false;
                    switch (Medida_Edad)
                    {
                        case "A":
                            medida = "Año" + (plural ? "s" : "");
                            break;
                        case "M":
                            medida = "Mes" + (plural ? "es" : "");
                            break;
                        case "D":
                            medida = "Día" + (plural ? "s" : "");
                            break;
                    }
                    data = $"{edad} {medida}";
                }
                return data;
            }
        }
        public string Medida_Edad { get; set; }
    }

    /* 
     SELECT f.ConsInterno, f.NitEntidad,
    (SELECT e.Nombre_Entidad FROM Entidades e
       WHERE e.Empresa = f.Empresa AND e.NitEntidad = f.NitEntidad) NombreEntidad,
     f.CodConvenio, c.Nombre_Convenio
  FROM Facturas f INNER JOIN Convenios c
   ON f.Empresa = c.Empresa AND
      f.CodConvenio = c.Cod_Convenio
  WHERE f.Empresa = 'C30'  AND
        f.NoCuenta = 400000
       */
    public class ResponsablesCaso
    {
        public Entero ConsInterno { get; set; }
        public string NitEntidad { get; set; }
        public string NombreEntidad { get; set; }
        public string CodConvenio { get; set; }
        public string Nombre_Convenio { get; set; }

    }

    public interface IEstadosTrazabilidad
    {
        string CodEstadoTrazabilidad { get; set; }
        string NombreEstadoTrazabilidad { get; set; }
    }

    public class ResponsablesCasoTrazabilidad : ResponsablesCaso, IEstadosTrazabilidad
    {
        public string CodEstadoTrazabilidad { get; set; }
        [NoDataBase]
        public string NombreEstadoTrazabilidad { get; set; }

        public EnteroLargo IdTrazabilidadActual { get; set; }

    }

    public class TrazabilidadCuentaResponsable : ResponsablesCasoTrazabilidad
    {
        public Entero NoCuenta { get; set; }
        public Hora FechaEstado { get; set; }
        public string UsuarioEstado { get; set; }
        public string Estado { get; set; }
        public Moneda ValorFactura { get; set; }
    }
    /// <summary>
    /// Historico de Trazabilidad
    /// </summary>
    /// 
    public class TrazabilidadHistorico : IEstadosTrazabilidad
    {
        [DataObjectField(true)]
        public EnteroLargo IdTrazabilidad { get; set; }
        public Hora FechaTrazabilidad { get; set; }
        public string UsuarioHistTrazabilidad { get; set; }
        public string CodEstadoTrazabilidad { get; set; }
        [NoDataBase]
        public string NombreEstadoTrazabilidad { get; set; }
        public Hora FechaEstado { get; set; }
        public string UsuarioEstado { get; set; }
        public string Estado { get; set; }
        public Moneda ValorFactura { get; set; }
    }

    /*
     SELECT d.fecha_cargo, horacargo,d.cod_servicio_origen,
    (SELECT nombre FROM Servicios_Clinica s
      WHERE s.Empresa = d.Empresa AND
            s.Cod_Servicio = d.cod_servicio_origen ) NombreServicioOrigen,
     d.depinterno CodDependencia,
     d.NoOrdenServicio, d.CodServicio, d.NombreServicio, d.Cantidad, d.Precio ValorServicio,
     d.Porcentaje, d.CodClase, d.Usuario_Grabacion,d.fecha_real_cargo,d.Hora_real_cargo, d.ValorIva,
     d.CostoServicio, d.CedulaMedico,
     (SELECT CONCAT_WS(' ',nombre1,nombre2,apellido1,apellido2) 
       FROM Medicos m WHERE m.Empresa = d.Empresa AND m.cedula = d.CedulaMedico ) NombreMedico,
       d.CodEspecialidad,
       (SELECT Descripcion FROM Especialidades e
         WHERE e.Cod_Especialidad = d.CodEspecialidad ) NombreEspecialidad
  FROM Cuenta_Detalle d
  WHERE d.Empresa = 'C30' AND
        d.NoCuenta =  428252 AND
        d.ConsInternoFactura = 1 AND
        d.EstadoOrden <> 'X'
  ORDER BY 1,2     
     */

    public class DetalleCuenta
    {
        public Fecha fecha_cargo { get; set; }
        public string horacargo { get; set; }
        public string cod_servicio_origen { get; set; }
        public string NombreServicioOrigen { get; set; }
        public string CodDependencia { get; set; }
        public string NoOrdenServicio { get; set; }
        public string CodServicio { get; set; }
        public string NombreServicio { get; set; }
        public Moneda Cantidad { get; set; }
        public Moneda ValorServicio { get; set; }
        [NoDataBase]
        public Moneda TotalServicio => Cantidad * ValorServicio;
        public Moneda Porcentaje { get; set; }
        public string CodClase { get; set; }
        public string Usuario_Grabacion { get; set; }
        public Fecha fecha_real_cargo { get; set; }
        public string Hora_real_cargo { get; set; }
        public Moneda ValorIva { get; set; }
        public Moneda CostoServicio { get; set; }
        public string CedulaMedico { get; set; }
        public string NombreMedico { get; set; }
        public string CodEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }


    }

    public class HistoriasClinicasEmpresas
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Acompanante { get; set; }
        public string NoHistoria { get; set; }
        public string NoFolio { get; set; }
        public string NoCuenta { get; set; }
        public string NoAdmision { get; set; }
        public string Cod_Servicio { get; set; }
        public string NombreServicio { get; set; }
        public string CodDependencia { get; set; }
        public string CedulaMedico { get; set; }
        public Fecha FechaIngreso { get; set; }
        public string HoraIngreso { get; set; }
        public string usuario { get; set; }
        public string estado { get; set; }
        public string Urgencias { get; set; }
        public string consultaExterna { get; set; }
        public string RequiereMotivoIngreso { get; set; }
        public string cod_parent { get; set; }
        public Fecha FechaEgreso { get; set; }
        public string HoraEgreso { get; set; }

    }

    public class DatosAdmision
    {
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admisión")]
        public string NoAdmision { get; set; }
        [DisplayName("Fec.Ingreso")]
        public Fecha FechaIngreso { get; set; }
        [DisplayName("Hor.Ingreso")]
        public string HoraIngreso { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [NoDataBase]
        [DisplayName("Estado Ingreso")]
        public string NombreEstado
        {
            get
            {
                var nom = "";
                switch (Estado)
                {
                    case "I":
                        nom = "Ingreso";
                        break;
                    case "T":
                        nom = "Traslado";
                        break;
                    case "E":
                        nom = "Egreso";
                        break;
                    case "X":
                        nom = "Anulado";
                        break;
                }
                return nom;
            }
        }

        [DisplayName("Fec.Egreso")]
        public Fecha FechaEgreso { get; set; }
        [DisplayName("Hor.Egreso")]
        public string HoraEgreso { get; set; }
        [DisplayName("Servicio")]
        public string CodServicio { get; set; }
        [DisplayName("Nombre del Servicio")]
        public string NombreServicio { get; set; }
        [NoDataBase]
        [DisplayName("Servicio de Ingreso")]
        public string ServicioAdmision => $"{CodServicio}-{NombreServicio}";
        [DisplayName("Habitacion")]
        public string Habitacion { get; set; }
        [DisplayName("Pendiente Habitación")]
        public string Pendiente_Hab { get; set; }
        [DisplayName("Habitación")]
        public string NombreHabitacion => Pendiente_Hab == "S" ? "Pendiente" : Habitacion;
        [DisplayName("Cédula Médico")]
        public string CodMedico { get; set; }
        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        [DisplayName("Medico del Servicio")]
        public string MedicoServicio => $"{CodMedico}-{NombreMedico}";
        [DisplayName("Reingreso")]
        [BooleanEquivalent("S", "N")]
        public bool Reingreso { get; set; }
        [DisplayName("Autorización Salida")]
        [BooleanEquivalent("S", "N")]
        public bool Salida { get; set; }

    }//fin clase

    public class DatosBasicosHistoria
    {
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }
        [DisplayName("Fecha Atención")]
        public Fecha FechaAtencion { get; set; }
        [DisplayName("Hora Atención")]
        public string HoraAtencion { get; set; }
        [DisplayName("Motivo Consulta")]
        public string MotivoConsulta { get; set; }
        [DisplayName("Enfermedad Actual")]
        public string EnfermedadActual { get; set; }
        [DisplayName("Frec.Cardiaca")]
        public Moneda FrecCardiaca { get; set; }
        [DisplayName("Frec.Respiratoria")]
        public Moneda FrecRespitatoria { get; set; }
        [DisplayName("Presión")]
        public string PresionArterial { get; set; }
        [DisplayName("Temperatura")]
        public Moneda TEmperatura { get; set; }
        [DisplayName("Peso")]
        public Moneda Peso { get; set; }
        [DisplayName("Cédula del Médico")]
        public string CedulaMedico { get; set; }
        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        public string MedicoHistoria => $"{CedulaMedico}-{NombreMedico}";
        [DisplayName("Diagnostico Presuntivo")]
        public string Diagnostico { get; set; }
    }//fin clase

    public class OrdenesMedicamentos
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }

        [DisplayName("No.Orden Medica")]
        public string NoOrdenMedica { get; set; }
        [DisplayName("Fecha Orden")]
        public Fecha FechaOrden { get; set; }
        [DisplayName("Hora de la Orden")]
        public string HoraOrden { get; set; }
        [DisplayName("Cédula Médico Orden")]
        public string CedulaMedico { get; set; }
        [DisplayName("Orden Salida")]
        public string ordensalida { get; set; }
        [DisplayName("Nombre del Medico")]
        public string NombreMedico { get; set; }
        [DisplayName("Medico Orden")]
        [NoDataBase]
        public string MedicoOrden => $"{CedulaMedico}-{NombreMedico}";
        [DisplayName("Usuario Grabación")]
        public string UsuarioGrabacion { get; set; }
        [DisplayName("EstadoM")]
        public string estadoM { get; set; }
        [DisplayName("Estado Orden")]
        public string estado { get; set; }
        [DisplayName("Estado Aplicación")]
        public string EstadoAplicacion { get; set; }
        [DisplayName("No. Radicado")]
        public string noRadicado { get; set; }
        [DisplayName("Tipo Orden")]
        public string TipoOrden { get; set; }
        [DisplayName("Fecha Real Cargo")]
        public Fecha FechaReal { get; set; }
    }//fin clase

    public class OrdenMedicamentoDetalle
    {
        public string NoOrdenMedica { get; set; }
        public string ConsecutivoDetalle { get; set; }
        public string CodMedicamento { get; set; }
        public string NombreMedicamento { get; set; }
        public string Frecuencia { get; set; }
        public string EstadoAplicacion { get; set; }
        public string NomEstadoAplicacion { get; set; }
    }//fin clase

    public class OrdenesParaclinicos
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }

        [DisplayName("No.Folio")]
        public string NoFolio { get; set; }
        [DisplayName("No.Orden Médica")]
        public string NoOrdenMedica { get; set; }
        [DisplayName("Fec.Orden")]
        public Fecha FechaOrden { get; set; }
        [DisplayName("Hor.Orden")]
        public string HoraOrden { get; set; }
        [DisplayName("Cédula del Médico")]
        public string CedulaMedico { get; set; }
        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        [DisplayName("Médico de la Orden")]
        public string MedicoOrden => $"{CedulaMedico}-{NombreMedico}";

        [DisplayName("Usuario Grabación")]
        public string UsuarioGrabacion { get; set; }
        [DisplayName("Dependencia Orden")]
        public string CodDependencia { get; set; }
        [DisplayName("Dependencia Encabezado")]
        public string DptoEnc { get; set; }
        [DisplayName("Nombre de la Dependencia")]
        public string NomDependecia { get; set; }
        [NoDataBase]
        [DisplayName("Dependencia de la Orden")]
        public string DependenciaOrden => $"{CodDependencia} {NomDependecia}";

        [DisplayName("No. Orden Servicio")]
        public string noordenservicio { get; set; }
        [DisplayName("Esquema Tarifario")]
        public string CodEsquema { get; set; }
        [DisplayName("Usuario Grabación")]
        public string usuariograbacion { get; set; }
        [DisplayName("Valor Estado Orden")]
        public string estadoM { get; set; }
        [DisplayName("Estado Orden")]
        public string estado { get; set; }
        [DisplayName("No.Radicado")]
        public string noradicado { get; set; }
        [DisplayName("Fecha Real Orden")]
        public Fecha FechaReal { get; set; }
        [DisplayName("Justificación Orden")]
        public string justificacion { get; set; }
    }//fin clase

    public class OrdenParaClinicoDetalle
    {
        [DisplayName("No.Orden Médica")]
        public string NoOrdenMedica { get; set; }
        [DisplayName("Consecutivo Detalle")]
        public string ConsecutivoDetalle { get; set; }
        [DisplayName("Código del Servicio")]
        public string CodServicio { get; set; }
        [DisplayName("Nombre del Servicio Solicitado")]
        public string NombreServicio { get; set; }
        [DisplayName("Observaciones Generales")]
        public string Observaciones { get; set; }
    }//fin clase

    public class OrdenCuidadoPaciente
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }

        [DisplayName("No. Orden Médica")]
        public string NoOrdenMedica { get; set; }

        [DisplayName("Fecha de la Orden")]
        public Fecha FechaOrden { get; set; }

        [DisplayName("Hora de la Orden")]
        public string HoraOrden { get; set; }

        [DisplayName("Cédula del Médico")]
        public string CedulaMedico { get; set; }

        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        [DisplayName("Medico de la Orden")]
        public string MedicoOrden => $"{CedulaMedico}-{NombreMedico}";

        [DisplayName("Usuario Grabación")]
        public string UsuarioGrabacion { get; set; }

        [DisplayName("Estado Orden")]
        public string Estado { get; set; }

        [DisplayName("No.Radicado")]
        public string NoRadicado { get; set; }
    }//fin clase

    public class OrdenCiuidadPacienteDetalle
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }

        [DisplayName("No. Orden Médica")]
        public string NoOrdenMedica { get; set; }

        [DisplayName("Fecha de la Orden")]
        public Fecha FechaOrden { get; set; }

        [DisplayName("Hora de la Orden")]
        public string HoraOrden { get; set; }

        [DisplayName("Cédula del Médico")]
        public string CedulaMedico { get; set; }

        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        [DisplayName("Medico de la Orden")]
        public string MedicoOrden => $"{CedulaMedico}-{NombreMedico}";

        [DisplayName("Usuario Grabación")]
        public string UsuarioGrabacion { get; set; }

        [DisplayName("Estado Orden")]
        public string Estado { get; set; }

        [DisplayName("No.Radicado")]
        public string NoRadicado { get; set; }

        [DisplayName("Descripcion Cuidado")]
        public string DescripcionCuidado { get; set; }

        [DisplayName("Observacion Cuidado")]
        public string ObservacionCuidado { get; set; }
    }//fin clase

    public class EvolucionMedica
    {
        public string CiudadServer { get; set; }
        public string Empresa { get; set; }
        [DisplayName("No.Caso")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admision")]
        public string NoAdmision { get; set; }
        [DisplayName("No. de Historia")]
        public string NoHistoria { get; set; }
        [DisplayName("No. de Folio")]
        public string NoFolio { get; set; }
        [DisplayName("No. Nota Clínica")]
        public string NoNotaClinica { get; set; }
        [DisplayName("Fecha Nota")]
        public Fecha FechaNotaClinica { get; set; }
        [DisplayName("Hora Nota ")]
        public string HoraNotaClinica { get; set; }
        [DisplayName("Cédula del Médico")]
        public string CedulaMedicoNota { get; set; }
        [DisplayName("Nombre del Médico")]
        public string NombreMedico { get; set; }
        [NoDataBase]
        [DisplayName("Medico de la Orden")]
        public string MedicoNota => $"{CedulaMedicoNota}-{NombreMedico}";
        [DisplayName("Usuario Grabación")]
        public string UsuarioGrabacion { get; set; }
        [DisplayName("Usuario")]
        public string usuario { get; set; }
        [DisplayName("Resumen de la Nota (Para incluir en Epicrisis")]
        public string TituloResumenNota { get; set; }
        [DisplayName("Texto de la Nota de Evolución del Paciente")]
        public string TextoNotaClinica { get; set; }
        [DisplayName("Diagnostico Actualizado")]
        public string diagnostico { get; set; }
        [DisplayName("Estado")]
        public string estado { get; set; }
        [DisplayName("Especialidad")]
        public string Especialidad { get; set; }
        [DisplayName("Servicio")]
        public string Servicio { get; set; }
        [DisplayName("Justificación de indicaciones terapeuticas")]
        public string justificacion { get; set; }
        [DisplayName("Adicionar Epicrisis")]
        public string AdicionarEpicrisis { get; set; }
        [DisplayName("Resultados de Todos los Procedimientos Diagnósticos")]
        public string ResultadosExamenes { get; set; }
        [DisplayName("Fecha de Grabación")]
        public Fecha fechagraba { get; set; }
        [DisplayName("Hora de Grabacion")]
        public string horagraba { get; set; }
        [DisplayName("Nombre del Servicio")]
        public string nombreServicio { get; set; }
        [NoDataBase]
        [DisplayName("Servicio Nota")]
        public string ServicioNota => $"{Servicio} {nombreServicio}";
    }//fin clase

    public class InformeQuirurgico
    {
        [DisplayName("No.Historia")]
        public string NoHistoria { get; set; }
        [DisplayName("No.Folio")]
        public string NoFolio { get; set; }
        [DisplayName("No.Epicrisis")]
        public string NoEpicrisis { get; set; }
        [DisplayName("Servicio")]
        public string CodServicio { get; set; }
        [DisplayName("No.Cuenta")]
        public string NoCuenta { get; set; }
        [DisplayName("No.Admisión")]
        public string NoAdmision { get; set; }
        [DisplayName("Fecha Inicio")]
        public Fecha FechaInicio { get; set; }
        [DisplayName("Hora Inicio")]
        public string HoraInicio { get; set; }
        [DisplayName("Fecha Final")]
        public Fecha FechaFinal { get; set; }
        [DisplayName("Hora Final")]
        public string HoraFinal { get; set; }
        [DisplayName("Cédula Medico Cirujano")]
        public string CedulaMedico { get; set; }
        [DisplayName("Nombre Médico Cirujano")]
        public string NombreCedulaMedico { get; set; }
        [NoDataBase]
        [DisplayName("Cirujano")]
        public string MedicoCirujano => $"{CedulaMedico}-{NombreCedulaMedico}";

        [DisplayName("Cédula Medico Ayudante")]
        public string CedulaAyudante { get; set; }
        [DisplayName("Nombre Médico Ayudante")]
        public string NombreCedulaAyudante { get; set; }
        [NoDataBase]
        [DisplayName("Ayudante")]
        public string MedicoAyudante => $"{CedulaAyudante}-{NombreCedulaAyudante}";

        [DisplayName("Cedula del Anestesiologo")]
        public string CedulaAnestesiologo { get; set; }
        [DisplayName("Nombre Anestesiologo")]
        public string NombreCedulaAnestesiologo { get; set; }
        [NoDataBase]
        [DisplayName("Anestesiologo")]
        public string MedicoAnestesiologo => $"{CedulaAnestesiologo}-{NombreCedulaAnestesiologo}";
        [DisplayName("Diagnosticos PreQuirurgico")]
        public string DiagnosticoPrequirurgico { get; set; }
        [DisplayName("Diagnostico Pre 1")]
        public string DiagnosticoPre1 { get; set; }
        [DisplayName("Diagnostico Pre 2")]
        public string DiagnosticoPre2 { get; set; }
        [DisplayName("Diagnostico Pre 3")]
        public string DiagnosticoPre3 { get; set; }
        [DisplayName("Diagnostico Pre 4")]
        public string DiagnosticoPre4 { get; set; }
        [DisplayName("Diagnostico Pre 5")]
        public string DiagnosticoPre5 { get; set; }
        [DisplayName("Hallazgos")]
        public string Hallazgos { get; set; }
        [DisplayName("Procedimientos Realizados")]
        public string ProcedimientosRealizados { get; set; }
        [DisplayName("Diagnostico Pos 1")]
        public string DiagnosticoPos1 { get; set; }
        [DisplayName("Diagnostico Pos 2")]
        public string DiagnosticoPos2 { get; set; }
        [DisplayName("Diagnostico Pos 3")]
        public string DiagnosticoPos3 { get; set; }
        [DisplayName("Diagnostico Pos 4")]
        public string DiagnosticoPos4 { get; set; }
        [DisplayName("Diagnostico Pos 5")]
        public string DiagnosticoPos5 { get; set; }
        [DisplayName("Justificación del Procedimiento")]
        public string JustificacionProc { get; set; }
        [DisplayName("Diagnosticos PostQuirurgico:")]
        public string DiagnosticoPostQuirurgico { get; set; }
        [DisplayName("Descripción del Procedimiento")]
        public string DescripcionProc { get; set; }
        [DisplayName("Usuario que Graba")]
        public string UsuarioGraba { get; set; }
        [DisplayName("Fecha de Grabación")]
        public Fecha FechaGraba { get; set; }
        [DisplayName("Hora de Grabación")]
        public string HoraGraba { get; set; }
        [DisplayName("Usuario Actualización")]
        public string UsuarioAct { get; set; }
        [DisplayName("Fecha Actualización")]
        public Fecha FechaAct { get; set; }
        [DisplayName("Hora Actualizacón")]
        public string HoraAct { get; set; }
        [DisplayName("Consecutivo Cirugía")]
        public string ConsCir { get; set; }
        [DisplayName("Código del Convenio")]
        public string CodConvenio { get; set; }
        [DisplayName("Consecutivo Interno Factura")]
        public string ConsInternoFactura { get; set; }
        [DisplayName("Consecutivo")]
        public string Consecutivo { get; set; }
        [DisplayName("Conducta a Seguir")]
        public string Conducta { get; set; }
        [DisplayName("Ordenes Médicas")]
        public string OrdenesMedicas { get; set; }
        [DisplayName("Tiene Foto")]
        public string TieneFoto { get; set; }
        [DisplayName("Cédula Médico que Opera")]
        public string CedulaMedicoOpera { get; set; }
        [DisplayName("Nombre Medico que Opera")]
        public string NombreCedulaMedicoOpera { get; set; }
        [NoDataBase]
        [DisplayName("Medico que Opera")]
        public string MedicoOpera => $"{CedulaMedicoOpera}-{NombreCedulaMedicoOpera}";

        [DisplayName("Deja Mechas, Drenes, Comprensas, Gasas, Cuantas y Explique:")]
        public string DrenesMechas { get; set; }
        [DisplayName("Material Osteosintesis")]
        public string MaterialOsteosintesis { get; set; }
        [DisplayName("Tipo de Anestesia")]
        public string tipoanestesia { get; set; }
        [DisplayName("Osteosintesis")]
        public string Osteosintesis { get; set; }
    }//fin clase

    public class ConsultaCasos
    {
        [DisplayName("No. de Cuenta")]
        public string NoCuenta { get; set; }
        [DisplayName("No. de Historia")]
        public string NoHistoria { get; set; }
        [DisplayName("Fecha de Ingreso")]
        public Fecha FechaIngreso { get; set; }
        [DisplayName("Nombre del Paciente")]
        public string NombrePaciente { get; set; }
    }//fin clase

    public class ConsultaPacienteEventoAdverso
    {
        [DisplayName("Nombres")]
        public string NombrePaciente { get; set; }
        [DisplayName("Apellidos")]
        public string ApellidoPaciente { get; set; }
        [DisplayName("No de Identificacion")]
        public string NoIdentificacion { get; set; }
        [DisplayName("No Caso")]
        public string NoCaso { get; set; }
    }//fin clase

    public class ConsultaEventosAdversos
    {

        [DisplayName("Fecha Evento")]
        public string Fecha { get; set; }
        [DisplayName("Hora Evento")]
        public string Hora { get; set; }
        [DisplayName("Servicio")]
        public string Servicio { get; set; }
        [DisplayName("No Caso")]
        public string NoCaso { get; set; }
        [DisplayName("Nombre del Paciente")]
        public string NombrePaciente { get; set; }
        [DisplayName("Apellido del Paciente")]
        public string ApellidoPaciente { get; set; }
        [DisplayName("Sucesos del Evento")]
        public string Sucesos { get; set; }
        [DisplayName("Causas del Evento")]
        public string Causas { get; set; }

    }//fin clase

    public class ConsultaUsuariosAdv
    {

        [DisplayName("Usuario")]
        public string Usuario { get; set; }
        [DisplayName("PassWord")]
        public string PassWordUsu { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Role {get;set;}
    }//fin clase

    public class ConsultaIPEmpresas
    {
        [DisplayName("Empresa")]
        public string Empresa { get; set; }
        [DisplayName("IpConexion")]
        public string IpConexion { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [DisplayName("Ubicacion")]
        public string Ubicacion { get; set; }
    }//fin clase

    public class ConsultaPacientesRonda
    {
        [DisplayName("No. de Cuenta")]
        public string NoCaso { get; set; }
        [DisplayName("No. de Historia")]
        public string NoHistoria { get; set; }
        [DisplayName("No. de Admision")]
        public string NoAdmision { get; set; }
        [DisplayName("No. de Folio")]
        public string NoFolio { get; set; }
        [DisplayName("Codigo Servicio")]
        public string Cod_Servicio { get; set; }
        [DisplayName("Nombre Servicio")]
        public string NombreServicio { get; set; }
        [DisplayName("Nombres")]
        public string NombrePaciente { get; set; }
        [DisplayName("Documento")]
        public string NoDocumento { get; set; }


    }//fin clase

    public class ConsultaEspecialidades
    {
        [DisplayName("Codigo Especialidad")]
        public string Cod_Especialidad { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Especialidad { get; set; }
    }

    public class ConsultaTiemposRM
    {
        [DisplayName("Id Tiempo")]
        public Int32 Id_Tiempo { get; set; }
        [DisplayName("Codigo Tiempo")]
        public string Cod_Tiempo { get; set; }
        public Int32 Duracion { get; set; }

    }

    public class ConsultaReconmendaciones
    {
        [DisplayName("Id Ronda")]
        public Int32 Id_RondaMed { get; set; }
        [DisplayName("Id Reconmendacion")]
        public string Cod_Reconmendacion { get; set; }
        [DisplayName("Descripcion")]
        public string Reconmendacion { get; set; }
        public string Tipo { get; set; }
        public Fecha Fecha_Creacion { get; set; }
        public string Estado { get; set; }
        public string Cod_Dieta { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }


    }

    public class ConsultaProcedimientoQx
    {
        [DisplayName("Id Ronda")]
        public Int32 Id_RondaMed { get; set; }
        [DisplayName("Codigo ProcedQx")]
        public string Cod_ProcedQx { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }

    public class ConsultaViasAplicaMed
    {
        [DisplayName("Codigo Via Aplicacion")]
        public string CodViaAplicacion { get; set; }
        [DisplayName("Nombre Via")]
        public string NombreViaAplicacion { get; set; }
        public string Estado { get; set; }
    }

    public class ConsultaUnidadMedicamentos
    {
        [DisplayName("Codigo Unidad")]
        public string Unidad { get; set; }
        [DisplayName("Nombre Unidad")]
        public string NombreUnidad { get; set; }
        public decimal EquivalenciaConsumo { get; set; }
    }

    public class MaestroArticulos
    {
        [DataObjectField(true)]
        public string CodArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string Unidad_Consumo { get; set; }
        public string Pos { get; set; }
        public string Empresa { get; set; }
        public string PrincipioActivo { get; set; }
    }

    public class Ronda_Med_ProcedimientoQx
    {
        public Int32 Id_RondaMed { get; set; }
        public string Cod_ProcedQx { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }

    public class historia_cuidadosenf_enc
    {
        [DisplayName("Codigo Cuidados")]
        public string CodigoCuidadoEnc { get; set; }
        public string DescripcionCuidadoEnc { get; set; }
        public string Alias { get; set; }
        public string Estado { get; set; }
    }

    public class ConsultaCuidados
    {
        [DisplayName("Codigo Cuidados")]
        public string CodigoCuidadoEnc { get; set; }
        public string DescripcionCuidadoEnc { get; set; }
        public string CodigoCuidado { get; set; }
        public string DescripcionCuidado { get; set; }
    }

    public class ConsultaDietas
    {
        [DisplayName("Codigo Cuidados")]
        public string CodigoCuidadoEnc { get; set; }
        public string DescripcionCuidadoEnc { get; set; }
        public string CodigoCuidado { get; set; }
        public string DescripcionCuidado { get; set; }
    }

    public class ConsultaMedicos
    {
        [DisplayName("Cedula Medicos")]
        public string Cedula { get; set; }
        public string Medico { get; set; }
        public string Cod_Especialidad { get; set; }
    }

    public class ConsultaRondaMed
    {
        [DisplayName("Id Ronda")]
        public Int32 Id_RondaMed { get; set; }
        public Fecha Fecha { get; set; }
        public string Hora { get; set; }
    }

    public class ConsultaKardex
    {
        public string CodPab { get; set; }
        public string Nombre { get; set; }
        public string CodHab { get; set; }
        public string Habitacion { get; set; }
        public string NoCuenta { get; set; }
        public string NombrePaciente { get; set; }
        public Fecha FechaIngreso { get; set; }
        public string NoHistoria { get; set; }
        public string Nombre_Convenio { get; set; }
        public string NoCuentaMaestra { get; set; }
        public string Edad { get; set; }
        public string CodMedico { get; set; }
        public string NombreMedico { get; set; }
        public string Cod_Especialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        public string Diagnostico { get; set; }
        public string NoAdmision { get; set; }
        public string Ruta { get; set; }

    }

    public class ConsultaServicios
    {
        [DisplayName("Id Servicio")]
        public string CodServicio { get; set; }
        public string NomServicio { get; set; }
        public string CodDependencia { get; set; }
        public string NomDependecia { get; set; }
    }

    public class ConsultaDependencias
    {
        public string CodDependencia { get; set; }

        public string NomDependecia { get; set; }

    }

    public class ConsultaResponsables
    {
        public string NitEntidad { get; set; }
        public string Nombre_Entidad { get; set; }
        public decimal ConsInterno { get; set; }
        public string CodConvenio { get; set; }
        public string CodEsquemaTar { get; set; }
        public string TipoTopeSoat { get; set; }
        public string soat { get; set; }
        public string CopagoPorNivel { get; set; }
        public decimal ValorFactura { get; set; }
        public decimal Valor_Descuento { get; set; }
        public string Cod_Ent_Admin { get; set; }
        public string Nofactura { get; set; }
        public string NomEPS { get; set; }
        public decimal Valor_coopago { get; set; }
        public decimal valor_moderadora { get; set; }
    }

    public class EsquemasTarifarios
    {
        [DisplayName("Id Esquema")]
        public string CodEsquema { get; set; }

    }

    public class HistoriaClinica
    {
        public decimal ConseOrdenesMedicas { get; set; }
        public string DiagnosticoIngreso { get; set; }
        //        Empresa varchar(4)
        //NoHistoria varchar(15)
        //NoFolio decimal (10,0)
        //NoCuenta decimal (10,0)
        //NoAdmision decimal (10,0)
        //Cod_Servicio char (2)

    }

    public class conse_hist_sol_cirugia
    {
        public decimal NoBoleta { get; set; }

    }

    public class ConsultaHabitacion
    {
        public string CodPab { get; set; }
        public string Nombre { get; set; }
        public string CodHab { get; set; }
        public string NomHab { get; set; }

    }

    public class ConsultaEmpresa
    {
        public string Empresa { get; set; }
        public string Nombre_Empresa { get; set; }
        public string EmpresaInv { get; set; }


    }

    public class CasoSIRAS
    {
        public string NoCaso { get; set; }
        public string NoHistoria { get; set; }
        public string Paciente { get; set; }
        public string RadicadoEnvio { get; set; }
        public string RadicadoAnulado { get; set; }
        public string Reporte { get; set; }
        public string Observacion { get; set; }
        public string DatosEnviados { get; set; }
        public Fecha Fecha { get; set; }
        public string NoCuenta { get; set; }
        public string Estado { get; set; }
        public Fecha FechaIngreso { get; set; }
        public string HoraIngreso { get; set; }

    }

    public class PacientesSIRAS
    {
        public string Empresa { get; set; }
        public string NoCuenta { get; set; }
        public string Paciente { get; set; }
        public string Identificacion { get; set; }
        public Fecha FechaIngreso { get; set; }
        public string HoraIngreso { get; set; }
        public string Tiempo { get; set; }
    }

    public class PacientesMIPRES
    {
        public string Empresa { get; set; }
        public string NoCuenta { get; set; }
        public string Paciente { get; set; }
        public string Identificacion { get; set; }
        public Fecha FechaIngreso { get; set; }
        public string HoraIngreso { get; set; }
        public string FechaEgreso { get; set; }
        public string HoraEgreso { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
    }

    public class tipos_gestion_siras
    {
        public string CodigoGestion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }

    public class Causas_Siras
    {
        public string CodCausa { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }

    public class parametros_siras
    {
        public string key { get; set; }
        public string token { get; set; }
        public string Url { get; set; }
        public string Maneja_Ws { get; set; }
        public string UrlAnulacion { get; set; }
        public string HoraAutomatico { get; set; }

    }

    public class parametros_mipres
    {
        public string Nit { get; set; }
        public string token { get; set; }
        public string Maneja_Ws { get; set; }
        public string Empresa { get; set; }
        public string UrlPrescripciones { get; set; }
        public string UrlUnaPrescripcion { get; set; }
        public string UrlValidaToken { get; set; }
        public string UrlEntregaAmbito { get; set; }
        public string UrlReporteEntrega { get; set; }
        public string TokenGenerado { get; set; }
        public string tokenproveedor { get; set; }
        public string UrlEntregasFechas { get; set; }
        public string UrlAnularNoEntrega { get; set; }
        public string UrlAnularReporteEntrega { get; set; }
        public string UrlEntregaPrescripcion { get; set; }
        public string UrlReportePrescripcion { get; set; }
        public string UrlReportesFechas { get; set; }
        public string UrlFacturacion { get; set; }


    }

    public class planilla_det_siras
    {
        public string Empresa { get; set; }
        public decimal NoCuenta { get; set; }
        public Fecha FechaGestion { get; set; }
        public string HoraGestion { get; set; }
        public string TipoGestion { get; set; }
        public string CodCausa { get; set; }
        public string Comentario { get; set; }
        public string RutaArchivo { get; set; }
        public string Estado { get; set; }
        public string UsuarioGraba { get; set; }
    }

    public class datosaccidente
    {
        //public string empresa { get; set; }
        //public string NoCuenta { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string FechaNacimiento { get; set; }
        public string TipoIngreso { get; set; }
        public string Fecha_Ingreso { get; set; }
        public string Hora_Ingreso { get; set; }
        public string EsVictimaRemitida { get; set; }
        public string RazonSocialRemitente { get; set; }
        public string CodigoHabilitacionRemitente { get; set; }
        public string CodigoDepartamentoRemitente { get; set; }
        public string CodigoMunicipioRemitente { get; set; }
        public string TrasladoTransporteEspecial { get; set; }
        public string CodigoPrestador { get; set; }
        public string PlacaTransporte { get; set; }
        public string Fecha_Accidente { get; set; }
        public string Hora_Accidente { get; set; }
        public string Direccion { get; set; }
        public string CodigoDepartamento { get; set; }
        public string CodigoMunicipio { get; set; }
        public string EsVehiculoIdentificado { get; set; }
        public string Placa { get; set; }
        public string PrimerApellido_Conductor { get; set; }
        public string SegundoApellido_Conductor { get; set; }
        public string PrimerNombre_Conductor { get; set; }
        public string SegundoNombre_Conductor { get; set; }
        public string TipoIdentificacion_Conductor { get; set; }
        public string Identificacion_Conductor { get; set; }
        public string NoDocumento_Cond { get; set; }
        public string Direccion_Conductor { get; set; }
        public string Telefono_Conductor { get; set; }
        public string CodigoDepartamento_Conductor { get; set; }
        public string CodigoMunicipio_Conductor { get; set; }
        public string TipoIdentificacion_Reporta { get; set; }
        public string Identificacion_Reporta { get; set; }
        public string CodigoPrestador_Reporta { get; set; }

    }

    public class requeridos_modulos
    {
        public string CampoReq { get; set; }
        public string Alfanumerico { get; set; }
        public string Panel { get; set; }

    }

    public class DataTableroCasosPendientes
    {
        public string FechaHoraIng { get; set; }
        public string NoCaso { get; set; }
        public string codServicio { get; set; }
        public string paciente { get; set; }
        public string Plan { get; set; }
        public string Responsable { get; set; }
        public string Diagnostico { get; set; }
        public string fechaingreso { get; set; }
        public string horaingreso { get; set; }

    }

    public class EgresosCasos
    {

        public string empresa { get; set; }
        public string NoCuenta { get; set; }
        public string NoAdmision { get; set; }
        public Fecha fechaIngreso { get; set; }
        public string horaingreso { get; set; }
        public string Servicio { get; set; }
        public string estado { get; set; }
        public Fecha fechaegreso { get; set; }
        public string horaEgreso { get; set; }
        public string estadoIng { get; set; }
        public string EstAdm { get; set; }
        public string servTras { get; set; }
        public string no_Autorizacion { get; set; }
        public string autorizado_Por { get; set; }
    }

    public class ExtractoCasos
    {
        public string empresa { get; set; }
        public string NoCuenta { get; set; }
        public string NoAdmision { get; set; }
        public Fecha fechaIngreso { get; set; }
        public string horaingreso { get; set; }
        public string Servicio { get; set; }
        public string estado { get; set; }
        public Fecha fechaegreso { get; set; }
        public string horaEgreso { get; set; }
        public string estadoIng { get; set; }
        public string EstAdm { get; set; }
        public string servTras { get; set; }
        public string no_Autorizacion { get; set; }
        public string autorizado_Por { get; set; }
    }

    public class DetalleExtractoMIPRES
    {
        public string NoCuenta { get; set; }
        public string fecha_cargo { get; set; }
        public decimal ConsInternofactura { get; set; }
        public string CodigoArt { get; set; }
        public string NombreArticulo { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal total { get; set; }
        public string CumArt { get; set; }
        public string CodigoCUM { get; set; }
        public string CodigoCUM_Alterno { get; set; }
    }

    public class entrega_mipres
    {
        public string Empresa { get; set; }
        public string NoPrescripcion { get; set; }
        public string NoCuenta { get; set; }
        public string NoIdentificacion { get; set; }
        public string Responsable { get; set; }
        public string CodigoCum { get; set; }
        public decimal Id { get; set; }
        public decimal IdEntrega { get; set; }
        public decimal IdReporte { get; set; }
        public decimal VlrEntrega { get; set; }
        public decimal IdAnulacion { get; set; }
        public Fecha FechaProceso { get; set; }
        public string Estado { get; set; }
        public string Paciente { get; set; }
        public string Valor { get; set; }
        public string NitEntidad { get; set; }
        public string NoHistoria { get; set; }
        public string CodEPS { get; set; }
        public decimal CantEnt { get; set; }
        public decimal PrecioUni { get; set; }
    }




    #endregion

    /// <summary>
    /// Información Visualizar en Tablero de Cirugía
    /// </summary>
    //public class DataTableroCirugia
    //{
    //    public Fecha MaxFechaHoraCirugia { get; set; }

    //    public Hora FechaActual { get; set; }
    //    public long DiferenciaMinutos { get; set; }

    //    [NoDataBase]
    //    public string DiferenciaMinitosFmt
    //    {
    //        get
    //        {
    //            var tm = (new TimeSpan(0, (int)DiferenciaMinutos, 0));
    //            var str = tm.ToString();
    //            return str.Substring(0, str.Length - 3);
    //        }
    //    }
    //    public Fecha Fecha { get; set; }
    //    public string Hora { get; set; }
    //    public Fecha FechaHoraBoleta { get; set; }
    //    public string NoBoleta { get; set; }
    //    public string Nocuenta { get; set; }
    //    public string NoAdmision { get; set; }
    //    public string NoHistoria { get; set; }
    //    public string NombrePaciente { get; set; }
    //    public string ServicioSolicitado { get; set; }
    //    public string EmpresaCuenta { get; set; }
    //    public string ConsInternoFactura { get; set; }
    //    public string CodServicio { get; set; }
    //    public string NoAdmisionIngCir { get; set; }
    //    public string NoCuentaIngCir { get; set; }
    //    public string EstadoCirugia { get; set; }
    //    public string EstadoBoleta { get; set; }
    //    public string Programada { get; set; }
    //    public Fecha FechaHoraProgramacion { get; set; }
    //    public string EstadoAdmisionCir { get; set; }
    //    public string ConsCirugia { get; set; }
    //    public string NitEntidad { get; set; }
    //    public string NombreEntidad { get; set; }
    //    public string CodConvenio { get; set; }
    //    public string Nombre_Convenio { get; set; }
    //    public string CodPlan { get; set; }
    //    public string NombrePlan { get; set; }
    //    public long MinutosMaximoCirugia { get; set; }
    //    [NoDataBase]
    //    public string NombreEstado
    //    {
    //        get
    //        {
    //            var nom = "En Tramite";//"Solicitud";
    //            if (EstadoBoleta != "A")
    //                nom = "En Tramite";
    //            else if (EstadoBoleta == "A")
    //            {
    //                nom = "Autorizada";
    //            }
    //            /*if (FechaHoraProgramacion != null)
    //              nom = "Programada";
    //            if (NoAdmisionIngCir != null)
    //              nom = "Preparación";*/
    //            return nom.ToUpper();
    //        }
    //    }

    //    [NoDataBase]
    //    public string NombreEntidadResponsablePago => $"{NombreEntidad}-{Nombre_Convenio}";

    //    [NoDataBase]
    //    public string ColorFila
    //    {
    //        get
    //        {
    //            var color = "Green";
    //            var v50 = (MinutosMaximoCirugia / 2);
    //            var v90 = ((MinutosMaximoCirugia / 10) * 9);
    //            if (DiferenciaMinutos < v50)
    //                color = "Green";
    //            else if (DiferenciaMinutos.Between(v50, v90))
    //                color = "Yellow";
    //            else
    //                color = "Red";
    //            return color;
    //        }
    //    }


    //    [NoDataBase]
    //    public string ColorLetra
    //    {
    //        get
    //        {
    //            var color = "Black";
    //            var v50 = (MinutosMaximoCirugia / 2);
    //            var v90 = ((MinutosMaximoCirugia / 10) * 9);
    //            if (DiferenciaMinutos < v50)
    //                color = "White";
    //            else if (DiferenciaMinutos.Between(v50, v90))
    //                color = "Black";
    //            else
    //                color = "White";
    //            return color;
    //        }
    //    }


    //    [NoDataBase]
    //    public int FontSize => "FontSize".AppValueConfig<int>(20);

    //}// fin clase

    //public class InfoResultados
    //{
    //    #region Parámetros
    //    public Entero IdReporte { get; set; }
    //    public string vNoInterno { get; set; }
    //    public string vUsuBacteriologo { get; set; }
    //    public string vRevisado { get; set; }
    //    public string vEmpresaLab { get; set; }
    //    public string vFechaResul { get; set; }
    //    public string vHoraResul { get; set; }
    //    public string vNoHistoria { get; set; }
    //    public string vNomPaciente { get; set; }
    //    public string vNoRadicado { get; set; }

    //    #endregion

    //    #region Formulas
    //    public string fFechaResultado { get; set; }
    //    public string fMedico { get; set; }
    //    public string fCama { get; set; }
    //    public string fFechaSYS { get; set; }
    //    public string fHoraSYS { get; set; }
    //    public string fLogoResultadosLab { get; set; }
    //    public string fEdad { get; set; }
    //    public string fDireccion { get; set; }

    //    #endregion
    //}//fin clase

}//fin namespa



