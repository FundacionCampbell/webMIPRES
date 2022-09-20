using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT004 : BaseRegistroFT
  {
    /// <summary>
    ///Escriba la línea de negocio del (reportante)
    //1:= Aseguramiento obligatorio
    //2:= Aseguramiento voluntario
    //3:= Prestación de servicios
    ///Numérico(1)
    /// </summary>
    public int lineaNegocio { get; set; }

    /// <summary>
    ///Tipo de documento de identificación de proveedor prestador de servicios de salud
    //NI:= NIT
    //CC:= Cédula de ciudadanía
    //CE:= Cédula de extranjería
    //OT:= Otro
    ///Texto(2)
    /// </summary>
    public string tipoIdAcreedor { get; set; }

    /// <summary>
    ///Número de identificación del proveedor / acreedor 
    ///Alfanumérico (16)
    /// </summary>
    public string idAcreedor { get; set; }

    /// <summary>
    ///Número del dígito de verificación del proveedor / acreedor 
    //Escriba 0 cuando no aplica

    ///Numérico (1)
    /// </summary>
    public int dvAcreedor { get; set; }

    /// <summary>
    ///Razón social o Apellidos y nombre del proveedor / acreedor 
    ///Texto(150)
    /// </summary>
    public string nombreAcreedor { get; set; }

    /// <summary>
    ///Código principal actividad económica (Código CIIU a 4 dígitos revisión 4 adoptada para Colombia)
    ///Alfanumérico(4)
    /// </summary>
    public string actividadAcreedor { get; set; }

    /// <summary>
    ///Escriba el tipo de concepto del acreedor o proveedor
    //1:= Prestación de Servicios de salud
    //2:= Insumos y medicamentos
    //3:= Dispositivo médico o Equipo biomédico
    //4:= Administrativo (Servicios públicos, aportes parafiscales, avances y anticipos, papelería, etc.)
    //5:= Restitución de recursos
    //6:= Otro
    ///Numérico (1)
    /// </summary>
    public int conceptoAcreencia { get; set; }

    /// <summary>
    ///Medición posterior seleccionada como política contable para el instrumento financiero
    //1:= Precio de la Transacción / Valor Nominal / Costo
    //2:= Costo Amortizado
    //3:= Valor Razonable
    //4:= Valor Razonable con cambios en el ORI
    //5:= Valor Presente Pagos Futuros
    //6:= No aplica (ejemplo anticipos)
    ///Numérico(1)
    /// </summary>
    public int medicionPosterior { get; set; }

    /// <summary>
    ///Valor nominal de las cuentas por pagar que se encuentran vigentes dentro del plazo establecido para el reconocimiento
    ///Numérico (18)
    /// </summary>
    public long cxpNoVencidas { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, entre 1 y 30 días de acuerdo al plazo convenido
    ///Numérico (18)
    /// </summary>
    public long cxpMora30dias { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, entre 31 y 60 días de acuerdo al plazo convenido
    ///Numérico (18)
    /// </summary>
    public long cxpMora60dias { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, entre 61 y 90 días de acuerdo al plazo convenido
    ///Numérico (18)
    /// </summary>
    public long cxpMora90dias { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, entre 91 y 180 días  
    ///Numérico (18)
    /// </summary>
    public long cxpMora180dias { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, entre 181 y 360 días  
    ///Numérico (18)
    /// </summary>
    public long cxpMora360dias { get; set; }

    /// <summary>
    ///Valor nominal de las obligaciones que se encuentran en mora, superiores a 360 días
    ///Numérico (18)
    /// </summary>
    public long cxpMoraMayor360dias { get; set; }

    /// <summary>
    ///Valor del ajuste por medición posterior
    ///Numérico  (18)
    /// </summary>
    public long ajuste { get; set; }

    /// <summary>
    ///Corresponde al saldo registrado en el catálogo de información financiera Archivo Tipo FT001 
    ///Numérico(18)
    /// </summary>
    public long saldo { get; set; }


  }//fin clase
}//fin namespace
