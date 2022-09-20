
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
  /// Tipos de Archivos Circular 016 2016
  /// </summary>
  [ShowColumn(ColumnaMostrar = "CodTipoArchivo")]
  public class TiposArchivos : BaseData 
  {

    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public TiposArchivos()
    {
      Estado = "A";
    }
    #endregion
    /// <summary>
    /// Tipo Archivo
    /// </summary>
    [Description("Tipo Archivo")]
    [DisplayName("Tipo Archivo")]
    [UpperCase]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodTipoArchivo { get; set; }
    /// <summary>
    /// Descripción del Tipo de Archivo
    /// </summary>
    [Description("Descripción del Tipo de Archivo")]
    [DisplayName("Descripción del Tipo de Archivo")]
    [UpperCase]
    [SearchColumn]
    public string NombreTipoArchivo { get; set; }
    /// <summary>
    /// Descripción del Tipo de Archivo Extendido
    /// </summary>
    [NoDataBase]
    public string NombreTipoArchivoExtendido
    {
      get
      {
        return CodTipoArchivo + " - "  + NombreTipoArchivo;
      }
    }
    /// <summary>
    /// Estado Activo Inactivo
    /// </summary>
    [Description("Estado Activo Inactivo")]
    [DisplayName("Estado Activo Inactivo")]
    public string Estado { get; set; }

  } // Fin Clase
} // Fin NameSpace

