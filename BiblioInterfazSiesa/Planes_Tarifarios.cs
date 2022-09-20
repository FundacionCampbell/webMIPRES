using LiloSoft.DataBase.ConectaDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz
{
  public class Planes_Tarifarios
  {
    public Planes_Tarifarios()
    {
      IncluirInterfazSiesa = true;
      UtilizarNitFactura = true;
    }
    [DataObjectField(true)]
    public string Empresa { get; set; }
    [DataObjectField(true)]
    public string CodPlan { get; set; }
    public string NombrePlan { get; set;}
    public string Estado { get; set; }
    /// <summary>
    /// Incluir este plan en la Intefaz de S1ESA
    /// </summary>
    [BooleanEquivalent("S","N")]
    public bool IncluirInterfazSiesa { get; set; }
    /// <summary>
    /// Utilizar el Nit de la Factura o el Predeterminado del Sistema
    /// </summary>
    [BooleanEquivalent("S", "N")]
    public bool UtilizarNitFactura { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [NoDataBase]
    public string NombrePlanExtendido { get { return "{0} - {1}".Formato(CodPlan, NombrePlan); } }
  }//fin clase
}//fin namespace
