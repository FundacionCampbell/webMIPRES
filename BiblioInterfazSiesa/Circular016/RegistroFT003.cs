using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT003 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba la línea de negocio acreedora de la obligación (reportante)
    //1:= Aseguramiento obligatorio
    //2:= Aseguramiento voluntario
    //3:= Prestación de servicios

    ///Numérico
    /// </summary>
    public int lineaNegocio { get; set; }

    /// <summary>
    ///Tipo de documento de identificación del deudor 
    //NI:= NIT
    //CC:= Cédula de ciudadanía
    //CE:= Cédula de extranjería
    //OT:= Otra
    ///Texto
    /// </summary>
    public string tipoIdDeudor { get; set; }

    /// <summary>
    ///Número de identificación del deudor
    ///Alfanumérico 
    /// </summary>
    public string idDeudor { get; set; }

    /// <summary>
    ///Número del dígito de verificación del deudor
    //Escriba 0 cuando no aplica
    ///Numérico 
    /// </summary>
    public int dvDeudor { get; set; }

    /// <summary>
    ///Razón social o nombres y apellidos del deudor
    ///Texto
    /// </summary>
    public string nombreDeudor { get; set; }

    /// <summary>
    ///Corresponde al Código del municipio del domicilio principal del deudor, Tabla de División Político Administrativa – DANE
    ///Alfanumérico 
    /// </summary>
    public string codigoMunicipio { get; set; }

    /// <summary>
    ///Escriba el tipo de concepto deudor (Reportar únicamente los códigos que presentan saldo)
    //1:= Plan obligatorio de Salud
    //2:= Planes adicionales de Salud
    //3:= Recobros No POS
    //4:= Reembolsos por incapacidades diferentes a enfermedad general
    //5:= SOAT y ARL
    //6:= Reclamaciones (ECAT)
    //7:= Otros

    ///Numérico 
    /// </summary>
    public int conceptoDeudores { get; set; }

    /// <summary>
    ///Escriba:
    //1:= Activo no financiero - Anticipo
    //2:= Instrumento financiero 

    ///Numérico 
    /// </summary>
    public int tipoDeuda { get; set; }

    /// <summary>
    ///Medición posterior seleccionada como política contable para el instrumento financiero
    //1:= Precio de la Transacción / Valor Nominal / Costo
    //2:= Costo Amortizado
    //3:= Valor Razonable
    //4:= Valor Razonable con cambios en el ORI
    //5:= Valor Presente Pagos Futuros
    //6:= No aplica (Ejemplo: Anticipos)
    ///Numérico
    /// </summary>
    public int medicionPosterior { get; set; }

    /// <summary>
    ///Valor de las cuentas por cobrar que se encuentran pendientes de radicar
    ///Numérico 
    /// </summary>
    public long cxcPendientesRadicar { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar que se encuentran vigentes dentro del plazo establecido para el reconocimiento
    ///Numérico  
    /// </summary>
    public long cxcNoVencidas { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas de 1 a 30 días de acuerdo al  plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMora30Dias { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas de 31 a 60 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMora60Dias { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas de 61 a 90 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMora90Dias { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas de 91 a 180 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMora180Dias { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas de 181 a 360 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMora360Dias { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por cobrar vencidas mayor a  360  días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long cxcMoraMayor360Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas de 1 a 30 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioro30Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas de 31 a 60 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioro60Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas de 61 a 90 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioro90Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas de 91 a 180 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioro180Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas de 181 a 360 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioro360Dias { get; set; }

    /// <summary>
    ///Valor deterioro de las cuentas por cobrar vencidas mayor a 360 días de acuerdo al plazo establecido
    ///Numérico  
    /// </summary>
    public long deterioroMayor90Dias { get; set; }

    /// <summary>
    ///Valor del ajuste por medición posterior
    ///Numérico  
    /// </summary>
    public long ajuste { get; set; }

    /// <summary>
    ///Corresponde al saldo registrado en el catálogo de información financiera Archivo Tipo FT001 
    ///Numérico
    /// </summary>
    public long saldo { get; set; }

  }
}
