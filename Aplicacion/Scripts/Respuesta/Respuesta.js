var Respuesta = function () {
    return {
        /*init_botones: function(){
            $("#btn_nuevo_usuario").off("click");
            $("#btn_nuevo_usuario").on("click",function(){
                Login.modal_nuevo_usuario();
            });
        },*/
        init_login: function () {

            $("#btnLogin").click(function () {
                // code

                var formData = new FormData();
                formData.append("dni", $("#dni").val());
                formData.append("password", $("#psw").val());

                $.ajax({
                    url: root + '/Usuario/verificarLogin',
                    data: formData,
                    type: "post",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.usuarioValido == true) {
                            window.location.href = root + data.url;
                            //alert("bien");
                        }
                        else {
                            alert(data.mensaje);
                        }
                    },
                    error: function (error) {
                        console.log(error);

                        //$('#GeneralSection').html(error.responseText);
                    }
                });

            });
        },
        modal_registro: function () {
            $("#modal_registro").modal("show");
        },
        init_botones: function () {
            $("#btn_cancel_respuesta").click(function () {
                //Login.empty_fields();
                $("#modal_ins_respuesta").modal("hide");
            });
        },
        init_validacion: function () {
            $('#frm_ins_respuesta').submit(function (e) {
                var f = $(this);
                f.parsley().validate

                if (f.parsley().isValid()) {

                    var formData = new FormData();
                    formData.append("descripcion", $("#ins_respuesta").val());
                    formData.append("pregunta.id", $("#btn_save_respuesta").attr("data-preguntaid"));

                    $.ajax({
                        url: root + '/Respuesta/InsertarRespuesta',
                        data: formData,
                        type: "POST",
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            /*if (data.Estado == "OK") {
                                var oTable = $("table[id^='tabla']").dataTable();
                                oTable.fnDraw();
                            }*/

                            //Login.empty_fields();
                            $("#modal_ins_respuesta").modal("hide");
                            alert(data.Mensaje)
                        },
                        error: function (error) {

                        }
                    });

                }

                e.preventDefault();
            });
        },
        empty_fields: function () {
            $("#frm_dni").val("");
            $("#nombres").val("");
            $("#appaterno").val("");
            $("#apmaterno").val("");
            $("#frm_psw").val("");
            $("#rolEstudiante").prop("checked", true);
            $("#rolDocente").prop("checked", false);
        }
    }
}();

$(document).ready(function () {

    //Login.init_login();
    //Login.init_botones();
    Respuesta.init_validacion();
});