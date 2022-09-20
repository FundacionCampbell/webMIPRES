using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  /// <summary>
  /// Detalle de las Facturas de Esculapio
  /// </summary>
  public class DetalleFacturas
  {
    public string NoFactura { get; set; }
    public Fecha Fecha_Emision { get; set; }
    public Fecha Fecha_Vencimiento { get; set; }
    public string NoCuenta { get; set; }
    public Entero ConsInterno{get;set;}
    public Fecha FechaIngreso { get; set; }
    public string HoraIngreso { get; set; }
    public Fecha FechaEgreso { get; set; }
    public string HoraEgreso { get; set; }
    public string NitEntidad { get; set; }
    public string Sucursal_Tercero { get; set; }
    public string Punto_Envio { get; set; }
    public string TipoCliente { get; set; }
    public string CodCondicionPagoFactura { get; set; }
    public string Nombre_Convenio { get; set; }
    public Moneda Valor { get; set; }
    public Moneda Valor_Descuento { get; set; }
    public Moneda ValorPagos { get; set; }
    public string Numero_Poliza { get; set; }
    public string Apellido_Paciente { get; set; }
    public string Nombre_Paciente { get; set; }
    public string Cedula { get; set; }
    public string Sticker_Radicado1 { get; set; }
    public string Sticker_Radicado2 { get; set; }
    public string Numero_Reclamacion { get;set; }
    public string CodPlan { get; set; }
    public string Estado { get; set; }
    public Fecha Fecha_Radicacion { get; set; }
    public string NoResolucionFact { get; set; }
    public string CodConvenio { get; set; }
    public string CodDependencia { get; set; }
    public string Cod_Servicio_Origen { get; set; }
    public Moneda ValorItem { get; set; }
  }//fin clase
}//fin namespace
