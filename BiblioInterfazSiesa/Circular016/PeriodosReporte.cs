
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LiloSoft.Types.Data;
using LiloSoft.Data;
using LiloSoft.DataBase.ConectaDB;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  /// <summary>
  /// Periodos Reporte de Información
  /// </summary>
  [ShowColumn(ColumnaMostrar = "NombrePeriodoReporte")]
  public class PeriodosReporte : BaseData 
  {
    /// <summary>
    /// Código de Periodo de Reporte
    /// </summary>
    [Description("Código de Periodo de Reporte")]
    [DisplayName("Código Periodo")]
    [DataObjectField(true)]
    [SearchColumn]
    public string CodPeriodoReporte { get; set; }
    /// <summary>
    /// Nombre Periodo de Reporte
    /// </summary>
    [Description("Nombre Periodo de Reporte")]
    [DisplayName("Nombre Periodo de Reporte")]
    [SearchColumn]
    public string NombrePeriodoReporte { get; set; }
    /// <summary>
    /// Nombre Periodo de Reporte Extendido
    /// </summary>
    [NoDataBase]
    public string NombrePeriodoReporteExtendido
    {
      get
      {
        return CodPeriodoReporte + " - "  + NombrePeriodoReporte;
      }
    }
    /// <summary>
    /// Mes Inicial de Reporte
    /// </summary>
    [Description("Mes Inicial de Reporte")]
    [DisplayName("Mes Inicial de Reporte")]
    public string MesInicial { get; set; }
    /// <summary>
    /// Nombre Para Mes Inicial de Reporte
    /// </summary>  
    [NoDataBase]
     public string NombreMesInicial
    {
      get
      {
        string nom = "";
        switch (MesInicial)
        {
          case "01":
            nom = "Enero";
            break;
          case "02":
            nom = "Febrero";
            break;
          case "03":
            nom = "Marzo";
            break;
          case "04":
            nom = "Abril";
            break;
          case "05":
            nom = "Mayo";
            break;
          case "06":
            nom = "Junio";
            break;
          case "07":
            nom = "Julio";
            break;
          case "08":
            nom = "Agosto";
            break;
          case "09":
            nom = "Septiembre";
            break;
          case "10":
            nom = "Octubre";
            break;
          case "11":
            nom = "Noviembre";
            break;
          case "12":
            nom = "Diciembre";
            break;
          default:
            nom = "No Inicializado";
            break;
        }
        return nom;
      }
    }
    /// <summary>
    /// Mes Final del Reporte
    /// </summary>
    [Description("Mes Final del Reporte")]
    [DisplayName("Mes Final del Reporte")]
    public string MesFinal { get; set; }
    /// <summary>
    /// Nombre Para Mes Final del Reporte
    /// </summary>  
    [NoDataBase]
     public string NombreMesFinal
    {
      get
      {
        string nom = "";
        switch (MesFinal)
        {
          case "01":
            nom = "Enero";
            break;
          case "02":
            nom = "Febrero";
            break;
          case "03":
            nom = "Marzo";
            break;
          case "04":
            nom = "Abril";
            break;
          case "05":
            nom = "Mayo";
            break;
          case "06":
            nom = "Junio";
            break;
          case "07":
            nom = "Julio";
            break;
          case "08":
            nom = "Agosto";
            break;
          case "09":
            nom = "Septiembre";
            break;
          case "10":
            nom = "Octubre";
            break;
          case "11":
            nom = "Noviembre";
            break;
          case "12":
            nom = "Diciembre";
            break;
          default:
            nom = "No Inicializado";
            break;
        }
        return nom;
      }
    }

  } // Fin Clase
} // Fin NameSpace

