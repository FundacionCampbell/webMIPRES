using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz
{
  public class GlosasDetalle
  {
    public string Empresa { get; set; }
    public string Factura { get; set; }
    public EnteroLargo consecutivoGlosa { get; set; }
    public string NombreConvenio { get; set; }
    public string Servicio { get; set; }
    public string DepInterno { get; set; }
    public string DptoServicio { get; set; }
    public string DeptInternoGlosa { get; set; }
    public string DptoServicioGlosa { get; set; }
    public string CodMotivo { get; set; }
    public string Motivo { get; set; }
    public string nombreplan { get; set; }
    public string Respuesta { get; set; }
    public Entero CantidadCargada { get; set; }
    public Moneda ValorCargadoServicio { get; set; }
    public Entero CantidadGlosada { get; set; }
    public Moneda ValorGlosa { get; set; }
    public Moneda PorcentajeGlosado { get; set; }
    public Entero CantidadAceptada { get; set; }
    public Moneda ValorGlosaAceptado { get; set; }
    public string PorcentajeAceptado { get; set; }
    public string ObsevacionDetalle { get; set; }
    public string ObservacionGeneral { get; set; }
    public string ExclusivaServicio { get; set; }
    public Fecha FechaRadicionFactura { get; set; }
    public Fecha FechaGlosa { get; set; }
    public string NitEntidad { get; set; }
    public string Sucursal_Tercero { get; set; }
    public string Punto_Envio { get; set; }
    public string TipoCliente { get; set; }
    public string CodCondicionPagoFactura { get; set; }
    public string CodConvenio { get; set; }
    public string Nombre_Convenio { get; set; }
    public Fecha FechaRegistroGlosa => FechaRadicionFactura > FechaGlosa ? FechaRadicionFactura : FechaGlosa;
    public string CodPlan { get; set; }
    public string NoResolucionFact { get; set; }
    public Fecha FechaVencimientoFactura { get; set; }

    public Fecha FechaFacturaRegistro
    {
      get
      {
        var fec = FechaVencimientoFactura;
        if (FechaRadicionFactura != null)
          fec = FechaRadicionFactura;
        return fec;
      }
    }
    public string PostPre { get; set; }
    

  }//fin clase
}//fin namespace
