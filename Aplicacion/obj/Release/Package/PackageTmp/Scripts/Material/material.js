var Material = function () {
    return{
        events: function() {
            $(".portfolio-ajax").addClass("loading");
            $(window).on("load", Material.layoutItems());
            $(window).on("resize", Material.layoutItems());
            $(document).on("click touchstart", ".toggle-sidebar", function () { Material.layoutItems(); });
            $(".superbox").imagesLoaded().always(function () {
                $(".superbox").fadeIn();
                $(".portfolio-ajax").removeClass("loading");
            }).done(function () {
                $(".gallery-loader").addClass("hide");
                $(".superbox").removeClass("hide");
                Material.layoutItems();
            });
        },
        layoutItems: function() {
            Material.initLayout();
        },
        initLayout: function() {
            var portfolioWidth = Material.getWidth();
            $(".superbox").find(".superbox-list").each(function () {
                $(this).css({ width: portfolioWidth + "px" });
            });
        },
        getWidth: function() {
            var wi = $(".superbox").width(), col = Math.floor(wi / 1);
            if (wi > 1024) { col = Math.floor(wi / 5); }
            else if (wi > 767) { col = Math.floor(wi / 4); }
            else if (wi > 480) { col = Math.floor(wi / 2); }
            else if (wi > 320) { col = Math.floor(wi / 1); }
            return col;
        },
        superbox: function(){
            $('.superbox').SuperBox();
        },
        init: function () {
            Material.events();
            //superbox();
        },
        init_botones: function () {
            $(".superbox-list").click(function () {
                var courseid = $(this).attr("data-cursoid");
                var materialid = $(this).attr("data-materialid");

                $.ajax({
                    //url: root + "/Pregunta/ObtenerPreguntasRandom",
                    //url: root + "/Pregunta/JsonListadoPreguntasRandom",
                    url: root + "/Pregunta/StartQuestion",
                    data: {
                        materialid: materialid
                    },
                    type: 'post',
                    success: function (data) {

                        $(".bs-modal-sm .btn-cancel").removeClass("hide");
                        $(".bs-modal-sm .btn-start-question").removeClass("hide");
                        $(".bs-modal-sm .btn-next").addClass("hide");
                        $(".bs-modal-sm .btn-finish").addClass("hide");

                        /*Obtener curso y material*/
                        $(".bs-modal-sm .btn-finish").attr("data-cursoid", courseid);
                        $(".bs-modal-sm .btn-finish").attr("data-materialid", materialid);


                        /*if (data.length > 0) {
                            
                            $(".bs-modal-sm .btn-next").click(function () {
                                alert();
                            })
                        }*/

                        $(".bs-modal-sm .btn-start-question").click(function () {
                            //reinicio la posición de la pregunta
                            $(".bs-modal-sm .btn-next").attr("data-preg", 0);

                            //alert(courseid+" "+materialid);

                            Material.load_question_quizz(courseid, materialid);
                        });

                        $(".bs-modal-sm .modal-content .modal-body").html(data);
                        $(".bs-modal-sm").modal("show");
                    }
                });
            });
        },
        load_question_quizz: function (cursoid,materialid) {
            $.ajax({
                url: root + "/Pregunta/JsonListadoPreguntasRandom",
                data: {
                    cursoid: cursoid,
                    materialid: materialid
                },
                type: 'post',
                success: function (data) {
                    
                    console.log(data);
                    $(".bs-modal-sm .btn-cancel").addClass("hide");
                    $(".bs-modal-sm .btn-start-question").addClass("hide");

                    //solo para examenes de escritura
                    /*if (data[0].material.esEscritura) {
                        $(".nPregunta").addClass("hide");
                        //$(".bs-modal-sm .btn-next").attr('disabled', false);
                    }*/

                    if (data.length > 0) {

                        /*if (data.material.esEscritura) {
                            $(".bs-modal-sm .btn-next").attr('disabled', false);
                        }*/

                        $(".bs-modal-sm .btn-next").removeClass("hide");
                        //$(".bs-modal-sm .btn-next").attr("disabled",true);

                        //hago la solicitud para la siguiente pregunta
                        $(".bs-modal-sm .btn-next").click(function () {

                            //alert();

                            $(".bs-modal-sm .btn-next").attr("disabled", true);

                            var currentPos = parseInt( $(this).attr("data-preg") );
                            $(this).attr("data-preg", currentPos+1);
                            
                            Material.load_question_in_modal(data[currentPos + 1], currentPos + 2, data.length, false, materialid);

                            if (data.length - 1 == currentPos + 1) {
                                $(".bs-modal-sm .btn-next").addClass("hide");
                                $(".bs-modal-sm .btn-finish").removeClass("hide");
                                $(".bs-modal-sm .btn-finish").click(function () {
                                    Material.request_finish(data[currentPos + 1]);
                                });
                            }

                        });

                        Material.load_question_in_modal(data[0], 1, data.length, true, materialid);

                        if (data.length == 1) {
                            $(".bs-modal-sm .btn-next").addClass("hide");
                            $(".bs-modal-sm .btn-finish").removeClass("hide");
                            $(".bs-modal-sm .btn-finish").click(function () {
                                Material.request_finish(data[0]);
                            });
                        } else {
                            if (data.length - 1 == 1) {
                                $(".bs-modal-sm .btn-next").addClass("hide");
                                $(".bs-modal-sm .btn-finish").removeClass("hide");
                            }
                        }     
                    }

                }
            });
        },
        load_question_in_modal: function (obj, currentPos, total, esInicio, materialid) {
            console.log(obj);

            /*var formData = new FormData();
            formData.append('id', obj.id);
            formData.append('descripcion', obj.descripcion);*/

            $.ajax({
                url: root + "/Pregunta/ObtenerPregunta",
                data: {
                    preguntaid: obj.id,
                    descripcionPregunta: obj.descripcion,
                    final_span: $("#final_span").text(),
                    esCorrecto: !($("#alert_success").hasClass("hide")),
                    esInicio: esInicio,
                    materialid: materialid
                },
                type: 'post',
                success: function (data) {

                    $(".bs-modal-sm .modal-content .modal-body").html(data);

                    if (obj.material.esEscritura) {
                        $(".nPregunta").remove();
                        //$(".bs-modal-sm .btn-next").attr('disabled', false);
                    } else {
                        $(".bs-modal-sm .nPregunta").text("Pregunta N " + currentPos + "/" + total);
                        Material.init_reconocimiento(obj.id);
                    }

                    //alert("Cargado.");
                    //var u = Sintesis.init($(".bs-modal-sm .speak").text());

                    window.speechSynthesis.cancel();
                    var u = new SpeechSynthesisUtterance();
                    u.text = $(".bs-modal-sm .speak").text();
                    u.lang = 'es-ES';
                    u.rate = 1.4;
                    u.pitch = 1.5;
                    u.volume = 1;

                    u.onstart = function (event) {
                        t = event.timeStamp;
                        console.log(t);
                    };
        
                    /*u.onend = function (event) {
                        //alert();
                        $(".bs-modal-sm .btn-next").attr('disabled', false);
                    };*/

                    window.speechSynthesis.speak(u);

                }
            });
        },
        request_respuestas: function (preguntaid,respuesta) {
            var existe = false;
            $.ajax({
                url: root + "/Respuesta/JsonObtenerRespuestaByPregunta",
                data: {
                    preguntaid: preguntaid
                },
                async: false,
                type: 'post',
                success: function (data) {
                    console.log(data);
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].descripcion.toUpperCase() == respuesta.toUpperCase()) {
                            existe = true;
                            break;
                        }
                    }
                }
            });

            return existe;
        },
        init_reconocimiento: function (preguntaid) {
            var recognizing = false;
            var recognition = null;
            var final_transcript = null;
            var ignore_onend = false;
            var start_img = $("#start_img");


            if (!('webkitSpeechRecognition' in window)) {
                alert("No permite reconocimiento de voz");
            } else {
                //start_button.style.display = 'inline-block';
                recognition = new webkitSpeechRecognition();
                recognition.continuous = true;
                recognition.interimResults = true;

                recognition.onstart = function () {
                    recognizing = true;
                    //showInfo('info_speak_now');
                    start_img.attr("src", "/Images/mic-animate.gif");
                };

                recognition.onerror = function (event) {
                    if (event.error == 'no-speech') {
                        alert('no-speech');
                        start_img.src = 'mic.gif';
                        //showInfo('info_no_speech');
                        ignore_onend = true;
                    }
                    if (event.error == 'audio-capture') {
                        alert('audio-capture');
                        start_img.src = 'mic.gif';
                        //showInfo('info_no_microphone');
                        ignore_onend = true;
                    }
                    if (event.error == 'not-allowed') {
                        alert('not-allowed');
                        ignore_onend = true;
                    }
                };

                recognition.onend = function () {
                    recognizing = false;
                    if (ignore_onend) {
                        return;
                    }
                    start_img.attr("src", "/Images/mic.gif");
                    //start_img.src = 'mic.gif';
                    if (!final_transcript) {
                        //showInfo('info_start');
                        return;
                    }
                };

                recognition.onresult = function (event) {
                    //var interim_transcript = '';
                    for (var i = event.resultIndex; i < event.results.length; ++i) {
                        if (event.results[i].isFinal) {
                            final_transcript += event.results[i][0].transcript;
                        }
                    }
                    if (final_transcript) {
                        //showButtons('inline-block');
                        //alert(final_transcript);
                        $("#final_span").text(final_transcript);
                        var existe = Material.request_respuestas(preguntaid, final_transcript);

                        var alert_success = $("#alert_success");
                        var alert_danger = $("#alert_danger");
                        var cont_respuesta = $("#contenedor_respuesta");

                        cont_respuesta.removeClass("hide");
                        if(existe){
                            alert_success.removeClass("hide");
                        }else{
                            alert_danger.removeClass("hide");
                        }

                        $(".bs-modal-sm .btn-next").attr('disabled', false);
                    }
                };
            }


            $("#start_button").click(function () {

                if (recognizing) {
                    recognition.stop();
                    return;
                }

                //alert();

                final_transcript = '';
                recognition.lang = 'es-PE';
                recognition.start();
                ignore_onend = false;
                final_span.innerHTML = '';
                start_img.attr("src", "/Images/mic-slash.gif");

            });
        },
        request_finish: function (obj) {
            $.ajax({
                url: root + "/Pregunta/FinishEvaluacion",
                data: {
                    courseid: $(".bs-modal-sm .btn-finish").attr("data-cursoid"),
                    materialid: $(".bs-modal-sm .btn-finish").attr("data-materialid"),
                    final_span: obj.material.esEscritura ? $("#final_span").val() : $("#final_span").text(),
                    esCorrecto: !($("#alert_success").hasClass("hide")),
                    esEscritura: obj.material.esEscritura
                },
                type: 'post',
                success: function (data) {
                    $(".bs-modal-sm").modal("hide");
                }
            });
        }
    }
}();


$(document).ready(function () {
    Material.init();
    Material.init_botones();
});