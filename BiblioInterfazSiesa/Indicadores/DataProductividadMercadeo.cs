using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  /// <summary>
  /// Data para Indicador Productividad de Mercadeo
  /// </summary>
  public class DataProductividadMercadeo
  {
    /// <summary>
    /// Empresa de Trabajo
    /// </summary>
    public string Empresa { get; set; }
    /// <summary>
    /// Nombre de la Empresa
    /// </summary>
    public string NombreEmpresa { get; set; }
    /// <summary>
    /// No. de Cuenta Maestra
    /// </summary>
    public string NoCuenta { get; set; }
    /// <summary>
    /// No. de Historia
    /// </summary>
    public string NoHistoria { get; set; }
    /// <summary>
    /// Nombre del Paciente
    /// </summary>
    public string NombrePaciente { get; set; }
    /// <summary>
    /// No. de Casos Generados
    /// </summary>
    public Entero NoCasos { get; set; }
    /// <summary>
    /// Plan Tarifario
    /// </summary>
    public string Plan { get; set; }
    /// <summary>
    /// Valor Venta
    /// </summary>
    public Moneda ValorVenta { get; set; }
    /// <summary>
    /// Valor Pagdo
    /// </summary>
    public Moneda ValorPagado { get; set; }
    /// <summary>
    /// Porcentaje de Productividad
    /// </summary>
    public decimal PorcProdutividad => ValorVenta > 0 ? (ValorPagado / ValorVenta) * 100 : 0; 
  }//fin clase
}//fin namespace
