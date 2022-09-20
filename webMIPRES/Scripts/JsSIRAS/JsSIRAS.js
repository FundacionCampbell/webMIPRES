var arrayMedicamentos = [];
var arrayProcedimientos = [];
var arrayProductosNutricionales = [];
var arraydispositivos = [];
var arrayserviciosComplementarios = [];
var arrayEntregas = [];


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

    $('#btnLoader').click(function (e) {

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

    $("#NoCuenta").val(opNoCuenta);
    $("#Paciente").val(opPaciente);
    $("#hfNoCuenta").val(opNoCuenta);
    $("#hfNoHistoria").val(opNoHistoria);

    GetValidaPaciente(opNoCuenta, opNoIdentificacion);

}

function detalleClick(control, NoCaso) {
    var opConsInterno = $(control).closest('tr').find('label2').html();
    var opResponsable = $(control).closest('tr').find('label1').html();

    $("#hfResponsable").val(opResponsable);
    
    GetDetalleExtracto(opConsInterno, NoCaso);

}

function GetParametros() {
    var UriRuta = 'GetParametros'; //"LovPacientes"; //
    //var parametros = { criterio: criterio1 }
    //alert('Entre al Lov');
    try {
        $.ajax({
            type: "POST", //GET
            url: UriRuta,
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
                    var Token = item["token"];
                    var UrlPrescripciones = item["UrlPrescripciones"];
                    var UrlUnaPrescripcion = item["UrlUnaPrescripcion"];
                    var UrlValidaToken = item["UrlValidaToken"];
                    var UrlEntregaAmbito = item["UrlEntregaAmbito"];
                    var UrlReporteEntrega = item["UrlReporteEntrega"];
                    var NoTokenGenerado = item["TokenGenerado"];

                    $("#hfNit").val(NitEntidad);
                    $("#hfToken").val(Token);
                    $("#hfEmpresa").val(Empresa);
                    $("#hfUrlPrescripciones").val(UrlPrescripciones);
                    $("#hfUrlUnaPrescripcion").val(UrlUnaPrescripcion);
                    $("#hfUrlValidaToken").val(UrlValidaToken);
                    $("#hfUrlEntregaAmbito").val(UrlEntregaAmbito);
                    $("#hfUrlReporteEntrega").val(UrlReporteEntrega);
                    $("#hfTokenGenerado").val(NoTokenGenerado);

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
                            temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return detMedicamento(this," + Tipo + ")'><img src='../../Content/images/true.gif'/></button></td>";
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
        url: UriRuta,
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
            beforeSend: function () {
                // AQUI PUEDEMOS COLOCAR UNA IMAGEN DE LOADING
                $("#loader").show();
            },
            success: function (data) {
                //alert(data);
                var msg = data.d;
                console.log(data);
                var res = JSON.parse(data);
                var temp = "<table class='table table-hover table-striped table-bordered'><tr><th>No Caso</th><th>No Identificacion</th><th>Paciente</th><th>Fecha Ing</th><th></th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.NoCuenta + "</label0></td>";
                    temp += "<td><label1>" + this.Identificacion + "</label1></td>";
                    temp += "<td><label2>" + this.Paciente + "</label2></td>";
                    temp += "<td><label3>" + this.FechaIngreso + "</label3></td>";
                    temp += "<td><label4>" + this.Identificacion + "</label4></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return funcionClick(this)'><img src='../../Content/images/true.gif'/></button></td>";
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

                if (Dpto === "08" && Mnpo === "001") {
                    alert("El Paciente pertenece al Distrito, Dirigirse a la plataforma del Distrital");
                    refresh();
                    return;
                }

                GetEgresosCaso(NoCuenta);
                GetExtractoCasos(NoCuenta);
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

function GetEgresosCaso(NoCaso) {
    var UriRuta = "GetEgresosCasos";
    $.ajax({
        type: "POST", //GET
        url: UriRuta,
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta: NoCaso }, //JSON.stringify(parametros),
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

function GetExtractoCasos(NoCaso) {
    var UriRuta = "GetExtractoCasos";
    $("#gvResponsables").empty();

    $.ajax({
        type: "POST", //GET
        url: UriRuta,
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta: NoCaso }, //JSON.stringify(parametros),
        success: function (data) {

            var res = JSON.parse(data);
            console.log(data);
            if (res !== "") {
                // Lista de Campos
                //f.nitentidad, e.nombre_entidad, f.ConsInterno, f.CodConvenio, cv.CodEsquemaTar, cv.TipoTopeSoat, cv.soat, cv.CopagoPorNivel

                var temp = "<table class='table table-hover table-striped table-bordered'><tr style='background-color: #15451b; font - stretch: condensed; color: white'><th>Nit</th><th>Responsable</th><th>Cons.Interno</th><th>Convenio</th><th>Esquema</th><th>Tipo SOAT</th><th>SOAT</th><th>Vlr.Factura</th><th>Descuento</th><th></th></tr>";
                $(res).each(function () {
                    temp += "<tr>";
                    temp += "<td><label0>" + this.NitEntidad + "</label0></td>";
                    temp += "<td><label1>" + this.Nombre_Entidad + "</label1></td>";
                    temp += "<td><label2>" + this.ConsInterno + "</label2></td>";
                    temp += "<td><label3>" + this.CodConvenio + "</label3></td>";
                    temp += "<td><label4>" + this.CodEsquemaTar + "</label4></td>";
                    temp += "<td><label5>" + this.TipoTopeSoat + "</label5></td>";
                    temp += "<td><label6>" + this.soat + "</label6></td>";
                    temp += "<td><label7>" + this.ValorFactura.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "</label7></td>";
                    temp += "<td><label8>" + this.Valor_Descuento.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "</label8></td>";
                    temp += "<td style='text-align: center'><button type='button' class='btn btn-xs btn-circle' value='' onclick='return detalleClick(this, " + NoCaso + ")'><img src='../../Content/images/true.gif'/></button></td>";
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

function GetDetalleExtracto(opConsInterno, NoCaso) {

    var UriRuta = "GetDetalleExtractoMIPRES";
    $("#gvDetalleExtracto").empty();

    $.ajax({
        type: "POST", //GET
        url: UriRuta,
        content: "application/json; charset=utf-8",
        dataType: "json",
        // De la siguiente manera indicamos que del div tome los input.
        data: { NoCuenta: NoCaso, ConsInterno: opConsInterno }, //JSON.stringify(parametros),
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
                        var arrayDate = fecMed.match(pattern);
                        var dd = arrayDate[1].toString();
                        var mm = (arrayDate[2].toString());
                        var mes = mm.toString();
                        if (dd.length < 2) {
                            dd = "0" + dd;
                        }
                        if (mes.length < 2) {
                            mes = "0" + mes;
                        }

                        fechaArt = arrayDate[3].toString() + "-" + mes.toString() + "-" + dd.toString();
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
                        var arrayDate = fecMed.match(pattern);
                        var dd = arrayDate[1].toString();
                        var mm = (arrayDate[2].toString() - 1);
                        var mes = mm.toString();
                        if (dd.length < 2) {
                            dd = "0" + dd;
                        }
                        if (mes.length < 2) {
                            mes = "0" + mes;
                        }

                        fechaArt = arrayDate[3].toString() + "-" + mes.toString() + "-" + dd.toString();
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
    if (ConseTec === 0) {
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

function GrabaEntrega()
{
    var opNoPrescripcion = $("#NoPrescripcionMIPRES").val();
    var opNoIdentificacion = $("#NoIDPaciente").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var opVlrEntrega = $("#hfVlrEntrega").val();
    var opNoCuenta = $("#hfNoCuenta").val();
    var opResponsable = $("#hfResponsable").val();
    var opNoHistoria = $("#hfNoHistoria").val();


    var NoID = $("#hfNoID").val();
    var IDEntrega = $("#hfIDEntrega").val();

    var Valor = opVlrEntrega.replace(/,/g, "");

    $("#hfVlrEntrega").val(Valor);

    var parametros = { "NoPrescripcion": opNoPrescripcion, "NoCuenta": opNoCuenta, "NoHistoria": opNoHistoria,"NoIdentificacion": opNoIdentificacion, "Responsable":opResponsable,"CodigoCum": opCUM, "NoId": NoID, "IdEntrega": IDEntrega, "ValEntrega": Valor };

    var UriRuta = "RegistrarEntrega";
    $.ajax({
        type: 'POST',
        url: UriRuta,
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

function GrabaReporte()
{
    var NoId = $("#hfNoID").val();
    var opNoPrescripcion = $("#NoPrescripcionMIPRES").val();
    var opCUM = $("#CodSerTecEntregado").val();
    var Nit = $("#hfNit").val();
    var Token = $("#hfTokenGenerado").val();
    var Estado = 0;
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

                alert('ID Reporete:' + Id + ' - Id Reporte Entrega:' + IdReporteEntrega);

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
        url: UriRuta,
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

//**** FUNCION BOTON SELECCION DE REGISTRO DE LA BUSQUEDAD *****//
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











