<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Page_Aportes_Personas_Tabs_Nuevo" %>

<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">
    <style>
        /*anteriores anderson*/
        .numeric {
            width: 110px;
            text-align: right;
        }

        .auto-style1 {
            width: 158px;
        }


        a {
            color: #383838;
        }

        * {
            margin: 0;
            padding: 0;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        body {
            background: #D4D4D4;
            font-family: 'Open sans';
        }

        .wrap {
            width: 100%;
            max-width: 100%;
            margin: 1px auto;
        }

        .header-tabs {
            padding-top: 0px;
            background: #d4d4d4;
        }

        ul.tabs {
            width: 100%;
            background: #f3f3f3;
            list-style: none;
            display: flex;
            color: #808080;
            text-align: center;
            margin: 0px auto;
        }

        ul.tabs li {
            width: 20%;
        }

        ul.tabs li a {
            text-decoration: none;
            font-size: 12px;
            text-align: center;
            display: block;
            padding: 14px 0px;
        }

        .active {
            background: #5295fa;
            color: #fff;
        }

        ul.tabs li a .tab-text {
            margin-left: 7px;
        }

        .secciones {
            width: 100%;
            background: #fff;
        }

        .secciones article {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .secciones article p {
            text-align: justify;
        }


        @media screen and (max-width: 700px) {
            ul.tabs li {
                width: none;
                flex-basis: 0;
                flex-grow: 1;
            }
        }

        @media screen and (max-width: 450px) {
            ul.tabs li a {
                padding: 15px 0px;
            }

                ul.tabs li a .tab-text {
                    display: none;
                }

            .secciones article {
                padding: 20px;
            }
        }
        /*nuevos w3c*/
        * {
            box-sizing: border-box;
        }

        /* Set height of body and the document to 100% */
        body, html {
            height: 100%;
            margin: 0;
            font-family: Arial;
        }

        /* Style tab links */
        .tablink {
            background-color: #555;
            color: white;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            font-size: 17px;
            width: 25%;
        }

        .tablink:hover {
                background-color: #777;
            }

        /* Style the tab content (and add height:100% for full page content) */
        .tabcontent {
            color: black;
            display: none;
            padding: 0px 0px;
            height: 500px;
        }

        .clsInactivos {
            display: none;
        }
        #WindowLoad
        {
            position:fixed;
            top:0px;
            left:0px;
            z-index:3200;
            filter:alpha(opacity=65);
            -moz-opacity:65;
            opacity:0.65;
            background:#999;
        }
    </style>
   
    <asp:ImageButton runat="server" ID="btnImpresion2" ClientIDMode="Static" ImageUrl="~/Images/btnImprimir.jpg" OnClick="btnImpresion2_Click" Style="display: none" />
    <div class="wrap">
        <ul class="tabs header-tabs">
            <li id="liPersonal" title="InfoPersonal"><a title="InfoPersonal" class="active" onclick="Validar('InfoPersonal')"><span style="font-size: 17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. Personal</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size: 17px;"></span></a></li>
            <li id="lilaboral" title="InfoLaboral" class="InfoLaboral"><a title="InfoLaboral" onclick="Validar('InfoLaboral')"><span style="font-size: 17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. Laboral - Pagaduria</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size: 17px;"></span></a></li>
            <li id="liBeneficiarios" title="InfoBenefi"><a title="InfoBenefi" onclick="Validar('InfoBenefi')"><span style="font-size: 17px;" class="far fa-folder-open"></span><span class="tab-text">Beneficiarios</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size: 17px;"></span></a></li>
            <li id="lilEconomica" title="InfoEconomi"><a title="InfoEconomi" onclick="Validar('InfoEconomi')"><span style="font-size: 17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. Económica</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size: 17px;"></span></a></li>
            <li id="liAdicional" title="InfoAdicion"><a title="InfoAdicion" onclick="Validar('InfoAdicion')"><span style="font-size: 17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. Adicional</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size: 17px;"></span></a></li>
        </ul>
    </div>
    <div>
        <strong>
            <asp:Label Text="Codigo: " runat="server" Visible="false" /></strong>
        <asp:Label ID="lblcodpersona" Text="" runat="server" />
        <strong>
            <asp:Label Text="Identificación: " runat="server" Visible="false" /></strong>
        <asp:Label ID="lblidentificacion" Text="" runat="server" />
        <strong>
            <asp:Label Text="Nombre: " runat="server" Visible="false" /></strong>
        <asp:Label ID="lblnombre" Text="" runat="server" />
    </div>

    <div id="InfoPersonal" class="tabcontent">
        <iframe src="Personal.aspx" style="width: 100%; height: 2000px; border: none" name="formularios" id="iframePersonal"></iframe>
    </div>
    <div id="InfoLaboral" class="tabcontent">
        <iframe src="Laboral.aspx" style="width: 100%; height: 2000px; border: none" name="formularios" id="iframeLaboral"></iframe>
    </div>
    <div id="InfoBenefi" class="tabcontent">
        <iframe src="Beneficiarios.aspx" style="width: 100%; height: 1000px; border: none" name="formularios" id="iframeBenefi"></iframe>
    </div>
    <div id="InfoEconomi" class="tabcontent">
        <iframe src="Economica.aspx" style="width: 100%; height: 2000px; border: none" name="formularios" id="iframeEconomica"></iframe>
    </div>
    <div id="InfoAdicion" class="tabcontent">
        <iframe src="Adicional.aspx" style="width: 100%; height: 1000px; border: none" name="formularios" id="iframeAdicional"></iframe>
    </div>
    <div id="Finalizar" style="text-align: right; display: none;">
        <asp:Button runat="server" ID="BtnDireccionar" Text="Direccionar" OnClick="redireccionar" UseSubmitBehavior="false" />
    </div>

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <script type="text/javascript" >
        window.onbeforeunload = function (e) {
            var e = e || window.event;
            //e.returnValue = 'sali pext';
            if (window.name == "*ukn*") {
                window.name = "*ukn*";
            }
        }
        $(document).ready(function () {
            //PREGUNTO SI LA PAGINA VIENE DE SER DUPLIACADA
            //coloresTabs("InfoPersonal");
            preventDuplicateTab();
            var boton_guardar = document.formularios.iframeLaboral.contentDocument.getElementById('GuardarLaboral');
            boton_guardar.click();
        });
        function preventDuplicateTab() {
            if (sessionStorage.createTS) {
                if (!window.name) {
                    window.name = "*uknd*";
                    sessionStorage.createTS = Date.now();
                    reCargar();
                } else {
                    if (window.name == "*uknd*") {
                        bloquearPantalla();
                    }
                }
            } else {
                sessionStorage.createTS = Date.now();
                reCargar();
            }
        }
        function reCargar() {
            var currentUrl = window.location.href;
            var url = new URL(currentUrl);
            var desP = url.pathname;
            var p = url.search;
            var dirt = desP + p;
            window.location.href = dirt;
        }
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

            //creamos el div que bloquea grande-
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

        function getRadio(name) {
            var radioObjs = document.formularios.iframePersonal.contentDocument.getElementsByName(name);
            var radioLength = radioObjs.length;
            var result = null;
            for (var i = 0; i < radioLength; i++) {
                if (radioObjs[i].checked) {
                    result = radioObjs[i].value;
                }
            }
            return result;
        }

        function setRadio(name, newValue) {
            var radioObjs = document.formularios.iframePersonal.contentDocument.getElementsByName(name);
            var radioLength = radioObjs.length;
            for (var i = 0; i < radioLength; i++) {
                radioObjs[i].checked = false;
                if (radioObjs[i].value == newValue) {
                    radioObjs[i].checked = true;
                }
            }
        }

        document.getElementById('InfoPersonal').style.display = "inline";

        function openPage(pageName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            var varia = document.getElementById(pageName);
            document.getElementById(pageName).style.display = "inline";
            var tab = "";

            if (pageName == "InfoPersonal")
                tab = "#liPersonal"
            else if (pageName == "InfoLaboral")
            {                
                tab = "#lilaboral"
            }
            else if (pageName == "InfoBenefi")
                tab = "#liBeneficiarios"
            else if (pageName == "InfoEconomi")
                tab = "#lilEconomica"
            else if (pageName == "InfoAdicion")
                tab = "#liAdicional"
            cambiar_tab(tab);

            var ocupa = document.formularios.iframePersonal.contentDocument.getElementById('ddlOcupacion').value;
            console.log(ocupa);
            if(ocupa == '3'){
                var datoslaboral = document.formularios.iframeLaboral.contentDocument.getElementsByClassName('datoEmpleado');
                for (var i = 0; i < datoslaboral.length; i++)
                {
                    datoslaboral[i].style.display = 'none';
                }
            } 
        }
        function cambiar_tab(tab) {
            $('ul.tabs li a').removeClass('active');
            $(tab + ' a').addClass('active');
            $('.secciones article').hide();
            var activeTab = $(tab).attr('href');
            $(activeTab).show();
            return false;
        }
        function Validar(pageName) {            
            var etiqueta = "";
            var valido = true;
            var paseLibre = <%= Convert.ToString(Session[Usuario.codusuario + "cod_per"]) != "" ? Session[Usuario.codusuario + "cod_per"] : "0"%>;
            //Si es Empleado
            var esVisible = $("#lilaboral").is(":visible");

            if ((pageName == "InfoLaboral" && esVisible == true) || (pageName == "InfoBenefi" && esVisible == false)) {
                if (document.formularios.iframePersonal.contentDocument.getElementById('txtIdentificacionE').value == "") {
                    valido = false;
                    etiqueta = "txtIdentificacionE"; 
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddloficina').value == "") {
                    valido = false;
                    etiqueta = "ddloficina";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtFechaexpedicion').value == "") {
                    valido = false;
                    etiqueta = "txtFechaexpedicion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlLugarExpedicion').value == "") {
                    valido = false;
                    etiqueta = "ddlLugarExpedicion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtPrimer_nombreE').value == "") {
                    valido = false;
                    etiqueta = "txtPrimer_nombreE";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtPrimer_apellidoE').value == "") {
                    valido = false;
                    etiqueta = "txtPrimer_apellidoE";
                }
                    //segundo acordeon
                else if (getRadio("txtDireccionE$rbtnDetalleZonaGeo") == null) {
                    valido = false;
                    //alert("VALOR RADIO txtDireccionE = " + getRadio("txtDireccionE$rbtnDetalleZonaGeo"));
                    etiqueta = "txtDireccionE_rbtnDetalleZonaGeo";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtDireccionE_txtDireccion').value == "") {
                    valido = false;
                    etiqueta = "txtDireccionE_txtDireccion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlCiudadResidencia').value == "") {
                    valido = false;
                    etiqueta = "ddlCiudadResidencia";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlBarrioResid').value == "") {
                    valido = false;
                    etiqueta = "ddlBarrioResid";
                }
                    //Tercer acordeon
                else if (getRadio("txtDirCorrespondencia$rbtnDetalleZonaGeo") == null) {
                    valido = false;
                    etiqueta = "txtDirCorrespondencia_rbtnDetalleZonaGeo";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtDirCorrespondencia_txtDireccion').value == "") {
                    valido = false;
                    etiqueta = "txtDirCorrespondencia_txtDireccion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlCiuCorrespondencia').value == "") {
                    valido = false;
                    etiqueta = "ddlCiuCorrespondencia";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlBarrioCorrespondencia').value == "") {
                    valido = false;
                    etiqueta = "ddlBarrioCorrespondencia";
                }
                    //Cuarto Acordeon
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtCelular').value == "") {
                    valido = false;
                    etiqueta = "txtCelular";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtActividadCIIU').value == "") {
                    valido = false;
                    etiqueta = "txtActividadCIIU";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlZona').value == "") {
                    valido = false;
                    etiqueta = "ddlZona";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlPais').value == "") {
                    valido = false;
                    etiqueta = "ddlPais";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtFechanacimiento').value == "") {
                    valido = false;
                    etiqueta = "txtFechanacimiento";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlLugarNacimiento').value == "") {
                    valido = false;
                    etiqueta = "ddlLugarNacimiento";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlsexo').value == "") {
                    valido = false;
                    etiqueta = "ddlsexo";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlNivelEscolaridad').value == "") {
                    valido = false;
                    etiqueta = "ddlNivelEscolaridad";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtProfesion').value == "") {
                    valido = false;
                    etiqueta = "txtProfesion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlEstadoCivil').value == "") {
                    valido = false;
                    etiqueta = "ddlEstadoCivil";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtEstrato').value == "") {
                    valido = false;
                    etiqueta = "txtEstrato";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('ddlOcupacion').value == "") {
                    valido = false;
                    etiqueta = "ddlOcupacion";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtArrendador').value == "") {
                    var dfsd = document.formularios.iframePersonal.contentDocument.getElementById('ViviendaP').value != 'P';
                    if (document.formularios.iframePersonal.contentDocument.getElementById('ViviendaP').value != 'P') {
                        valido = false;
                        etiqueta = "txtArrendador";
                    }
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtTelefonoarrendador').value == "") {
                    valido = false;
                    etiqueta = "txtTelefonoarrendador";
                }
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtAntiguedadlugar').value == "") {
                    valido = false;
                    etiqueta = "txtAntiguedadlugar";
                }
                /*else if (document.formularios.iframePersonal.contentDocument.getElementByName('vivienda').value != "P" && document.formularios.iframePersonal.contentDocument.getElementById('txtValorArriendo').value == "") {
                    valido = false;
                    etiqueta = "txtValorArriendo";
                }*/
                else if (document.formularios.iframePersonal.contentDocument.getElementById('txtEmail').value != "") {
                    var expression = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
                    var re = new RegExp(expression);
                    if (!re.test(document.formularios.iframePersonal.contentDocument.getElementById('txtEmail').value)) {
                        valido = false;
                        etiqueta = "txtEmail";
                    }
                }
                var result = ValidarGridViewActividadCIIU();
                if (!result)
                    valido = false;                               
            }
            else if (pageName == "InfoBenefi" && esVisible == true) {
                if (document.formularios.iframeLaboral.contentDocument.getElementById('txtEmpresa').value == "") {
                    var valido = false;
                    var etiqueta = "txtEmpresa";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlTipoEmpresa').value == "") {
                    var valido = false;
                    var etiqueta = "ddlTipoEmpresa";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('txtCIIUEmpresa').value == "") {
                    var valido = false;
                    var etiqueta = "txtCIIUEmpresa";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlActividadE0').value == "") {
                    var valido = false;
                    var etiqueta = "ddlActividadE0";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlCargo').value == "") {
                    var valido = false;
                    var etiqueta = "ddlCargo";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlTipoContrato').value == "") {
                    var valido = false;
                    var etiqueta = "ddlTipoContrato";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('txtFechaIngreso').value == "") {
                    var valido = false;
                    var etiqueta = "txtFechaIngreso";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlSector').value == "") {
                    var valido = false;
                    var etiqueta = "ddlSector";
                /*} else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlescalafon').value == "") {
                    var valido = false;
                    var etiqueta = "ddlescalafon";*/
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlTipoUbicEmpresa').value == "") {
                    var valido = false;
                    var etiqueta = "ddlTipoUbicEmpresa";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('txtDireccionEmpresa').value == "") {
                    var valido = false;
                    var etiqueta = "txtDireccionEmpresa";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('ddlCiu0').value == "") {
                    var valido = false;
                    var etiqueta = "ddlCiu0";
                } else if (document.formularios.iframeLaboral.contentDocument.getElementById('txtTelCell0').value == "") {
                    var valido = false;
                    var etiqueta = "txtTelCell0";
                }
                //else if (document.formularios.iframeLaboral.contentDocument.getElementById('txtTelefonoempresa').value == "") {
                //    var valido = false;
                //    var etiqueta = "txtTelefonoempresa";
                //}
            }
            else if (pageName == "InfoEconomi") {
                if (document.getElementById('cphMain_lblcodpersona').textContent == "") {
                    var valido = false;
                }
                if (window.parent.document.formularios.iframePersonal.contentDocument.getElementById('ddlEstadoCivil').value == 1 || window.parent.document.formularios.iframePersonal.contentDocument.getElementById('ddlEstadoCivil').value == 3) {
                    if (document.formularios.iframeBenefi.contentDocument.getElementById('txtnombre1_cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtnombre1_cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtapellido1_cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtapellido1_cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtIdent_cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtIdent_cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtFechaExp_Cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtFechaExp_Cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtfechaNac_Cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtfechaNac_Cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('ddlLugNacimiento_Cony').value == "") {
                        var valido = false;
                        var etiqueta = "ddlLugNacimiento_Cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtEstrato_Cony').value == "") {
                        var valido = false;
                        var etiqueta = "txtEstrato_Cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('ddlOcupacion_Cony').value == "") {
                        var valido = false;
                        var etiqueta = "ddlOcupacion_Cony";
                    } else if (document.formularios.iframeBenefi.contentDocument.getElementById('txtemail_cony').value != "") {
                        var expression = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
                        var re = new RegExp(expression);
                        if (!re.test(document.formularios.iframeBenefi.contentDocument.getElementById('txtemail_cony').value)) {
                            var valido = false;
                            etiqueta = "txtemail_cony";
                        }
                    }
                }
            }
            else if (pageName == "InfoAdicion") {
                if ((document.formularios.iframeEconomica.contentDocument.getElementById('txtCodLinApor') != "" || document.formularios.iframeEconomica.contentDocument.getElementById('txtCodLinApor') != "0") && (document.formularios.iframeEconomica.contentDocument.getElementById('txtTipCuoApor') == "4" || document.formularios.iframeEconomica.contentDocument.getElementById('txtTipCuoApor') == "5")) {
                    if (document.formularios.iframeEconomica.contentDocument.getElementById('txtsueldo_soli').value == "") {
                        var valido = false;
                        var etiqueta = "txtsueldo_soli";
                    }
                } else {
                    document.formularios.iframeEconomica.contentDocument.getElementById('txtsueldo_soli').classList.toggle('required');
                }
            }

            //Tab actual
            var tabact = "";
            var atras = false;
            $(".active").each(function (index) {
                tabact = $(this).attr('title');
            });

            if (pageName == "InfoPersonal") {
                if (tabact == "InfoLaboral" && esVisible == true) {
                    if(paseLibre==0){
                        atras = true;
                    }
                }
                else if (tabact == "InfoBenefi" && esVisible == false) {
                    if(paseLibre==0){
                        atras = true;
                    }
                }
            } else if (pageName == "InfoLaboral" && tabact == "InfoBenefi") {
                if(paseLibre==0){
                    atras = true;
                }
            } else if (pageName == "InfoBenefi" && tabact == "InfoEconomi") {
                if(paseLibre==0){
                    atras = true;
                }
            } else if (pageName == "InfoEconomi" && tabact == "InfoAdicion") {
                if(paseLibre==0){
                    atras = true;
                }
            }


            if (tabact == "InfoPersonal") {
                if (pageName == "InfoLaboral" && esVisible == true && valido == true) {
                    valido = true;
                }
                else if (pageName == "InfoBenefi" && esVisible == false && valido == true) {
                    valido = true;
                }else if(paseLibre!=0) {
                    valido = true;
                }else{valido = false;}
            } else if (tabact == "InfoLaboral") {
                if (pageName == "InfoBenefi" && valido == true) {
                    valido = true;
                }else if(paseLibre!=0) {
                    valido = true;
                }else{valido = false;}
            } else if (tabact == "InfoBenefi") {
                if (pageName == "InfoEconomi" && valido == true) {
                    valido = true;
                }else if(paseLibre!=0) {
                    valido = true;
                }else{valido = false;}
            } else if (tabact == "InfoEconomi") {
                if (pageName == "InfoAdicion" && valido == true) {
                    valido = true;
                }else if(paseLibre!=0) {
                    valido = true;
                }else{valido = false;}
            } else if (tabact == "InfoAdicion") {
                if (pageName == "InfoEconomi" && valido == true) {
                    valido = true;
                }else if(paseLibre!=0) {
                    valido = true;
                }else{valido = false;}
            }
            if(paseLibre==0){
                //coloresTabs(pageName);
            }
            if (atras == false) {
                if (valido == true) {
                    if ((pageName == "InfoLaboral" && esVisible == true) || (pageName == "InfoBenefi" && esVisible == false)) {
                        if(paseLibre!=0){
                            navegacion(tabact);
                        }else{
                            navegacion("InfoPersonal");
                            var boton_cargar = document.formularios.iframeBenefi.contentDocument.getElementById('CargarBeneficiario');                       
                            boton_cargar.click(); 
                        }
                        openPage(pageName);
                    }
                    else if (pageName == "InfoBenefi" && esVisible == true) {
                        if(paseLibre!=0){
                            navegacion(tabact);
                        }else{
                            navegacion("InfoLaboral");
                        }
                        openPage(pageName);
                    } else if (pageName == "InfoEconomi") {
                        if(paseLibre!=0){
                            navegacion(tabact);
                        }else{
                            navegacion("InfoBenefi");
                        }
                        openPage(pageName);
                    } else if (pageName == "InfoAdicion") {
                        if(paseLibre!=0){
                            navegacion(tabact);
                        }else{
                            navegacion("InfoEconomi");
                            var btnActEmpresas = document.formularios.iframeAdicional.contentDocument.getElementById('btnActEmpresas');
                            btnActEmpresas.click();
                        }
                        openPage(pageName);
                    }else if(paseLibre!=0 && pageName == "InfoPersonal"){
                        navegacion(tabact);
                        openPage(pageName);
                    }
                } else {
                    //Realiza un foco a la etiqueta que esta vacia
                    if ((pageName == "InfoLaboral" && esVisible == true) || (pageName == "InfoBenefi" && esVisible == false))
                        document.formularios.iframePersonal.contentDocument.getElementById(etiqueta).focus();
                    else if (pageName == "InfoBenefi" && esVisible == true)
                        document.formularios.iframeLaboral.contentDocument.getElementById(etiqueta).focus();
                    else if (pageName == "InfoEconomi")
                        document.formularios.iframeBenefi.contentDocument.getElementById(etiqueta).focus();
                    else if (pageName == "InfoAdicion")
                        document.formularios.iframeEconomica.contentDocument.getElementById(etiqueta).focus();
                    else if (pageName == "Terminar")
                        document.formularios.iframeAdicional.contentDocument.getElementById(etiqueta).focus();
                }
            }
            else {
                openPage(pageName);
            }

        }
        function coloresTabs(page){
            switch(page){
                case "InfoPersonal":
                    $("#lilEconomica").css("background", "#C3CFC3");
                    $("#liAdicional").css("background", "#C3CFC3");
                    $("#liBeneficiarios").css("background", "#C3CFC3");
                    break;
                case "InfoLaboral":
                    $("#liAdicional").css("background", "#C3CFC3");
                    $("#liBeneficiarios").css("background", "white");
                    break;
                case "InfoBenefi":
                    $("#lilEconomica").css("background", "white");
                    $("#liPersonal").css("background", "#C3CFC3");
                    break;
                case "InfoEconomi":
                    $("#liAdicional").css("background", "white");
                    $("#liPersonal").css("background", "#C3CFC3");
                    $("#lilaboral").css("background", "#C3CFC3");
                    break;
                case "InfoAdicion":
                    break;
            }
            if(page=="sdfs"){
                $("#InfoBenefi").css("background", "white");
            }
        }
        function navegacion(paginaGuardar){
            switch(paginaGuardar){
                case "InfoPersonal":
                    var boton_guardar = document.formularios.iframePersonal.contentDocument.getElementById('GuardarPersonal');
                    boton_guardar.click();
                    break;
                case "InfoLaboral":
                    var boton_guardar = document.formularios.iframeLaboral.contentDocument.getElementById('GuardarLaboral');
                    boton_guardar.click();
                    break;
                case "InfoBenefi":
                    var boton_guardar = document.formularios.iframeBenefi.contentDocument.getElementById('GuardarBeneficiarios');
                    boton_guardar.click(); 
                    break;
                case "InfoEconomi":
                    var boton_guardar = document.formularios.iframeEconomica.contentDocument.getElementById('GuardarEconomica');
                    boton_guardar.click();
                    break;
            }
        }
        /*
        SI SE CAMBIA ALGUNA COSA DE LA GRIDVIEW TAB PERSONAL/ GRIDVIEW ACTIVIDAD CIIU TENER EN CUENTA ESTA VALIDACION Y REALIZAR LOS AJUSTES
        */
        function ValidarGridViewActividadCIIU() {
            var result = true;
            var tblActivididad = document.formularios.iframePersonal.contentDocument.getElementById('gvActividadesCIIU');
            //var tblActivididad = document.getElementById(' gvActividadesCIIU.ClientID ');
            var tblCurrentRow;
            var tblCellRow1;
            var tblCellRow2;
            var tblCellRow3;
            var tblElementLbl;
            var tblElementChk1;
            var tblElementChk2;
            var NumActiSeleccionadas = 0;
            var ActPrincipalSeleccionada = false;
            debugger;
            for (var i = 1; i < tblActivididad.rows.length; i++) {
                tblCurrentRow = tblActivididad.rows[i];
                tblCellRow1 = tblCurrentRow.childNodes[2];
                tblCellRow2 = tblCurrentRow.childNodes[3];
                tblCellRow3 = tblCurrentRow.childNodes[4].childNodes[1];
                if (tblCellRow1 != null)
                    tblElementLbl = tblCellRow1.childNodes[1];
                if (tblCellRow2 != null)
                    tblElementChk1 = tblCellRow2.childNodes[1];
                if (tblCellRow3 != null) {
                    tblElementChk2 = tblCellRow3.childNodes[0];
                }

                if (tblElementChk1.checked) {
                    ActPrincipalSeleccionada = true;
                }
                if (tblElementChk2.checked) {
                    if (tblElementChk1.checked) {
                        alert('La actividad económica ' + tblElementLbl.innerHTML + ' fue seleccionada tanto como principal como secundaria');
                        result = false;
                    }
                    NumActiSeleccionadas++;
                }
            }
            if (!ActPrincipalSeleccionada) {
                alert('La activida económica principal no fue seleccionada');
                result = false;
            }
            if (NumActiSeleccionadas > 3) {
                alert('Se han seleccionado mas de 3 actividades económicas secundarias');
                result = false;
            }
            return result;
        }

    </script>
</asp:Content>

