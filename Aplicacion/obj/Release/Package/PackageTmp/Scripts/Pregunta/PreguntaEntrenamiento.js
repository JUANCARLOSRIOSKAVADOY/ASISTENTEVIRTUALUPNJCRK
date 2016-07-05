var PreguntaEntrenamiento = function () {
    return{
        request_respuestas: function (pregunta,cursoid,materialid) {
            $.ajax({
                url: root + "/Pregunta/JsonListadoRespuestasByPregEntrenamiento",
                data: {
                    pregunta: pregunta,
                    cursoid: cursoid,
                    materialid: materialid
                },
                //async: false,
                type: 'post',
                success: function (data) {

                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            $(".speak").text(data[i].descripcion);
                        }
                    } else {
                        $(".speak").text("No se encontró respuesta.");
                    }

                    PreguntaEntrenamiento.init_speak($(".speak").text());
                }
            });

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

                        var cursoid = $("#start_button").attr("data-cursoid");
                        var materialid = $("#start_button").attr("data-materialid");

                        PreguntaEntrenamiento.request_respuestas(final_transcript,cursoid,materialid);

                    }
                };
            }


            $("#start_button").click(function () {

                if (recognizing) {
                    recognition.stop();
                    return;
                }
                final_transcript = '';
                recognition.lang = 'es-PE';
                recognition.start();
                ignore_onend = false;
                final_span.innerHTML = '';
                start_img.attr("src", "/Images/mic-slash.gif");

            });
        },
        init_speak: function (text) {

            //var btn = document.getElementById('speak');
            speechSynthesis.cancel();
            var u = new SpeechSynthesisUtterance();
            u.text = text;
            u.lang = 'es-ES';
            u.rate = 0.5;
            u.pitch = 1.5;
            u.volume = 1;

            speechSynthesis.speak(u);
        }
    }
}();


$(document).ready(function () {
    PreguntaEntrenamiento.init_reconocimiento();
});