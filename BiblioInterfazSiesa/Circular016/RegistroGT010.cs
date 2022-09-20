using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroGT010 : BaseRegistroFT
  {
    /// <summary>
    ///Tipo de reporte al que corresponda la información
    /// 1:= Reporte periódico
    /// 2:= Modificación
    /// 3:= Requerimiento de la Superintendencia 
    ///Numérico(1)
    /// </summary>
    public int tipoReporte { get; set; }

    /// <summary>
    ///Tipo de identificación del aportante
    /// NI:= NIT
    /// CC:= Cédula de ciudadanía
    /// CE:= Cédula de extranjería
    /// OT:= Otro
    ///Texto(2)
    /// </summary>
    public string tipoIdAportante { get; set; }

    /// <summary>
    ///Número del documento de identificación del aportante, socio, cooperado o su equivalente de acuerdo a la naturaleza jurídica de la entidad
    ///Alfanumérico (16)
    /// </summary>
    public string idAportante { get; set; }

    /// <summary>
    ///Número del dígito de verificación del aportante, socio, cooperado o su equivalente de acuerdo a la naturaleza jurídica de la entidad
    /// Escriba 0 cuando no aplique
    ///Numérico (1)
    /// </summary>
    public int dvAportante { get; set; }

    /// <summary>
    ///Razón social o nombres y apellidos del aportante, socio, cooperado o su equivalente de acuerdo a la naturaleza jurídica de la entidad
    ///Texto(150)
    /// </summary>
    public string nombreAportante { get; set; }

    /// <summary>
    ///Código del municipio del domicilio principal. Tabla de División Político Administrativa – DANE
    ///Alfanumérico (5)
    /// </summary>
    public string codigoMunicipio { get; set; }

    /// <summary>
    ///Valor total del aporte o de las acciones en la fecha de corte
    ///Numérico (18)
    /// </summary>
    public long valor { get; set; }


  }
}
