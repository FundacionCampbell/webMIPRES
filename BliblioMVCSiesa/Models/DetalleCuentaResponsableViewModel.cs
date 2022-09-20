using LiloSoft.Siesa.Interfaz;
using LiloSoft.Types.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIS.EsculapioWeb.HistoriaClinica.Models
{
  /// <summary>
  /// Modelo para Visualizar DAtos del servicios Facturados
  /// </summary>
  public class DetalleCuentaResponsableViewModel
  {
    /// <summary>
    /// Fecha Inicial
    /// </summary>
    [Display(Name = "Fecha Inicial")]
    public Fecha FechaInicial { get; set; }
    /// <summary>
    /// Fecha Final
    /// </summary>
    [Display(Name = "Fecha Final")]
    public Fecha FechaFinal { get; set; }
    [Display(Name = "Servicio Origina Orden")]
    public string CodServicio {get;set;}
    public SelectList ListaServicios { get; set; }

    [Display(Name = "Dependencia Presta Servicio")]
    public string CodDependencia { get; set; }
    public SelectList ListaDependencias { get; set; }

    public List<DetalleCuenta> ServiciosFacturados { get; set; }
    public Entero NoCaso { get;  set; }
    public Entero ConsInternoFactura { get;  set; }
  }
}