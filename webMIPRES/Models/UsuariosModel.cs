using LiloSoft.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webMIPRES.Models
{
    public class UsuariosModel : BaseData
    {

        public string Empresa { get; set; } /* Codigo Empresa de Conexion*/

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe Ingresar Usuario")]
        public string usuario { get; set; } /* Usuario de conexion*/

        [Display(Name = "PassWord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe Digitar un PassWord")]
        public string PassWordUsu { get; set; }

        public string Nombre { get; set; } /*Nombre Usuario*/
        public string LoginErrorMsg { get; set; } /* Mensaje de Error*/

        public string userRole{get;set;}


    }

}