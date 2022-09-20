using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Servicios de la Institución
  /// </summary>
  public class Servicios_Clinica
  {
    /// <summary>
    /// Código del Servicio
    /// </summary>
    [DataObjectField(true)]
    public string Cod_Servicio { get; set; }
    /// <summary>
    /// Nombre del Servicio
    /// </summary>
    public string Nombre { get; set; }
    public string NombreExtendido
    {
      get { return "{0} - {1}".Formato(Cod_Servicio, Nombre); }
    }
    public string Estado { get; set; }
    public string Habitacion { get; set; }
  }//fin clase
}//fin namespace
