using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT011 : BaseRegistroFT
  {
    /// <summary>
    ///Corresponde al numeral asignado a cada concepto determinado en la tabla de condiciones financieras en los casos que aplique
    ///Alfanumérico
    /// Ver “Tabla Condiciones Financieras” (2)
    /// </summary>
    public string concepto { get; set; }

    /// <summary>
    ///Valor correspondiente al concepto requerido a la fecha de corte
    ///Numérico(18)
    /// </summary>
    public long valor { get; set; }


  }
}
