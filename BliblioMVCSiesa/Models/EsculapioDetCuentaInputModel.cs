using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LiloSoft.Web.Mvc.Models;
using LiloSoft.Siesa.Interfaz;

namespace SIS.EsculapioWeb.HistoriaClinica.Models.PruebaEsculapio
{
  public class EsculapioDetCuentaInputModel
  {
    public string Empresa { get; set; }
    /// <summary>
    /// No. del Caso
    /// </summary>
    [Display(Name = "No. Caso")]
    [Required(ErrorMessage = "El No. de Cuenta del Pacientes es Obligatorio")]
    public string NoCaso { get; set; }
    public bool MostrarDatos { get; set; }
    public DatosCasoBasico Caso { get; set; }
    public List<ResponsablesCaso> Responsables { get; set; }
    public List<ResponsablesCasoTrazabilidad> ResponsablesTrazabilidad { get; set; }
    public List<HistoriasClinicasEmpresas> Historias { get; set; }
    public TreeWeb ArbolHistorias { get; set; }
    public List<TrazabilidadHistorico> HistoriaTrazabilidad { get; set; }
  }
}