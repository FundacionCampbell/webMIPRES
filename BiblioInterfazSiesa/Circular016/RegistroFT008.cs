using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT008 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba la línea de negocio Deudor de la obligación (reportante)
    /// 1:= Aseguramiento obligatorio
    /// 2:= Aseguramiento voluntario
    /// 3:= Prestación de servicios
    ///Numérico(1)
    /// </summary>
    public int lineaNegocio { get; set; }

    /// <summary>
    ///País origen de la emisión del título
    ///Texto(20)
    /// </summary>
    public string país { get; set; }

    /// <summary>
    ///Número de NIT de la entidad emisora del título.

    /// En caso que la Entidad Emisora no esté radicada en Colombia, escriba 0
    ///Numérico(16)
    /// </summary>
    public long idEmisora { get; set; }

    /// <summary>
    ///Número del dígito de verificación del NIT de la entidad emisora.
    /// En caso que la Entidad Emisora no esté radicada en Colombia, escriba 0.
    ///Numérico(1)
    /// </summary>
    public int dvEmisora { get; set; }

    /// <summary>
    ///Nombre de la entidad emisora del título 
    ///Texto(150)
    /// </summary>
    public string nombreEmisora { get; set; }

    /// <summary>
    ///Tipo de inversión:
    /// 1:= Títulos de Deuda Pública
    /// 2:= Certificados de Depósito a Término (CDT)
    /// 3:= Bonos Ordinarios
    /// 4:= Bonos Subordinados
    /// 5:= Bonos Opcionalmente Convertibles en acciones
    /// 6:= Bonos Obligatoriamente Convertibles en Acciones 
    /// 7:= Bonos de Capitalización
    /// 8:= Acciones Ordinarias
    /// 9:= Acciones Preferenciales
    /// 10:= Otro
    ///Numérico(2)
    /// </summary>
    public int tipoInversion { get; set; }

    /// <summary>
    ///Escriba el tipo de inversión en caso que tipo de inversión sea 10 (otro). 

    /// En otro caso escriba NA:=No aplica
    ///Texto(100)
    /// </summary>
    public string otraInversion { get; set; }

    /// <summary>
    ///Escriba la fecha en la cual se emitió el título valor. 

    /// Si No aplica, escriba 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaEmision { get; set; }

    /// <summary>
    ///Escriba la fecha en la cual se vence el título valor. 
    /// Si No aplica, escriba 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaVencimiento { get; set; }

    /// <summary>
    ///Fecha en la que se adquirió el título
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaCompra { get; set; }

    /// <summary>
    ///Valor por el cual se adquiere el titulo 
    /// *Si el título es en otra moneda, reexpresarlo en pesos con la TRM del día de la compra
    ///Numérico (18)
    /// </summary>
    public long valorCompra { get; set; }

    /// <summary>
    ///Escriba la tasa de compra del título. 
    /// NA:=En el caso que No aplica

    /// Máximo dos decimales separados por coma

    ///Alfanumérico sin porcentaje: 8,52% = 8,52(5)
    /// </summary>
    public decimal tasaCompra { get; set; }

    /// <summary>
    ///Valor nominal del titulo 

    /// *Si el título es en otra moneda, reportarlo en esa moneda
    ///Numérico(18)
    /// </summary>
    public long valorNominal { get; set; }

    /// <summary>
    ///Tipo moneda en la que se encuentra denominada la inversión. Escriba
    /// COP:= Pesos Colombianos
    /// USD:= Dólar de los Estados Unidos de América
    /// GBP:= Libra Británica
    /// EUR:= Euro
    /// CAD:= Dólar Canadiense
    /// CHF:= Franco Suizo
    /// JPY:= Yen Japonés
    /// OTR:= Otra
    ///Texto(3)
    /// </summary>
    public string tipoMoneda { get; set; }

    /// <summary>
    ///Escriba la tasa facial o tasa cupón del título. Si la tasa es indexada escriba la Tasa de indexación o tasa flotante más los puntos adicionales cuando aplique. 

    /// Se debe escribir la indexación y los puntos adicionales sin espacios ni porcentaje, máximo dos decimales separados por coma Ej.: IPC+3,55

    /// Máximo dos decimales separados por coma.
    /// 0:= No aplica para acciones
    ///Numérico sin porcentaje: 8,52% = 8,52(15)
    /// </summary>
    public decimal tasaFacial { get; set; }

    /// <summary>
    ///Escriba la modalidad de pago de cupón
    /// 0:= No Aplica
    /// 1:= Vencida
    /// 2:= Anticipado
    ///Numérico(1)
    /// </summary>
    public int modalidad { get; set; }

    /// <summary>
    ///Escriba la periodicidad de pago de cupón
    /// M:= Mensual
    /// B:= Bimestral
    /// T:= Trimestral 
    /// S:= Semestral
    /// A:= Anual
    /// O:=Otro
    ///Texto(1)
    /// </summary>
    public string periodicidad { get; set; }

    /// <summary>
    ///Escriba la tasa de mercado del título. 
    /// Máximo dos decimales separados por coma
    /// 0:=No aplica para acciones
    ///Numérico sin porcentaje: 8,52% = 8,52(5)
    /// </summary>
    public decimal tasaMercado { get; set; }

    /// <summary>
    ///Saldo disponible o Valoración de la inversión a la fecha de corte

    /// *Si el título es en otra moneda, reexpresarlo en pesos con la TRM del día de la valoración
    ///Numérico(18)
    /// </summary>
    public long valoMercado { get; set; }

    /// <summary>
    ///Escriba en este campo la Duración Macaulay (media ponderada de los distintos vencimientos de los flujos de caja, ponderados por el valor actual de cada uno de esos flujos) calculada para el Instrumento
    /// Máximo dos decimales separados por coma.
    /// 0:=No aplica para acciones
    ///Numérico(5)
    /// </summary>
    public int duracion { get; set; }

    /// <summary>
    ///Porcentaje de participación en la entidad donde se tiene la inversión; solo aplica para las inversiones en acciones
    /// Máximo dos decimales separados por coma.

    /// En cualquier otro caso escribir 0

    ///Numérico sin porcentaje: 8,52% = 8,52(5)
    /// </summary>
    public string participacion { get; set; }

    /// <summary>
    ///Valor total de los intereses o dividendos recibidos durante último periodo
    ///Numérico(18)
    /// </summary>
    public long rendimientos { get; set; }

    /// <summary>
    /// Escriba si el título es sujeto a algún tipo de gravamen
    /// 0:= No tiene gravamen
    /// 1:= Si tiene gravamen
    ///Numérico(1)
    /// </summary>
    public int gravamen { get; set; }

    /// <summary>
    ///Escriba si el título es sujeto a alguna medida judicial
    /// 0:= No Aplica
    /// 1:= Libre de afectación
    /// 2:= Embargos
    /// 3:= Medida Preventiva
    /// Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última
    ///Numérico(1)
    /// </summary>
    public int estado { get; set; }

    /// <summary>
    ///Fecha en la cual se efectuó el embargo,  medidas preventivas
    /// Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última
    /// Si la respuesta de estado es 0, escribir 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaMedida { get; set; }

    /// <summary>
    ///Valor por el cual se tienen los embargos, gravámenes o medidas preventivas que tiene la cuenta asociada, si no tiene medidas asociadas escriba cero 0
    ///Numérico(18)
    /// </summary>
    public long valorMedida { get; set; }

    /// <summary>
    ///De acuerdo a la definición del Artículo 2.31.3.1.12 del Decreto No. 2555 de 2010, donde se entiende por vinculado: 

    /// 1. El o los accionistas o beneficiarios reales del cinco por ciento (5%) o más de la participación en la
    /// entidad o quien tenga la capacidad de designar un
    /// miembro de junta.
    /// 2. Las personas jurídicas en las cuales la entidad sea
    /// /// beneficiaria real del cinco por ciento (5%) o más de la participación en las mismas o aquellas en las
    /// cuales tenga la capacidad de designar un miembro de junta. 
    /// 3. Las personas jurídicas en las cuales la o las personas a que se refiere el numeral 1 del presente
    /// artículo sean accionistas o beneficiarios reales, individual o conjuntamente, del cinco por ciento (5%)
    /// o más de la participación en las mismas o aquellas en las cuales tengan la capacidad de elegir un
    /// miembro de junta.

    /// De acuerdo con esta definición escriba
    /// /// 0:= No Aplica
    /// 1:= Inversión en vinculado
    ///Numérico(1)
    /// </summary>
    public int vinculado { get; set; }


  }
}
