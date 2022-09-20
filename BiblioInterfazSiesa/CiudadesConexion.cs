
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
  /// Conexión de Ciudades
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NombreCiudadServer")]
  public class CiudadesConexion : BaseData 
  {

    #region Constructores
    /// <summary>
    /// Constructor por Defecto
    /// </summary>
    public CiudadesConexion()
    {
      Estado = "A";
    }
    #endregion
    /// <summary>
    /// Ciudad del Servidor
    /// </summary>
    [Description("Ciudad del Servidor")]
    [DisplayName("Ciudad del Servidor")]
    [UpperCase]
    [DataObjectField(true)]
    [SearchColumn]
    public string CiudadServer { get; set; }
    /// <summary>
    /// Nombre Ciudad del Servidor
    /// </summary>
    [Description("Nombre Ciudad del Servidor")]
    [DisplayName("Nombre Ciudad del Servidor")]
    [UpperCase]
    [SearchColumn]
    public string NombreCiudadServer { get; set; }
    /// <summary>
    /// Nombre Ciudad del Servidor Extendido
    /// </summary>
    [NoDataBase]
    public string NombreCiudadServerExtendido
    {
      get
      {
        return CiudadServer + " - "  + NombreCiudadServer;
      }
    }
    /// <summary>
    /// Empresa para Conexion
    /// </summary>
    [Description("Empresa para Conexion")]
    [DisplayName("Empresa para Conexion")]
    public string EmpresaConexion { get; set; }
    /// <summary>
    /// Nombre Para Empresa para Conexion
    /// </summary>
    [NoDataBase(SelectFromDB = true,
      NombreTabla = "Empresas", CampoOrigen = "EmpresaConexion", CampoValor = "empresa",
      CampoVisual = "NombreEmpresa", NombreColumna = "NombreEmpresa",
      ManejaEmpresa = false, ManejaSucursal = false,
      NombreClase = "LiloSoft.Siesa.Interfaz.Empresas", EnsambladoClase = "[]",
      CampoValorEquivalente="")]
     public string NombreEmpresa { get; set; }
    /// <summary>
    /// Estado Activo Inactivo
    /// </summary>
    [Description("Estado Activo Inactivo")]
    [DisplayName("Estado Activo Inactivo")]
    public string Estado { get; set; }

  } // Fin Clase

} // Fin NameSpace

