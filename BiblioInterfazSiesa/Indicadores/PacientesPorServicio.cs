using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  /// <summary>
  /// Total de Pacientes Atendidos por Servicio
  /// </summary>
  public class PacientesPorServicio
  {
    /// <summary>
    /// Código del Servicio
    /// </summary>
    public string CodServicio { get; set; }
    /// <summary>
    /// Nombre del Servicio
    /// </summary>
    public string NombreServicio { get; set; }
    /// <summary>
    /// Número de Pacientes Atendidos para el Servicio
    /// </summary>
    public Entero NumeroPacientes { get; set; }
  }//fin clase
}//fin namespace
