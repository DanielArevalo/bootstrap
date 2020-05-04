(function ($) {
    $.fn.DuplicateWindow = function () {
        var localStorageTimeout = (5) * 1000; // 15,000 milliseconds = 15 seconds.
        var localStorageResetInterval = (1/2) * 1000; // 10,000 milliseconds = 10 seconds.
        var localStorageTabKey = 'my-application-browser-tab';
        var sessionStorageGuidKey = 'browser-tab-guid';

        var ItemType = {
            Session: 1,
            Local: 2
        };

        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/";
        }
        function getCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        }

        function GetItem(itemtype) {
            var val = "";
            switch (itemtype) {
                case ItemType.Session:
                    val = window.name;
                    break;
                case ItemType.Local:
                    val = decodeURIComponent(getCookie(localStorageTabKey));
                    if (val == undefined)
                        val = "";
                    break;
            }
            return val;
        }

        function SetItem(itemtype, val) {
            switch (itemtype) {
                case ItemType.Session:
                    window.name = val;
                    break;
                case ItemType.Local:
                    setCookie(localStorageTabKey, val);
                    break;
            }
        }

        function createGUID() {
            this.s4 = function () {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            };
            return this.s4() + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' + this.s4() + this.s4() + this.s4();
        }
      function TestIfDuplicate() {
            //console.log("In testTab");
            var sessionGuid = GetItem(ItemType.Session) || createGUID();
            SetItem(ItemType.Session, sessionGuid);

            var val = GetItem(ItemType.Local);
            var tabObj = (val == "" ? null : JSON.parse(val)) || null;
            console.log(val);
            console.log(sessionGuid);
            console.log(tabObj);

            // If no or stale tab object, our session is the winner.  If the guid matches, ours is still the winner
            if (tabObj === null || (tabObj.timestamp < (new Date().getTime() - localStorageTimeout)) || tabObj.guid === sessionGuid) {
                function setTabObj() {
                    //console.log("In setTabObj");
                    var newTabObj = {
                        guid: sessionGuid,
                        timestamp: new Date().getTime()
                    };
                    SetItem(ItemType.Local, JSON.stringify(newTabObj));
                }
                setTabObj();
                setInterval(setTabObj, localStorageResetInterval);//every x interval refresh timestamp in cookie
                return false;
            } else {
                // An active tab is already open that does not match our session guid.
                return true;
            }
        }

        window.IsDuplicate = function () {
            var duplicate = TestIfDuplicate();
            //console.log("Is Duplicate: "+ duplicate);
            return duplicate;
        };

        $(window).on("beforeunload", function () {
            if (TestIfDuplicate() == false) {
                SetItem(ItemType.Local, "");
            }
        })
    }
    $(window).DuplicateWindow();
}(jQuery));


function bloquearPantalla() {
    var mensaje = "";
    mensaje = "Ya tiene una pagina abierta, para evitar el cruce de información por favor cierre esta pagina";

    //centrar el titulo
    height = 20;//El div del titulo, para que se vea mas arriba (H)
    var ancho = 0;
    var alto = 0;

    //obtenemos el ancho y alto de la ventana de nuestro navegador, compatible con todos los navegadores
    if (window.innerWidth == undefined) ancho = window.screen.width;
    else ancho = window.innerWidth;
    if (window.innerHeight == undefined) alto = window.screen.height;
    else alto = window.innerHeight;

    //operación necesaria para centrar el div que muestra el mensaje
    var heightdivsito = alto / 2 - parseInt(height) / 2;//Se utiliza en el margen superior, para centrar
    imgCentro = "<div style='text-align:center;height:" + alto + "px;'><div  style='color:#000;margin-top:" + heightdivsito + "px; font-size:27px;font-weight:bold'>" + mensaje + "</div></div>";

    //creamos el div que bloquea grande------------------------------------------
    div = document.createElement("div");
    div.id = "WindowLoad"
    div.style.width = ancho + "px";
    div.style.height = alto + "px";
    $("body").append(div);

    //creamos un input text para que el foco se plasme en este y el usuario no pueda escribir en nada de atras
    input = document.createElement("input");
    input.id = "focusInput";
    input.type = "text"

    //asignamos el div que bloquea
    $("#WindowLoad").append(input);

    //asignamos el foco y ocultamos el input text
    $("#focusInput").focus();
    $("#focusInput").hide();

    //centramos el div del texto
    $("#WindowLoad").html(imgCentro);
}


$(document).ready(function () {
    
    if (window.IsDuplicate()) {

        // alert user the tab is duplicate
        bloquearPantalla();
    }
});