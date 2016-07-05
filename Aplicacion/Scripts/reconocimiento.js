var Reconocimiento = new function () {
    var recognizing = false;
    var recognition = null;
    var final_transcript = null;
    var ignore_onend = false;
    var start_img = $("#start_img");

    return {

        init_botones: function () {
            $("#start_button").click(function () {
                Reconocimiento.startButton();
            });
        },
        startButton: function () {

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
            //start_img.src = 'mic-slash.gif';
            /*showInfo('info_allow');
            showButtons('none');
            start_timestamp = event.timeStamp;*/
        },
        init: function () {

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
                        showInfo('info_no_speech');
                        ignore_onend = true;
                    }
                    if (event.error == 'audio-capture') {
                        alert('audio-capture');
                        start_img.src = 'mic.gif';
                        showInfo('info_no_microphone');
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
                        showInfo('info_start');
                        return;
                    }
                };

                recognition.onresult = function (event) {
                    //var interim_transcript = '';
                    for (var i = event.resultIndex; i < event.results.length; ++i) {
                        if (event.results[i].isFinal) {
                            final_transcript += event.results[i][0].transcript;
                        } /*else {
                            interim_transcript += event.results[i][0].transcript;
                        }*/
                    }
                    /*final_transcript = capitalize(final_transcript);
                    final_span.innerHTML = linebreak(final_transcript);
                    interim_span.innerHTML = linebreak(interim_transcript);*/
                    if (final_transcript) {
                        //showButtons('inline-block');
                        $("#final_span").val(final_transcript);
                        //alert(final_transcript);
                        //alert("final_transcript and interim_transcript are true");
                    }
                };
            }
        }
    }
}();

$(document).ready(function () {
    Reconocimiento.init();
    Reconocimiento.init_botones();
    
})