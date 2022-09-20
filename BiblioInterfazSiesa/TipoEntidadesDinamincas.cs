
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
  /// Tipos de Entidades Dinámicas de las Facturas
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NombreTipoEntidad")]
  public class TipoEntidadesDinamincas : BaseData 
  {

    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public TipoEntidadesDinamincas()
    {
      Estado = "A";
    }
    #endregion
    /// <summary>
    /// Código Entidad Dinámica
    /// </summary>
    [Description("Código Entidad Dinámica")]
    [DisplayName("Código Entidad Dinámica")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodTipoEntidad { get; set; }
    /// <summary>
    /// Nombre Entidad Dinámica
    /// </summary>
    [Description("Nombre Entidad Dinámica")]
    [DisplayName("Nombre Entidad Dinámica")]
    [SearchColumn]
    public string NombreTipoEntidad { get; set; }
    /// <summary>
    /// Nombre Entidad Dinámica Extendido
    /// </summary>
    [NoDataBase]
    public string NombreTipoEntidadExtendido
    {
      get
      {
        return CodTipoEntidad + " - "  + NombreTipoEntidad;
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

