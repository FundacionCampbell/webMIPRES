using LiloSoft.DataBase.ConectaDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  [TableName("Empresa")]
  public class EmpresaEsculapioIps
  {
    [DataObjectField(true)]
    public string empresa { get; set; }
    public string Nombre_Empresa { get; set; }
    public string Nit { get; set; }
    public string Ciudad { get; set; }
    public string CodHabilitacion { get; set; }
    public string CodPrestador { get; set; }
    public string NombreComercial { get; set; }
    [NoDataBase]
    public string NombreEmpresaCompleto => $"{Nombre_Empresa} {NombreComercial}";

    [NoDataBase]
    public string NombreEmpresaExtendido => $"{empresa}-{NombreEmpresaCompleto}";

  }
}
