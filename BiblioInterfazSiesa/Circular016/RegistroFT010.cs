using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT010 : BaseRegistroFT
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
    ///Escriba el concepto de activos no monetarios
    /// 1:= Terrenos
    /// 2:= Construcciones y edificaciones 
    /// 3:= Equipo médico científico
    /// 4:= Equipo de transporte
    ///Numérico (1)
    /// </summary>
    public int concepto { get; set; }

    /// <summary>
    ///La descripción del bien corresponde a la dirección del inmueble, terreno, tipo y placas del vehículo, descripción de los equipos médicos
    ///Alfanumérico ( 150)
    /// </summary>
    public string descripcion { get; set; }

    /// <summary>
    ///Escriba que finalidad tiene el activo no monetario de inversión u operativa
    /// 1:= Propiedad planta y equipo 
    /// 2:= Propiedad de inversión
    /// 3:= Activo no corriente, mantenido para la venta (incluye no corriente mantenido para la distribución a los propietarios)
    ///Numérico (1)
    /// </summary>
    public int finalidad { get; set; }

    /// <summary>
    ///Escriba el tipo de medición definido como política de la entidad
    /// 1:= Modelo del Costo 
    /// 2:= Método de Revaluación
    /// 3:= Modelo del Valor Razonable
    /// 4:= Menor entre importe en libros y valor razonable menos costos de venta (o costo de distribución cuando los activos no corrientes se mantengan para distribuir a los propietarios)
    ///Numérico (1)
    /// </summary>
    public int medición { get; set; }

    /// <summary>
    ///Escriba el valor por el que inicialmente fue reconocido el activo
    ///Numérico (18)
    /// </summary>
    public long reconocimiento { get; set; }

    /// <summary>
    ///Escriba el valor en que el activo no monetario se ha depreciado
    ///Numérico(18)
    /// </summary>
    public long depreciacion { get; set; }

    /// <summary>
    ///Valor correspondiente al deterioro de los activos no monetarios
    ///Numérico (18)
    /// </summary>
    public long deterioro { get; set; }


    /// <summary>
    ///Escriba si el activo no monetario está sujeto a alguna medida judicial
    /// 0:= No aplica
    /// 1:= Libre de Afectación
    /// 2:= Embargo
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
