using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  public class DataEstudiosOrdMedicos
  {
  /// <summary>
  /// Nombre de la Empresa
  /// </summary>
  public string Empresa { get; set; }
    /// <summary>
    /// Nombre de la Empresa
    /// </summary>
    public string NombreEmpresa { get; set; }
    /// <summary>
    /// Cédula del Médico
    /// </summary>
    public string CedulaMedico { get; set; }
    /// <summary>
    /// Nombre del Médico
    /// </summary>
    public string NombreMedico { get; set; }
    /// <summary>
    /// Empresa Cuenta Maestra
    /// </summary>
    public string EmpresaCuentaMaestra { get; set; }
    /// <summary>
    /// No. de Empresa Cuenta Maestra
    /// </summary>
    public string NoCuentaMaestra { get; set; }
    /// <summary>
    /// Historia Paciente
    /// </summary>
    public string NoHistoria { get; set; }
    /// <summary>
    /// Nombre del Paciente
    /// </summary>
    public string NombrePaciente { get; set; }
    /// <summary>
    /// Valor de la Venta 
    /// </summary>
    public Moneda Venta { get; set; }
    /// <summary>
    /// Estudios Realizados por el Médico
    /// </summary>
    public Moneda Estudios { get; set; }
    public object GrupoCuentaMaestra => $"{EmpresaCuentaMaestra}-{NoCuentaMaestra}";
  }//fin clase

  public class IndicadorEstudiosOrdMedicos
  {
    public string Empresa { get; set; }
    public string NombreEmpresa { get; set; }

    /// <summary>
    /// Cédula del Médico
    /// </summary>
    public string CedulaMedico { get; set; }
    /// <summary>
    /// Nombre del Médico
    /// </summary>
    public string NombreMedico { get; set; }
    public Moneda TotalCuentasMaestras { get; set; }
    public Moneda TotalVenta { get; set; }
    public Moneda TotalEstudios { get; set; }
    
    public decimal PromedioEstudios => (TotalCuentasMaestras.ValorInterno == 0 ? 0 : (TotalEstudios / TotalCuentasMaestras));
    public decimal PromedioVenta => (TotalCuentasMaestras.ValorInterno == 0 ? 0 : (TotalVenta / TotalCuentasMaestras));
  }
}//fin namespace
