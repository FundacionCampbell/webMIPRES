
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
  /// Conexiones a las Empresas
  /// </summary>
  [ShowColumn(ColumnaMostrar = "Empresa")]
  [MultiEmpresa]
  public class EmpresaConeccion : BaseData 
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    [Description("Empresa de Trabajo")]
    [DisplayName("Empresa de Trabajo")]
    [DataObjectField(true)]
    [SearchColumn]
    public string Empresa { get; set; }
    /// <summary>
    /// Nombre Para Empresa de Trabajo
    /// </summary>
    [NoDataBase(SelectFromDB = true,
      NombreTabla = "Empresas", CampoOrigen = "Empresa", CampoValor = "empresa",
      CampoVisual = "NombreEmpresa", NombreColumna = "NombreEmpresa",
      ManejaEmpresa = false, ManejaSucursal = false)]
     public string NombreEmpresa { get; set; }
    /// <summary>
    /// Empresa en Esculapio
    /// </summary>
    [Description("Empresa en Esculapio")]
    [DisplayName("Empresa en Esculapio")]
    public string EmpresaEsculapio { get; set; }
    /// <summary>
    /// Servidor de Base de Datos Esculapio
    /// </summary>
    [Description("Servidor de Base de Datos Esculapio")]
    [DisplayName("Servidor de Base de Datos Esculapio")]
    public string Servidor { get; set; }
    /// <summary>
    /// Puerto del Servidor
    /// </summary>
    [Description("Puerto del Servidor")]
    [DisplayName("Puerto del Servidor")]
    public string Puerto { get; set; }
    /// <summary>
    /// Base de Datos Servidor
    /// </summary>
    [Description("Base de Datos Servidor")]
    [DisplayName("Base de Datos Servidor")]
    public string BaseDatos { get; set; }
    /// <summary>
    /// Usuario de Base de Datos
    /// </summary>
    [Description("Usuario de Base de Datos")]
    [DisplayName("Usuario de Base de Datos")]
    public string Usuario { get; set; }
    /// <summary>
    /// Clave de Usuario
    /// </summary>
    [Description("Clave de Usuario")]
    [DisplayName("Clave de Usuario")]
    public string Clave { get; set; }

  } // Fin Clase
} // Fin NameSpace

