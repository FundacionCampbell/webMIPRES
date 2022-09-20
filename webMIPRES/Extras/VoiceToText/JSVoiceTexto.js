var textvox = "";
$(document).ready(function () {
    var SpeechRecognition = window.webkitSpeechRecognition;

    var recognition = new SpeechRecognition();

    var Textbox = $('#textbox');
    var instructions = $('instructions');

    var Content = '';

    recognition.continuous = true;

    recognition.onresult = function (event) {

        var current = event.resultIndex;

        var transcript = event.results[current][0].transcript;

        Content += transcript;
        Textbox.val(Content);
        alert('Dice:'+Content.toString());

    };

    recognition.onstart = function () {
        instructions.text('Voice recognition is ON.');
        alert('Reconociendo Voz');
    }

    recognition.onspeechend = function () {
        instructions.text('No activity.');
    }

    recognition.onerror = function (event) {
        if (event.error == 'no-speech') {
            instructions.text('Try again');
        }
    }

    $('#start-btn').on('click', function (e) {
        e.preventDefault();
        if (Content.length) {
            Content += 'Inicia';
        }
        recognition.start();
    });

    $('#stop-btn').on('click', function (e) {
        e.preventDefault();
        recognition.stop();
        recognition.onresult = function (event) {

            var current = event.resultIndex;

            var transcript = event.results[current][0].transcript;

            Content += transcript;
            Textbox.val(Content);
            alert('Dice:' + Content.toString());

        };
        
    });

    Textbox.on('input', function () {
        Content = $(this).val();
    })
    
});


