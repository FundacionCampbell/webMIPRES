using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LiloSoft.Siesa.Interfaz
{
  public class ControlCargueSiesa
  {
    [DataObjectField(true)]
    public string Empresa { get; set; }
    [DataObjectField(true)]
    public EnteroLargo IdControlCargue { get; set; }
    public string MotivoCargue { get; set; }
    public Fecha Fecha { get; set; }
    public string Hora { get; set; }
    public string Usuario { get; set; }
  }
}
