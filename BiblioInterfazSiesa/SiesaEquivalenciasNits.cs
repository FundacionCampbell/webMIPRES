
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LiloSoft.Types.Data;
using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Equivalencia de Nits para S1ESA
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NitOriginal")]
  [MultiEmpresa]
  public class SiesaEquivalenciasNits : BaseData 
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [Description("Empresa de Trabajo")]
    [DisplayName("Empresa de Trabajo")]
    [DataObjectField(true)]
    public string Empresa { get; set; }
    /// <summary>
    /// Nit Original
    /// </summary>
    [Description("Nit Original")]
    [DisplayName("Nit Original")]
    [UpperCase]
    [DataObjectField(true)]
    [SearchColumn]
    public string NitOriginal { get; set; }
    /// <summary>
    /// Nit Original Extendido
    /// </summary>
    [NoDataBase]
    public string NitOriginalExtendido
    {
      get
      {
        return NitOriginal + " - "  + NitOriginal;
      }
    }
    /// <summary>
    /// Nit Reemplazar
    /// </summary>
    [Description("Nit Reemplazar")]
    [DisplayName("Nit Reemplazar")]
    [UpperCase]
    [SearchColumn]
    public string NitReemplazo { get; set; }

  } // Fin Clase

} // Fin NameSpace

