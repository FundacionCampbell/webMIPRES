using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT007 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba la línea de negocio Deudor de la obligación (reportante)
    // 1:= Aseguramiento obligatorio
    //2:= Aseguramiento voluntario
    // 3:= Prestación de servicios
    ///Numérico(1)
    /// </summary>
    public int lineaNegocio { get; set; }

    /// <summary>
    ///Escriba
    // 1:= Títulos de deuda pública emitidos o garantizados por la Nación o por el Banco de la República
    // 2:= Títulos de renta fija emitidos, aceptados, garantizados o avalados por entidades vigiladas por la Superintendencia Financiera de Colombia, FOGAFIN y FOGACOOP
    // 3:= Renta variable
    ///Numérico(1)
    /// </summary>
    public int instrumento { get; set; }

    /// <summary>
    ///Tipo de inversión
    // 1:= Títulos de Deuda Pública (TES)
    // 2:= Certificados de Depósito a Término (CDT)
    // 3:= Bonos Ordinarios
    // 4:= Bonos Subordinados
    // 5:= Bonos Opcionalmente Convertibles en acciones
    // 6:= Bonos Obligatoriamente Convertibles en Acciones 
    // 7:= Bonos de Capitalización
    // 8:= Acciones Ordinarias
    // 9:= Acciones Preferenciales
    // 10:= Otro
    ///Numérico(2)
    /// </summary>
    public int tipoInversion { get; set; }

    /// <summary>
    ///Registre el nemotécnico o código DCV del título 
    /// Alfanumérico(20)
    /// </summary>
    public string nemotécnico { get; set; }

    /// <summary>
    ///Registre el código ISIN (International Securities Identification Numbering System por sus siglas en inglés) o CFI (Classification of Financial Instruments) del título.

    // Escribir solo uno de los dos.
    /// Alfanumérico(20)
    /// </summary>
    public string codigoTitulo { get; set; }

    /// <summary>
    ///Registre el Número de identificación tributaria de la entidad emisora
    ///Numérico(16)
    /// </summary>
    public long idEmisora { get; set; }

    /// <summary>
    ///Número del digito de verificación de la entidad emisora
    ///Numérico(1)
    /// </summary>
    public int dvEmisora { get; set; }

    /// <summary>
    ///Código registrado en el SIMEV (Sistema Integral de Información del Mercado de Valores) de la Entidad Emisora del título.

    // Escriba en formato de texto: 000-000
    ///Alfanumérico(7)
    /// </summary>
    public string codigoSIMEV { get; set; }

    /// <summary>
    ///Escriba la calificadora de riesgo del título valor.  En caso que el título no tenga calificación, se debe registrar la del emisor del título
    // 0:= No Aplica
    // 1:= BRC Investor Services S.A. (Standard & Poor’s)
    // 2:= Fitch Ratings Colombia S.A. 
    // 3:= Value And Risk Rating S.A. 
    // 4:= Otra
    ///Numérico(1)
    /// </summary>
    public int entidadCalificadora { get; set; }

    /// <summary>
    ///Escriba el nombre de la calificadora de riesgos en caso que la opción anterior sea 4 (otra).

    // En otro caso escriba NA:= No aplica
    ///Alfanumérico(50)
    /// </summary>
    public string otraCalificadora { get; set; }

    /// <summary>
    ///Calificación otorgada al título valor o en su defecto al emisor del título por una sociedad calificadora debidamente autorizada por la Superintendencia Financiera de Colombia dependiendo si es de largo plazo o corto plazo.

    // Las calificaciones deben estar escritas sin espacios, ejemplo: BRC1+, AAA+.  
    ///Alfanumérico (7)
    /// </summary>
    public string calificacionRiesgo { get; set; }

    /// <summary>
    ///Escriba la fecha en la cual se emitió el título valor. 

    // Si No aplica, escribir 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaEmision { get; set; }

    /// <summary>
    ///Escriba la fecha en la cual se vence el título valor. 

    // Si No aplica, escribir 00000000
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

    // *Si el título es en UVR, reexpresarlo en pesos con la UVR del día de la compra
    ///Numérico(18)
    /// </summary>
    public long valorCompra { get; set; }

    /// <summary>
    ///Escriba la tasa de compra del título. 

    // 0:= No aplica para acciones 
    // Máximo dos decimales separados por coma

    ///Numérico sin porcentaje: 8,52% = 8,52(5)
    /// </summary>
    public decimal tasaCompra { get; set; }

    /// <summary>
    ///Valor nominal del titulo

    // *Si el título es en UVR, reportarlo en UVR 
    ///Numérico(18)
    /// </summary>
    public long valorNominal { get; set; }

    /// <summary>
    ///Tipo moneda de la denominación del título.
    // Escriba
    // COP:= Pesos Colombianos
    // USD:= Dólar de los Estados Unidos de América
    // GBP:= Libra Británica
    // EUR:= Euro
    // CAD:= Dólar Canadiense
    // CHF:= Franco Suizo
    // JPY:= Yen Japonés
    // OTR:= Otra
    ///Texto(3)
    /// </summary>
    public string tipoMoneda { get; set; }

    /// <summary>
    ///Escriba la tasa facial o tasa cupón del título. Si la tasa es indexada (IPC o DTF por ejemplo) escriba la Tasa de indexación o tasa flotante más los puntos adicionales cuando aplique. 
    // Se debe escribir la indexación y los puntos adicionales sin espacios ni porcentaje, máximo dos decimales separados por coma Ej.: IPC+3,55

    // Máximo dos decimales separados por coma.
    // 0:= No aplica para acciones
    ///Numérico  sin porcentaje: 8,52% = 8,52(15)
    /// </summary>
    public decimal tasaFacial { get; set; }

    /// <summary>
    ///Escriba la modalidad de pago de cupón
    // 0:= No Aplica
    // 1:= Vencida
    // // 2:= Anticipado
    ///Numérico(1)
    /// </summary>
    public int modalidad { get; set; }

    /// <summary>
    ///Escriba la periodicidad de pago de cupón
    // M:= Mensual
    // B:= Bimestral
    // T:= Trimestral 
    // S:= Semestral
    // A:= Anual
    // O:=Otro o No Aplica
    ///Texto(1)
    /// </summary>
    public string periodicidad { get; set; }

    /// <summary>
    ///Escriba la tasa de mercado del título.

    // 0:=No aplica para acciones
    // Máximo dos decimales separados por coma
    ///Numérico sin porcentaje: 8,52% = 8,52(5)
    /// </summary>
    public decimal tasaMercado { get; set; }

    /// <summary>
    ///Saldo disponible o Valoración de la inversión a la fecha de corte

    // *Si el título es en UVR, reexpresarlo en pesos con la UVR del día de la valoración
    ///Numérico(18)
    /// </summary>
    public long valorMercado { get; set; }

    /// <summary>
    ///Escriba en este campo la Duración Macaulay (media ponderada de los distintos vencimientos de los flujos de caja, ponderados por el valor actual de cada uno de esos flujos) calculada para el Instrumento. Máximo dos decimales separado por coma.
    // 0:=No aplica para acciones
    ///Numérico(5)
    /// </summary>
    public int duracion { get; set; }

    /// <summary>
    ///Escriba si el título es sujeto a algún tipo de gravamen
    // 0:= No tiene gravamen
    // 1:= Si tiene gravamen
    ///Numérico(1)
    /// </summary>
    public int gravamen { get; set; }

    /// <summary>
    ///Escriba si el título es sujeto a alguna medida judicial
    // 0:= No Aplica
    // 1:= Libre de afectación
    // 2:= Embargos
    // 3:= Medida Preventiva
    // Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última

    ///Numérico(1)
    /// </summary>
    public int estado { get; set; }

    /// <summary>
    ///Fecha en la cual se efectuó el embargo, medidas preventivas

    // Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última

    // Si la respuesta de estado es 0, escribir 00000000

    ///Numérico Fecha con formato AAAAMMDD(10)
    /// </summary>
    public string fechaMedida { get; set; }

    /// <summary>
    ///Valor por el cual se tienen los embargos, gravámenes o medidas preventivas que tiene la cuenta asociada, si no tiene medidas asociadas escriba cero 0
    ///Numérico(18)
    /// </summary>
    public long valorMedida { get; set; }

    /// <summary>
    ///De acuerdo a la definición del Artículo 2.31.3.1.12 del Decreto No. 2555 de 2010, donde se entiende por vinculado: 

    // 1. El o los accionistas o beneficiarios reales del cinco por ciento (5%) o más de la participación en la
    // entidad o quien tenga la capacidad de designar un
    // miembro de junta.
    // 2. Las personas jurídicas en las cuales la entidad sea
    // beneficiaria real del cinco por ciento (5%) o más de la participación en las mismas o aquellas en las
    // // cuales tenga la capacidad de designar un miembro de junta. 
    // 3. Las personas jurídicas en las cuales la o las personas a que se refiere el numeral 1 del presente
    // artículo sean accionistas o beneficiarios reales, individual o conjuntamente, del cinco por ciento (5%)
    // o más de la participación en las mismas o aquellas en las cuales tengan la capacidad de elegir un
    // miembro de junta.

    // De acuerdo con esta definición escriba

    // 0:= No Aplica
    // 1:= Inversión en vinculado
    ///Numérico(1)
    /// </summary>
    public int vinculado { get; set; }

    /// <summary>
    ///Titulo desmaterializado o custodiado, escribaTitulo desmaterializado o custodiado, escriba
    // 0:= No Aplica
    // 1:= DECEVAL S.A. - Depósito Centralizado de Valores. 
    // 2:= DCV - Depósito Central de Valores.
    ///Numérico(1)
    /// </summary>
    public int desmaterializado { get; set; }

    /// <summary>
    ///Indique si es una inversión que respalda las Reservas técnicas.
    // Escriba
    // 0:= No respalda las reservas Técnicas o  No Aplica
    // 1:= Si Respalda las reservas Técnicas
    ///Numérico(1)
    /// </summary>
    public int inversionReservas { get; set; }


  }
}
