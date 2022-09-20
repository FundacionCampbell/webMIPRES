using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT005 : BaseRegistroFT
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
    ///Tipo de documento de identificación del proveedor
    //NI:=NIT
    //CC:=Cédula de ciudadanía
    //CE:=Cédula de extranjería
    //OT:=Otra
    //NA:= No aplica. Para los registros agregados en los que no se tiene el tipo de identificación del tercero. (Ejemplo: Nómina)
    ///Texto(2)
    /// </summary>
    public string tipoIdProveedor { get; set; }

    /// <summary>
    ///Número de identificación del proveedor al que se le registró la operación económica
    //Nota: NO incluir el dígito de verificación
    //En el evento en que el tipo de identificación sea NA escriba 
    ///Alfanumérico (16)
    /// </summary>
    public string idProveedor { get; set; }

    /// <summary>
    ///Número del dígito de verificación del proveedor
    //Escriba 0 cuando no aplica
    ///Numérico (1)
    /// </summary>
    public int dvProveedor { get; set; }

    /// <summary>
    ///Razón social o Apellidos y nombre del proveedor
    //En caso que el tipo de identificación sea NA escriba el concepto del registro para el cual hay agregación
    ///Texto(150)
    /// </summary>
    public string nombreProveedor { get; set; }

    /// <summary>
    ///Escriba el código del concepto contable al nivel más desagregado donde registra la operación económica, al nivel máximo de 8 dígitos. Estos conceptos deben corresponder exclusivamente a los conceptos de costo y gasto por el que fue causado el bien o servicio originalmente de acuerdo a la estructura del Archivo Tipo FT001.

    //Excluya aquellos conceptos que no tengan asociado transacción con un tercero tales como deterioro, amortización, provisiones, entre otros
    ///Numérico (10)
    /// </summary>
    public int codigoConcepto { get; set; }

    /// <summary>
    ///1:= Servicios de Salud incluidos en el POS
    //2 :=Medicamentos incluidos en el POS
    //3 := Servicios de Salud no incluidos en el POS
    //4:= Medicamentos no incluidos en el POS
    //5:= Servicios de Salud incluidos en el plan de atención Complementaria (PAC)
    //6:= Medicamentos  incluidos en el Plan de atención Complementaria (PAC)
    //7:= Otras coberturas incluidas en el Plan de atención Complementaria (PAC)
    //8:= Gastos Generales
    //9:= Otros Costos
    //0:= Otros Gastos
    ///Numérico(1)
    /// </summary>
    public int descripcionCyG { get; set; }

    /// <summary>
    ///Monto total causado al proveedor en el año
    ///Numérico(18)
    /// </summary>
    public long valorCausado { get; set; }

    /// <summary>
    ///Valor efectivamente pagado al proveedor en el año por cada concepto.  (Este valor corresponde al total pagado en el año, aunque haya sido causado en vigencias anteriores)
    ///Numérico(18)
    /// </summary>
    public long valorPagado { get; set; }


  }//fin clase
}//fin  namespace
