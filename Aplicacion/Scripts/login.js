var Login = function () {
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
            $("#btn_cancel").click(function () {
                Login.empty_fields();
                $("#modal_registro").modal("hide");
            });
        },
        init_validacion: function () {
            $('#frm_usuario').submit(function (e) {
                var f = $(this);
                f.parsley().validate

                if (f.parsley().isValid()) {

                    var formData = new FormData();
                    formData.append("nombres", $("#nombres").val());
                    formData.append("ap_paterno", $("#appaterno").val());
                    formData.append("ap_materno", $("#apmaterno").val());
                    formData.append("dni", $("#frm_dni").val());
                    formData.append("password", $("#frm_psw").val());
                    formData.append("esEstudiante", $("#rolEstudiante").is(':checked'));
                    formData.append("esDocente", $("#rolDocente").is(':checked'));

                    $.ajax({
                        url: root + '/Usuario/InsertarUsuario',
                        data: formData,
                        type: "POST",
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            /*if (data.Estado == "OK") {
                                var oTable = $("table[id^='tabla']").dataTable();
                                oTable.fnDraw();
                            }

                            Material.empty_field();
                            Material.disable_field();
                            $("#btn_new").removeAttr("disabled");
                            $("#btn_save").attr("disabled", true);
                            $("#btn_cancel").attr("disabled", true);*/
                            Login.empty_fields();
                            $("#modal_registro").modal("hide");
                            alert(data.Mensaje)
                        },
                        error: function (error) {
                            //$('#GeneralSection').html(error.responseText);
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

//$(document).ready(function () {

//    Login.init_login();
//    Login.init_botones();
//    Login.init_validacion();
//});


$(document).ready(function () {
    window.speechSynthesis.cancel();
    Login.init_login();
    Login.init_botones();
    Login.init_validacion();
});