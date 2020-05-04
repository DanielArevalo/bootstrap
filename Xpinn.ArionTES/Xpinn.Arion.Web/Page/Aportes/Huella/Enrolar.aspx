<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<header>
    <title>Biometria - Enrolar</title>
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js" type="text/javascript"> </script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />     
    <script type="text/javascript">
        function cerrar() {
            this.close();
        }

        function captureFP() {
            debugger;
            CallSGIFPGetData(SuccessFunc, ErrorFunc);
        }

        function fnOpenDevice() {
            document.frmmain.objFP[0].DeviceID = 0;
            document.frmmain.objFP[0].CodeName = 3;
            document.frmmain.objFP[0].MinutiaeMode = 256;
            document.frmmain.btnCapture.disabled = false;
            document.frmmain.btnopen.disabled = true;            
            // template format of objVerify should be the same to those of objFP[]
            objVerify.MinutiaeMode = 256;
            document.frmmain.lblMensaje.value = 'Ponga el dedo en el lector para capturar su huella';
            return;
        }


        

        function CallSGIFPGetData(successCall, failCall) {
            // 8.16.2017 - At this time, only SSL client will be supported.
            var uri = "https://localhost:8443/SGIFPCapture";

            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    fpobject = JSON.parse(xmlhttp.responseText);
                    successCall(fpobject);
                }
                else if (xmlhttp.status == 404) {
                    failCall(xmlhttp.status)
                }
            }
            //    var secugen_lic = "hE/78I5oOUJnm5fa5zDDRrEJb5tdqU71AVe+/Jc2RK0=";   // webapi.secugen.com
            var secugen_lic = "";
            var params = "Timeout=" + "10000";
            params += "&Quality=" + "50";
            params += "&licstr=" + encodeURIComponent(secugen_lic);
            params += "&templateFormat=" + "ISO";
            console.log
            xmlhttp.open("POST", uri, true);
            xmlhttp.send(params);

            xmlhttp.onerror = function () {
                failCall(xmlhttp.statusText);
            }
        }



        function SuccessFunc(result) {
            if (result.ErrorCode == 0) {
                /* 	Display BMP data in image tag
                    BMP data is in base 64 format 
                */
                if (result != null && result.BMPBase64.length > 0) {
                    document.getElementById("FPImage1").src = "data:image/bmp;base64," + result.BMPBase64;
                }
                var tbl = "<table border=1>";
                tbl += "<tr>";
                tbl += "<td> Dispositivo </td>";
                tbl += "<td> <b>" + result.SerialNumber + "</b> </td>";
                tbl += "</tr>";
                tbl += "<tr>";
                tbl += "<td> Imagen Ancho</td>";
                tbl += "<td> <b>" + result.ImageHeight + "</b> </td>";
                tbl += "</tr>";
                tbl += "<tr>";
                tbl += "<td> Imagen Alto</td>";
                tbl += "<td> <b>" + result.ImageWidth + "</b> </td>";
                tbl += "</tr>";
                tbl += "<tr>";
                tbl += "<td>Resolucion</td>";
                tbl += "<td> <b>" + result.ImageDPI + "</b> </td>";
                tbl += "</tr>";                               
                tbl += "<tr>";
                tbl += "<td> Template(base64)</td>";
                tbl += "<td> <b> <textarea rows=8 cols=50>" + result.TemplateBase64 + "</textarea></b> </td>";
                tbl += "</tr>";
                tbl += "</table>";
                document.getElementById('result').innerHTML = tbl;
            }
            else {
                alert("Fingerprint Capture Error Code:  " + result.ErrorCode + ".\nDescription:  " + ErrorCodeToString(result.ErrorCode) + ".");
            }
        }

        function ErrorFunc(status) {

            /* 	
                If you reach here, user is probabaly not running the 
                service. Redirect the user to a page where he can download the
                executable and install it. 
            */
            alert("Check if SGIBIOSRV is running; Status = " + status + ":");

        }

    </script>
    <script type="text/javascript">
        function fnCapture() {
            document.frmmain.objFP[0].Capture();
            var result = document.frmmain.objFP[0].ErrorCode;
            if (result == 0) {
                var strmin = document.frmmain.objFP[0].MinTextData;
                document.frmmain.min[0].value = strmin;
                document.frmmain.btnguardar.disabled = false;              
            }
            else
                alert('failed - ' + result);
            return;
        }
    </script>
    <script type="text/javascript">
        function fnSetimage() {
            document.frmmain.objFP[0].ImageTextData = document.frmmain.img1.value;
            return;
        }
    </script>
    <script type="text/javascript">
        function CallService() {
            // Determinar el dedo al que corresponde la huella
            var formulario = document.forms[0];
            for (var i = 0; i < formulario.cosa.length; i++) {
                if (formulario.cosa[i].checked) {
                    break;
                }
            }
            // Determinar el template
            var strmin1 = document.frmmain.min[0].value;            
            // Determinar la identificación
            var variable_param = gup('variable');
            var soapMessage = '<?xml version="1.0" encoding="utf-8"?><soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">  <soap:Body>    <GrabarHuella xmlns="http://tempuri.org/">      <pIdentificacion>' + variable_param + '</pIdentificacion>      <pDedo>' + formulario.cosa[i].value + '</pDedo>      <pHuella>' + strmin1 + '</pHuella>      <pHuellaTemplate>' + strmin1 + '</pHuellaTemplate>    </GrabarHuella>  </soap:Body></soap:Envelope>';
            $.ajax({
                url: "http://Xpinnserver/WebServices/WSBiometria.asmx?op=GrabarHuella",
                type: "POST",
                dataType: "xml",
                data: soapMessage,
                contentType: "text/xml; charset=\"utf-8\"",
                success: OnSuccess,
                error: OnError
            });
            return false;
        }
    </script>
    <script type="text/javascript">
    

        function OnSuccess(data, status) {
            var variable_param = gup('variable');          
            $("#resultado_json").empty();
            $("#resultado_json").append("Identificacion: " + variable_param + "<br>");
            alert("Datos Grabados Correctamente");
        }

        function OnError(request, status, error) {
            alert('No se pudo grabar la huella. Error:' + error);
       
        }
        function gup( name ){
	        var regexS = "[\\?&]" + name + "=([^&#]*)";
	        var regex = new RegExp ( regexS );
	        var tmpURL = window.location.href;
	        var results = regex.exec( tmpURL );
	        if( results == null )
		        return "";
	        else
		        return results[1];
        }

        $(document).ready(function () {
            alert("Ingreso a la Huella");
            var variable_param = gup('variable');           
            jQuery.support.cors = true;
            $("#resultado_json").empty();
            $("#resultado_json").append("Identificacion: " + variable_param + "<br>");
        });

    </script>
</header>
<body style="overflow-y:hidden; overflow-x:hidden;">
    <div style="background: url('../Images/backgroundHeader.jpg') repeat-x #359af2;">
        <asp:Image ID="Image1" ImageUrl="logoInterna.jpg" runat="server" Height="32px" Width="69px"></asp:Image>
    </div>
    <object id="objVerify" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:8D613732-7D38-4664-A8B7-A24049B96117"
        name="objVerify" viewastext>
    </object>    
    <form name="frmmain" style="width: 100%">
        <div style="text-align: center; width: 100%; color: #359af2; font-size: 24px; font-weight: normal; font-family: 'Segoe UI, Arial, Helvetica, Verdana, sans-serif';">
            Capturar Huella
        </div><br />
        <table style="width: 550px">
            <tr>
                <td>
                    <input type="radio" name="cosa" value="1" checked="checked" /><span style="font-size: x-small">1</span>
                    <input type="radio" name="cosa" value="2" /><span style="font-size: x-small">2</span>
                    <input type="radio" name="cosa" value="3" /><span style="font-size: x-small">3</span>
                    <input type="radio" name="cosa" value="4" /><span style="font-size: x-small">4</span>
                    <input type="radio" name="cosa" value="5" /><span style="font-size: x-small">5</span>
                </td>
                <td></td>
                <td>

                    <input type="radio" name="cosa" value="6" /><span style="font-size: x-small">6</span>
                    <input type="radio" name="cosa" value="7" /><span style="font-size: x-small">7</span>
                    <input type="radio" name="cosa" value="8" /><span style="font-size: x-small">8</span>
                    <input type="radio" name="cosa" value="9" /><span style="font-size: x-small">9</span>
                    <input type="radio" name="cosa" value="10" /><span style="font-size: x-small">10</span>
                </td>
            </tr>
            <tr>
                <td>

                    <img src="Manoizquierda.png" style="border-style: ridge; width: 135px; height: 180px; visibility: inherit;" />
                </td>
                <td>
                    <img id="FPImage1" alt="Huella" height="220" width="135">
                    
                </td>
                <td>
                    <img src="Manoderecha.png" style="border-style: ridge; width: 135px; height: 180px; margin-left: 5px; margin-top: 0px; visibility: inherit;" />
                </td>

            </tr>
        </table>

        <div style="text-align: center; width: 100%">
            <input type="button" name="btnopen" value="Inicializar Lector" onclick="fnOpenDevice();"                 
                style="font-size: x-small; background-color: #0099FF; height: 28px; color: #FFFFFF; width: 95px;" />
            <input type="button" name="btnCapture" value="Capturar" onclick="captureFP()"                 
                style="font-size: x-small; background-color: #0099FF; height: 28px; color: #FFFFFF; width: 95px;" />
            <input type="button" name="btnguardar" value="Guardar"  onclick="fnCapture();"               
                style="font-size: x-small; background-color: #0099FF; height: 28px; color: #FFFFFF; width: 95px;"/>
            <input type="button" name="btncerrar" value="Cerrar" onclick="cerrar();"               
                style="font-size: x-small; background-color: #0099FF; height: 28px; color: #FFFFFF; width: 95px;"/>
            <div id='resultado_json' />
            <input type="text" name="min" style="visibility:hidden" src="chrome-extension://hehijbfgiekmjfkfjpbkbammjbdenadd/nhc.htm#url=https://Xpinnserver/cooaceded/Page/Aportes/Huella/Enrolar.aspx" />        
            <input type="hidden" name="min" style="visibility:hidden"/>
        </div>
        <div style="text-align: left; width: 100%">
            <input id="lblMensaje" type="text" style="border-style: none; width: 100%; color: #008000; text-decoration: blink;"
                value="Conecte el lector al PC y presione botón Inicializar" disabled="disabled" />            
        </div>
         <p id="result"/>
    </form>
    </body>
</html>