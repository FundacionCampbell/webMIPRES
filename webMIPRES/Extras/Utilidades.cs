using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


namespace ERPymeWeb.Extras
{
    public class Utilidades
    {



        public string strDireccionIp;
        public string strPuertoIp;
        public string strCarpetaImg;

        public MailMessage correo;

        public enum enTipoMensaje
        {
            enTpMsgError = 1,
            enTpMsgAdvertencia = 2,
            enTpMsgAfirmacion = 3,
        }


        /// <summary>
        /// Metodo que muestra mensajes en ventanas jquery.
        /// </summary>
        /// <param name="tipo">(int) Los tipos son 1 Error, 2 Advertencia, 3 Afirmacion, 4 Para mostar Imagenes se le manda la url</param>
        /// <param name="mensajeDeError">(string) es donde va la descripcion o el mensaje de error </param>
        public void MostrarMensajesJQuery(Control control, int tipo, string mensajeDeError)
        {

           // string text = mensajeDeError;
            // Quito las comillas simple de la cadena, si es que viene con comillas
            mensajeDeError = mensajeDeError.Replace("'", "");

            //ERROR
            if (tipo == 1)
            {
                ScriptManager.RegisterStartupScript(control, GetType(), "Popup", "mensajeError('" + mensajeDeError + "');", true);
            }

            //ADVERTENCIA
            if (tipo == 2)
            {
                ScriptManager.RegisterStartupScript(control, GetType(), "Popup", "mensajeAdvertencia('" + mensajeDeError + "');", true);
            }

            // AFIRMACION
            if (tipo == 3)
            {
                ScriptManager.RegisterStartupScript(control, GetType(), "Popup", "mensajeAfirmacion('" + mensajeDeError + "');", true);

            }

            if (tipo == 0)
            {
                ScriptManager.RegisterStartupScript(control, GetType(), "Popup", "mensajeConfirmacion('" + mensajeDeError + "');", true);
            }
        }

        /// <summary>
        /// Este metodo será utilizado para la confirmar al usuario si quiere realizar una acción
        /// </summary>
        /// <param name="mensajeDeError">Descripción del mensaje</param>
        /// <param name="titulo">Titulo del mensaje</param>
        /// <param name="urlMetodo">urlMetodo es donde se encuentra el metodo statico que se va a llamar desde el json </param>
        public void MensajeConfirmacioJQuery(Control control, string mensajeDeError, string titulo, string urlMetodo)
        {
            ScriptManager.RegisterStartupScript(control, GetType(), "Popup", "mensajeConfirmarAccion('" + mensajeDeError + "' , '" + titulo + "', '" + urlMetodo + "');", true);
        }

        /// <summary>
        /// Metodo para colocar el focus a los controles aspx
        /// </summary>
        /// <param name="NombreControlASP">El control Puede ser un Texbox</param>
        /// <param name="control">es el famos this </param>
        public void Focus(String NombreControlASP, Control control)
        {
            ScriptManager.RegisterStartupScript(control, GetType(), "prueba", "Focus('" + NombreControlASP + "');", false);
            return;
        }

        /// <summary>
        /// Metodo para validar los TextBox no esten vacio
        /// </summary>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public Boolean validarTexboxVacios(TextBox textbox)
        {
            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                // Valido si el campo es de tipo fecha
                if (textbox.CssClass == "datepicker form-control input-sm")
                {
                    textbox.CssClass = "datepicker form-control input-sm list-group-item-danger";
                }
                else
                {
                    textbox.CssClass = " form-control input-sm list-group-item-danger";
                    textbox.Focus();
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo para cuando el valor de un texbox no es correcto
        /// </summary>
        /// <param name="textbox"></param>
        public void ErrorTexbox(TextBox textbox)
        {

            // Valido si el campo es de tipo fecha
            if (textbox.CssClass == "datepicker form-control input-sm")
            {
                textbox.CssClass = "datepicker form-control input-sm list-group-item-danger";
            }
            else
            {
                textbox.CssClass = " form-control input-sm list-group-item-danger";
            }

        }


        /// <summary>
        /// Metodo para validar DropDownList no este vacio
        /// </summary>
        /// <param name="DropDownList"></param>
        /// <returns></returns>
        public Boolean validarComboboxVacios(DropDownList DropDownList)
        {

            if (DropDownList.SelectedIndex == 0)
            {
                DropDownList.Focus();
                DropDownList.CssClass = "form-control input-sm list-group-item-danger";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo para quitar el borde de error
        /// </summary>
        /// <param name="textbox"></param>
        public void quitarBorderTextBox(TextBox textbox)
        {
            // Valido si el campo es de tipo fecha y viene con border rojo
            if (textbox.CssClass == "datepicker form-control input-sm list-group-item-danger")
            {
                textbox.CssClass = " datepicker form-control input-sm";
            }
            else
            {
                textbox.CssClass = "form-control input-sm";
            }

        }

        /// <summary>
        /// Metodo para quitar el border de error
        /// </summary>
        /// <param name="DropDownList"></param>
        public void quitarBorderCombobox(DropDownList DropDownList)
        {
            DropDownList.CssClass = "form-control input-sm";
        }

        /// <summary>
        /// Metodo que valida si el correo esta bien
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// Metodo que valida si el correo esta bien
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="This"></param>
        /// <param name="Btn"></param>
        public void DesabilitarBtn(Control This, Button Btn ) 
        {
            This.Page.ClientScript.GetPostBackClientHyperlink(Btn, string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.Append(This.Page.ClientScript.GetPostBackEventReference(Btn, string.Empty));
            sb.Append(";");
            Btn.Attributes["onclick"] = sb.ToString();
        }


        public void LimpiarTxt(TextBox txt)
        {
            txt.Text = string.Empty;
        }

        public void BloquearTxt(TextBox txt,Boolean bloquear)
        {
            if (bloquear)
            {
                txt.Enabled = false;
            }
            else
            {
                txt.Enabled = true;
            }           
        }

        public void BloquearCombobox(DropDownList combobox, Boolean bloquear)
        {
            if (bloquear)
            {
                combobox.Enabled = false;
            }
            else
            {
                combobox.Enabled = true;
            }
        }

        public void BloquearBtn(Button Btn, Boolean bloquear)
        {
            if (bloquear)
            {
                Btn.Enabled = false;
            }
            else
            {
                Btn.Enabled = true;
            }
        }


        public bool EnviarCorreo(string remitente, string destino, string asunto, string mensaje)
        {
            //correo.IsBodyHtml = true;

            string error;
            correo = new MailMessage();
            correo.To.Add(new MailAddress(destino));
            correo.From = new MailAddress(remitente);
            correo.Subject = asunto;
            correo.Body = mensaje;
            correo.IsBodyHtml = false;

            SmtpClient client = new SmtpClient("coopmultiactiva.com.co", 25);
            using (client)
            {
                try
                {
                    client.Credentials = new System.Net.NetworkCredential("wilsonsierra@coopmultiactiva.com.com", "wilsonsierra.089");
                    client.EnableSsl = true;
                    client.Send(correo);
                    return true;

                }
                catch (Exception ex)
                {
                    error = "Error:" + ex.Message;
                    return false;
                }
            }
        }
    }
}