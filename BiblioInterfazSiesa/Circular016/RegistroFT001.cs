using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT001:BaseRegistroFT
  {
    public int codigoConcepto { get; set; }
    /// <summary>
    /// Clase de Concepto 1:Corriente, 2:No Corriente, 3:No Aplica
    /// </summary>
    public int claseConcepto { get; set; }
    public long valor { get; set; }
  }//fin  clase
}//fin namespace