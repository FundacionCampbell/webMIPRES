using LiloSoft.DataBase.ConectaDB;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  /// <summary>
  /// Datos para Venta Promedio del Paciente
  /// </summary>
  public class DataVentaPromedioPaciente
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
    /// Código del Plan
    /// </summary>
    public string CodPlan { get; set; }
    /// <summary>
    /// Nombre del Plna
    /// </summary>
    public string NombrePlan { get; set; }
    /// <summary>
    /// Código del Servicio de la Clinica
    /// </summary>
    public string CodServicio { get; set; }
    /// <summary>
    /// Empresa de la Cuenta Maestra
    /// </summary>
    public string EmpresaCuentaMaestra { get; set; }
    /// <summary>
    /// No. de la Cuenta Maestra
    /// </summary>
    public string NoCuentaMaestra { get; set; }
    [NoDataBase]
    public string GrupoCuentaMaestra => $"{EmpresaCuentaMaestra}-{NoCuentaMaestra}";

    /// <summary>
    /// No de Historia del Paciente
    /// </summary>
    public string NoHistoria { get; set; }
    /// <summary>
    /// Nombre del Paciente
    /// </summary>
    public string NombrePaciente { get; set; }
    /// <summary>
    /// Nombre del Servicio
    /// </summary>
    public string NombreServicio { get; set; }
    /// <summary>
    /// Valor de la Venta
    /// </summary>
    public Moneda Venta { get; set; }
  }// fin clase

  public class IndicadorVentaPromedioPaciente
  {
    public string Empresa { get; set; }
    public string NombreEmpresa { get; set; }

    public string CodPlan { get; set; }
    public string NombrePlan { get; set; }
    public string CodServicio { get; set; }
    public string NombreServicio { get; set; }

    public Moneda TotalCuentasMaestras { get; set; }
    public Moneda TotalVenta { get; set; }

    public decimal Indicador => (TotalVenta.ValorInterno == 0 ? 0 : (TotalVenta / TotalCuentasMaestras));
  }//fin clase
}// fin namespace
