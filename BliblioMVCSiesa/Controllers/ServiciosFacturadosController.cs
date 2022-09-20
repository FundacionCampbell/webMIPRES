using SIS.EsculapioWeb.HistoriaClinica.Models.PruebaEsculapio;
using LiloSoft.Types.Data;
using LiloSoft.Utils;
using LiloSoft.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiloSoft.Siesa.Interfaz.Controllers;
using LiloSoft.Siesa.Interfaz;
using SIS.EsculapioWeb.HistoriaClinica.Models;

namespace SIS.EsculapioWeb.HistoriaClinica.Controllers.PruebaEsculapio
{
  [Authorize]
  public class ServiciosFacturadosController : BaseInterfazController
  {
    public ActionResult DetalleCuentaResponsable(Entero NoCaso, Entero ConsInterFactura)
    {
      if (NoCaso == null || ConsInterFactura == null) return ErrorResult("Faltan Datos del Caso");
      var dataDb = new ConsultasEsculapioDB(SqlDbMysql);

      var data2Db = new ConsultaInstitucionDB(SqlDbMysql);

      var model = new DetalleCuentaResponsableViewModel
      {
        NoCaso = NoCaso,
        ConsInternoFactura = ConsInterFactura,
        ServiciosFacturados=dataDb.GetDetalleServiciosFacturados(this.EmpresaEsculapio, NoCaso,ConsInterFactura)
      };

      var dependencias = new List<Dependencias>
      {
        new Dependencias{CodDependencia ="", NomDependecia ="(Seleccione Dependencia)"}
      };

      var servicios = new List<Servicios_Clinica>
      {
        new Servicios_Clinica {Cod_Servicio ="", Nombre="(Seleccione Servicio)"}
      };

      var lstDepen = data2Db.GetDependencias(EmpresaEsculapio);
      if (lstDepen.CountExt() > 0)
        dependencias.AddRange(lstDepen);
      var lstServ = data2Db.GetServiciosClinica(EmpresaEsculapio);
      if (lstServ.CountExt() > 0)
        servicios.AddRange(lstServ);
      model.ListaServicios = new SelectList(servicios, "Cod_Servicio", "Nombre", "");
      model.ListaDependencias = new SelectList(dependencias, "CodDependencia", "NomDependecia", "");
      var fecHoy = DateTime.Now.ToUtcReal();
      model.FechaInicial = fecHoy.FirstDay();
      model.FechaFinal = fecHoy;
      return PartialView(model);
    }

    [HttpPost]
    public ActionResult FiltrarServicios(DetalleCuentaResponsableViewModel model)
    {
      if (ModelState.IsValid)
      {
        try
        {
          var dataDb = new ConsultasEsculapioDB(SqlDbMysql);
          var servicios = dataDb.GetDetalleServiciosFacturados(this.EmpresaEsculapio, model.NoCaso, model.ConsInternoFactura);
          if (servicios.CountExt() > 0)
          {
            var lstServi = from s in servicios
                           where s.fecha_cargo.ValorInterno >= model.FechaInicial.ValorInterno
                            && s.fecha_cargo.ValorInterno <= model.FechaFinal.ValorInterno &&
                               ((model.CodServicio.IsNotNullOrEmpty() && s.cod_servicio_origen == model.CodServicio)
                               || model.CodServicio.IsNullOrEmpty()) &&
                               ( (model.CodDependencia.IsNotNullOrEmpty() && s.CodDependencia == model.CodDependencia)
                               ||
                                model.CodDependencia.IsNullOrEmpty()
                               )
                           select s;
            if (lstServi.CountExt() == 0)
              return ErrorResult($"No Hay Datos Fecha Ini:{model.FechaInicial} Fecha Fin:{model.FechaFinal}  Servicio:{model.CodServicio} Dependencia:{model.CodDependencia}");
            model.ServiciosFacturados = lstServi.ToList();
            return PartialView("ListaServicios", model);
          }
          else
            return ErrorResult($"No Fay Datos para el Filtro Fecha Ini:{model.FechaInicial} Fecha Fin:{model.FechaFinal}  Caso:{model.NoCaso} ConsFactura:{model.ConsInternoFactura}");
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("", "Error Busquta " + ex.Message);
        }
      }
      else return ErrorResult("DAtos Invalidos");
      return ErroresModelResult();
    }
  }//fin clase
}//fin namespace