using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
/// <summary>
/// Lista de Correos para el Area de Admisiones
/// </summary>
  public class AdmisionesCorreos
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [DataObjectField(true)]
    public string Empresa { get; set; }
    /// <summary>
    /// Correo Electrónico
    /// </summary>
    [DataObjectField(true)]
    public string Correo { get; set; }
    /// <summary>
    /// Clave del Correo Electrónico
    /// </summary>
    public string Clave { get; set; }
  }//fin clase
}//fin namepsace
