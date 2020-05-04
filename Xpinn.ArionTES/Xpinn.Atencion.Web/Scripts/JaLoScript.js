$(document).ready(function () {
    $('.carousel').carousel({
        interval: 4000
    });

    //Captura de la distancia del menu al inicio de la pantalla
    var altura = $('#menu').offset().top;
    //alert(altura);
    //Ingresando al evento scroll del navegador
    $(window).on('scroll', function () {

        if ($(window).scrollTop() > altura) {
            $('#menu').addClass('menu-fixed');
        } else {
            $('#menu').removeClass('menu-fixed');
        }

    });


});