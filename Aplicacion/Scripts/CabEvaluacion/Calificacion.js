var Calificacion = function () {
    return{
        save_nota: function (cabevaluacionid) {

            //alert();

            $.ajax({
                url: root + '/CabEvaluacion/SaveNota',
                data: {
                    cabevaluacionid: cabevaluacionid,
                    nota: $("#nota_" + cabevaluacionid).val()
                },
                type: "POST",
                success: function (data) {
                    if (data.Estado == "OK") {
                        location.reload();
                    }

                    alert(data.Mensaje)
                },
                error: function (error) {
                    //$('#GeneralSection').html(error.responseText);
                }
            });
        },
        load_respuesta: function (evaluacionid) {

            $.ajax({
                url: root + "/Evaluacion/VistaEvaluacionById",
                data: {
                    evaluacionid: evaluacionid
                },
                type: 'post',
                success: function (data) {
                    $(".bs-modal-sm .modal-content .modal-body").html(data);
                    $(".bs-modal-sm").modal("show");
                }
            });
        }
    }
}();


$(document).ready(function () {

});