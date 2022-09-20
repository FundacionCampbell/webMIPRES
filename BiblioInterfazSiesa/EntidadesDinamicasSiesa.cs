
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
  /// Parametrización Entidades Dinámicas
  /// </summary>
  [ShowColumn(ColumnaMostrar = "CodTipoEntidad")]
  [MultiEmpresa]
  public class EntidadesDinamicasSiesa : BaseData 
  {
    /// <summary>
    /// Empres de Trabajo
    /// </summary>
    [Description("Empres de Trabajo")]
    [DisplayName("Empres de Trabajo")]
    [DataObjectField(true)]
    [SearchColumn]
    public string Empresa { get; set; }
    /// <summary>
    /// Código Tipo de Entidad Dinámica
    /// </summary>
    [Description("Código Tipo de Entidad Dinámica")]
    [DisplayName("Código Tipo de Entidad Dinámica")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodTipoEntidad { get; set; }
    /// <summary>
    /// Código Tipo de Entidad Dinámica Extendido
    /// </summary>
    [NoDataBase]
    public string CodTipoEntidadExtendido
    {
      get
      {
        return CodTipoEntidad + " - "  + NombreTipoEntidad;
      }
    }
    /// <summary>
    /// Nombre Para Código Tipo de Entidad Dinámica
    /// </summary>
    [NoDataBase(SelectFromDB = true,
      NombreTabla = "TipoEntidadesDinamincas", CampoOrigen = "CodTipoEntidad", CampoValor = "CodTipoEntidad",
      CampoVisual = "NombreTipoEntidad", NombreColumna = "NombreTipoEntidad",
      ManejaEmpresa = false, ManejaSucursal = false)]
     public string NombreTipoEntidad { get; set; }
    /// <summary>
    /// Clase de Documento S1ESA
    /// </summary>
    [Description("Clase de Documento S1ESA")]
    [DisplayName("Clase de Documento S1ESA")]
    public string CodClaseDocumento { get; set; }
    /// <summary>
    /// Código del Grupo de Entidad S1ESA
    /// </summary>
    [Description("Código del Grupo de Entidad S1ESA")]
    [DisplayName("Código del Grupo de Entidad S1ESA")]
    public string CodGrupoEntidad { get; set; }
    /// <summary>
    /// Código de la Entidad
    /// </summary>
    [Description("Código de la Entidad")]
    [DisplayName("Código de la Entidad")]
    public string CodEntidad { get; set; }
    /// <summary>
    /// Código del Atributo Entidad S1ESA
    /// </summary>
    [Description("Código del Atributo Entidad S1ESA")]
    [DisplayName("Código del Atributo Entidad S1ESA")]
    public string CodAtributo { get; set; }
    /// <summary>
    /// Código del Maestro Entidades
    /// </summary>
    [Description("Código del Maestro Entidades")]
    [DisplayName("Código del Maestro Entidades")]
    public string CodMaestroEntidades { get; set; }
    /// <summary>
    /// Código del Maestro Detalle Entidades S1ESA
    /// </summary>
    [Description("Código del Maestro Detalle Entidades S1ESA")]
    [DisplayName("Código del Maestro Detalle Entidades S1ESA")]
    public string CodMaestroDetalleEntidad { get; set; }
    /// <summary>
    /// Tipo de Entidad S1ESA
    /// </summary>
    [Description("Tipo de Entidad S1ESA")]
    [DisplayName("Tipo de Entidad S1ESA")]
    public string CodTipoEntidadSiesa { get; set; }

  } // Fin Clase
} // Fin NameSpace

