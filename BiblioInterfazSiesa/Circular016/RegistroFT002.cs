using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiloSoft.Siesa.Interfaz.Circular016
{
  public class RegistroFT002:BaseRegistroFT
  {
    /// <summary>
    ///Escriba el medio en el que se publica los estados financieros
    //1:= Página web oficial entidad
    //2:=Diario Oficial
    //3:=Prensa de amplia circulación
    //4:= Página web oficial alcaldía ( únicamente ESE)
    ///Numérico
    /// </summary>
    public int medioPublicacion { get; set; }

    /// <summary>
    ///Fecha en la cual se realizó la publicación de los estados financieros
    ///Numérico Fecha con formato AAAAMMDD
    /// </summary>
    public string fechaPublicacion { get; set; }

    /// <summary>
    ///Escribir URL donde publica los estados financieros o número del diario o nombre del periódico 
    ///Alfanumérico
    /// </summary>
    public string lugarPublicacion { get; set; }
  }
}
