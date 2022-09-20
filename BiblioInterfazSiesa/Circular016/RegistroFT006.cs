using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT006 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba la línea de negocio Deudor de la obligación (reportante)
    //1:= Aseguramiento obligatorio
    //2:= Aseguramiento voluntario
    //3:= Prestación de servicios
    ///Numérico(1)
    /// </summary>
    public int lineaNegocio { get; set; }

    /// <summary>
    ///Nombre  del Establecimiento Financiero, Fiduciaria o Comisionista de Bolsa definido en el SIMEV (Sistema Integral de Información del Mercado de Valores)
    ///Alfanumérico(100)
    /// </summary>
    public string establecimiento { get; set; }

    /// <summary>
    ///Número de identificación tributario del Establecimiento Financiero, Fiduciaria o Comisionista de Bolsa
    ///Numérico(16)
    /// </summary>
    public long idEstablecimiento { get; set; }

    /// <summary>
    ///Código del Establecimiento Financiero, Fiduciaria o Comisionista de Bolsa registrado en el SIMEV (Sistema Integral de Información del Mercado de Valores)
    //Escriba en formato de texto: 000-000
    ///Alfanumérico(7)
    /// </summary>
    public string codigoSIMEV { get; set; }

    /// <summary>
    ///Calificación a largo plazo o corto plazo del Establecimiento Financiero, Fiduciaria o Comisionista de Bolsa. Calificación otorgada al establecimiento por una sociedad calificadora debidamente autorizada. Las calificaciones deben estar escritas sin espacios, ejemplo: BRC1+, AAA+
    ///Alfanumérico(7)
    /// </summary>
    public string CalificacionRiesgo { get; set; }

    /// <summary>
    ///Calificadora del Establecimiento Financiero, Fiduciaria o Comisionista de Bolsa. Escriba:
    //0:= No Aplica
    //1:= BRC Investor Services S.A. (Stand & Poors)
    //2:= Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)
    //3:= Value And Risk Rating S.A. 
    //4:=Otra
    ///Numérico(1)
    /// </summary>
    public int entidadCalificadora { get; set; }

    /// <summary>
    ///Escriba el nombre de la calificadora de riesgos en caso que la opción anterior sea 4 (otra).

    //En otro caso escriba NA:=No aplica
    ///Alfanumérico(50)
    /// </summary>
    public string otraCalificadora { get; set; }

    /// <summary>
    ///Escriba
    //1:= Cuenta Corriente
    //2:= Cuenta de Ahorros
    //3:= Cuenta Maestra de Recaudo
    //4:= Cartera Colectiva Abierta o Fondos de Inversión Mercado Monetario
    //5:= Cartera Colectiva Cerrada
    //6:= Otro tipo de Encargo Fiduciario o Fondo de Inversión, Fideicomiso, Fondos de Inversión Colectiva Inmobiliarios y/o Fondos de Capital Privado
    ///Numérico(1)
    /// </summary>
    public int claseCuenta { get; set; }

    /// <summary>
    ///Tipo moneda de la cuenta registrada en la entidad. Escriba
    //COP:= Pesos Colombianos
    //USD:= Dólar de los Estados Unidos de América
    //GBP:= Libra Británica
    //EUR:= Euro
    //CAD:= Dólar Canadiense
    //CHF:= Franco Suizo
    //JPY:= Yen Japonés
    //OTR:= Otra
    ///Texto(3)
    /// </summary>
    public string tipoMoneda { get; set; }

    /// <summary>
    ///Número de la cuenta que le corresponda de acuerdo con la relación del Campo 8 Clase de Cuenta
    ///Alfanumérico(25)
    /// </summary>
    public string idCuenta { get; set; }

    /// <summary>
    ///En caso que el campo 8 Clase de cuenta sea 4, 5 o 6, escriba el nombre de la Cartera Colectiva o Fondo de Inversión
    ///Alfanumérico(100)
    /// </summary>
    public string nombreCuenta { get; set; }

    /// <summary>
    ///Valor del saldo en el Extracto
    ///Numérico(18)
    /// </summary>
    public long saldoExtracto { get; set; }

    /// <summary>
    ///Registre el valor en libros de la cuenta que le corresponda de acuerdo con la relación del Campo 8 Clase de Cuenta 
    ///Numérico(18)
    /// </summary>
    public long saldoLibros { get; set; }

    /// <summary>
    ///Valor del Sobregiro Bancario registrado al periodo de corte si es una cuenta corriente, de lo contrario reportar en cero 0 
    ///Numérico(18)
    /// </summary>
    public long sobregiro { get; set; }

    /// <summary>
    ///Valor de los Rendimientos Financieros, de lo contrario reportar en cero 0 
    ///Numérico(18)
    /// </summary>
    public long rendimientos { get; set; }

    /// <summary>
    ///El Número de la cuenta corriente, de ahorro o de encargo fiduciario está sujeta a gravámenes a los movimientos financieros 
    //0:= No tiene gravamen
    //1:= Si tiene gravamen
    ///Numérico(1)
    /// </summary>
    public int gravamen { get; set; }

    /// <summary>
    ///Escriba si la cuenta corriente, de ahorro o de encargo fiduciario es sujeto a alguna medida judicial
    //0:= No Aplica
    //1:=Libre de afectación
    //2:= Embargos
    //3:= Medida Preventiva
    //Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última
    ///Numérico(1)
    /// </summary>
    public int estado { get; set; }

    /// <summary>
    ///Fecha en la cual se efectuó el embargo, medidas preventivas

    //Si la respuesta de estado es 0, escribir 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaMedida { get; set; }

    /// <summary>
    ///Valor por el cual se tienen los embargos, medidas preventivas
    ///Numérico(18)
    /// </summary>
    public long valorMedida { get; set; }

    /// <summary>
    ///En caso que sea una inversión que respalde las Reservas Técnicas, por favor indique el monto en el que las respalda, de lo contrario  reportar en cero 0

    //0:= No aplica para SAP, EMP e IPS.
    ///Numérico(18)
    /// </summary>
    public long inversionReservas { get; set; }


  }
}
