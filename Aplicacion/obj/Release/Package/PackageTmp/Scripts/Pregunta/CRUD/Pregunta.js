var Pregunta = function () {
    return{
        init_validacion: function () {
            $('#frm_pregunta').off('submit');
            $('#frm_pregunta').on('submit', function (e) {
                var f = $(this);
                f.parsley().validate

                if (f.parsley().isValid()) {
                    //alert('O formulario es valido');
                    var formData = new FormData();

                    formData.append("id", $("#preguntaid").val());
                    formData.append("descripcion", $("#Descripcion").val());
                    formData.append("curso.id", $("#DropdownCurso").val());
                    formData.append("material.id", $("#DropdownMaterial").val());
                    formData.append("esEntrenamiento", $("#esEntrenamiento").is(":checked"));

                    $.ajax({
                        url: root + '/Pregunta/GuardarPregunta',
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

                            Pregunta.empty_field();
                            Pregunta.disable_field();
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
            $("#esEntrenamiento").iCheck();
        },
        init_filtros: function(){
            //$("[name='DropdownEstado']").chosen();

            //alert( $("[name='CheckBoxEstado']").bootstrapSwitch('state') );
        },
        init_form: function () {
            $("#DropdownCurso").attr("disabled", true);
            $("#DropdownMaterial").attr("disabled", true);
            
            $("#DropdownCurso option:first").attr("value", "");
            $("#DropdownMaterial option:first").attr("value", "");

            $("#DropdownCurso").attr("data-parsley-required","true");
            $("#DropdownMaterial").attr("data-parsley-required", "true");
        },
        init_events_btn_submit: function () {
            var btn_new = $("#btn_new");
            var btn_save = $("#btn_save");
            var btn_cancel = $("#btn_cancel");

            btn_new.click(function () {

                //Curso.empty_field();

                $("#DropdownCurso").removeAttr("disabled");
                $("#DropdownMaterial").removeAttr("disabled");
                $("#Descripcion").removeAttr("disabled");

                $("#esEntrenamiento").removeAttr("disabled");
                //$("#estado").iCheck("check");
                $("#esEntrenamiento").iCheck('update');
                //$("#estado").removeAttr("disabled");
                //$("#estado").iCheck('update');

                
                btn_new.attr("disabled", true);
                btn_save.removeAttr("disabled");
                btn_cancel.removeAttr("disabled");
            });

            btn_cancel.click(function () {
                Pregunta.empty_field();

                //reiniciar campos validados
                $("#DropdownCurso").attr("disabled", true);
                $("#DropdownMaterial").attr("disabled", true);
                $("#Descripcion").attr("disabled", true);

                $("#esEntrenamiento").attr("disabled", true);
                $("#esEntrenamiento").attr("checked", false);
                $("#esEntrenamiento").iCheck('update');

                /*$("#descripcion").attr("disabled",true);
                $("#estado").attr("disabled", true);
                $("#estado").iCheck("check");
                $("#estado").iCheck('update');*/

                $("#frm_pregunta").parsley().reset();

                btn_new.removeAttr("disabled");
                btn_save.attr("disabled", true);
                btn_cancel.attr("disabled", true);
            });
        },
        enable_field: function () {
            /*$("#cursoid").removeAttr("disabled");
            $("#descripcion").removeAttr("disabled");
            $("#estado").removeAttr("disabled");
            $("#estado").iCheck('update');*/
            $("#DropdownCurso").removeAttr("disabled");
            $("#DropdownMaterial").removeAttr("disabled");
            $("#Descripcion").removeAttr("disabled");

            $("#esEntrenamiento").removeAttr("disabled");
            $("#esEntrenamiento").iCheck('update');
        },
        disable_field: function () {
            $("#DropdownCurso").attr("disabled", true);
            $("#DropdownMaterial").attr("disabled", true);
            $("#Descripcion").attr("disabled", true);

            $("#esEntrenamiento").attr("disabled", true);
            $("#esEntrenamiento").iCheck('update');
        },
        empty_field: function () {

            $("#preguntaid").val(0);
            $("#DropdownCurso").val("");
            $("#DropdownMaterial").val("");
            $("#Descripcion").val("");

            $("#esEntrenamiento").attr("checked", false);
            $("#esEntrenamiento").iCheck('update');

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
        load_editar: function (preguntaid) {
            $.ajax({
                url: root + '/Pregunta/ObtenerPreguntaById',
                data: {
                    preguntaid: preguntaid
                },
                type: "POST",
                success: function (data) {
                    Pregunta.enable_field();

                    $("#btn_new").attr("disabled", true);
                    $("#btn_save").removeAttr("disabled");
                    $("#btn_cancel").removeAttr("disabled");


                    $.post(root + "/CursoMaterial/ObtenerListadoMaterialByCurso", { cursoid: data.curso.id }, function (materiales) {
                        var options = "<option value>Seleccione un material</option>"

                        $.each(materiales, function (key, value) {
                            if (value.id == data.material.id) {
                                options += "<option value=" + value.id + " selected >" + value.descripcion + "</option>";
                            }
                            else {
                                options += "<option value=" + value.id + " >" + value.descripcion + "</option>";
                            }
                            
                        });

                        $("#DropdownMaterial").html(options);
                    });

                    $("#DropdownCurso").val(data.curso.id);
                    $("#DropdownMaterial").val(data.material.id);
                    $("#preguntaid").val(data.id);                    
                    $("#Descripcion").val(data.descripcion);
                    $("#esEntrenamiento").iCheck(data.esEntrenamiento ? "check" : "uncheck");
                    //$("#estado").iCheck(data.estado ? "check" : "uncheck");

                    Pregunta.init_validacion();
                },
                error: function (error) {
                    //$('#GeneralSection').html(error.responseText);
                }
            });

        },
        load_select_anidado: function () {
            $("#DropdownCurso").change(function () {
                $("#DropdownCurso option:selected").each(function () {
                    elegido = $(this).val();
                    $.post(root + "/CursoMaterial/ObtenerListadoMaterialByCurso", { cursoid: elegido }, function (data) {                        
                        var options = "<option value selected>Seleccione un material</option>"

                        $.each(data, function (key, value) {
                            options += "<option value="+value.id+">"+value.descripcion+"</option>";
                        });

                        $("#DropdownMaterial").html(options);
                    });
                });
            })

            $("#DropdownCursoFiltro").change(function () {
                $("#DropdownCursoFiltro option:selected").each(function () {
                    elegido = $(this).val();
                    $.post(root + "/CursoMaterial/ObtenerListadoMaterialByCurso", { cursoid: elegido }, function (data) {
                        var options = "<option value=-1 selected>Seleccione un material</option>"

                        $.each(data, function (key, value) {
                            options += "<option value=" + value.id + ">" + value.descripcion + "</option>";
                        });

                        $("#DropdownMaterialFiltro").html(options);
                    });
                });
            })
        },
        load_respuesta: function (preguntaid) {
            $.ajax({
                url: root + "/Respuesta/ObtenerRespuestaByPregunta",
                data: {
                    preguntaid: preguntaid
                },
                type: 'post',
                success: function (data) {
                    $("#modal_respuestas .modal-content .modal-body").html(data);
                    $("#modal_respuestas").modal("show");
                }
            });
        },
        load_ins_respuesta: function (preguntaid) {
            $.ajax({
                url: root + "/Respuesta/VistaInsertarRespuesta",
                data: {
                    preguntaid: preguntaid
                },
                type: 'post',
                success: function (data) {
                    $("#modal_ins_respuesta .modal-content .modal-body").html(data);
                    $("#modal_ins_respuesta").modal("show");
                }
            });
        }
    }
}();


$(document).ready(function () {
    Pregunta.init_form();
    Pregunta.init_check();
    Pregunta.load_select_anidado();
    Pregunta.init_events_btn_submit();
    Pregunta.init_datatable();
    Pregunta.init_validacion();
    /*
    */
    /*Curso.init_check();
    Curso.init_filtros();*/
});