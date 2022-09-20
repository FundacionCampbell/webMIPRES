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
    [DataObject(true)]
    [Description("")]
    class historia_ordenes_medicasDB : LiloSoft.Web.ProveedorWeb.BaseDB
    {
        #region Constructores
        /// <summary>
        /// Constructor por Defecto
        /// </summary>
        public historia_ordenes_medicasDB()
        {
            ComportamientoIndividualComandos = true;
            TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
        }
        /// <summary>
        /// Constructor con Objeto de Base de Datos
        /// </summary>
        /// <param name="db">Manejador de Bases de Datos</param>
        public historia_ordenes_medicasDB(DB db)
          : this()
        {
            SqlDb = db;
        }
        #endregion

        #region Metodos de Acceso a Datos
        /// <summary>
        /// Insertar un Registro 
        /// </summary>
        /// <param name="m">Objeto CiudadesConexion a Insertar</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(historia_ordenes_medicas m)
        {
            List<Parametro> lst = GetParametros(m);
            var sql = GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Insert);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Insert), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }

        /// <summary>
        /// Actualizar un Registro 
        /// </summary>
        /// <param name="m">Objeto Eventoadverso a Actualizar</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(historia_ordenes_medicas m)
        {
            List<Parametro> lst = GetParametros(m);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Update), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }

        /// <summary>
        /// Eliminar un Registro
        /// </summary>
        /// <param name="m">Objeto EventoAdverso a Eliminar</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(historia_ordenes_medicas m)
        {
            List<Parametro> lst = GetParametros(m, true);
            if (!SqlDb.EjecutarComando(GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Delete), EsProcedimientoAlmacenado, lst.ToArray()))
            {
                throw new Exception(SqlDb.UltimoError);
            }
        }


        #endregion

        #region Metodos de Listas
        /// <summary>
        /// Lista de Todo
        /// </summary>
        /// <param name="Criterio">Criterio de Busqueda de la Clase</param>
        /// <param name="Empresa">Empresa de Trabajo Seleccionada</param>
        /// <returns>Lista de Convenios</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<historia_ordenes_medicas> GetOrdenesMedicas(string Criterio, String Empresa)
        {
            var lstParam = new List<Parametro>();
            var sqlText = "";
            if (string.IsNullOrEmpty(Criterio))
            {
                sqlText = GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Todos);
                lstParam.AddParametro("Empresa", Empresa);

            }
            else
            {
                sqlText = GetInstruccionSql<historia_ordenes_medicas>(TiposProcedimientosAlmacenados.Criterio);
                lstParam.AddParametro("Criterio", Criterio);
                lstParam.AddParametro("Empresa", Empresa);

            }
            var lista = ExecuteGetLista<historia_ordenes_medicas>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
            return lista;
        }
        #endregion


    }
}
