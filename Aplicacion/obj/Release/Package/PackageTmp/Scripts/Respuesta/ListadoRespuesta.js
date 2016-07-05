var ListadoRespuesta = function () {
    return {
        init_botones: function () {

            $(".btn_eliminar_respuesta").click(function(){
                $.ajax({
                    url: root + '/Respuesta/EliminarRespuesta',
                    data: {
                        respuestaid: $(".btn_eliminar_respuesta").attr("data-respuestaid")
                    },
                    type: "POST",
                    success: function (data) {
                        /*if (data.Estado == "OK") {
                            var oTable = $("table[id^='tabla']").dataTable();
                            oTable.fnDraw();
                        }*/

                        //Login.empty_fields();
                        $("#modal_respuestas").modal("hide");
                        alert(data.Mensaje)
                    },
                    error: function (error) {

                    }
                });
            });
        },

        delete_respuesta: function (respuestaid) {
            $.ajax({
                url: root + '/Respuesta/EliminarRespuesta',
                data: {
                    respuestaid: respuestaid
                },
                type: "POST",
                success: function (data) {
                    $("#modal_respuestas").modal("hide");
                    alert(data.Mensaje)
                },
                error: function (error) {

                }
            });
        }
    }
}();

$(document).ready(function () {

    //Login.init_login();
    //Login.init_botones();
    ListadoRespuesta.init_botones();
});