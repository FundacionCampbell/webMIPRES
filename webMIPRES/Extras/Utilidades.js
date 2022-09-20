ParametroCampo = {
    NombreCampo: 0,
    TipoDato: 1,
    TipoEdicion: 2,
    TieneAyuda: 3,
    TipoAyuda: 4,
    Requerido: 5,
    Longitud: 6,
    AnchoCol: 7
};

//-

function DefineTabla(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";

    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered table-responsive" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white">' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length; i++) {
        Totales[i] = 0;
    }

    // Recorre el objeto de datos
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr onclick="dt_' + DivTabla + '_RowClick(this,' + "'" + Titulos + "'" + ')" > ';

        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                //alert("Campo: "+dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }


        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;

    })

    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        "searching": VerBusqueda

    });

    //"columnDefs": [

    //    {
    //        targets: [1], width: "500"
    //    }
    //]



    //var table = $('#tc_' + DivTabla).DataTable();

    //for (var i = 0; i < vTitulos.length; i++) {

    //    // Toggle the visibility
    //    if (i === 1) {
    //        table.context[0].aoColumns[i].bVisible = false;
    //    }
    //    table.context[0].aoColumns[i].sWidth = TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol);
    //    //table.destroy();
    //}

}

//--

function DefineTabla(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales, Tipo) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";

    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered table-responsive display" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white"><th></th>' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length; i++) {
        Totales[i] = 0;
    }

    //console.log(dDatos)

    // Recorre el objeto de datos
    var LinCheck = 0;
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr onclick="dt_' + DivTabla + '_RowClick(this,' + "'" + Titulos + "'" + ')" >';
        htmlTabla += "<td><input type=checkbox id=chckHead" + LinCheck + " onclick='return SeleccionRegistro(this, " + dDatos[Ix]["Saldo"] + ")' /></td >";


        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            //alert("Index:"+ Ix.toString() + "Campo:" + strCampo + "Valor: " + dDatos[Ix][strCampo]);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                //                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }

        //htmlTabla += '<td><p data-placement="top" data-toggle="tooltip" title="Edit"><button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit" ><span class="glyphicon glyphicon-pencil"></span></button></p></td>';
        //htmlTabla += '<td><p data-placement="top" data-toggle="tooltip" title="Delete"><button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete" ><span class="glyphicon glyphicon-trash"></span></button></p></td>';



        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;
        LinCheck += 1;

    })


    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();
    //$('div.dataTables_filter input').attr('type', 'text');

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        "searching": true

    });


}

//--

function DesingTabla(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";
    //dt-responsive
    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered dt-responsive" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");
    
    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white">' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length-1; i++) {
        Totales[i] = 0;
    }

    // Recorre el objeto de datos
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr onclick="dt_' + DivTabla + '_RowClick(this,' + "'" + Titulos + "'" + ')" > ';

        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';
                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                var vbFecha = new Date(dDatos[Ix][strCampo]);

                //htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + vbFecha.toLocaleDateString() + '</td>';
                
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }

        //htmlTabla += '<td><button type="button" class="btn btn-primary btn-xs dt-edit" style="margin-right:16px;"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button></td>';

        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;

    })

    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        "lengthChange": false,
        responsive: true,
        dom: 'Bfrt<"col-md-6 inline"i> <"col-md-6 inline"p>',
        "buttons":
            [
                {
                    extend: 'collection',
                    init: (api, node, config) => $(node).removeClass('btn-secondary'),
                    className: 'btn btn-outline-success',
                    text: 'Export',
                    buttons: [
                        { extend: 'csvHtml5' },
                        { extend: 'excelHtml5' }
                    ]
                },
                {
                    extend: 'print',
                    init: (api, node, config) => $(node).removeClass('btn-secondary'),
                    className: 'btn btn-outline-success'
                }
            ],
        //--
        //{
        //    dom: {
        //        container: {
        //            tag: 'div',
        //            className: 'flexcontent'
        //        },
        //        buttonLiner: {
        //            tag: null
        //        }
        //    },

        //    buttons: [
        //        {
        //        extend: 'pdfHtml5',
        //        text: '<i class="fa fa-file-pdf-o"></i>PDF',
        //        title: 'Titulo de tabla en pdf',
        //        titleAttr: 'PDF',
        //        className: 'btn btn-app export pdf',
        //        exportOptions: {
        //            columns: [0,1,2,3,4,5,6,7,8,9,10,11,12]
        //        },
        //        customize: function (doc) {

        //            doc.styles.title = {
        //                color: '#4c8aa0',
        //                fontSize: '30',
        //                alignment: 'center'
        //            }
        //            doc.styles['td:nth-child(2)'] = {
        //                width: '100px',
        //                'max-width': '100px'
        //            },
        //            doc.styles.tableHeader = {
        //                fillColor: '#4c8aa0',
        //                color: 'white',
        //                alignment: 'center'
        //            },
        //            doc.content[1].margin = [100, 0, 100, 0]
        //        }
        //        },

        //        {
        //            extend: 'csvHtml5',
        //            text: '<i class="fa fa-file-text-o"></i>CSV',
        //            title: 'Titulo de tabla en CSV',
        //            titleAttr: 'CSV',
        //            className: 'btn btn-app export csv',
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
        //            }
        //        },
                
        //        {
        //            extend: 'excelHtml5',
        //            text: '<i class="fa fa-file-excel-o"></i>Excel',
        //            title: 'Titulo de tabla en excel',
        //            titleAttr: 'Excel',
        //            className: 'btn btn-app export excel',
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
        //            },
        //        },

        //        {
        //            extend: 'print',
        //            text: '<i class="fa fa-print"></i>Imprimir',
        //            title: 'Titulo de tabla en impresion',
        //            titleAttr: 'Imprimir',
        //            className: 'btn btn-app export imprimir',
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
        //            }
        //        }
        //    ]
        //},
    //--
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        "searching": VerBusqueda,
        "columnDefs": [{
            "targets": -1,
            "data": null,
            "defaultContent": "<button id='btnPrint' type='button' class= 'btn btn-xs dt-print'><span class='glyphicon glyphicon-print' aria-hidden='true'></span></button>"
               
        }]

    });

 }

//--

function MakeTabla(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";
    //dt-responsive
    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered dt-responsive" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla #5cb85c 15451b
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white">' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length - 1; i++) {
        Totales[i] = 0;
    }

    // Recorre el objeto de datos
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr onclick="dt_' + DivTabla + '_RowClick(this,' + "'" + Titulos + "'" + ')"  style="font-stretch:condensed;font-size: smaller;" > ';

        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';
                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                var vbFecha = new Date(dDatos[Ix][strCampo]);

                //htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + vbFecha.toLocaleDateString() + '</td>';

            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }

        //htmlTabla += '<td><button type="button" class="btn btn-primary btn-xs dt-edit" style="margin-right:16px;"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button></td>';

        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;

    })

    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        responsive: true,
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        "searching": VerBusqueda

        //"defaultContent": "<button>Recibo!</button>"
    });

}

//---

function MakeDatatable(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";
    //dt-responsive
    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered dt-responsive" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white">' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length - 1; i++) {
        Totales[i] = 0;
    }

    // Recorre el objeto de datos
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr> ';

        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';
                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                var vbFecha = new Date(dDatos[Ix][strCampo]);

                //htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + vbFecha.toLocaleDateString() + '</td>';

            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }

        //htmlTabla += '<td><button type="button" class="btn btn-primary btn-xs dt-edit" style="margin-right:16px;"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button></td>';

        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;

    })

    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        responsive: true,
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        "searching": VerBusqueda,
        "columnDefs": [{
            "targets": -1,
            "data": null,
            "defaultContent": "<button class='btn btn-danger btn-xs dt-delete'><span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></button>"
        }]
            //"defaultContent": "<button id='btnPrev' type='button' class= 'btn btn-danger btn-xs dt-delete'><span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></button>"
            //"defaultContent": "<a href='' class='btn-mipres'><span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></a>"

        //"defaultContent": "<button>Recibo!</button>"
        //"defaultContent": "<button id='btnDown' type='button' class='btn btn-primary btn-xs dt-edit' style='margin-right:16px;'><span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span></button>"
       //< button id='btnPrev' type='button' class= 'btn btn-danger btn-xs dt-delete' > <span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></button> "
    });

}

//--

function DatatableHide(DivTabla, Titulos, Datos, VerPaginacion, VerBusqueda, VerOrdenamiento, VerTotales) {
    $('#' + DivTabla + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlTabla = "";
    var tHead = "";
    var tFoot = "";
    //dt-responsive
    // Define las caracteristicas de la tabla
    htmlTabla = '<table id="tc_' + DivTabla + '" class="table table-hover table-striped  table-bordered dt-responsive" cellspacing="1" width="100%">';

    $('#tc_' + DivTabla).DataTable();

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Quito el tipo de datos del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        // Coloco de titulo solo el nombre del campo
        tHead = tHead + '<th class="text-center"> ' + strCampo + '</th> ';
        tFoot = tFoot + '<th> ' + '@' + strCampo + '</th> ';
    }

    // Coloca titulos en la tabla
    htmlTabla += ' <thead> <tr style="background-color:	#15451b;font-stretch:condensed;color:white">' + tHead + ' </tr> </thead> ';

    // Coloca la etiqueta de apertura del cuerpo
    htmlTabla += ' <tbody> ';

    // Desserializa los datos inicia en 0 variable de recorrido
    var dDatos = JSON.parse(Datos);
    var Ix = 0;

    var Totales = Array();

    for (var i = 0; i < vTitulos.length - 1; i++) {
        Totales[i] = 0;
    }

    // Recorre el objeto de datos
    $(dDatos).each(function () {
        // Abre la etiqueta de fila
        htmlTabla += '<tr> ';

        //htmlTabla += '<tr> ';

        // Recorre las columnas y las agrega
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            // Si el tipo de dato es (V)alores debe asignar el class que formatea la columna
            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                htmlTabla += '<td class="money textDerecha"  width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';
                Totales[i] = parseInt(Totales[i]) + parseInt(dDatos[Ix][strCampo]);
            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
                var vbFecha = new Date(dDatos[Ix][strCampo]);

                //htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + formatDate(dDatos[Ix][strCampo], "DD/MM/YYYY") + '</td>';
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + vbFecha.toLocaleDateString() + '</td>';

            }
            else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
                htmlTabla += '<td class="textDerecha" width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td>';

            } else {
                htmlTabla += '<td width="' + TraeParametroCampo(Titulos, i, ParametroCampo.AnchoCol) + '">' + dDatos[Ix][strCampo] + '</td> ';
            }

        }

        //htmlTabla += '<td><button type="button" class="btn btn-primary btn-xs dt-edit" style="margin-right:16px;"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button></td>';

        // Cierra la etiqueta de fila
        htmlTabla += ' </tr>';

        // Incrementa el contador de registros
        Ix = Ix + 1;

    })

    // Cierra etiqueta del cuerpo
    htmlTabla += ' </tbody> ';

    if (VerTotales === true) {
        // Reemplaza las variables de totales del footer
        for (var i = 0; i < vTitulos.length; i++) {

            // Cargo el nombre del campo
            var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

            if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
                tFoot = tFoot.replace("@" + strCampo, FormatVr(Totales[i]));
            } else {
                tFoot = tFoot.replace("@" + strCampo, "");
            }
        }

        htmlTabla += ' <tfoot> <tr> ' + tFoot + ' </tr> </tfoot> ';

    }

    // Cierra etiqueta de la tabla
    htmlTabla += ' </table>';

    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivTabla + '').empty();

    // Agrega la tabla al div
    $('#' + DivTabla + '').html(htmlTabla);

    // Ejecuta la consulta de los datos en la tabla y agrega propiedades
    $('#tc_' + DivTabla).DataTable({
        responsive: true,
        //"fixedHeader": true,
        //scrollY: "300px",
        //scrollX: true,
        //"scrollCollapse": true,
        "paging": VerPaginacion,
        //"searching": VerBusqueda,
        "columnDefs": [{ 'visible': false, 'targets': [7, 8, 9, 10, 11, 12, 13] }]
        //"defaultContent": "<button id='btnPrev' type='button' class= 'btn btn-danger btn-xs dt-delete'><span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></button>"
        //"defaultContent": "<a href='' class='btn-mipres'><span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></a>"

        //"defaultContent": "<button>Recibo!</button>"
        //"defaultContent": "<button id='btnDown' type='button' class='btn btn-primary btn-xs dt-edit' style='margin-right:16px;'><span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span></button>"
        //< button id='btnPrev' type='button' class= 'btn btn-danger btn-xs dt-delete' > <span class='glyphicon glyphicon-list-alt' aria-hidden='true'></span></button> "
    });

}


//---
function DefineCampos(DivCampos, Titulos) {
    $('#' + DivCampos + '').html("<div class='text-center'> <img src='https://www.wordbee.com/Content/Wordbee/Images/loading1.gif' class='loader' border='0' /> </div>");

    // Define variables de evaluacion
    var htmlFields = "";
    var tLabels = "";
    var tFields = "";

    // Quita espacion de la cadena de campos de titulo
    Titulos = Titulos.replace(/ /g, '');
    Titulos = Titulos.trim();

    // Vectoriza los titulos
    var vTitulos;
    vTitulos = Titulos.split(",");

    // Crea los div de agrupamiento principales
    htmlFields += '<div class="container-fluid" style="padding-top: 0.1em;">';
    htmlFields += '<div class="showback panel panel-primary">';
    //htmlFields += '<div class="panel panel-heading"><i class="fa fa-bar-chart-o fa-fw"></i></div>';
    htmlFields += '<div class="panel-body">';
    htmlFields += '<fieldset class="form-vertical">';
    //htmlFields += '<legend></legend>';

    var prmTipoDato;
    var prmTipoEdicion;
    var prmTieneAyuda;
    var prmTipoAyuda;

    // Recorre el vector y arma la cadena del titulo de la tabla
    for (var i = 0; i < vTitulos.length; i++) {

        // Cargo el nombre del campo
        var strCampo = TraeParametroCampo(Titulos, i, ParametroCampo.NombreCampo);

        tFields += '<div class="form-group col-xs-12 col-md-6">';

        // Coloco de titulo solo el nombre del campo
        tFields += '<label for="cplContenido_txt' + strCampo + '" class="control-label col-md-3">' + strCampo + '</label>';

        // Valida parametro de tipo de dato
        if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "N") {
            prmTipoDato = "numeros";
        } else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "V") {
            prmTipoDato = "valores";
        } else if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoDato) === "F") {
            prmTipoDato = "datepicker placeholder";
        } else {
            prmTipoDato = "";
        }

        if (TraeParametroCampo(Titulos, i, ParametroCampo.TipoEdicion) !== "E") {
            prmTipoEdicion = "disabled";
        } else {
            prmTipoEdicion = "";
        }

        if (TraeParametroCampo(Titulos, i, ParametroCampo.TieneAyuda) === "S" && TraeParametroCampo(Titulos, i, ParametroCampo.TipoAyuda) === "L") {
            //tFields += '<div class="col-md-6"><select ID="txt' + strCampo + '" AutoComplete="off" class="form-control input-sm ' + prmTipoDato + '" ' + prmTipoEdicion + ' >---Selecciona una Opcion---</Select></div> ';
            tFields += '<div class="row"> <div class="col-lg-6"><div class="input-group"> <input type="text" class="form-control" placeholder="Search for..."> <span class="input-group-btn"> <button onclick="Click_hlp_btn_' + strCampo + '()" id="btn_txt' + strCampo + '" class="btn btn-default" type="button">' + strCampo + '</button> </span> </div></div></div>'
            tFields += '<div id="hlp_txt' + strCampo + '" class="Ocultar  boxshadow divRedondos" style="padding:5px" ></div>'

        } else {
            tFields += '<div class="col-md-6"><input ID="txt' + strCampo + '" AutoComplete="off" type="text" class="form-control input-sm ' + prmTipoDato + '" ' + prmTipoEdicion + '  /></div> ';
        }


        tFields += '</div>';

    }

    // Agrega los fields
    htmlFields += tFields;

    // Cierra los Divs
    htmlFields += '</fieldset>';

    htmlFields += '<!--- Botonera --->';
    htmlFields += '<fieldset>';
    htmlFields += '<legend>Acciones</legend>';

    htmlFields += '<div class="form-group col-xs-12 col-md-12">';
    htmlFields += '<div class="col-xs-6 col-md-6">';
    htmlFields += '<input type="button" class="btn btn-primary btn-group-sm" id="bntGuardar" value="Guardar" />';
    htmlFields += '</div>';

    htmlFields += '<div class="col-xs-6 col-md-6">';
    htmlFields += '<input type="button" class="btn btn-primary btn-group-sm" id="btnCancelar" value="Cancelar" />';
    htmlFields += '</div>';
    htmlFields += '</div>';

    htmlFields += '</fieldset>';


    htmlFields += '</div>';
    //htmlFields += '</div>';
    htmlFields += '</div>';


    // Limpia el div para evitar montar sobre la imagen de carga
    $('#' + DivCampos + '').empty();

    // Agrega la tabla al div
    $('#' + DivCampos + '').html(htmlFields);

}

function TraeParametroCampo(LstCampos, IndexCampo, ParamCampo) {

    // Define variables de parametros
    var Parametro = "";

    // Vectoriza la lista de campos
    var vCampos;

    //console.log(LstCampos);
    vCampos = LstCampos.split(",");

    // Cargo el campo en una variable para quitarle los parametros
    var strAux = vCampos[IndexCampo];

    //Cargo parametros de valor (Longitud y Ancho de columna) a otra variable
    var strAux2 = strAux.substring(strAux.indexOf("["))

    //Quito parametros de valor (Longitud y Ancho de columna)
    var strAux = strAux.substring(0, strAux.indexOf("["))

    //Devuelvo parametros de valor
    if (ParamCampo === ParametroCampo.Longitud) {
        Parametro = strAux2.substring(1, strAux2.indexOf("]"));
    } else if (ParamCampo === ParametroCampo.AnchoCol) {
        strAux2 = strAux2.substring(strAux2.indexOf("]") + 1)
        Parametro = strAux2.substring(1, strAux2.indexOf("]"));
    } else if (ParamCampo === ParametroCampo.Requerido) {
        Parametro = strAux.substring(strAux.length - 2, strAux.length - 1)
    } else if (ParamCampo === ParametroCampo.TipoAyuda) {
        Parametro = strAux.substring(strAux.length - 5, strAux.length - 4)
    } else if (ParamCampo === ParametroCampo.TieneAyuda) {
        Parametro = strAux.substring(strAux.length - 8, strAux.length - 7)
    } else if (ParamCampo === ParametroCampo.TipoEdicion) {
        Parametro = strAux.substring(strAux.length - 11, strAux.length - 10)
    } else if (ParamCampo === ParametroCampo.TipoDato) {
        Parametro = strAux.substring(strAux.length - 14, strAux.length - 13)
    } else if (ParamCampo === ParametroCampo.NombreCampo) {
        Parametro = strAux.substring(0, strAux.indexOf("("));
    }

    // Devuelve el valor del parametro
    return Parametro;

}

//-- Formato Fecha
function getFormattedDate(date) {
    //alert('Si Fech');
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;
    var str = day+ '/' + month + '/' + year;
    console.log(str);
    return str; //year + '-' + month + '/' + day;
}



