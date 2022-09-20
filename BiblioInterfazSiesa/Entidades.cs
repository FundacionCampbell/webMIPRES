using LiloSoft.DataBase.ConectaDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Entidades del Sistema Esculapio
  /// </summary>
  [MultiEmpresa]
  public class Entidades
  {
    [DataObjectField(true)]
    public string Empresa { get; set; }
    [DataObjectField(true)]
    [SearchColumn]
    public string NitEntidad { get; set; }
    [SearchColumn]
    public string Nombre_Entidad { get; set; }
    public string Estado { get; set; }
  }//fin clase
}//fin namespace
