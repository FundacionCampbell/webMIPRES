using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT016 : BaseRegistroFT
  {
    /// <summary>
    ///Tipo de reporte al que corresponda la información
    /// 1:= Al retiro del liquidador 
    /// 2:= Suspensión del proceso liquidatorio 
    /// 3:= Terminación de existencia y representación legal.
    ///Numérico(1)
    /// </summary>
    public int tipoReporte { get; set; }

    /// <summary>
    ///Escriba el código del concepto del ingreso o egreso
    /// 01:= Ingresos
    /// 02:= Saldo disponible
    /// 03:= Venta de inversiones
    /// 04:= Recaudo de cartera
    /// 05:= Enajenación de activos
    /// 06:= Bienes de terceros
    /// 07:= Devolución títulos judiciales
    /// 08:= Recursos del SGSSS girados por el Gobierno
    /// 09:= Otros Ingresos
    /// 10:= Egresos
    /// 11:= Gastos Administrativos
    /// 12:= Pago sumas excluidas de la masa
    /// 13:= Restitución bienes excluidos de la masa
    /// 14:= Pago acreencias oportunas
    /// 15:= Pago pasivo cierto no reclamado
    /// 16:= Pago pérdida de poder adquisitivo
    /// 17:= Provisión para archivo
    /// 18:= Reserva para procesos judiciales
    /// 19:= Constitución patrimonio autónomo
    /// 20:= Contratos de mandato
    /// 21:= Otros egresos
    ///Alfanumérico (3)
    /// </summary>
    public string conceptoIyG { get; set; }

    /// <summary>
    ///Escriba el valor recaudado o pagado 
    ///Numérico(18)
    /// </summary>
    public long valor { get; set; }


  }
}
