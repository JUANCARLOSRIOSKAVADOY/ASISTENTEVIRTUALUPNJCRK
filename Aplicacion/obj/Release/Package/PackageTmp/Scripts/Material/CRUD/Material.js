var Material = function () {
    return{
        init_validacion: function () {
            $('#frm_material').off('submit');
            $('#frm_material').on('submit', function(e) {
                var f = $(this);
                f.parsley().validate

                if (f.parsley().isValid()) {
                    //alert('O formulario es valido');
                    var formData = new FormData();                   
                    formData.append("id", $("#materialid").val());
                    formData.append("descripcion", $("#descripcion").val());
                    formData.append("estado", $("#estado").is(":checked"));
                    formData.append("esEscritura", $("#esEscritura").is(":checked"));

                    $.ajax({
                        url: root + '/Material/GuardarMaterial',
                        data: formData,
                        type: "POST",
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data.Estado == "OK") {
                                var oTable = $("table[id^='tabla']").dataTable();
                                oTable.fnDraw();
                            }

                            Material.empty_field();
                            Material.disable_field();
                            $("#btn_new").removeAttr("disabled");
                            $("#btn_save").attr("disabled",true);
                            $("#btn_cancel").attr("disabled", true);

                            alert(data.Mensaje)
                        },
                        error: function (error) {
                            //$('#GeneralSection').html(error.responseText);
                        }
                    });

                } else {
                    //alert('O formulario no es valido');
                }

                e.preventDefault();
            });
        },
        init_check: function () {
            //$("#estado").bootstrapSwitch();
            $("#estado").iCheck();
            $("#esEscritura").iCheck();
        },
        init_filtros: function(){
            //$("[name='DropdownEstado']").chosen();

            //alert( $("[name='CheckBoxEstado']").bootstrapSwitch('state') );
        },
        init_events_btn_submit: function () {
            var btn_new = $("#btn_new");
            var btn_save = $("#btn_save");
            var btn_cancel = $("#btn_cancel");

            btn_new.click(function () {

                Material.empty_field();

                $("#descripcion").removeAttr("disabled");
                $("#esEscritura").removeAttr("disabled");
                $("#esEscritura").iCheck('update');

                //$("#estado").removeAttr("disabled");
                //$("#estado").iCheck('update');

                
                btn_new.attr("disabled", true);
                btn_save.removeAttr("disabled");
                btn_cancel.removeAttr("disabled");
            });

            btn_cancel.click(function () {
                Material.empty_field();

                //reiniciar campos validados
                $("#descripcion").attr("disabled",true);
                $("#estado").attr("disabled", true);
                $("#estado").iCheck("check");
                $("#estado").iCheck('update');

                $("#esEscritura").attr("disabled", true);
                $("#esEscritura").iCheck("check");
                $("#esEscritura").iCheck('update');

                $("#frm_material").parsley().reset();

                btn_new.removeAttr("disabled");
                btn_save.attr("disabled", true);
                btn_cancel.attr("disabled", true);
            });
        },
        enable_field: function () {
            $("#materialid").removeAttr("disabled");
            $("#descripcion").removeAttr("disabled");
            $("#estado").removeAttr("disabled");
            $("#estado").iCheck('update');

            $("#esEscritura").removeAttr("disabled");
            $("#esEscritura").iCheck('update');
        },
        disable_field: function () {
            $("#materialid").attr("disabled", true);
            $("#descripcion").attr("disabled", true);
            $("#estado").attr("disabled",true);
            $("#estado").iCheck('update');

            $("#esEscritura").attr("disabled", true);
            $("#esEscritura").iCheck('update');
        },
        empty_field: function () {
            $("#materialid").val(0);
            $("#descripcion").val("");
            //$("#estado").removeAttr("disabled");
        },
        init_datatable: function () {
            var tabla = $("table[id^='tabla']"); //busco una tabla cuyo id inicie con la palabra tabla .... Ejemplo: tabla_Empleado, tabla_Proforma
            var entidad = ""; //aqui necesito extraer el nombre del controlador con el que me tendria que comunicar
            var json_columnas = []; //tengo que crear un arreglo json para las columnas que tiene la tabla
            var columnas = $("table[id^='tabla_'] thead tr th"); //capturo todas las columnas actuales de la tabla

            //aqui por cada columna creo un objeto json dentro del arreglo json_columnas
            for (var i = 0; i < columnas.length; i++) {
                /*if ($(columnas[i]).data("additional-class")) {
                    //agrego la columna a mi listado json_columnas
                    json_columnas.push({
                        "bSortable": false, "aTargets": [i], 'sClass': $(columnas[i]).data("additional-class")
                    });

                } else {*/
                //agrego la columna a mi listado json_columnas
                json_columnas.push({
                    "bSortable": false, "aTargets": [i]
                });
                //}
            }

            var filtros = $(".row-filtros-listado input[type=text], .row-filtros-listado input[type=hidden], .row-filtros-listado input[type=checkbox], .row-filtros-listado select, .row-filtros-listado a");
            var arreglo_filtros = []; //creo mi arreglo de filtros

            for (var i = 0; i < filtros.length; i++) { //recorro los controles que estaban dentro de .row-filtros-listado
                var clave;
                var elemento;

                if ($(filtros[i]).is("select")) {
                    clave = $(filtros[i]).attr("name"); //la clave con la que enviaremos el parametro al controlador es el nombre de lcontrol
                    elemento = filtros[i]; //este es el elemento actual
                    $(elemento).change(function () { //cuando el elemento actual cambie
                        var oTable = $(tabla).dataTable(); //entonces obtengo el objeto datatable asociado a la tabla
                        oTable.fnDraw(); //y le indicamos que se vuelva a dibujar
                        //$('.tipS').tipsy({ gravity: 's', fade: true, html: true });
                    });
                } //else if ($(filtros[i]).is("input") && $(filtros[i]).attr("type") != "hidden") {
                else if ($(filtros[i]).attr("type") == "text") {
                    clave = $(filtros[i]).attr("name");
                    elemento = filtros[i];
                    $(elemento).keyup(function (e) {
                        if (e.keyCode == 13) {
                            // cuando presione una tecla en el campo
                            var oTable = $(tabla).dataTable(); //entonces obtengo el objeto datatable asociado a la tabla
                            oTable.fnDraw(); //y le indicamos que se vuelva a dibujar
                        }
                    });

                } else if ($(filtros[i]).attr("type") == "hidden") {
                    clave = $(filtros[i]).attr("name");
                    elemento = filtros[i];
                    $(elemento).change(function () {
                        var oTable = $(tabla).dataTable(); //entonces obtengo el objeto datatable asociado a la tabla
                        oTable.fnDraw(); //y le indicamos que se vuelva a dibujar

                    });

                } else if ($(filtros[i]).hasClass("range_datetime_picker")) {
                    clave = $(filtros[i]).attr("name");
                    elemento = filtros[i];
                }
            }

            if (tabla) {
                //aqui capturo el controlador Ejemplo : tabla_Empleado me devolveria solo Empleado
                entidad = $(tabla).attr("id").split("_")[1];
                //inicializo el plugin datatable sobre la tabla
                $(tabla).dataTable({
                    "oLanguage": {
                        "sUrl": root + "/Scripts/spanish-config.txt"
                    },
                    "bProcessing": true,
                    "bServerSide": true,
                    "iDisplayLength": 10,
                    "bAutoWidth": true,
                    "sDom": '<""r>t<"F"p>',
                    "bJQueryUI": false,
                    "bAutoWidth": false,
                    "bDestroy": true,
                    "sAjaxSource": root + "/" + $(tabla).attr("data-source"),
                    "sPaginationType": "full_numbers",
                    "aoColumnDefs": json_columnas, //aqui le asigno las columas que previamente almacene en el arreglo json
                    "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                        /* Append the grade to the default row class name */
                        /*if ($(nRow).find(".color_estado")) {

                         var color = $(nRow).find(".color_estado").val();
                         $(nRow).css({
                         borderLeft: "3px solid " + color
                         })
                         }

                         return nRow;*/
                    },
                    "fnServerParams": function (aoData) {

                        console.log(filtros);

                        for (var i = 0; i < filtros.length; i++) {//inicio del recorrido de todosl os filtros
                            var clave;
                            var elemento;
                            if ($(filtros[i]).is("select")) {
                                clave = $(filtros[i]).attr("name");
                                elemento = filtros[i];

                                aoData.push({
                                    name: clave,
                                    value: $(elemento).find(":selected").val()
                                });
                            } else if ($(filtros[i]).is("input")) {

                                

                                clave = $(filtros[i]).attr("name");
                                elemento = filtros[i];

                                aoData.push({
                                    name: clave,
                                    value: $(elemento).val()
                                });

                            } else if ($(filtros[i]).hasClass("range_datetime_picker")) {
                                clave = $(filtros[i]).attr("name");
                                elemento = filtros[i];

                                if ($(elemento).find("span").text().indexOf("-") != -1) {
                                    aoData.push({
                                        name: clave,
                                        value: $(elemento).find("span").text()
                                    });
                                } else {
                                    aoData.push({
                                        name: clave,
                                        value: ""
                                    });
                                }
                            }
                        } //fin del corrido de todos los filtros
                    }
                });
            }
        },
        load_editar: function (materialid) {
            $.ajax({
                url: root + '/Material/ObtenerMaterialById',
                data: {
                    materialid: materialid
                },
                type: "POST",
                success: function (data) {

                    console.log(data);

                    Material.enable_field();

                    $("#btn_new").attr("disabled", true);
                    $("#btn_save").removeAttr("disabled");
                    $("#btn_cancel").removeAttr("disabled");

                    $("#materialid").val(data.id);
                    $("#descripcion").val(data.descripcion);
                    $("#estado").iCheck(data.estado ? "check" : "uncheck");
                    $("#esEscritura").iCheck(data.esEscritura ? "check" : "uncheck");

                    Material.init_validacion();
                },
                error: function (error) {
                    //$('#GeneralSection').html(error.responseText);
                }
            });

        }
    }
}();


$(document).ready(function () {
    Material.init_check();
    Material.init_filtros();
    Material.init_datatable();
    Material.init_events_btn_submit();
    Material.init_validacion();
});