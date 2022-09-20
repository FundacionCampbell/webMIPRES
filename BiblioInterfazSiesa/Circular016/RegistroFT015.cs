using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT015 : BaseRegistroFT
  {
    /// <summary>
    ///Tipo de documento de identificación del acreedor
    /// NI:= NIT
    /// CC:= Cédula de ciudadanía
    /// CE:= Cédula de extranjería
    /// OT:= Otro
    ///Texto(2)
    /// </summary>
    public string tipoIdAcreedor { get; set; }

    /// <summary>
    ///Número de identificación del acreedor 
    ///Alfanumérico (16)
    /// </summary>
    public string idAcreedor { get; set; }

    /// <summary>
    ///Número del dígito de verificación del acreedor 
    /// Escriba 0 cuando no aplique
    ///Numérico (1)
    /// </summary>
    public int dvAcreedor { get; set; }

    /// <summary>
    ///Apellidos y nombre o Razón social del acreedor 
    ///Texto(150)
    /// </summary>
    public string acreedor { get; set; }

    /// <summary>
    ///Escriba el tipo de acreedor:
    /// 1:= IPS Pública
    /// 2:= IPS Privada
    /// 3:= IPS Mixta
    /// 4:= FOSYGA
    /// 5:= Entidad Territorial
    /// 6:= Otro
    /// Numérico(1 )
    /// </summary>
    public string tipoAcreedor { get; set; }

    /// <summary>
    ///Dirección del acreedor
    ///Alfanumérico(100)
    /// </summary>
    public string dirección { get; set; }

    /// <summary>
    ///Código del municipio del domicilio principal del acreedor. Tabla de División Político Administrativa – DANE
    ///Alfanumérico(5)
    /// </summary>
    public string codigoMunicipio { get; set; }

    /// <summary>
    ///Número telefónico  del Acreedor
    ///Numérico(20)
    /// </summary>
    public long telefono { get; set; }

    /// <summary>
    ///Dirección correo electrónico del Acreedor
    ///Alfanumérico(50)
    /// </summary>
    public string email { get; set; }

    /// <summary>
    ///Escriba el número que le fue asignada a la reclamación 
    ///Alfanumérico (20)
    /// </summary>
    public string idReclamacion { get; set; }

    /// <summary>
    ///Escriba el  concepto de acreencias
    /// 01:= Laborales
    /// 02:= Fiscales
    /// 03:= Devolución de aportes SGSS
    /// 04:= Prestaciones económicas
    /// 05:= Obligaciones financieras
    /// 06:= Contratos de prestación de servicios de salud modalidad Cápita
    /// 07:= Contratos de prestación de servicios de salud modalidad Evento
    /// 08:= Contratos de servicios de salud 
    /// 09:= Reintegros al FOSYGA
    /// 10:= Fondo de Compensación Cuenta de Alto Costo 
    /// 11:= Entes Territoriales - Liquidación de Contratos
    /// 12:= Obligaciones por procesos en curso.
    /// 13:= Otras reclamaciones no incluidas en los conceptos anteriores
    ///Alfanumérico(3)
    /// </summary>
    public string conceptoAcreencia { get; set; }

    /// <summary>
    ///Indique el tipo de reclamación:
    /// 1:= Sumas excluidas de la masa
    /// 2:= Bienes excluidos de la masa
    /// 3:= Oportunas con cargo a la masa
    /// 4:= Pasivo Cierto No reclamado 
    /// Numérico( 1)
    /// </summary>
    public string tipoAcreencia { get; set; }

    /// <summary>
    ///Indique la oportunidad en que se realizó la reclamación:
    /// 1:= Oportuno  
    /// 2:= Extemporánea
    /// 3:= Registros contables
    /// Numérico( 1)
    /// </summary>
    public string oportunidad { get; set; }

    /// <summary>
    ///Indique la calificación y graduación que le correspondió a la acreencia de acuerdo con lo establecido en el Código Civil:
    /// 1:= Primera Clase
    /// 2:= Segunda Clase
    /// 3:= Tercera Clase
    /// 4:= Cuarta Clase
    /// 5:= Quinta Clase
    /// 6:= Excluido de la masa
    /// Numérico( 1)
    /// </summary>
    public string calificacion { get; set; }

    /// <summary>
    ///Indique el número de acto administrativo con el cual se calificó y graduó la acreencia 
    ///Alfanumérico(8)
    /// </summary>
    public string actoAdmonCalificacion { get; set; }

    /// <summary>
    ///Indique el valor reclamado
    ///Numérico(18)
    /// </summary>
    public long valorReclamado { get; set; }

    /// <summary>
    ///Indique el valor que fue aprobado para el acreedor de acuerdo con la auditoria adelantada
    ///Numérico(18)
    /// </summary>
    public long valorReconocido { get; set; }

    /// <summary>
    ///Indique el valor rechazado de la acreencia presentada
    ///Numérico(18)
    /// </summary>
    public long valorRechazado { get; set; }

    /// <summary>
    ///Señale el número del acto administrativo por el cual se resolvió recurso de reposición si fue presentado 
    ///Alfanumérico(8)
    /// </summary>
    public string actoAdmonRecurso { get; set; }

    /// <summary>
    ///Indique el valor que fue reconocido mediante resolución de recurso de reposición interpuesto 
    ///Numérico(18)
    /// </summary>
    public long valorRecurso { get; set; }

    /// <summary>
    ///Indique el valor total reconocido después de resueltos los recursos interpuestos por el acreedor 
    ///Numérico(18)
    /// </summary>
    public long totalReconocido { get; set; }

    /// <summary>
    ///Indique el valor pagado dentro del trimestre reportado 
    ///Numérico(18)
    /// </summary>
    public long valorPago { get; set; }

    /// <summary>
    ///Indique acto administrativo por medio del cual reconoce o realiza el pago 
    ///Alfanumérico(8)
    /// </summary>
    public string actoAdmonPago { get; set; }

    /// <summary>
    ///Saldo por pagar al cierre del Trimestre 
    ///Numérico(18)
    /// </summary>
    public long valorSaldo { get; set; }


  }
}
