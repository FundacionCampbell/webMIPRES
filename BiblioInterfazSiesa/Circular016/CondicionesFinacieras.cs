
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LiloSoft.Types.Data;
using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  /// <summary>
  /// Tabla de Condiciones Financieras Circular 016 2017
  /// </summary>
  [ShowColumn(ColumnaMostrar = "CodConcepto")]
  public class CondicionesFinacieras : BaseData 
  {

    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public CondicionesFinacieras()
    {
      Estado = "A";
    }
    #endregion
    /// <summary>
    /// Código Concepto
    /// </summary>
    [Description("Código Concepto")]
    [DisplayName("Código Concepto")]
    [DataObjectField(true)]
    [SearchColumn]
    public Entero CodConcepto { get; set; }
    /// <summary>
    /// Código Concepto Extendido
    /// </summary>
    [NoDataBase]
    public string CodConceptoExtendido
    {
      get
      {
        return CodConcepto.ToString() + " - "  + CodConcepto;
      }
    }
    /// <summary>
    /// Descripción del Concepto
    /// </summary>
    [Description("Descripción del Concepto")]
    [DisplayName("Descripción del Concepto")]
    [SearchColumn]
    public string NombreConcepto { get; set; }
    /// <summary>
    /// Estado Activo Inactivo
    /// </summary>
    [Description("Estado Activo Inactivo")]
    [DisplayName("Estado Activo Inactivo")]
    public string Estado { get; set; }

  } // Fin Clase
} // Fin NameSpace

