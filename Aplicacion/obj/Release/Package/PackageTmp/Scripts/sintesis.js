var Sintesis = function () {
    return {
        init: function (text) {

            //var btn = document.getElementById('speak');
            speechSynthesis.cancel();
            var u = new SpeechSynthesisUtterance();
            u.text = text;
            u.lang = 'es-ES';
            u.rate = 0.5;
            u.pitch = 1.5;
            u.volume = 1;

            /*u.onstart = function (event) {
                t = event.timeStamp;
                console.log(t);
            };

            u.onend = function (event) {
                t = event.timeStamp - t;
                console.log(event.timeStamp);
                console.log((t / 1000) + ' seconds');
            };*/

            //btn.onclick = function () { speechSynthesis.speak(u); };

            speechSynthesis.speak(u);

            return u;
        }
    }
}();

/*$(document).ready(function () {
    Sintesis.init();
})*/