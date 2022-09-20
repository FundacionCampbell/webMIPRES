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
    [TableName("evento_adverso")]
    public class EventoAdverso : BaseData
    {
        [DisplayName("Empresa de Trabajo")]
        [DataObjectField(true)]
        public string Empresa { get; set; } /* Codigo Empresa de Conexion*/

        public string NoDocumento { get; set; } /* No Identificacion del paciente */
        public string NoCuenta { get; set; } /* No Caso del paciente*/
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string Servicio { get; set; } /* Descripcion del servicio en el evento*/
        public Fecha Fecha { get; set; } /* Fecha del Suceso*/
        public string Hora { get; set; } /* Hora del suceso*/
        public string Sucesos { get; set; } /* Suceso del evento*/
        public string Causas { get; set; } /* Que Influyo para el suceso*/
        public string IpConexion { get; set; } /* Ip de Conexion del usuario para identificar la sede o ciudad y empresa*/
        [BooleanEquivalent("A","I")]
        public bool Estado { get; set; } /* Estado */
        public Fecha FechaCreacion { get; set; } /*Fecha Registro en el sistema*/

    }
}
