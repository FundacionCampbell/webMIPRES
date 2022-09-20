using LiloSoft.Types.Data;
using LiloSoft.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Indicadores
{
  public class IndicadorVentaUrgencias
  {
    public IndicadorVentaUrgencias()
    {
      IngresosUrgencias = new List<DataIngresosUrgencias>();
      VentaUrgencias = new List<DataVentaUrgencias>();
    }
    public List<DataIngresosUrgencias> IngresosUrgencias { get; set; }
    public List<DataVentaUrgencias> VentaUrgencias { get; set; }

    public Entero TotalIngreso => IngresosUrgencias.CountExt();

    public decimal TotalVentas => VentaUrgencias.HasElements() ? VentaUrgencias.Sum(c => c.Venta) : 0m;
    public decimal IndicadorGeneral
    {
      get
      {
        var indicador = 0m;
        var totIngreso = 0m;
        var totVentas = 0m;
        if (IngresosUrgencias.HasElements()) totIngreso = IngresosUrgencias.CountExt();
        if (VentaUrgencias.HasElements()) totVentas = VentaUrgencias.Sum(c => c.Venta);
        if (totIngreso > 0)
          indicador = totVentas / totIngreso;
        return indicador;
      }
    }

    public string CodPlan { get; set; }
    public string NombrePlan { get; set; }
    public List<Planes_Tarifarios> Planes { get;  set; }

    /// <summary>
    /// Generación de Indicadores por Planes Tarifaros
    /// </summary>
    public IEnumerable<IndicadorVentaUrgencias> IndicadorPlanes
   {
      get
      {
        var indicador = new List<IndicadorVentaUrgencias>();
        Planes.ForEachExt(pla =>
        {
          var ind = new IndicadorVentaUrgencias();
          ind.IngresosUrgencias.AddRange(IngresosUrgencias.Where(c => c.CodPlan == pla.CodPlan));
          ind.VentaUrgencias.AddRange(VentaUrgencias.Where(c => c.CodPlan == pla.CodPlan));
          ind.CodPlan = pla.CodPlan;
          ind.NombrePlan = pla.NombrePlan;
          indicador.Add(ind);
        });
        return indicador;
      }
    }
  }//fin clase

  public class DataIngresosUrgencias
  {
    public string Empresa { get; set; }
    public string NombreEmpresa { get; set; }
    public string NoCuenta { get; set; }
    public string NoAdmision { get; set; }
    public Fecha FechaIngreso { get; set; }
    public string CodServicio { get; set; }
    public string NombreServicio { get; set; }
    public string CodPlan { get; set; }
    public string NombrePlan { get; set; }
  }//fin clase

  public class DataVentaUrgencias
  {
    public string Empresa { get; set; }
    public string NombreEmpresa { get; set; }
    public string NoCuenta { get; set; }
    public Entero ConsInternoFactura { get; set; }
    public string CodServicio { get; set; }
    public string NombreServicio { get; set; }
    public Moneda Venta { get; set; }
    public Entero Casos { get; set; }
    public string CodPlan { get; set; }
    public string NombrePlan { get; set; }
  }
}//fin namespace
