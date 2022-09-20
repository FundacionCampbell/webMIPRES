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
    public class EventoAdversoDB: LiloSoft.Web.ProveedorWeb.BaseDB
    {

        #region Constructores
        /// <summary>
        /// Constructor por Defecto
        /// </summary>
        public EventoAdversoDB()
        {
            ComportamientoIndividualComandos = true;
            TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
        }
        /// <summary>
        /// Constructor con Objeto de Base de Datos
        /// </summary>
        /// <param name="db">Manejador de Bases de Datos</param>
        public EventoAdversoDB(DB db)
          : this()
        {
            SqlDb = db;
        }
        #endregion


        #region Metodos de Acceso a Datos
        /// <summary>
        /// Insertar un Registro en Evento Adv
        /// </summary>
        /// <param name="m">Objeto CiudadesConexion a Insertar</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(EventoAdverso m)
        {
            List<Parametro> lst = GetParametros(m);
              var sql = GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Insert);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Insert), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }

        /// <summary>
        /// Actualizar un Registro en Evento Adv
        /// </summary>
        /// <param name="m">Objeto Eventoadverso a Actualizar</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(EventoAdverso m)
        {
            List<Parametro> lst = GetParametros(m);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Update), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }

        /// <summary>
        /// Eliminar un Registro Evento Adv
        /// </summary>
        /// <param name="m">Objeto EventoAdverso a Eliminar</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(EventoAdverso m)
        {
            List<Parametro> lst = GetParametros(m, true);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Delete), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }


        #endregion



        #region Metodos de Listas
        /// <summary>
        /// Lista de Todos los Convenios
        /// </summary>
        /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
        /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
        /// <returns>Lista de Convenios</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<EventoAdverso> GetEventosAdv(string Criterio, String Empresa)
        {
            var lstParam = new List<Parametro>();
            var sqlText = "";
            if (string.IsNullOrEmpty(Criterio))
            {
                sqlText = GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Todos);
                lstParam.AddParametro("Empresa", Empresa);

            }
            else
            {
                sqlText = GetInstruccionSql<EventoAdverso>(TiposProcedimientosAlmacenados.Criterio);
                lstParam.AddParametro("Criterio", Criterio);
                lstParam.AddParametro("Empresa", Empresa);

            }
            var lista = ExecuteGetLista<EventoAdverso>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
            return lista;
        }
        #endregion
    }
}
