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
    class UsuariosDB : LiloSoft.Web.ProveedorWeb.BaseDB
    {
        #region Constructores
        /// <summary>
        /// Constructor por Defecto
        /// </summary>
        public UsuariosDB()
        {
            ComportamientoIndividualComandos = true;
            TipoComandoIndividual = LiloSoft.Web.ProveedorWeb.TipoComando.InstruccionSQL;
        }
        /// <summary>
        /// Constructor con Objeto de Base de Datos
        /// </summary>
        /// <param name="db">Manejador de Bases de Datos</param>
        public UsuariosDB(DB db)
          : this()
        {
            SqlDb = db;
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
        public List<Usuarios> GetUsuariosAdv(string Criterio, String Empresa)
        {
            var lstParam = new List<Parametro>();
            var sqlText = "";
            if (string.IsNullOrEmpty(Criterio))
            {
                sqlText = GetInstruccionSql<Usuarios>(TiposProcedimientosAlmacenados.Todos);
                lstParam.AddParametro("Empresa", Empresa);



            }
            else
            {
                sqlText = GetInstruccionSql<Usuarios>(TiposProcedimientosAlmacenados.Criterio);
                lstParam.AddParametro("Criterio", Criterio);
                lstParam.AddParametro("Empresa", Empresa);

            }
            var lista = ExecuteGetLista<Usuarios>(sqlText, EsProcedimientoAlmacenado, lstParam.ToArray());
            return lista;
        }
        #endregion


    }
}
