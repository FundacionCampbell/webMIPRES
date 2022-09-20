using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  public class Origen
  {
    [DataObjectField(true)]
    public string Empresa { get; set; }
    [DataObjectField(true)]
    public string origen { get; set; }
    public string Nombre { get; set; }
    public string Estado { get; set; }
  }//fin clase
}//fin namespace
