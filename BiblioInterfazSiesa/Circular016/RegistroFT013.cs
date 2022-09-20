using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT013 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba el código del concepto del gasto generado durante el proceso de liquidación:
    /// 01:= Gastos asociado a la nómina
    /// 02:= Servicios personales indirectos (Ordenes de Prestación de servicios)
    /// 03:= Personal tercerizado
    /// 04:= Gastos de personal designado por la Supersalud
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
    ///Alfanumérico(3)
    /// </summary>
    public string conceptoGasto { get; set; }

    /// <summary>
    ///Reporte el valor por concepto de gastos causados desde el inicio del proceso de liquidación hasta el  corte del trimestre que está reportando 
    ///Numérico(18)
    /// </summary>
    public long gastosCausados { get; set; }

    /// <summary>
    ///Reporte  el valor de los pagos acumulados realizados desde el inicio del proceso de liquidación hasta el corte  del trimestre que está reportando 
    ///Numérico(18)
    /// </summary>
    public long pagosAcumulados { get; set; }


  }
}
