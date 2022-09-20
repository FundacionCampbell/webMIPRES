using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT014 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba los códigos de concepto de ingreso  con los cuales planea financiar el proceso de liquidación y los códigos de concepto de gasto  que proyecta ejecutar a lo largo del proceso de liquidación 
    /// 01:= Gastos asociado a la nómina
    /// 02:= Servicios personales indirectos (Ordenes de Prestación de servicios)
    /// 03:= Personal tercerizado
    /// 04:= Gastos de personal designado por la Superasalud
    /// 05:= Avalúos técnicos 
    /// 06:= Conceptos técnicos
    /// 07:= Organización, administración y custodia de archivo
    /// 08:= Impuestos, Tasas, Contribuciones, Derechos y Multas
    /// 09:= Arrendamientos
    /// 10:= Pólizas adquiridas durante el proceso liquidatorio
    /// 11:= Gastos legales
    /// 12:= Viáticos y gastos de viaje
    /// 13:= Gastos de transporte y comunicación
    /// 14:= Impresos, publicaciones 
    /// 15:= Gastos de notificación
    /// 16:= Gastos financieros
    /// 17:= Compra de activos
    /// 18:= Servicios Públicos
    /// 19:= Servicios de Vigilancia
    /// 20:= Servicios de Aseo 
    /// 21:= Gastos de mantenimiento y reparaciones
    /// 22:= Otros Gastos no incluidos en los anteriores conceptos
    /// 23:= Ingresos del Sistema General de Seguridad Social en Salud 
    /// 24:= Recuperación de Cartera
    /// 25:= Venta de Activos
    /// 26:= Recuperación de Títulos Judiciales
    /// 27:= Realización de Inversiones
    /// 28:= Otros Ingresos no incluidos en los anteriores conceptos
    ///Alfanumérico (3)
    /// </summary>
    public string conceptoPyG { get; set; }

    /// <summary>
    ///Reporte el valor de la proyección de ingresos / gastos por concepto en los cuales planea ejecutar el proceso de liquidación 
    ///Numérico(18)
    /// </summary>
    public long presupuestoInicial { get; set; }

    /// <summary>
    ///Reporte el valor de las modificaciones de adición o reducción que ha tenido que realizar al corte del trimestre que está reportando.  
    /// (Escriba el signo negativo si es reducción)
    ///Numérico(18)
    /// </summary>
    public long modificaciones { get; set; }

    /// <summary>
    ///Reporte  el valor del recaudo de ingresos y gastos comprometidos por concepto, desde el inicio del proceso de liquidación hasta el corte del trimestre que está reportando 
    ///Numérico(18)
    /// </summary>
    public long valorRecaudado { get; set; }

    /// <summary>
    ///Reporte  el valor efectivo de los ingresos y pagos realizados   para cada concepto, desde el inicio del proceso de liquidación hasta el corte del trimestre que está reportando 
    ///Numérico(18)
    /// </summary>
    public long valorPago { get; set; }


  }
}
