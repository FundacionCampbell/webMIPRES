function ColocarMask()
{
    $('.date').mask('00/00/0000');
    $('.dateF').mask('0000/00/00');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    //$('.cep').mask('00000-000');
    //$('.phone').mask('0000-0000');
    //$('.phone_with_ddd').mask('(00) 0000-0000');
    //$('.phone_us').mask('(000) 000-0000');
    //$('.mixed').mask('AAA 000-S0S');
    //$('.cpf').mask('000.000.000-00', { reverse: true });
    //$('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('000,000,000,000', { reverse: true });
    $('.valores').mask("#,##0.00", { reverse: true });
    //$('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
    //    translation: {
    //        'Z': {
    //            pattern: /[0-9]/, optional: true
    //        }
    //    }
    //});
    $('.ip_address').mask('099.099.099.099');
    $('.porcentaje').mask('##0,00%', { reverse: true });
    //$('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
    //$('.fallback').mask("00r00r0000", {
    //    translation: {
    //        'r': {
    //            pattern: /[\/]/,
    //            fallback: '/'
    //        },
    //        placeholder: "__/__/____"
    //    }
    //});
    //$('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });
}

function ValidarTextbox(Textbox, Mensaje) {
    Textbox.focus();
    console.log('Textbox : ' + Textbox.val());
    if (Textbox.val() == null || Textbox.val() == '' || /^\s+$/.test(Textbox.val())) {
        swal("", Mensaje, "warning");
        return false;
    } else {
        OcultarDiv("ui-datepicker-div");
        return true;
    }

}

function QuitarMask() {
    // $('.date').unmask('00/00/0000');
    // $('.dateF').unmask('0000/00/00');
    $('.time').unmask('00:00:00');
    $('.date_time').unmask('00/00/0000 00:00:00');
    //$('.cep').unmask('00000-000');
    //$('.phone').unmask('0000-0000');
    //$('.phone_with_ddd').unmask('(00) 0000-0000');
    //$('.phone_us').unmask('(000) 000-0000');
    //$('.mixed').unmask('AAA 000-S0S');
    //$('.cpf').unmask('000.000.000-00', { reverse: true });
    //$('.cnpj').unmask('00.000.000/0000-00', { reverse: true });
    $('.money').unmask('000,000,000,000.00', { reverse: true });
    $('.valores').unmask("#.##0,00", { reverse: true });
    $('.ip_address').unmask('0ZZ.0ZZ.0ZZ.0ZZ', {
        translation: {
            'Z': {
                pattern: /[0-9]/, optional: true
            }
        }
    });
    $('.ip_address').unmask('099.099.099.099');
    $('.porcentaje').unmask('##0,00%', { reverse: true });
    //$('.clear-if-not-match').unmask("00/00/0000", { clearIfNotMatch: true });
    //$('.placeholder').unmask("00/00/0000", { placeholder: "__/__/____" });
    //$('.fallback').unmask("00r00r0000", {
    //    translation: {
    //        'r': {
    //            pattern: /[\/]/,
    //            fallback: '/'
    //        },
    //        placeholder: "__/__/____"
    //    }
    //});
    //$('.selectonfocus').unmask("00/00/0000", { selectOnFocus: true });
}

// Valido que ingresen caracteres el textbox
function LimpiarTextbox(Textbox) {
    Textbox.val('');
}

// Valido que ingresen caracteres el textbox
function ReestablecerCombobox(Combobox) {
    Combobox.selectedIndex = 0;
}

// Valida los combobox que este seleccionado los item validos
function ValidarCombobox(Combobox, Mensaje) {
    console.log('Combobox.selectedIndex: ' + Combobox.selectedIndex);
    console.log('Combobox.value: ' + Combobox.value);
    Combobox.focus();
    if (Combobox.selectedIndex == null || Combobox.selectedIndex == 0) {
        //alert(Mensaje);
        swal("", Mensaje, "warning");
        return false;
    }
}


function BloquearTextbox(Textbox) {
    Textbox.attr("disabled", "disabled");
}

function DesbloquearTextbox(Textbox) {
    Textbox.removeAttr("disabled");
}

function BloquearButton(Textbox) {
    $('#' + Textbox).attr("disabled", true);

}

function DesbloquearButton(Textbox) {
    $('#' + Textbox).removeAttr("disabled");
}


function BloquearCombobox(Combobox) {
    Combobox.disabled = true;
}

function DesbloquearCombobox(Combobox) {
    Combobox.disabled = false;
}

//--- Carga de DropDownList
function CargarDropDownList(UrlMetodo, IdDropDownList) {
    $.ajax({
        type: "POST",
        //url: 'WebForm1.aspx/CargarTipoPoliza', // el URL del método que vamos a llamar
        url: UrlMetodo,
        data: JSON.stringify(),  // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            //var DropDownList = $("[id*=cplContenido_ddlTipoPoliza]");
            var DropDownList = $("[id*=cplContenido_" + IdDropDownList + "]");
            DropDownList.empty().append('<option selected="selected" value="0">Seleccione una Opción</option>');

            $.each(r.d, function () {
                DropDownList.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (req, stat, err) // función que se va a ejecutar si el pedido falla
        {
            var error = eval("(" + req.responseText + ")");
            alert(error.Message);
        }
    });

}

//function CargarDropDownList(UrlMetodo, IdDropDownList, Usuario) {
//    var Parameter = { Usuario: Usuario }

//    $.ajax({
//        type: "POST",
//        url: UrlMetodo,
//        data: JSON.stringify(Parameter),  // los parámetros en formato JSON
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {

//            var DropDownList = $("[id*=cplContenido_" + IdDropDownList + "]");
//            DropDownList.empty().append('<option selected="selected" value="0">Seleccione una Opción</option>');

//            $.each(r.d, function () {
//                DropDownList.append($("<option></option>").val(this['Value']).html(this['Text']));
//            });
//        },
//        error: function (req, stat, err) // función que se va a ejecutar si el pedido falla
//        {
//            var error = eval("(" + req.responseText + ")");
//            alert(error.Message);
//        }
//    });



//}

function CargarDropDownCuotas(UrlMetodo, IdDropDownList) {
   
    $.ajax({
        type: "POST",
        url: UrlMetodo, // el URL del método que vamos a llamar
        data: JSON.stringify(),  // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            //var DropDownList = $("[id*=cplContenido_ddlTipoPoliza]");
            var DropDownList = $("[id*=cplContenido_" + IdDropDownList + "]");
            DropDownList.empty().append("<option selected='selected' value='0'>Seleccione una Opción</option>");

            $.each(r.d, function () {
                DropDownList.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (req, stat, err) // función que se va a ejecutar si el pedido falla
        {
            var error = eval("(" + req.responseText + ")");
            alert(error.Message);

        }
    });
}

function CargarDropDownContab(UrlMetodo, IdDropDownList, valor) {


    var Parameter = { CodCuenta: valor }

    $.ajax({
        type: "POST",
        url: UrlMetodo, // el URL del método que vamos a llamar
        data: JSON.stringify(Parameter),  // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            //var DropDownList = $("[id*=cplContenido_ddlTipoPoliza]");
            var DropDownList = $("[id*=cplContenido_" + IdDropDownList + "]");
            DropDownList.empty().append("<option selected='selected' value='0'>Seleccione una Opción</option>");

            $.each(r.d, function () {
                DropDownList.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (req, stat, err) // función que se va a ejecutar si el pedido falla
        {
            var error = eval("(" + req.responseText + ")");
            alert(error.Message);

        }
    });
}

//--- Validacion de Enter en TextBox
function ValidarEnter(Textbox, Button) {
    $(Textbox).keypress(function (event) {
        if (event.keyCode == '13') {
            $(Button).click();
            event.preventDefault();
        }
    });
}

//Funcion para mostrar cualquier div
function MostrarDiv(DivModal) {
    $("#" + DivModal).show('blind');
};

//Funcion para Ocultar cualquier div
function OcultarDiv(DivModal) {
    $("#" + DivModal).hide('blind');
};

// Funcion para cerrar Modal
function CerrarModal(DivModal) {
    $("#" + DivModal).dialog("close");
};

// Funcion para abrir Modal
function AbrirModal(DivModal) {

    //Establezco las propiedades del modal
    $("#" + DivModal).dialog({
        autoOpen: true,
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        }
    });

};

//Funcion que me dice la ruta de la imagen, recibe como parametro 'http' o 'https'
function RutaImagenes(TipoProtocolo) {
    var url = TipoProtocolo + '://' + window.location.host + '/Extras/Images/';
    return url;
}

//-- Formato de Fecha, Numeros, Valores
function formatDate(Fecha, Formato) {

    Fecha = Fecha.substring(0, Fecha.indexOf("T"));
    Formato = Formato.toUpperCase();

    var posSeparador = Fecha.indexOf("/");

    if (posSeparador === -1) {
        posSeparador = Fecha.indexOf("-");
    }

    var posYear = 0;

    if (posSeparador === 4) {
        posYear = 1;
    }

    if (posYear === 1) {

        var Year = Fecha.substring(0, posSeparador);
        var Mes = Fecha.substring(posSeparador + 1, posSeparador + 3);
        var Dia = Fecha.substring(Fecha.length - 2);
    }
    else {

        var Dia = Fecha.substring(0, posSeparador);
        var Mes = Fecha.substring(posSeparador + 1, posSeparador + 3);
        var Year = Fecha.substring(Fecha.length - 4);
    }

    Formato = Formato.replace("DD", Dia);
    Formato = Formato.replace("MM", Mes);
    Formato = Formato.replace("YYYY", Year);
    console.log(Formato, 'Fecha');
    return Formato;
}

function DateFormato(texto) {
    var diaActual = new Date(texto);
    //texto.toISOString().slice(0, 10);
    //Datetime.now().format('YYYY-MM-dd');
    //var str = diaActual.format('dd/mm/YYYY');

    //var str = diaActual.toLocaleDateString("en-US");
    //var str = texto.toLocaleDateString("en-US");
    //var str = diaActual.toString("dd/MM/yyyy");
    var str = Date.parse(texto.toString()).toString("dd/MM/YYYY");
    console.log(str);
    return str;
    //return texto.replace(/^(\d{4})-(\d{2})-(\d{2})$/g, '$3/$2/$1');
}

function FormatNumeric(Textbox, CantDecimales) {
    Textbox.number(true, CantDecimales);
}

function currencyFormatDE(Textbox) {
    var MaskMoney = Textbox.val();
    Textbox.val(MaskMoney.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    //Textbox.val().toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");

}

function QuitarCurrencyDE(Textbox) {
    var txtValor = Textbox.val();
    txtValor = txtValor.replace(/[^\d\.\-]/g, "");
    var num = parseFloat(txtValor);
    Textbox.val(num);

}

function FV(rate, nper, pmt, pv, type) {
    var pow = Math.pow(1 + rate, nper),
        fv;
    if (rate) {
        fv = (pmt * (1 + rate * type) * (1 - pow) / rate) - pv * pow;
    } else {
        fv = -1 * (pv + pmt * nper);
    }
    return fv.toFixed(2);
}


//--- Barra de Progreso...
function progressBarSim(al) {
    var bar = document.getElementById('progressBar');
    var status = document.getElementById('status');
    status.innerHTML = al + "%";
    bar.value = al;
    al++;
    var sim = setTimeout("progressBarSim(" + al + ")", 10);
    if (al == 100) {
        status.innerHTML = "100%";
        bar.value = 100;
        clearTimeout(sim);
        var finalMessage = document.getElementById('finalMessage');
        finalMessage.innerHTML = "Process is complete";
    }
}

function MostrarModal(NomDiv){
    $('#'+NomDiv).dialog({
        autoOpen: true,
        resizable: false,
        height: "auto",
        width: 520,
        modal: true,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        }
    });
}
