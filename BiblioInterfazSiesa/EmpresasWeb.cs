using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
    public class EmpresasWeb
    {
        public string BaseDatos
        {
            get;
            set;
        }

        [DataObjectField(true)]
        public string Empresa
        {
            get;
            set;
        }

        public string EmpresaEsculapio
        {
            get;
            set;
        }

        public string NombreEmpresa
        {
            get;
            set;
        }

        public string Puerto
        {
            get;
            set;
        }

        public string Servidor
        {
            get;
            set;
        }

        public EmpresasWeb()
        {
        }
    }
}
