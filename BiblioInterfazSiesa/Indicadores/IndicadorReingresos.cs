using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  public class IndicadorReingresos : PacientesPorServicio
  {
    /// <summary>
    /// Número de 
    /// </summary>
    public Entero NumeroReingresos { get; set; }
    public Moneda Indicador
    {
      get
      {
        var indicador = 0m;
        if (NumeroPacientes!=null && NumeroPacientes != 0)
        {
          indicador = (decimal)NumeroReingresos / (decimal)NumeroPacientes;
        }
        return indicador;
      }
    }
  }
}
