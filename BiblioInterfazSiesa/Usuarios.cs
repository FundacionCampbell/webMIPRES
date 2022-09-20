using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
    [MultiEmpresa]
    [TableName("Usuarios")]
    class Usuarios : BaseData
    {
        //[DisplayName("Empresa de Trabajo")]
        // public string Empresa { get; set; } /* Codigo Empresa de Conexion*/

        [DisplayName("Usuario")]
        [DataObjectField(true)]
        public string Usuario { get; set; } /* Usuario de conexion */

        [DisplayName("PassWord")]
        public string PassWordUsu { get; set; } /* No Caso del paciente*/

        public string Nombre { get; set; }


    }
}
