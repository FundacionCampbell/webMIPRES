using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiloSoft.Utils;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  /// <summary>
  /// Clase para Identificación del Archivo a Exportar
  /// </summary>
  public class ArchivoExportar
  {
    public ArchivoExportar()
    {
      ExtensionArchivo = "xml";
      NumArchivo = 1;
    }
    /// <summary>
    /// Número de identificación tributaria de la entidad que reporta
    /// </summary>
    public string NitEntidad { get; set; }
    /// <summary>
    /// Dígito de Verificación
    /// </summary>
    public string DigitoVerificacion { get; set; }
    /// <summary>
    /// Periodo de corte de la información reportada
    /// </summary>
    public string CodPeriodoReporte { get; set; }
    /// <summary>
    /// Año de Corte de la Información Reportada
    /// </summary>
    public Entero AnhoReporte { get; set; }
    /// <summary>
    /// Tipo de Archivo a Reportar
    /// </summary>
    public string TipoArchivo { get; set; }
    /// <summary>
    /// Número del Archivo
    /// </summary>
    public Entero NumArchivo { get; set; }
    /// <summary>
    ///  Extensión del archivo
    /// </summary>
    public string ExtensionArchivo { get; set; }

    public string NombreArchivoInformacion
    {
      get
      {
        var num = TipoArchivo;
        if ("ArchivoCircular016".AppValueConfig() == "Numero") num = NumArchivo.ToString().PadLeft(5, '0');
        return "{0}{1}{2}{3}{4}.{5}".Formato(NitEntidad, DigitoVerificacion, CodPeriodoReporte, AnhoReporte, num, ExtensionArchivo);
      }
    }
  }
}
