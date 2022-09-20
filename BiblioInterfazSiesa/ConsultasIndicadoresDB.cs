using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Siesa.Interfaz.Indicadores;
using LiloSoft.Types.Data;
using LiloSoft.Utils;
using LiloSoft.Web.ProveedorWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  public class ConsultasIndicadoresDB:BaseDB
  {
    #region Constructores
    public ConsultasIndicadoresDB() : base() { }
    public ConsultasIndicadoresDB(DB SqlDb) : this()
    {
      this.SqlDb = SqlDb;
    }
    #endregion

    #region Consultas de Lista
    /// <summary>
    /// Datos para Indicador de Reingreso de Urgencias
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <param name="MarcadoReingreso">Marcado para Reingreso S:Los Marcados en Admisiones N:Los Potenciasles Reingresos de Acuerdo a la Norma</param>
    /// <returns></returns>
    public List<DataReingresoUrgencias> GetIndicadorUrgencias(string Empresa, Fecha FechaInicial, Fecha FechaFinal, string MarcadoReingreso)
    {
      var sql = "GetIndicadorUrgencias";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      lstPars.AddParametro("MarcadoReingreso", MarcadoReingreso);
      return ExecuteGetLista<DataReingresoUrgencias>(sql, true, lstPars.ToArray());
    }

    public List<PacientesPorServicio> GetPacientesPorServicio(string Empresa, Fecha FechaInicial, Fecha FechaFinal, bool Urgencias = false, bool Camas = false, bool Cirugia = false, bool UnServicio = false, string CodServicio = "")
    {
      var sql = "GetTotalPacientesServicio";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      lstPars.AddParametro("Urgencias", (Urgencias ? "S" : "N"));
      lstPars.AddParametro("Camas", Camas ? "S" : "N");
      lstPars.AddParametro("Cirugia", Cirugia ? "S" : "N");
      lstPars.AddParametro("UnServicio", UnServicio ? "S" : "N");
      lstPars.AddParametro("CodServicio", CodServicio);

      return ExecuteGetLista<PacientesPorServicio>(sql, true, lstPars.ToArray());
    }

    /// <summary>
    /// Datos para Indicador de Reingreso de Hospitalizacion
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <param name="MarcadoReingreso">Marcado para Reingreso S:Los Marcados en Admisiones N:Los Potenciasles Reingresos de Acuerdo a la Norma</param>
    /// <param name="CodServicio">Código del Servicio Particular</param>
    /// <param name="ServiciosCamas">Servicios que Manejan Camas T:Todos los Servicios U:Servicio Particular </param>
    /// <returns></returns>
    public List<DataReingresoHospitalizacion> GetIndicadorHospitalizacion(string Empresa, Fecha FechaInicial, Fecha FechaFinal, string MarcadoReingreso, string ServiciosCamas, string CodServicio)
    {
      var sql = "GetIndicadorHospitalizacion";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      lstPars.AddParametro("MarcadoReingreso", MarcadoReingreso);
      lstPars.AddParametro("ServiciosCamas", ServiciosCamas);
      lstPars.AddParametro("CodServicio", CodServicio);
      return ExecuteGetLista<DataReingresoHospitalizacion>(sql, true, lstPars.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Empresa">Empresa de Trabajo</param>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <returns></returns>
    public List<DataIndicadorGeneral> GetIndicadorGeneralPrimeraVez(string Empresa, Fecha FechaInicial, Fecha FechaFinal, bool SeleccionarPlanes, IEnumerable<string> Planes, bool SeleccionarOrigen, IEnumerable<string> Origenes)
    {
      List<DataIndicadorGeneral> lista = null;
      try
      {
        var lstParamTable = new List<Parametro>();
        var indexParam = 1;
        var sqlTable = new StringBuilder(@"
CREATE TEMPORARY TABLE IF NOT EXISTS SelectPlanTmp ( Valor varchar(6)) Engine=Memory;
CREATE TEMPORARY TABLE IF NOT EXISTS SelectOrigenTmp ( Valor varchar(6)) Engine=Memory;
");
        if (!SqlDb.EjecutarComando(sqlTable.ToString(), false))
          throw SqlDb.UltimaExcepcion;
        /*if (!SqlDb.EjecutarComando(sqlTable.ToString(), false, lstParamTable.ToArray()))
          throw SqlDb.UltimaExcepcion;*/
        if (SeleccionarPlanes)
        {
          Planes.ForEachExt(c =>
          {
            /*sqlTable.AppendLine($"Insert Into SelectPlanTmp Values (:Plan{indexParam});");
            lstParamTable.AddParametro($"Plan{indexParam}", c);
            indexParam++;*/
            if (!SqlDb.EjecutarComando("Insert Into SelectPlanTmp Values (:Plan)", false, new Parametro("Plan", c)))
              throw SqlDb.UltimaExcepcion;
          });
        }
        if (SeleccionarOrigen)
        {
          Origenes.ForEachExt(c =>
          {
            /*sqlTable.AppendLine($"Insert Into SelectOrigenTmp Values (:Origen{indexParam});");
            lstParamTable.AddParametro($"Origen{indexParam}", c);
            indexParam++;*/
            if (!SqlDb.EjecutarComando("Insert Into SelectOrigenTmp Values (:Origen)", false, new Parametro("Origen", c)))
              throw SqlDb.UltimaExcepcion;
          });
        }
        /*if (!SqlDb.EjecutarComando(sqlTable.ToString(), false,lstParamTable.ToArray()))
          throw SqlDb.UltimaExcepcion;*/
        var sql = "GetIndicadorGeneralPrimeraVez";
        var lstPars = new List<Parametro>();
        lstPars.AddParametro("Empresa", Empresa);
        lstPars.AddParametro("FechaInicial", FechaInicial);
        lstPars.AddParametro("FechaFinal", FechaFinal);
        lstPars.AddParametro("SeleccionarPlan", (SeleccionarPlanes ? "S" : "N"));
        lstPars.AddParametro("SeleccionarOrigen", (SeleccionarOrigen ? "S" : "N"));
        lista = ExecuteGetLista<DataIndicadorGeneral>(sql, true, lstPars.ToArray());
      }
      finally
      {
        var sqlTable = @"
DROP TEMPORARY TABLE IF EXISTS SelectPlanTmp;
DROP TEMPORARY TABLE IF EXISTS SelectOrigenTmp;";
        if (!SqlDb.EjecutarComando(sqlTable, false))
          throw SqlDb.UltimaExcepcion;
      }
      return lista;
    }

    /// <summary>
    /// Indicador de Productividad de Mercadeo
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <returns></returns>
    public List<DataProductividadMercadeo> GetIndicadorProductivoMercadeo(Fecha FechaInicial, Fecha FechaFinal)
    {
      var sql = "IndicadorProductivoMercadeo";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      return ExecuteGetLista<DataProductividadMercadeo>(sql, true, lstPars.ToArray());
    }

    /// <summary>
    /// Indicador de Productividad de Mercadeo
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <returns></returns>
    public List<DataVentaPromedioPaciente> GetIndicadorVentaPromedioPaciente(string Empresa, Fecha FechaInicial, Fecha FechaFinal)
    {
      var sql = "IndicadorVentaPromedioPaciente";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      return ExecuteGetLista<DataVentaPromedioPaciente>(sql, true, lstPars.ToArray());
    }

    /// <summary>
    /// Indicador de Estudios Ordenados por Médicos
    /// </summary>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <returns></returns>
    public List<DataEstudiosOrdMedicos> GetIndicadorEstudosPorMedico(Fecha FechaInicial, Fecha FechaFinal)
    {
      var sql = "IndicadorEstudosPorMedico";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      return ExecuteGetLista<DataEstudiosOrdMedicos>(sql, true, lstPars.ToArray());
    }

    /// <summary>
    /// Indicador de Venta de Urgencias
    /// </summary>
    /// <param name="Empresa">Empresa</param>
    /// <param name="CodPlan">Plan Tarifario</param>
    /// <param name="FechaInicial">Fecha Inicial</param>
    /// <param name="FechaFinal">Fecha Final</param>
    /// <returns>Indicador Venta Urgencias</returns>
    public IndicadorVentaUrgencias GetIndicadorVentasUrgencias(string Empresa, string CodPlan, Fecha FechaInicial, Fecha FechaFinal)
    {
      var indica = new IndicadorVentaUrgencias();
      var sql = "IndicadorVentasUrgencias";
      var lstPars = new List<Parametro>();
      lstPars.AddParametro("Empresa", Empresa);
      lstPars.AddParametro("CodPlan", CodPlan);
      lstPars.AddParametro("FechaInicial", FechaInicial);
      lstPars.AddParametro("FechaFinal", FechaFinal);
      var dsFact = SqlDb.EjecutarConsultaSet(sql, true, lstPars.ToArray());
      if (dsFact == null) throw SqlDb.UltimaExcepcion;
      var dtIngre = dsFact.Tables[0];
      var dtVenta = dsFact.Tables[1];
      var dtPlanes = dsFact.Tables[2];
      var lstIngre = dtIngre.ToListObject<DataIngresosUrgencias>();
      var lstVentas = dtVenta.ToListObject<DataVentaUrgencias>();
      var lstPlanes = dtPlanes.ToListObject<Planes_Tarifarios>();
      indica.IngresosUrgencias = lstIngre;
      indica.VentaUrgencias = lstVentas;
      indica.Planes = lstPlanes;
      return indica;
    }

    #endregion
  }//fin clase
}//fin namespace
