using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT012 : BaseRegistroFT
  {
    /// <summary>
    ///Identifique el tipo de Bien según corresponda:
    /// 01= Terrenos 
    /// 02=Construcciones en curso 
    /// /// 03=Propiedad, planta y equipo en montaje
    /// 04=Propiedades planta y equipo en transito
    /// 05=Maquinaria y repuestos
    /// 06=Construcciones y edificaciones 
    /// 07=Equipo de oficina 
    /// 08=Equipo de computación y comunicación 
    /// 09=Equipo médico - científico 
    /// 10=Dotación de Clínicas y Restaurantes 
    /// 11=Flota y equipo de transporte 
    /// 12=Acciones 
    /// 13=Cuotas o partes de interés social 
    /// 14=Títulos 
    /// 15=Otros
    ///Alfanumérico (2)
    /// </summary>
    public string clasificacion { get; set; }

    /// <summary>
    ///La descripción del bien corresponde a la dirección del inmueble, terreno, tipo y placas del vehículo, descripción de los equipos médicos, descripción del título valor, etc.
    ///Alfanumérico ( 150)
    /// </summary>
    public string descripcion { get; set; }

    /// <summary>
    ///Señale el valor de cada uno de los activos  registrado en libros contables al inicio del proceso liquidatorio 
    ///Numérico(18)
    /// </summary>
    public long inventarioInicial { get; set; }

    /// <summary>
    ///Registre el valor de los activos dados de baja 
    ///Numérico(18)
    /// </summary>
    public long valorBajaActivos { get; set; }

    /// <summary>
    ///Registre el número del acto administrativo mediante el cual se resuelve dar de baja los activos
    ///Alfanumérico(8)
    /// </summary>
    public string actoAdmonActivos { get; set; }

    /// <summary>
    ///Fecha del acto administrativo de activos dados de baja
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaActoAdmonActivos { get; set; }

    /// <summary>
    ///Registre el valor del avalúo técnico de los activos de la entidad
    ///Numérico (18)
    /// </summary>
    public long valorAvaluo { get; set; }

    /// <summary>
    ///Registre el número del acto administrativo de aceptación de la valoración del activo 
    ///Alfanumérico(8)
    /// </summary>
    public string actoAdmonAceptacion { get; set; }

    /// <summary>
    ///Fecha del acto administrativo de aceptación de valoración del activo 
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaActoAdmonAceptacion { get; set; }

    /// <summary>
    ///Corresponde al valor ajustado de los activos después de avalúo técnico 
    ///Numérico (18)
    /// </summary>
    public long valorActivo { get; set; }

    /// <summary>
    ///Valor de la enajenación de activos del trimestre actual 
    ///Numérico (18)
    /// </summary>
    public long enajenacion { get; set; }

    /// <summary>
    ///Valor de activos dados en calidad de pago en el trimestre actual 
    ///Numérico (18)
    /// </summary>
    public long dacionPago { get; set; }

    /// <summary>
    ///Registre los ajustes que se puedan presentar durante la enajenación de activos
    ///Numérico (18)
    /// </summary>
    public long ajustes { get; set; }

    /// <summary>
    ///Escriba si  la Propiedad Planta y/o Equipo es sujeto a alguna medida judicial
    /// 0:= No aplica
    /// 1:= Libre de Afectación
    /// 2:= Embargos
    /// 3:= Medida Preventiva
    /// Si la cuenta presenta diferentes medidas, hacer referencia sólo a la última
    ///Numérico(1)
    /// </summary>
    public int estado { get; set; }

    /// <summary>
    ///Fecha en la cual se efectuó el embargo,  medidas preventivas
    /// Si la respuesta de estado es 0, escribir 00000000
    ///Numérico Fecha con formato AAAAMMDD(8)
    /// </summary>
    public string fechaMedida { get; set; }

    /// <summary>
    ///Valor por el cual se tienen los embargos, medidas preventivas
    ///Numérico(18)
    /// </summary>
    public long valorMedida { get; set; }


  }
}
