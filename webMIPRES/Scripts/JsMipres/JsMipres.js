
var arrayMedicamentos = [];
var arrayProcedimientos = [];
var arrayProductosNutricionales = [];
var arraydispositivos = [];
var arrayserviciosComplementarios = [];
var arrayEntregas = [];

var swfCuenta = "";
var Token = "";
var TokenProv = "";

$(document).ready(function () {

    GetParametros();

    $('#btnBuscar').click(function (e) {

        var FechaIni = $("#FechaIni").val();

        if (FechaIni === null) {
            alert('Debe Ingresar una Fecha...');
            return;
        }

        arrayMedicamentos = [];
        arrayProcedimientos = [];
        arrayProductosNutricionales = [];
        arraydispositivos = [];
        arrayserviciosComplementarios = [];


        $("#gvPrescripciones").empty();
        //progress();
        GetPrescripciones();

    });

    $('#btnPrescripcion').click(function (e) {

        var NoPrescripcion = $("#NoPrescripcion").val();

        if (NoPrescripcion === null) {
            alert('Debe Ingresar un Numero de Prescripcion...');
            return;
        }

        arrayMedicamentos = [];
        arrayProcedimientos = [];
        arrayProductosNutricionales = [];
        arraydispositivos = [];
        arrayserviciosComplementarios = [];


        $("#gvPrescripciones").empty();

        GetNoPrescripcion();

    });

    $('#btnModal2').click(function (e) {
        $("#modalMIPRES").modal('hide');
        $("#modMedicamentos").modal('show');
    });

    $('#btnModal1').click(function (e) {
        $("#modMedicamentos").modal('hide');

    });

    $('#btnEnviar').click(function (e) {

        GetEnvioMIPRES();
        //        GrabaEntrega();
        //        ReporteEntrega();


        $("#modalMIPRES").modal('hide');
        $("#modMedicamentos").modal('show');
    });

    $("#TipoTec").change(function () {

        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        $("#hfTipoTecnologia").val(selectedValue);

    });

    $("#ddlProceso").change(function () {

        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        $("#hfProceso").val(selectedValue);
        if (selectedValue === "P") {
            $("#TokenEmpresa").val(Token);
        }
        else {
            $("#TokenEmpresa").val(TokenProv);
        }

    });

    $('#btnLoader').click(function (e) {

    });

    $('#btnGenerar').click(function (e) {
        var NitEntidad = $("#Nit").val();
        var Token = $("#TokenEmpresa").val();

        GetGenerarToken(NitEntidad, Token);

    });

    $('#btnBuscarEF').click(function (e) {

        var FechaIni = $("#FechaIni").val();

        if (FechaIni === null) {
            alert('Debe Ingresar una Fecha...');
            return;
        }

        $("#gvPrescripciones").empty();
        ListEntregasFechas();

    });

    $('#btnBuscarRF').click(function (e) {

        var FechaIni = $("#FechaIni").val();

        if (FechaIni === null) {
            alert('Debe Ingresar una Fecha...');
            return;
        }

        $("#gvPrescripciones").empty();
        GetReporteFechas();

    });

    $('#btnPrescripcionE').click(function (e) {

        var NoPrescripcion = $("#NoPrescripcion").val();
        $("#hfNoPrescripcion").val(NoPrescripcion);

        if (NoPrescripcion === null) {
            alert('Debe Ingresar un Numero de Prescripcion...');
            return;
        }

        $("#gvPrescripciones").empty();
        GetEntregaPrescripcion();

    });

    $('#btnPrescripcionR').click(function (e) {

        var NoPrescripcion = $("#NoPrescripcion").val();

        if (NoPrescripcion === null) {
            alert('Debe Ingresar un Numero de Prescripcion...');
            return;
        }

        $("#gvPrescripciones").empty();
        GetReportePrescripcion();

    });

    $('#btnPrescripcionF').click(function (e) {

        var NoPrescripcion = $("#NoPrescripcion").val();
        $("#hfNoPrescripcion").val(NoPrescripcion);

        if (NoPrescripcion === null) {
            alert('Debe Ingresar un Numero de Prescripcion...');
            return;
        }

        $("#gvPrescripciones").empty();
        GetPrescripcionFact();

    });

    $('#btnRegFactura').click(function (e) {

        var IdEPS = $("#hfNitEPS").val();
        var separa = IdEPS.split(/-| /);
        var vNitEps = separa[0];
        $("#hfNitEPS").val(vNitEps);
        var CodEps = $("#CodEPS").val();
        var numFact = $("#hfFactEPS").val();
        var numEntrega = $("#NoEntrega").val();
        var valEntrega = $("#ValorTotFacturado").val();
        var valUnit = $("#ValorUnitFacturado").val();

        //if (CodEps === null || CodEps.length == 0) {
        //    alert('Debe Ingresar el Codigo EPS...');
        //    return;
        //}
        if (numFact == "0" || numFact.length == 0) {
            alert('Debe Ingresar Numero de Factura...');
            return;
        }
        if (parseInt(numEntrega) == 0 || numEntrega.length == 0) {
            alert('Debe Ingresar un Numero de Entrega...');
            return;
        }
        if (IdEPS === null || IdEPS.length == 0) {
            alert('Debe Ingresar Seleccionar una Entidad de Salud...');
            return;
        }
        if (parseInt(valEntrega) == 0 || valEntrega.length == 0) {
            alert('Debe digitar un Valor de Entrega...');
            return;
        }
        if (parseInt(valUnit) == 0 || valUnit.length == 0) {
            alert('Debe digitar un Valor Unitario del medicamento...');
            return;
        }

        GetRegistrarFact();

    });

    $('#btnAnularEnt').click(function (e) {

        $("#gvPrescripciones").empty();
        PutAnularEntrega("1");

    });

    $('#btnAnulaRep').click(function (e) {

        $("#gvPrescripciones").empty();
        PutAnularEntrega("2");

    });

    $("#btnBuscarInf").click(function () {

        var parFechaI = $("#FechaIni").val();
        var parFechaF = $("#FechaFin").val();

        GetInformeMipres(parFechaI, parFechaF);

    });

    $("#lstCasos").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        $("#hfNoCuenta").val(selectedValue);
        GetTraerEPS();
    });

    $("#lstNoIDEPS").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        $("#hfNitEPS").val(selectedValue);

        var arrayCodEPS = selectedText.split("-");
        var CodigoEPS = arrayCodEPS[1];
        var NomEps = arrayCodEPS[0];
        $("#CodEPS").val(CodigoEPS);
        $("#labelEPS").text(NomEps);

        //document.getElementById("lstFactEPS").options.length = 0;
        document.getElementById("lstFactEPS").innerHTML = "";

        GetFacturasEPS(selectedValue);

    });

    $("#lstFactEPS").change(function () {

        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        $("#hfFactEPS").val(selectedValue);

    });

    $("#btnExport").click(function (e) {
        e.preventDefault();
        descargarExcel();

    });

    // Loader
    $('#myModal').on('shown.bs.modal', function () {

        var progress = setInterval(function () {
            var $bar = $('.bar');

            if ($bar.width() == 500) {

                // complete

                //clearInterval(progress);
                //$('.progress').removeClass('active');
                //$('#myModal').modal('hide');
                //$bar.width(0);

            } else {

                // perform processing logic here

                $bar.width($bar.width() + 50);
            }
            $bar.text($bar.width() / 5 + "%");
        }, 800);

    });

    //
});


/// *** Valida Token

function GetParametros() {
    var UriRuta = 'GetParametros'; //"LovPacientes"; //
    //var parametros = { criterio: criterio1 }
    //alert('Entre al Lov');
    try {
        $.ajax({
            type: "POST", //GET
            url: "GetParametros",
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: {},
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                $.each(res, function (i, item) {
                    var NitEntidad = item["Nit"];
                    var Empresa = item["Empresa"];
                    Token = item["token"];
                    TokenProv = item["tokenproveedor"];
                    var UrlPrescripciones = item["UrlPrescripciones"];
                    var UrlUnaPrescripcion = item["UrlUnaPrescripcion"];
                    var UrlValidaToken = item["UrlValidaToken"];
                    var UrlEntregaAmbito = item["UrlEntregaAmbito"];
                    var UrlReporteEntrega = item["UrlReporteEntrega"];
                    var UrlEntregasFechas = item["UrlEntregasFechas"];
                    var UrlEntregasPrescripcion = item["UrlEntregaPrescripcion"];
                    var UrlReportesPrescripcion = item["UrlReportePrescripcion"];
                    var UrlAnularNoEntrega = item["UrlAnularNoEntrega"];
                    var UrlAnularNoReporte = item["UrlAnularReporteEntrega"];
                    var UrlReportesFechas = item["UrlReportesFechas"];
                    var UrlFacturacion = item["UrlFacturacion"];
                    var NoTokenGenerado = item["TokenGenerado"];
                    var hfProceso = $("#hfProceso").val();

                    $("#hfNit").val(NitEntidad);
                    $("#hfToken").val(Token);
                    $("#hfTokenProveedor").val(TokenProv);
                    $("#hfEmpresa").val(Empresa);
                    $("#hfUrlPrescripciones").val(UrlPrescripciones);
                    $("#hfUrlUnaPrescripcion").val(UrlUnaPrescripcion);
                    $("#hfUrlValidaToken").val(UrlValidaToken);
                    $("#hfUrlEntregaAmbito").val(UrlEntregaAmbito);
                    $("#hfUrlReporteEntrega").val(UrlReporteEntrega);
                    $("#hfUrlEntregasFechas").val(UrlEntregasFechas);
                    $("#hfUrlEntregaPrescripcion").val(UrlEntregasPrescripcion);
                    $("#hfUrlReportePrescripcion").val(UrlReportesPrescripcion);
                    $("#hfUrlReportesFechas").val(UrlReportesFechas);
                    $("#hfUrlAnularNoEntrega").val(UrlAnularNoEntrega);
                    $("#hfUrlAnularReporteEntrega").val(UrlAnularNoReporte);
                    $("#hfUrlFacturacion").val(UrlFacturacion);
                    $("#hfTokenGenerado").val(NoTokenGenerado);
                    $("#Nit").val(NitEntidad);
                    //if (hfProceso === "P") {
                    //    $("#TokenEmpresa").val(Token);
                    //}
                    //else
                    //{
                    //    $("#TokenEmpresa").val(TokenProv);
                    //}
                    $("#TokenGenerado").val();
                });

            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }
}

function GetGenerarToken(Nit, Token) {
    var UriRuta = 'GetGenerarToken'; //"LovPacientes"; //
    var UrlPrescripcion = $("#hfUrlValidaToken").val();

    //var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "token": Token };

    try {
        $.ajax({
            type: "POST", //GET
            url: "GetGenerarToken",
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { "Uri": UrlPrescripcion, "nit": Nit, "token": Token },
            beforeSend: function () {
                // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
                $("#loader").show();
            },
            success: function (data) {
                //alert(data);
                $("#loader").hide();
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                $("#TokenGenerado").val(res);
                $("#hfTokenGenerado").val(res);
                $("#hfNit").val(Nit);
                $("#hfTokenEmp").val(Token);

                $('#btnGenerar').attr('disabled', 'disabled');
                alert('Token Generado:' + res);
                GrabarToken(res);

            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }
}

function GrabarToken(notoken) {
    var UriRuta = 'RegistrarTokenGen'; //"LovPacientes"; //
    var nitEmp = $("#hfNit").val();

    try {
        $.ajax({
            type: "POST", //GET
            url: "RegistrarTokenGen",
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { "Nit":nitEmp,"token": notoken },
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                if (res.substring(0,6) == "Error:") {
                    alert('Error:' + res);
                }
                //alert('Token Generado:' + notoken);

            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }
}


/// ** Prescripciones

function GetCallView() {

    $.ajax({
        url: 'GetDatosCasos',
        type: 'POST',
        data: {},
        success: function (result) {
            //return;
        }
    });

}

function funcionClick(control) {
    var opNoCuenta = $(control).closest('tr').find('label0').html();
    var opNoIdentificacion = $(control).closest('tr').find('label1').html();
    var opPaciente = $(control).closest('tr').find('label2').html();
    var opNoHistoria = $(control).closest('tr').find('label4').html();
    var opEmpresa = $(control).closest('tr').find('label5').html();

    $("#NoCuenta").val(opNoCuenta);
    $("#Paciente").val(opPaciente);
    $("#hfNoCuenta").val(opNoCuenta);
    $("#hfNoHistoria").val(opNoHistoria);
    $("#hfEmpCta").val(opEmpresa);

    GetValidaPaciente(opNoCuenta, opNoIdentificacion);

}

function detalleClick(control, NoCaso) {
    var opConsInterno = $(control).closest('tr').find('label2').html();
    var opResponsable = $(control).closest('tr').find('label1').html();
    var opNitEntidad = $(control).closest('tr').find('label0').html();
    var opCodEps = $(control).closest('tr').find('label4').html();
    var opEmpresa = $("#hfEmpCta").val();

    $("#hfResponsable").val(opResponsable);
    $("#hfNitEntidad").val(opNitEntidad);
    $("#hfCodEPS").val(opCodEps);


    GetDetalleExtracto(opConsInterno, NoCaso, opEmpresa);

}

function detMedicamento(control, tipo) { // control 

    $("#gvMedicamento").empty();
    $("#gvDatosPaciente").empty();

    if (tipo === 2) {
        var opNoPrescripcion = $(control).closest('tr').find('label0').html();
        var opNoIdentificacion = $(control).closest('tr').find('label4').html();
        var opTipoIdentificacion = $(control).closest('tr').find('label3').html();
    }
    else {
        var opNoPrescripcion = control;
    }

    var proceso = new Object();
    var temp = "<table class='table table-hover table-striped table-bordered'><tr><th>Proceso</th><th>Orden No</th><th>Tipo</th><th>Medicamento</th><th>Indicaciones</th><th>Cantidad</th><th></th></tr>";

    for (var i = 0; i < arrayMedicamentos.length; i++) {
        var NoDocumento = arrayMedicamentos[i]["NoPrescripcion"];
        if (opNoPrescripcion === NoDocumento) {
            temp += "<tr>";
            temp += "<td><label0>" + arrayMedicamentos[i]["opcion"] + "</label0></td>";
            temp += "<td><label1>" + arrayMedicamentos[i]["ConOrden"] + "</label1></td>";
            temp += "<td><label2>" + arrayMedicamentos[i]["TipoPrest"] + "</label2></td>";
            temp += "<td><label3>" + arrayMedicamentos[i]["DescServ"] + "</label3></td>";
            temp += "<td><label4>" + arrayMedicamentos[i]["IndRec"] + "</label4></td>";
            temp += "<td><label5>" + arrayMedicamentos[i]["CantTotalF"] + "</label5></td>";
            //temp += "<td style='text-align: center'><button class='btn btn-success btn-sm btn-circle'><span class='glyphicon glyphicon-ok-sign' aria-hidden='true'></span></button></td>";
            temp += "</tr>";
        }
    }

    for (var i = 0; i < arrayProductosNutricionales.length; i++) {
        var NoDocumento = arrayProductosNutricionales[i]["NoPrescripcion"];
        if (opNoPrescripcion === NoDocumento) {
            temp += "<tr>";
            temp += "<td><label0>" + arrayProductosNutricionales[i]["opcion"] + "</label0></td>";
            temp += "<td><label1>" + arrayProductosNutricionales[i]["ConOrden"] + "</label1></td>";
            temp += "<td><label2>" + arrayProductosNutricionales[i]["TipoPrest"] + "</label2></td>";
            temp += "<td><label3>" + arrayProductosNutricionales[i]["DescServ"] + "</label3></td>";
            temp += "<td><label4>" + arrayProductosNutricionales[i]["IndRec"] + "</label4></td>";
            temp += "<td><label5>" + arrayProductosNutricionales[i]["CantTotalF"] + "</label5></td>";
            //temp += "<td style='text-align: center'><button class='btn btn-success btn-sm btn-circle'><span class='glyphicon glyphicon-ok-sign' aria-hidden='true'></span></button></td>";
            temp += "</tr>";
        }
    }

    for (var i = 0; i < arrayserviciosComplementarios.length; i++) {
        var NoDocumento = arrayserviciosComplementarios[i]["NoPrescripcion"];
        if (opNoPrescripcion === NoDocumento) {
            temp += "<tr>";
            temp += "<td><label0>" + arrayserviciosComplementarios[i]["opcion"] + "</label0></td>";
            temp += "<td><label1>" + arrayserviciosComplementarios[i]["ConOrden"] + "</label1></td>";
            temp += "<td><label2>" + arrayserviciosComplementarios[i]["TipoPrest"] + "</label2></td>";
            temp += "<td><label3>" + arrayserviciosComplementarios[i]["DescServ"] + "</label3></td>";
            temp += "<td><label4>" + arrayserviciosComplementarios[i]["IndRec"] + "</label4></td>";
            temp += "<td><label5>" + arrayserviciosComplementarios[i]["CantTotalF"] + "</label5></td>";
            //temp += "<td style='text-align: center'><button class='btn btn-success btn-sm btn-circle'><span class='glyphicon glyphicon-ok-sign' aria-hidden='true'></span></button></td>";
            temp += "</tr>";
        }
    }

    for (var i = 0; i < arrayProcedimientos.length; i++) {
        var NoDocumento = arrayProcedimientos[i]["NoPrescripcion"];
        if (opNoPrescripcion === NoDocumento) {
            temp += "<tr>";
            temp += "<td><label0>" + arrayProcedimientos[i]["opcion"] + "</label0></td>";
            temp += "<td><label1>" + arrayProcedimientos[i]["ConOrden"] + "</label1></td>";
            temp += "<td><label2>" + arrayProcedimientos[i]["TipoPrest"] + "</label2></td>";
            temp += "<td><label3>" + arrayProcedimientos[i]["DescServ"] + "</label3></td>";
            temp += "<td><label4>" + arrayProcedimientos[i]["IndRec"] + "</label4></td>";
            temp += "<td><label5>" + arrayProcedimientos[i]["CantTotalF"] + "</label5></td>";
            //temp += "<td style='text-align: center'><button class='btn btn-success btn-sm btn-circle'><span class='glyphicon glyphicon-ok-sign' aria-hidden='true'></span></button></td>";
            temp += "</tr>";
        }
    }

    for (var i = 0; i < arraydispositivos.length; i++) {
        var NoDocumento = arraydispositivos[i]["NoPrescripcion"];
        if (opNoPrescripcion === NoDocumento) {
            temp += "<tr>";
            temp += "<td><label0>" + arraydispositivos[i]["opcion"] + "</label0></td>";
            temp += "<td><label1>" + arraydispositivos[i]["ConOrden"] + "</label1></td>";
            temp += "<td><label2>" + arraydispositivos[i]["TipoPrest"] + "</label2></td>";
            temp += "<td><label3>" + arraydispositivos[i]["DescServ"] + "</label3></td>";
            temp += "<td><label4>" + arraydispositivos[i]["IndRec"] + "</label4></td>";
            temp += "<td><label5>" + arraydispositivos[i]["CantTotalF"] + "</label5></td>";
            //temp += "<td style='text-align: center'><button class='btn btn-success btn-sm btn-circle'><span class='glyphicon glyphicon-ok-sign' aria-hidden='true'></span></button></td>";
            temp += "</tr>";
        }
    }

    temp += "</table>";
    $("#gvMedicamento").append(temp);

    if (tipo == 2) {
        GetPacienteIngreso(opNoIdentificacion);
    }

}

function GetPrescripciones() {
    var FechaIni = $("#FechaIni").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfToken").val();
    var UrlPrescripcion = $("#hfUrlPrescripciones").val();
    var date_format = new Date(FechaIni).toDateString("yyyy-MM-dd");

    var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "fecha": FechaIni, "token": Token, "tipo": "P" };
    var NoPrescripcion = "";
    var NoIdentificacion = "";

    var UriRuta = "GetPrescripciones";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            //$("#btnLoader").click();
            $("#myModal").modal('show');
            //$("#loader").show();
        },
        success: function (data) {
            //$("#loader").hide();
            $("#myModal").modal('hide');
            //alert(data);
            var res = JSON.parse(data);
            var Tipo = '2';
            var temp = "<table id='RelLib' class='table table-hover table-striped table-bordered'><tr><th>NoPrescripcion</th><th>Fecha</th><th>Hora</th><th>TipoId</th><th>NoIdentificacion</th><th>1er Nombre</th><th>2do Nombre</th><th>1er Apellido</th><th>2do Apellido</th><th>Est. Prescrip</th><th></th></tr>";
            if (res === 0) {
                // Valores en nulo o 0
                temp += "<tr> No se encontraron datos...";
                temp += "</tr>";
            }
            else {
                // Traer datos de la prescripcion 
                for (let i in res) {
                    for (let j in res[i]) {
                        var proceso = j.toString();

                        // Encabezado de la Prescripcion
                        if (proceso == "prescripcion") {
                            var numero = res[i][j].NoPrescripcion;
                            NoIdentificacion = res[i][j].NroIDPaciente;
                            NoPrescripcion = res[i][j].NoPrescripcion;

                            //Valida Entrega
                            var opCUM = "";
                            var valentrega = new Boolean(false);
                            valentrega = ValidaEntrega(NoPrescripcion, opCUM, "1");
                            if (valentrega) {
                                temp += "<tr bgcolor='#A9F5A9'>";
                            }
                            else {
                                temp += "<tr>";
                            }

                            temp += "<td><label0>" + res[i][j].NoPrescripcion + "</label0></td>";
                            temp += "<td><label1>" + res[i][j].FPrescripcion.replace(/"\/Date\((\d+)\)\/"/g, 'new Date($1)') + "</label1></td>";
                            temp += "<td><label2>" + res[i][j].HPrescripcion + "</label2></td>";
                            temp += "<td><label3>" + res[i][j].TipoIDPaciente + "</label3></td>";
                            temp += "<td><label4>" + res[i][j].NroIDPaciente + "</label4></td>";
                            temp += "<td><label5>" + res[i][j].PNPaciente + "</label5></td>";
                            temp += "<td><label6>" + res[i][j].SNPaciente + "</label6></td>";
                            temp += "<td><label7>" + res[i][j].PAPaciente + "</label7></td>";
                            temp += "<td><label8>" + res[i][j].SAPaciente + "</label8></td>";
                            temp += "<td><label9>" + res[i][j].EstPres + "</label9></td>";
                            temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return detMedicamento(this," + Tipo + ")'><img src='../../Content/images/true.png'/></button></td>";
                            temp += "</tr>";
                            console.log(numero);
                        }
                        //- Medicamentos
                        if (proceso == "medicamentos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayMedicamentos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- productosnutricionales
                        if (proceso == "productosnutricionales") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescProdNutr = res[i][j][n].DescProdNutr;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayProductosNutricionales.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescProdNutr,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- procedimientos
                        if (proceso == "procedimientos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayProcedimientos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- dispositivos
                        if (proceso == "dispositivos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arraydispositivos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- serviciosComplementarios
                        if (proceso == "serviciosComplementarios") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescSerComp = res[i][j][n].DescSerComp;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotal;

                                arrayserviciosComplementarios.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescSerComp,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                    }
                }
            }

            temp += "</table>";
            $("#gvPrescripciones").append(temp);
            //$("#gvPrescripciones").html(temp);

            //$("#RelLib").DataTable();

        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });

}

function GetNoPrescripcion() {
    var NoPrescripcion = $("#NoPrescripcion").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfToken").val();
    var UrlPrescripcion = $("#hfUrlUnaPrescripcion").val();
    $("#hfNoPrescripcion").val(NoPrescripcion);

    var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "numero": NoPrescripcion, "token": Token };
    var NoIdentificacion = "";

    var UriRuta = "GetUnaPrescripcion";
    $.ajax({
        type: 'POST',
        url: "GetUnaPrescripcion",
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            //$("#loader").show();
            progress();
        },
        success: function (data) {
            waitingDialog.hide();
            //$("#loader").hide();
            //alert(data);
            var res = JSON.parse(data);
            var Tipo = '1';
            var temp = "<table id='RelLib' class='table table-hover table-striped table-bordered'><tr><th>NoPrescripcion</th><th>Fecha</th><th>Hora</th><th>TipoId</th><th>NoIdentificacion</th><th>1er Nombre</th><th>2do Nombre</th><th>1er Apellido</th><th>2do Apellido</th><th>Est. Prescrip</th></tr>";
            if (res === 0) {
                // Valores en nulo o 0
                temp += "<tr> No se encontraron datos...";
                temp += "</tr>";
                //alert('Error web Service:' + res);

            }
            else {
                // Traer datos de la prescripcion 
                for (let i in res) {
                    for (let j in res[i]) {
                        var proceso = j.toString();

                        // Encabezado de la Prescripcion
                        if (proceso == "prescripcion") {
                            var numero = res[i][j].NoPrescripcion;
                            NoIdentificacion = res[i][j].NroIDPaciente;
                            NoPrescripcion = res[i][j].NoPrescripcion;

                            temp += "<tr onclick='detMedicamento(this)'>";
                            temp += "<td><label0>" + res[i][j].NoPrescripcion + "</label0></td>";
                            temp += "<td><label1>" + res[i][j].FPrescripcion.replace(/"\/Date\((\d+)\)\/"/g, 'new Date($1)') + "</label1></td>";
                            temp += "<td><label2>" + res[i][j].HPrescripcion + "</label2></td>";
                            temp += "<td><label3>" + res[i][j].TipoIDPaciente + "</label3></td>";
                            temp += "<td><label4>" + res[i][j].NroIDPaciente + "</label4></td>";
                            temp += "<td><label5>" + res[i][j].PNPaciente + "</label5></td>";
                            temp += "<td><label6>" + res[i][j].SNPaciente + "</label6></td>";
                            temp += "<td><label7>" + res[i][j].PAPaciente + "</label7></td>";
                            temp += "<td><label8>" + res[i][j].SAPaciente + "</label8></td>";
                            temp += "<td><label9>" + res[i][j].EstPres + "</label9></td>";
                            temp += "</tr>";

                            var opTipoIdentificacion = res[i][j].TipoIDPaciente.toString();
                            var opNoIdentificacion = res[i][j].NroIDPaciente.toString();

                            $("#hfTipoIdentificacion").val(opTipoIdentificacion);
                            $("#hfNoIdentificacion").val(opNoIdentificacion);

                            console.log(numero);
                        }

                        //- Medicamentos
                        if (proceso == "medicamentos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayMedicamentos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- productosnutricionales
                        if (proceso == "productosnutricionales") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescProdNutr = res[i][j][n].DescProdNutr;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayProductosNutricionales.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescProdNutr,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- procedimientos
                        if (proceso == "procedimientos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arrayProcedimientos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- dispositivos
                        if (proceso == "dispositivos") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescMedPrinAct = res[i][j][n].DescMedPrinAct;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotalF;

                                arraydispositivos.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescMedPrinAct,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                        //- serviciosComplementarios
                        if (proceso == "serviciosComplementarios") {
                            for (let n in res[i][j]) {

                                var ConOrden = res[i][j][n].ConOrden;
                                var TipoPrest = res[i][j][n].TipoPrest;
                                var DescSerComp = res[i][j][n].DescSerComp;
                                var IndRec = res[i][j][n].IndRec;
                                var CantTotalF = res[i][j][n].CantTotal;

                                arrayserviciosComplementarios.push({
                                    opcion: proceso,
                                    NoPrescripcion: NoPrescripcion,
                                    NoIdentificacion: NoIdentificacion,
                                    ConOrden: ConOrden,
                                    TipoPrest: TipoPrest,
                                    DescServ: DescSerComp,
                                    IndRec: IndRec,
                                    CantTotalF: CantTotalF
                                });
                            }
                        }
                    }
                }
            }

            temp += "</table>";
            $("#gvPrescripciones").append(temp);

            detMedicamento(NoPrescripcion, Tipo);
            GetPacienteIngreso(NoIdentificacion);
            //$("#gvPrescripciones").html(temp);

            //$("#RelLib").DataTable();

        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });

}

function GetPacienteIngreso(NoIdentificacion) {
    var UriRuta = 'GetPacienteIngreso'; //"LovPacientes"; //
    var parametros = { 'NoHistoria': NoIdentificacion }
    //alert('Entre al Lov');
    try {
        $.ajax({
            type: "POST", //GET
            url: "GetPacienteIngreso",
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { "NoHistoria": NoIdentificacion }, //JSON.stringify(parametros), //,
            beforeSend: function () {
                // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
                $("#loader").show();
            },
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                var temp = "<table class='table table-hover table-striped table-bordered'><tr><th>No Caso</th><th>No Identificacion</th><th>Paciente</th><th>Fecha Ing</th><th>Historia</th><th>Empresa</th><th></th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.NoCuenta + "</label0></td>";
                    temp += "<td><label1>" + this.Identificacion + "</label1></td>";
                    temp += "<td><label2>" + this.Paciente + "</label2></td>";
                    temp += "<td><label3>" + this.FechaIngreso + "</label3></td>";
                    temp += "<td><label4>" + this.Identificacion + "</label4></td>";
                    temp += "<td><label5>" + this.Empresa + "</label5></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return funcionClick(this)'><img src='../../Content/images/true.png'/></button></td>";
                    temp += "</tr>";
                })

                temp += "</table>";
                $("#gvDatosPaciente").append(temp);

            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }
}

function GetValidaPaciente(NoCuenta, NoIdentificacion) {
    var UriRuta = 'GetPacienteDistrito'; //"LovPacientes"; //
    var Dpto = "";
    var Mnpo = "";
    //var parametros = { criterio: criterio1 }
    //alert('Entre al Lov');
    try {
        $.ajax({
            type: "POST", //GET
            url: UriRuta,
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { NoHistoria: NoIdentificacion },
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                $.each(res, function (i, item) {
                    Dpto = item["Departamento"];
                    Mnpo = item["Municipio"];
                });

                //var DistMipres = GetDistrito(Dpto, Mnpo);
                //var mipresDpto = DistMipres[0];
                //var mipresMnpo = DistMipres[1];

                //if (Dpto === mipresDpto && Mnpo === mipresMnpo) {
                //    alert("El Paciente pertenece al Distrito, Dirigirse a la plataforma del Distrital");
                //    refresh();
                //    return;
                //}

                var EmpCta = $("#hfEmpCta").val();
                GetEgresosCaso(NoCuenta, EmpCta);
                GetExtractoCasos(NoCuenta, EmpCta);
                $('#modMedicamentos').modal('show');
            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }

}

function GetDistrito(Depto, Munpo) {
    var UriRuta = 'GetDistritoMipres'; //"LovPacientes"; //
    var CDepto = "";
    var CMunpo = "";

    //var parametros = { criterio: criterio1 }
    //alert('Entre al Lov');
    try {
        $.ajax({
            type: "POST", //GET
            url: UriRuta,
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { Departamento: Depto, Municipio: Munpo },
            success: function (data) {
                var res = JSON.parse(data);
                if (res !== null) {
                    $.each(res, function (i, item) {
                        CDepto = item["Cod_Depto"];
                        CMunpo = item["Cod_Munpo"];
                    });
                }
                return [CDepto, CMunpo];
            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }
}

function GetEgresosCaso(NoCaso, EmpCta) {
    var UriRuta = "GetEgresosCasos";
    $.ajax({
        type: "POST", //GET
        url: "GetEgresosCasos",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta:NoCaso, EmpCaso:EmpCta }, //JSON.stringify(parametros),
        success: function (data) {

            var msg = JSON.parse(data);
            console.log(data);

            if (msg !== "") {
                // Lista de Campos

                var LstCampos = "NoCuenta(T(L)(N)(G)(N)[5][15],";
                LstCampos += "NoAdmision(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "Servicio(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "fechaegreso(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "horaEgreso(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "estado(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "estadoIng(T)(L)(N)(G)(N)[10][15]";
                //LstCampos += "fechaIngreso(T)(L)(N)(G)(N)[5][15],";
                //LstCampos += "horaingreso(T)(L)(N)(G)(N)[10][15],";
                //LstCampos += "EstAdm(T)(L)(N)(G)(N)[10][15],";
                //LstCampos += "servTras(T)(L)(N)(G)(N)[10][15],";
                //LstCampos += "no_Autorizacion(T)(L)(N)(G)(N)[10][15],";
                //LstCampos += "autorizado_Por(T)(L)(N)(G)(N)[10][15]";

                DatatableHide('gvEgresosCasos', LstCampos, data, true, true, false, false);

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });

}

function GetExtractoCasos(NoCaso, EmpCta) {
    var UriRuta = "GetExtractoCasos";
    $("#gvResponsables").empty();

    $.ajax({
        type: "POST", //GET
        url: "GetExtractoCasos",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta:NoCaso, EmpCaso:EmpCta }, //JSON.stringify(parametros),
        success: function (data) {

            var res = JSON.parse(data);
            console.log(data);
            if (res !== "") {
                // Lista de Campos
                //f.nitentidad, e.nombre_entidad, f.ConsInterno, f.CodConvenio, cv.CodEsquemaTar, cv.TipoTopeSoat, cv.soat, cv.CopagoPorNivel

                var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Nit</th><th>Responsable</th><th>Cons.Interno</th><th>Convenio</th><th>CodEPS</th><th>Esquema</th><th>Tipo SOAT</th><th>SOAT</th><th>Vlr.Factura</th><th>Descuento</th><th></th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.NitEntidad + "</label0></td>";
                    temp += "<td><label1>" + this.Nombre_Entidad + "</label1></td>";
                    temp += "<td><label2>" + this.ConsInterno + "</label2></td>";
                    temp += "<td><label3>" + this.CodConvenio + "</label3></td>";
                    temp += "<td><label4>" + this.Cod_Ent_Admin + "</label4></td>";
                    temp += "<td><label5>" + this.CodEsquemaTar + "</label5></td>";
                    temp += "<td><label6>" + this.TipoTopeSoat + "</label6></td>";
                    temp += "<td><label7>" + this.soat + "</label7></td>";
                    temp += "<td><label8>" + this.ValorFactura.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "</label8></td>";
                    temp += "<td><label9>" + this.Valor_Descuento.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "</label9></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return detalleClick(this, " + NoCaso + ")'><img src='../../Content/images/true.png'/></button></td>";
                    temp += "</tr>";
                })

                temp += "</table>";
                $("#gvResponsables").append(temp);

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });

}

function GetDetalleExtracto(opConsInterno, NoCaso, EmpCta) {

    var UriRuta = "GetDetalleExtractoMIPRES";
    $("#gvDetalleExtracto").empty();

    $.ajax({
        type: "POST", //GET
        url: "GetDetalleExtractoMIPRES",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta: NoCaso, ConsInterno: opConsInterno, EmpCaso:EmpCta }, //JSON.stringify(parametros),
        success: function (data) {

            var res = JSON.parse(data);
            console.log(data);
            //f.nitentidad, e.nombre_entidad, f.ConsInterno, f.CodConvenio,cv.CodEsquemaTar, cv.TipoTopeSoat, cv.soat, cv.CopagoPorNivel
            if (res !== "") {

                var Nombre = "";
                var Nombreant = "";
                var cantidad = 0;
                var precio = 0;
                var total = 0;
                var Codcum = "";
                var CodCumant = "";
                var Codart = "";
                var cod_ant = "";
                var fechaArt = "";
                var fecMed = "";
                var swF1 = 0;
                var tot_cant = 0;
                var tot_art = 0;
                var ultreg = 0;

                var jSONholderArr = [];
                var temp = "<div class='table-responsive'>"
                temp += "<table id='tabExtracto' class='table table-hover table-striped table-bordered table-condensed table-fixed'><tbody><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Fecha</th><th>Codigo</th><th>Medicamento</th><th>Cantidad</th><th>Precio</th><th>Total</th><th>CUM</th><th></th></tr></tbody>";
                $.each(res, function (i, item) {
                    if (swF1 === 0) {
                        //fechaArt = item["fecha_cargo"];
                        cod_ant = item["CodigoArt"];
                        Nombreant = item["NombreArticulo"];
                        CodCumant = item["CodigoCUM"];
                        fecMed = item["fecha_cargo"];

                        var pattern = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
                        //var arrayDate = fecMed.match(pattern);
                        var arrayDate = fecMed.split(/[-/]/);
                        var dd = arrayDate[0].toString(); //1
                        var mm = arrayDate[1].toString(); //2
                        var aa = arrayDate[2].toString(); //2
                        aa = aa.substring(0, 4);
                        var mes = mm.toString();
                        if (dd.length < 2) {
                            dd = "0" + dd;
                        }
                        if (mes.length < 2) {
                            mes = "0" + mes;
                        }

                        //fechaArt = arrayDate[2].toString() + "-" + mes.toString() + "-" + dd.toString(); //3
                        fechaArt = aa + "-" + mes + "-" + dd; //3
                        $("#FecEntrega").val(fechaArt);
                        //fechaArt = arrayDate[3].toString() + "-" + (arrayDate[2].toString() - 1) + "-" + arrayDate[1].toString();

                        swF1 = 1;
                    }

                    Codart = item["CodigoArt"];
                    Nombre = item["NombreArticulo"];
                    cantidad = item["Cantidad"];
                    precio = item["precio"];
                    total = item["total"];
                    Codcum = item["CodigoCUM"];

                    if (cod_ant === Codart) {
                        tot_cant += parseInt(this.cantidad);
                        tot_art += (parseInt(this.cantidad) * parseInt(this.precio));
                    }
                    else {
                        jSONholderArr.push(new Array(fechaArt, cod_ant, Nombreant, tot_cant.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), precio.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), tot_art.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), CodCumant));
                        swF1 = 1;
                        cod_ant = item["CodigoArt"];
                        fecMed = item["fecha_cargo"];
                        Nombreant = item["NombreArticulo"];
                        CodCumant = item["CodigoCUM"];
                        var pattern = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
                        var arrayDate = fecMed.split(/[-/]/);
                        //var arrayDate = fecMed.match(pattern);
                        var dd = arrayDate[0].toString();
                        var mm = arrayDate[1].toString();
                        var aa = arrayDate[2].toString(); //2
                        aa = aa.substring(0, 4);
                        var mes = mm.toString();
                        if (dd.length < 2) {
                            dd = "0" + dd;
                        }
                        if (mes.length < 2) {
                            mes = "0" + mes;
                        }

                        fechaArt = aa + "-" + mes + "-" + dd; //3
                        //fechaArt = arrayDate[3].toString() + "-" + mes.toString() + "-" + dd.toString();
                        $("#FecEntrega").val(fechaArt);

                        tot_cant = parseInt(this.cantidad);
                        tot_art = (parseInt(this.cantidad) * parseInt(this.precio));
                    }

                    var ultreg = (res.length - 1);
                    if (ultreg == i) {
                        //alert('ultimo:' + Codart);
                        jSONholderArr.push(new Array(fechaArt, cod_ant, Nombreant, tot_cant.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), precio.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), tot_art.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"), CodCumant));
                    }

                })

                var col = 0;
                for (var prop in jSONholderArr) {

                    temp += "<tr>";
                    temp += "<td><label0>" + jSONholderArr[prop][col] + "</label0></td>"; //Fecha_cargo
                    col++;
                    temp += "<td><label1>" + jSONholderArr[prop][col] + "</label1></td>"; // Codigo serv
                    col++;
                    temp += "<td><label2>" + jSONholderArr[prop][col] + "</label2></td>"; // Nombre serv
                    col++;
                    temp += "<td><label3>" + jSONholderArr[prop][col] + "</label3></td>"; // Cantidad
                    col++;
                    temp += "<td><label4>" + jSONholderArr[prop][col] + "</label4></td>"; // Precio
                    col++;
                    temp += "<td><label5>" + jSONholderArr[prop][col] + "</label5></td>"; // Total 
                    col++;
                    temp += "<td><label6>" + jSONholderArr[prop][col] + "</label6></td>"; // Codigo CUM
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return codArtMIPRES(this)'><img src='../../Content/images/check11.png'/></button></td>";
                    //                    temp += "<td style='text-align: center'><input type='image' name='btnMIPRES' id='btnMIPRES' src='../../Content/images/check11.png' onclick='codArtMIPRES(this)'></td>";
                    temp += "</tr>";
                    col = 0;

                }

                temp += "</table></div>";

                $("#gvDetalleExtracto").append(temp);
                //$('#gvDetalleExtracto').DataTable({
                //    "scrollY": "200px",
                //    "scrollCollapse": true,
                //});

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });

}

function codArtMIPRES(control) {
    var opFecha = $(control).closest('tr').find('label0').html();
    var opCodigo = $(control).closest('tr').find('label1').html();
    var opNombre = $(control).closest('tr').find('label2').html();
    var opcantidad = $(control).closest('tr').find('label3').html();
    var opPrecio = $(control).closest('tr').find('label4').html();
    var opTotal = $(control).closest('tr').find('label5').html();
    var opCUM = ($(control).closest('tr').find('label6').html() != null) ? $(control).closest('tr').find('label6').html() : "";

    var opNoPrescripcion = $("#hfNoPrescripcion").val();
    var opTipoID = $("#hfTipoIdentificacion").val();
    var opNoIdentificacion = $("#hfNoIdentificacion").val();

    var TipoTec = $("#hfTipoTecnologia").val();
    var ConseTec = 1;
    var NoEntrega = 1;
    var CausaNoEntrega = 0;
    var NoLote = "";
    var EntTotal = 1;

    var tipovalida = "2";
    var valentrega = new Boolean(false);
    valentrega = ValidaEntrega(opNoPrescripcion, opCUM, tipovalida);
    if (valentrega) {
        alert('Prescripcion No.:' + opNoPrescripcion + ' Con el Codigo CUM:' + opCUM + 'Tiene Entrega');
        return;
    }

    $("#NoPrescripcionMIPRES").val(opNoPrescripcion);
    $("#TipoIDPaciente").val(opTipoID);
    $("#NoIDPaciente").val(opNoIdentificacion);
    $("#FecEntrega").val(opFecha);
    $("#CodSerTecEntregado").val(opCUM);
    $("#TipoTec").val(TipoTec);
    $("#ConTec").val(ConseTec);
    $("#NoEntrega").val(NoEntrega);
    $("#CantTotEntregada").val(opcantidad);
    $("#EntTotal").val(EntTotal);
    $("#CausaNoEntrega").val(CausaNoEntrega);
    $("#NoLote").val(NoLote);
    $("#hfVlrEntrega").val(opTotal);
    $("#hfCantidad").val(opcantidad);
    $("#hfPrecioUni").val(opPrecio);



    $("#modalMIPRES").modal('show');

}

function ValidaEntrega(NoPrescripcion, CodigoCum, tipo) {
    var UriRuta = 'GetValidaEntrega'; //"LovPacientes"; //
    if (CodigoCum == null) {
        CodigoCum = "";
    }
    try {
        $.ajax({
            type: "POST", //GET
            url: UriRuta,
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { "NoPrescripcion": NoPrescripcion, "CodigoCum": CodigoCum, "tipo": tipo },
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                $.each(res, function (i, item) {
                    var valNo = item["NoPrescripcion"];
                    if (valNo == "" || valNo == null || valNo == undefined) {
                        return false;
                    }
                    else {
                        return true;
                    }

                });


            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    catch (err) {
        console.log(err.Message);
        alert(err.Message);
    }


}

function ListEntregasFechas() {
    var FechaIni = $("#FechaIni").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var UrlReportesFechas = $("#hfUrlEntregasFechas").val();
    var date_format = new Date(FechaIni).toDateString("yyyy-MM-dd");

    var parametros = { "Uri": UrlReportesFechas, "nit": Nit, "fecha": FechaIni, "token": Token, "tipo": "E" };
    var NoId = "";
    var NoIdReporte = "";

    var UriRuta = "GetPrescripciones";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            $("#myModal").modal('show');
        },
        success: function (data) {
            $("#myModal").modal('hide');
            var res = JSON.parse(data);
            console.log(data);

            if (res !== "") {
                // Lista de Campos
                $.each(res, function (i, item) {
                    var Id = item["ID"];

                    if (Id === undefined) {
                        alert('Token:' + Token + ' ' + data);
                        return
                    }

                });

                var LstCampos = "ID(N)(N)(N)(G)(N)[5][15],";
                LstCampos += "IDEntrega(N)(L)(N)(G)(N)[5][15],";
                LstCampos += "NoPrescripcion(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "TipoIDPaciente(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "NoIDPaciente(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "FecEntrega(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "NoEntrega(N)(L)(N)(G)(N)[5][10],";
                LstCampos += "TipoTec(T)(L)(N)(G)(N)[5][10],";
                LstCampos += "ConTec(N)(L)(N)(G)(N)[5][10],";
                LstCampos += "CodSerTecEntregado(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "CantTotEntregada(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "EstEntrega(T)(L)(N)(G)(N)[5][10]";

                DatatableHide('gvPrescripciones', LstCampos, data, true, true, false, false);

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function GetEntregaPrescripcion() {
    var NoPrescripcion = $("#NoPrescripcion").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var UrlPrescripcion = $("#hfUrlEntregaPrescripcion").val();
    $("#hfNoPrescripcion").val(NoPrescripcion);

    var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "numero": NoPrescripcion, "token": Token };
    var NoIdentificacion = "";

    var UriRuta = "GetUnaPrescripcion";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            $("#loader").show();
        },
        success: function (data) {
            $("#loader").hide();
            //alert(data);
            var res = JSON.parse(data);
            var Tipo = '1';
            var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Anular</th></tr>";
            if (res === "") {
                // Valores en nulo o 0
                temp += "<tr> No se encontraron datos...";
                temp += "</tr>";

            }
            else {

                var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Anular</th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.ID + "</label0></td>";
                    temp += "<td><label1>" + this.IDEntrega + "</label1></td>";
                    temp += "<td><label2>" + this.NoPrescripcion + "</label2></td>";
                    temp += "<td><label3>" + this.TipoIDPaciente + "</label3></td>";
                    temp += "<td><label4>" + this.NoIDPaciente + "</label4></td>";
                    temp += "<td><label5>" + this.FecEntrega + "</label5></td>";
                    temp += "<td><label6>" + this.NoEntrega + "</label6></td>";
                    temp += "<td><label7>" + this.TipoTec + "</label7></td>";
                    temp += "<td><label8>" + this.CodSerTecEntregado + "</label8></td>";
                    temp += "<td><label9>" + this.CantTotEntregada + "</label9></td>";
                    temp += "<td><label10>" + this.EstEntrega + "</label10></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return anularClick(this, " + this.IDEntrega + ")'><img src='../../Content/images/Delete1.ico'/></button></td>";
                    temp += "</tr>";
                })

                temp += "</table>";
                $("#gvPrescripciones").append(temp);

            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });

}

function GetEnvioMIPRES() {
    var opNoPrescripcion = $("#NoPrescripcionMIPRES").val();
    var opTipoID = $("#TipoIDPaciente").val();
    var opNoIdentificacion = $("#NoIDPaciente").val();
    var opFecha = $("#FecEntrega").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var TipoTec = $("#TipoTec").val();
    var ConseTec = $("#ConTec").val();
    var NoEntrega = $("#NoEntrega").val();
    var opcantidad = $("#CantTotEntregada").val();
    var opTotal = $("#EntTotal").val();
    var CausaNoEntrega = $("#CausaNoEntrega").val();
    var NoLote = $("#NoLote").val();
    var opVlrEntrega = $("#hfVlrEntrega").val();

    if (opCUM.length === 0) {
        alert('El Codigo CUM no esta digitado...');
        return;
    }
    if (TipoTec === null || TipoTec === "") {
        alert('El Tipo Tecnologia no esta digitado...');
        return;
    }
    if (parseInt(ConseTec) === 0) {
        alert('El Consecutivo Tecnologia es Cero(0)...');
        return;
    }

    var UrlEntregaAmbito = $("#hfUrlEntregaAmbito").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();

    var BodyPut = "{";
    BodyPut += "'NoPrescripcion':'" + opNoPrescripcion + "',";
    BodyPut += "'TipoTec':'" + TipoTec + "',";
    BodyPut += "'ConTec':" + parseInt(ConseTec) + ",";
    BodyPut += "'TipoIDPaciente':'" + opTipoID + "',";
    BodyPut += "'NoIDPaciente':'" + opNoIdentificacion + "',";
    BodyPut += "'NoEntrega':" + parseInt(NoEntrega) + ",";
    BodyPut += "'CodSerTecEntregado':'" + opCUM + "',";
    BodyPut += "'CantTotEntregada':'" + opcantidad + "',";
    BodyPut += "'EntTotal':" + parseInt(opTotal) + ",";
    BodyPut += "'CausaNoEntrega':" + parseInt(CausaNoEntrega) + ",";
    BodyPut += "'FecEntrega':'" + opFecha + "',";
    BodyPut += "'NoLote':'" + NoLote + "'";
    BodyPut += "}";

    var parametros = { "Uri": UrlEntregaAmbito, "nit": Nit, "token": Token, "Datos": BodyPut };

    var UriRuta = "PutEntregaAmbito";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            var Id = 0;
            var IdEntrega = 0;
            $.each(res, function (i, item) {
                Id = item["Id"];
                IdEntrega = item["IdEntrega"];

                if (Id === undefined) {
                    alert(data);

                    return
                }

                $("#hfNoID").val(Id);
                $("#hfIDEntrega").val(IdEntrega);

                alert('ID:' + Id + ' - Id Entrega:' + IdEntrega);

            });
            if (Id != undefined) {
                GrabaEntrega();
                GrabaReporte();
                refresh();
            }
            //refresh();
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function GrabaEntrega() {
    var opNoPrescripcion = $("#NoPrescripcionMIPRES").val();
    var opNoIdentificacion = $("#NoIDPaciente").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var opCantidad = $("#hfCantidad").val();
    var opPrecio = $("#hfPrecioUni").val();
    var opVlrEntrega = $("#hfVlrEntrega").val();
    var opNoCuenta = $("#hfNoCuenta").val();
    var opResponsable = $("#hfResponsable").val();
    var opNitEntidad = $("#hfNitEntidad").val();
    var opCodEPS = $("#hfCodEPS").val();
    var opNoHistoria = $("#hfNoHistoria").val();


    var NoID = $("#hfNoID").val();
    var IDEntrega = $("#hfIDEntrega").val();

    var Valor = opVlrEntrega.replace(/,/g, "");

    $("#hfVlrEntrega").val(Valor);

    var parametros = { "NoPrescripcion": opNoPrescripcion, "NoCuenta": opNoCuenta, "NoHistoria": opNoHistoria, "NoIdentificacion": opNoIdentificacion, "NitEntidad": opNitEntidad, "Responsable": opResponsable, "CodEPS": opCodEPS, "CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "CantEnt": opCantidad, "PrecioUni": opPrecio, "ValEntrega": Valor };

    var UriRuta = "RegistrarEntrega";
    $.ajax({
        type: 'POST',
        url: "RegistrarEntrega",
        data: JSON.stringify(parametros), //{ "NoPrescripcion": opNoPrescripcion, "NoIdentificacion": opNoIdentificacion, "CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "ValEntrega": Valor },
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);

            alert('Registro de Entrega Grabado..');
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function GrabaReporte() {
    var NoId = $("#hfNoID").val();
    var opNoPrescripcion = $("#NoPrescripcionMIPRES").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var Estado = 1;
    var CausaNoEntresa = 0;
    var ValEntrega = $("#hfVlrEntrega").val();

    var BodyPut = "{";
    BodyPut += "'ID':" + parseInt(NoId) + ",";
    BodyPut += "'EstadoEntrega':" + Estado + ",";
    BodyPut += "'CausaNoEntrega':" + CausaNoEntresa + ",";
    BodyPut += "'ValorEntregado':'" + ValEntrega + "'";
    BodyPut += "}";

    var UriRuta = "PutEntregaAmbito";
    var UrlReporteEntrega = $("#hfUrlReporteEntrega").val();
    var parametros = { "Uri": UrlReporteEntrega, "nit": Nit, "token": Token, "Datos": BodyPut };

    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros), //{ "NoPrescripcion": opNoPrescripcion, "NoIdentificacion": opNoIdentificacion, "CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "ValEntrega": Valor },
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            var Id = 0;
            var IdReporteEntrega = 0;

            $.each(res, function (i, item) {
                Id = item["Id"];
                IdReporteEntrega = item["IdReporteEntrega"];

                if (Id === undefined) {
                    alert(data);

                    return
                }

                $("#hfNoIDReporte").val(Id);
                $("#hfIDReporteEntrega").val(IdReporteEntrega);

                alert('ID Reporte:' + Id + ' - Id Reporte Entrega:' + IdReporteEntrega);

            });
            if (Id != undefined) {
                var IdRepEntrega = $("#hfIDReporteEntrega").val();
                RegistroReporteEntrega(opNoPrescripcion, opCUM, IdRepEntrega);
            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function RegistroReporteEntrega(NoPrescripcion, CodigoCum, IdReporteEntrega) {
    var NoID = $("#hfNoIDReporte").val();
    var parametros = { "NoPrescripcion": NoPrescripcion, "CodigoCum": CodigoCum, "IdReporteEntrega": parseInt(IdReporteEntrega), "IdAnulacion": 0, "Estado": "A" };
    //$("#hfIDReporteEntrega").val(IdEntrega);

    var UriRuta = "ReporteEntrega";

    $.ajax({
        type: 'POST',
        url: "ReporteEntrega",
        data: JSON.stringify(parametros), //{ "NoPrescripcion": opNoPrescripcion, "NoIdentificacion": opNoIdentificacion, "CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "ValEntrega": Valor },
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            alert('Reporte de Entrega Grabado Exitosamente...');

        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });

}

function anularClick(control, IdNoEntrega) {
    var opId = $(control).closest('tr').find('label0').html();
    var opIdEntrega = $(control).closest('tr').find('label1').html();
    var opNoPrescripcion = $(control).closest('tr').find('label2').html();
    var opFecha = $(control).closest('tr').find('label5').html();
    var opCUM = $(control).closest('tr').find('label8').html();
    var NoEntrega = $(control).closest('tr').find('label6').html();

    $("#hfNoPrescripcion").val(opNoPrescripcion);
    $("#hfIDEntrega").val(opIdEntrega);

    $("#NoPrescripcionMIPRES").val(opNoPrescripcion);
    $("#FecEntrega").val(opFecha);
    $("#CodSerTecEntregado").val(opCUM);
    $("#NoEntrega").val(opIdEntrega);


    $("#modEntregas").modal('show');


}

function anulareporteClick(control, IdNoEntrega) {
    var opId = $(control).closest('tr').find('label0').html();
    var opNoPrescripcion = $(control).closest('tr').find('label2').html();
    var opFecha = $(control).closest('tr').find('label6').html();
    var opCUM = $(control).closest('tr').find('label8').html();
    var opValor = $(control).closest('tr').find('label10').html();

    $("#hfNoPrescripcion").val(opNoPrescripcion);
    $("#hfIDReporteEntrega").val(IdNoEntrega);

    $("#IDReporteEntrega").val(IdNoEntrega);
    $("#NoPrescripcionMIPRES").val(opNoPrescripcion);
    $("#FecEntrega").val(opFecha);
    $("#CodSerTecEntregado").val(opCUM);
    $("#ValorEntrega").val(opValor);

    $("#modEntregas").modal('show');

}


/// ** Facturacion
function facturacionClick(control, IdNoEntrega) {

    var opId = $(control).closest('tr').find('label0').html();
    var opNoPrescripcion = $(control).closest('tr').find('label2').html();
    var TipoTec = $(control).closest('tr').find('label7').html();
    var ConseTec = $(control).closest('tr').find('label8').html();;
    var opFecha = $(control).closest('tr').find('label5').html();
    var opCUM = $(control).closest('tr').find('label9').html();
    var opCantidad = $(control).closest('tr').find('label10').html();
    var TipoDocPaciente = $(control).closest('tr').find('label3').html();
    var IdPaciente = $(control).closest('tr').find('label4').html();
    var opIdEntrega = $(control).closest('tr').find('label1').html();


    $("#hfNoID").val(opId);
    $("#NoPrescripcionMIPRES").val(opNoPrescripcion);
    //$("#NoPrescripcion").val(opNoPrescripcion);
    $("#TipoTec").val(TipoTec);
    $("#ConTec").val(ConseTec);
    $("#TipoIDPaciente").val(TipoDocPaciente);
    $("#NoIDPaciente").val(IdPaciente);
    $("#NoEntrega").val(1);
    $("#CodSerTecEntregado").val(opCUM);
    $("#CantTotEntregada").val(opCantidad);
    $("#labelEPS").text("");
    $("#hfCantidad").val(opCantidad);
    $("#hfIDEntrega").val(opIdEntrega);

    GetEntregaMipres(opNoPrescripcion, opId);
    $("#panelFact").show();
    if (swfCuenta == "") {

        GetCuentaPaciente(IdPaciente);
        $("#divCaso").show();

       // GetTraerEPS();
    }
    else {
        GetTraerEPS();
    }

    //$("#modRegFacturas").modal('show');

}

function GetTraerEPS() {

    var UriRuta = '@Url.Action("MIPRESController","lovEPS")'; //"lovEPS"; //'@Url.Action("controller","action")'
    var numCuenta = $("#hfNoCuenta").val();
    if (numCuenta != "" || numCuenta.length > 1) {

        $.ajax({
            type: "POST", //GET
            url: "lovEPS", //UriRuta,
            content: "application/json; charset=utf-8",
            dataType: "json",
            // De la siguiente manera indicamos que del div tome los input.
            data: { NoCuenta: numCuenta }, //JSON.stringify(parametros),
            success: function (data) {

                var msg = data.d;
                console.log(data);

                if (msg !== "") {
                    //alert('Llegue al lovMed');
                    var res = JSON.parse(data);
                    $.each(res, function (i, item) {
                        $("#lstNoIDEPS").append('<option value="' + item["NitEntidad"] + '">' + item["NomEPS"] + '</option>');
                    });
                }
                else {
                    alert("Datos en Blanco");
                }
            },
            error: function (xhr, status, error) {
                alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            }
        });
    }
    else {
        alert("No hay Datos de Entidades de salud.. presione nuevamente el boton Factura");
        $("#panelFact").hide();
    }

}

function GetFacturasEPS(NitEps) {

    var UriRuta = "lovFactEPS"; //'@Url.Action("controller","action")'
    var numCuenta = $("#hfNoCuenta").val();
    $("#lstFactEPS").append('<option value="0">Seleccione Factura</option>');

    $.ajax({
        type: "POST", //GET
        url: "lovFactEPS",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta: numCuenta, NitEntidad: NitEps }, //JSON.stringify(parametros),
        success: function (data) {

            var msg = data.d;
            console.log(data);

            if (msg !== "") {
                //alert('Llegue al lovMed');
                var res = JSON.parse(data);
                $.each(res, function (i, medical) {
                    $("#lstFactEPS").append('<option value="' + medical["Nofactura"] + '">' + medical["Nofactura"] + '</option>');
                });
            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
}

function GetPrescripcionFact() {
    var NoPrescripcion = $("#NoPrescripcion").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var UrlPrescripcion = $("#hfUrlEntregaPrescripcion").val();
    $("#hfNoPrescripcion").val(NoPrescripcion);

    var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "numero": NoPrescripcion, "token": Token };
    var NoIdentificacion = "";

    var UriRuta = "GetUnaPrescripcion";
    $.ajax({
        type: 'POST',
        url: "GetUnaPrescripcion",
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            $("#loader").show();
        },
        success: function (data) {
            $("#loader").hide();
            //alert(data);
            var res = JSON.parse(data);
            var Tipo = '1';
            var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Factura</th></tr>";
            if (res === "") {
                // Valores en nulo o 0
                temp += "<tr> No se encontraron datos...";
                temp += "</tr>";

            }
            else {

                var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Cons Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Factura</th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.ID + "</label0></td>";
                    temp += "<td><label1>" + this.IDEntrega + "</label1></td>";
                    temp += "<td><label2>" + this.NoPrescripcion + "</label2></td>";
                    temp += "<td><label3>" + this.TipoIDPaciente + "</label3></td>";
                    temp += "<td><label4>" + this.NoIDPaciente + "</label4></td>";
                    temp += "<td><label5>" + this.FecEntrega + "</label5></td>";
                    temp += "<td><label6>" + this.NoEntrega + "</label6></td>";
                    temp += "<td><label7>" + this.TipoTec + "</label7></td>";
                    temp += "<td><label8>" + this.ConTec + "</label8></td>";
                    temp += "<td><label9>" + this.CodSerTecEntregado + "</label9></td>";
                    temp += "<td><label10>" + this.CantTotEntregada + "</label10></td>";
                    temp += "<td><label11>" + this.EstEntrega + "</label11></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return facturacionClick(this, " + this.IDEntrega + ")'><img src='../../Content/images/check11.png'/></button></td>";
                    temp += "</tr>";
                })

                temp += "</table>";
                $("#gvPrescripciones").append(temp);

            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });

}

function GetEntregaMipres(opNoPrescripcion, OpId) {
    var UriRuta = "GetEntregaMipres"; //'@Url.Action("controller","action")'
    var valEntrega = 0;
    var opVlrUni = 0;
    var swfDatos = 0;

    $.ajax({
        type: "POST", //GET
        url: "GetEntregaMipres",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoPrescripcion: opNoPrescripcion, IdEntrega: OpId }, //JSON.stringify(parametros),
        success: function (data) {


            var msg = data.d;
            console.log(data);

            if (msg !== "") {
                //alert('Llegue al lovMed');
                var res = JSON.parse(data);
                $.each(res, function (i, item) {
                    valEntrega = item["VlrEntrega"];
                    var opCantidad = $("#CantTotEntregada").val();
                    opVlrUni = parseInt(valEntrega) / parseInt(opCantidad);
                    var numCuenta = item["NoCuenta"];
                    swfDatos += 1;
                    swfCuenta = "1";
                    $("#ValorUnitFacturado").val(opVlrUni);
                    $("#ValorTotFacturado").val(valEntrega);
                    $("#hfNoCuenta").val(numCuenta);
                });
            }
            else {
                alert("Datos en Blanco");
                return;
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
            return;
        }
    });

    if (swfDatos == 0) //(valEntrega === 0 || valEntrega === null || valEntrega === "undefined") 
    {
        $("#ValorTotFacturado").val("0");
        $("#ValorUnitFacturado").val("0");
        swfCuenta = "";
        //var IdPaciente = $("#NoIDPaciente").val();
        //GetCuentaPaciente(IdPaciente)
    }

}

function GetCuentaPaciente(NoIdPaciente) {
    var UriRuta = "GetCuentaMIPRES"; //'@Url.Action("controller","action")'
    var numCuenta = "";
    $.ajax({
        type: "POST", //GET
        url: "GetCuentaMIPRES",
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { "NoIdentificacion": NoIdPaciente }, //JSON.stringify(parametros),
        success: function (data) {

            var msg = data.d;
            console.log(data);

            if (msg !== "") {
                //alert('Llegue al lovMed');
                var res = JSON.parse(data);
                $.each(res, function (i, item) {
                    $("#lstCasos").append('<option value="' + item["NoCuenta"] + '">' + item["NoCuenta"] + '</option>');
                });
            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });
}

function GetRegistrarFact() {
    var opNoPrescripcion = $("#NoPrescripcion").val();
    var opTipoID = $("#TipoIDPaciente").val();
    var opNoIdentificacion = $("#NoIDPaciente").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var TipoTec = $("#TipoTec").val();
    var ConseTec = $("#ConTec").val();
    var NoEntrega = $("#NoEntrega").val();
    var opFactura = $("#hfFactEPS").val();
    var opIdEPS = $("#hfNitEPS").val();
    var opCodEPS = $("#CodEPS").val();
    var opcantidad = $("#CantTotEntregada").val();
    var opPrecioUni = $("#ValorUnitFacturado").val();
    var opVlrEntrega = $("#ValorTotFacturado").val();
    var VlrCoopago = "0";
    var NomEPS = $("#labelEPS").text();

    var CuotaModeradora = "0";
    $("#hfPrecioUni").val(opPrecioUni);
    $("#hfResponsable").val(NomEPS);
    $("#hfNitEntidad").val(opIdEPS);
    $("#hfNoHistoria").val(opNoIdentificacion);

    var UrlEntregaAmbito = $("#hfUrlFacturacion").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();

    var BodyPut = "{";
    BodyPut += "'NoPrescripcion':'" + opNoPrescripcion + "',";
    BodyPut += "'TipoTec':'" + TipoTec + "',";
    BodyPut += "'ConTec':" + parseInt(ConseTec) + ",";
    BodyPut += "'TipoIDPaciente':'" + opTipoID + "',";
    BodyPut += "'NoIDPaciente':'" + opNoIdentificacion + "',";
    BodyPut += "'NoEntrega':" + parseInt(NoEntrega) + ",";
    BodyPut += "'NoFactura':'" + opFactura + "',";
    BodyPut += "'NoIDEPS':'" + opIdEPS + "',";
    BodyPut += "'CodEPS':'" + opCodEPS + "',";
    BodyPut += "'CodSerTecAEntregado':'" + opCUM + "',";
    BodyPut += "'CantUnMinDis':'" + opcantidad + "',";
    BodyPut += "'ValorUnitFacturado':'" + opPrecioUni + "',";
    BodyPut += "'ValorTotFacturado':'" + opVlrEntrega + "',";
    BodyPut += "'CuotaModer':'" + VlrCoopago + "',";
    BodyPut += "'Copago':'" + CuotaModeradora + "'";
    BodyPut += "}";

    var parametros = { "Uri": UrlEntregaAmbito, "nit": Nit, "token": Token, "Datos": BodyPut };

    var UriRuta = "PutFacturacion";
    $.ajax({
        type: 'POST',
        url: "PutFacturacion",
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            console.log(data);

            var Id = 0;
            var IdEntrega = 0;
            $.each(res, function (i, item) {
                Id = item["Id"];
                IdEntrega = item["IdFacturacion"];

                if (Id === undefined) {
                    alert(data);

                    return
                }

                $("#hfNoID").val(Id);
                $("#hfIDEntrega").val(IdEntrega);

                alert('ID:' + Id + ' - Id Facturacion:' + IdEntrega);

            });
            if (Id != undefined) {
                if (swfCuenta == "")
                {
                    GrabaEntrega();
                }
                GrabaFactura(Id, IdEntrega);
                $("#gvPrescripciones").empty();
                $("#divCaso").hide();
                $("#panelFact").hide();
               //$("#modRegFacturas").modal('hide');
                refresh();
            }
            //refresh();
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function GrabaFactura(Id_F, IdFacturacion) {
    var NoIDEntrega = $("#hfNoID").val();
    var NoPrescripcion = $("#NoPrescripcion").val();
    var numFactura = $("#hfFactEPS").val();
    var cantidadEnt = $("#CantTotEntregada").val();
    var valorUni = $("#ValorUnitFacturado").val();
    var codigoEPS = $("#CodEPS").val();

    var parametros = { "NoPrescripcion": NoPrescripcion, "IdEntrega": parseInt(NoIDEntrega), "NoFactura": numFactura, "CodEPS": codigoEPS, "CantEnt": parseInt(cantidadEnt), "ValorUni": parseInt(valorUni), "ID_F": parseInt(Id_F), "IdFacturacion": parseInt(IdFacturacion) };
    //$("#hfIDReporteEntrega").val(IdEntrega);

    var UriRuta = "RegistraFacturacion";

    $.ajax({
        type: 'POST',
        url: "RegistraFacturacion",
        data: JSON.stringify(parametros), //{ "NoPrescripcion": opNoPrescripcion, "NoIdentificacion": opNoIdentificacion, "CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "ValEntrega": Valor },
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            console.log(data);

            alert('Registro de Facturacion Grabado Exitosamente...');

        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}


// Metodos MIPRES
function PutAnularEntrega(Tipo) {

    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();

    var UrlAnularEntrega = $("#hfUrlAnularNoEntrega").val();
    var IdEntrega = $("#hfIDEntrega").val();

    if (Tipo === "2") {
        UrlAnularEntrega = $("#hfUrlAnularReporteEntrega").val();
        IdEntrega = $("#hfIDReporteEntrega").val();
    }

    var parametros = { "Uri": UrlAnularEntrega, "nit": Nit, "token": Token, "IdEntrega": IdEntrega };

    var UriRuta = "PutAnularEntrega";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            //alert(data);
            var res = JSON.parse(data);
            alert('Mensaje...' + data);
            //$.each(res, function (i, item) {
            //    var Mensaje = item["Mensaje"];
            //    alert('Mensaje:'+Mensaje);

            //});

            RegistrarAnularEntrega();

            $("#modEntregas").modal('hide');

        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });


}

function RegistrarAnularEntrega() {


}

function GetReporteFechas() {
    var FechaIni = $("#FechaIni").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var UrlReportesFechas = $("#hfUrlReportesFechas").val();
    var date_format = new Date(FechaIni).toDateString("yyyy-MM-dd");

    var parametros = { "Uri": UrlReportesFechas, "nit": Nit, "fecha": FechaIni, "token": Token, "tipo": "E" };
    var NoId = "";
    var NoIdReporte = "";

    var UriRuta = "GetPrescripciones";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            $("#myModal").modal('show');
        },
        success: function (data) {
            $("#myModal").modal('hide');
            var res = JSON.parse(data);
            console.log(data);

            if (res !== "") {
                // Lista de Campos
                $.each(res, function (i, item) {
                    var Id = item["ID"];

                    if (Id === undefined) {
                        alert('Token:' + Token + ' ' + data);
                        return
                    }

                });

                var LstCampos = "ID(N)(N)(N)(G)(N)[5][15],";
                LstCampos += "IDReporteEntrega(N)(L)(N)(G)(N)[5][15],";
                LstCampos += "NoPrescripcion(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "TipoIDPaciente(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "NoIDPaciente(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "FecEntrega(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "FecRepEntrega(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "TipoTec(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "ConTec(N)(L)(N)(G)(N)[10][15],";
                LstCampos += "CodTecEntregado(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "CantTotEntregada(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "ValorEntregado(T)(L)(N)(G)(N)[10][15],";
                LstCampos += "EstRepEntrega(T)(L)(N)(G)(N)[10][15]";

                DatatableHide('gvPrescripciones', LstCampos, data, true, true, false, false);

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });
}

function GetReportePrescripcion() {
    var NoPrescripcion = $("#NoPrescripcion").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var UrlPrescripcion = $("#hfUrlReportePrescripcion").val();
    $("#hfNoPrescripcion").val(NoPrescripcion);

    var parametros = { "Uri": UrlPrescripcion, "nit": Nit, "numero": NoPrescripcion, "token": Token };
    var NoIdentificacion = "";

    var UriRuta = "GetUnaPrescripcion";
    $.ajax({
        type: 'POST',
        url: UriRuta,
        data: JSON.stringify(parametros),
        contentType: 'application/json; utf-8',
        dataType: 'json',
        beforeSend: function () {
            // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
            $("#loader").show();
        },
        success: function (data) {
            $("#loader").hide();
            //alert(data);
            var res = JSON.parse(data);
            var Tipo = '1';
            var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Anular</th></tr>";
            if (res === 0) {
                // Valores en nulo o 0
                temp += "<tr> No se encontraron datos...";
                temp += "</tr>";

            }
            else {

                var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Id</th><th>Id Entrega</th><th>No Prescripcion</th><th>Tipo</th><th>No Identificacion</th><th>Fecha</th><th>No Entrega</th><th>Tipo Tec.</th><th>Codigo CUM</th><th>Cantidad</th><th>Estado</th><th>Anular</th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.ID + "</label0></td>";
                    temp += "<td><label1>" + this.IDReporteEntrega + "</label1></td>";
                    temp += "<td><label2>" + this.NoPrescripcion + "</label2></td>";
                    temp += "<td><label3>" + this.TipoIDPaciente + "</label3></td>";
                    temp += "<td><label4>" + this.NoIDPaciente + "</label4></td>";
                    temp += "<td><label5>" + this.FecEntrega + "</label5></td>";
                    temp += "<td><label6>" + this.FecRepEntrega + "</label6></td>";
                    temp += "<td><label7>" + this.TipoTec + "</label7></td>";
                    temp += "<td><label8>" + this.CodTecEntregado + "</label8></td>";
                    temp += "<td><label9>" + this.CantTotEntregada + "</label9></td>";
                    temp += "<td><label10>" + this.ValorEntregado + "</label10></td>";
                    temp += "<td><label11>" + this.EstRepEntrega + "</label11></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return anulareporteClick(this, " + this.IDReporteEntrega + ")'><img src='../../Content/images/Delete1.ico'/></button></td>";
                    temp += "</tr>";
                })

                temp += "</table>";
                $("#gvPrescripciones").append(temp);

            }
        },
        error: function (jqXHR, status, error) {
            alert("ERROR: " + status + " " + error + " " + jqXHR.status + " " + jqXHR.statusText)
        }
    });


}

function GetInformeMipres(pFechaI, pFechaF) {
    var UriRuta = "GetReportePrescripciones"; //'@Url.Action("Consultas","LovEventos")'; //"LovPacientes"; //
    //var pNoHistoria = $("#hfNoHistoria").val();
    //var parametros = { NoHistoria:pNoHistoria,FechaIni: pFechaI, FechaFin: pFechaF };

    $.ajax({
        type: "POST", //GET
        url: UriRuta,
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { FechaIni: pFechaI, FechaFin: pFechaF }, //JSON.stringify(parametros),
        success: function (data) {

            var msg = data.d;
            console.log(data);

            if (msg !== "") {
                //$("#dialog").dialog("open");

                var LstCampos = "FechaProceso(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "NoPrescripcion(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "Responsable(T)(L)(N)(G)(N)[10][25],";
                LstCampos += "NoCuenta(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "NoIdentificacion(T)(L)(N)(G)(N)[5][15],";
                LstCampos += "Paciente(T)(L)(N)(G)(N)[10][25],";
                LstCampos += "CodigoCum(T)(L)(N)(G)(N)[10][25],";
                LstCampos += "Id(N)(L)(N)(G)(N)[5][15],";
                LstCampos += "IdEntrega(N)(L)(N)(G)(N)[5][15],";
                LstCampos += "IdReporte(N)(L)(N)(G)(N)[5][15],";
                LstCampos += "Valor(V)(L)(N)(G)(N)[10][25],";
                LstCampos += "Estado(T)(L)(N)(G)(N)[5][10],";
                LstCampos += "Accion(T)(L)(N)(G)(N)[5][10]";
                DesingTabla('gvPrescripcion', LstCampos, data, true, false, false, false);
                //Tabla = $('#tc_gvPrescripcion').DataTable();

            }
            else {
                alert("Datos en Blanco");
            }
        },
        error: function (xhr, status, error) {
            alert("ERROR: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
        }
    });



}

function dt_gvPrescripcion_RowClick(ObjetoTR, tit) {

}

function descargarExcel() {
    var htmltable = document.getElementById('tabLib');
    var html = htmltable.outerHTML;
    window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));

}


//**** FUNCION BOTON SELECCION DE REGISTRO DE LA BUSQUEDAD *****//

function dt_gvDatosPaciente_RowClick(ObjetoTR, tit) {
    //DatosFilas(ObjetoTR)
    var txtNoCaso = ObjetoTR.cells[0].childNodes[0].nodeValue;
    var txtNombrePaciente = ObjetoTR.cells[3].childNodes[0].nodeValue;
    var txtNoHistoria = ObjetoTR.cells[1].childNodes[0].nodeValue;
    var txtFechaIngreso = ObjetoTR.cells[2].childNodes[0].nodeValue;

    //document.getElementById('NoCaso').innerText = "Caso No: " + txtNoCaso + "";
    //document.getElementById('Paciente').innerText = "" + txtNombrePaciente + "";


}

function dt_gvEgresosCasos_RowClick(ObjetoTR, tit) {

}

function dt_gvResponsables_RowClick(ObjetoTR, tit) {

}

//***** FUNCION REFRESCAR O ACTUALIZAR LA PAGINA
function refresh() {
    location.reload(true);
}
