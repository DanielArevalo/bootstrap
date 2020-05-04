<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Biometria.aspx.cs" Inherits="Page_Aportes_Biometria_Biometria" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<header>
    <title>Biometria</title>
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js" type="text/javascript"> </script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
     
    <script lang=javascript>
        function cerrar() {
            this.close();
        }
        
        var aaa = 0;
        function fnOpenDevice() {

            document.frmmain.btnCapture1.disabled = false;




            for (i = 0; i < 3; i++) {
                document.frmmain.objFP[i].DeviceID = document.frmmain.inc.value;
                document.frmmain.objFP[i].CodeName = document.frmmain.dev.value;
                document.frmmain.objFP[i].MinutiaeMode = document.frmmain.templateFormat.value;

            }
            document.frmmain.open.disabled = true;
            // template format of objVerify should be the same to those of objFP[]
            objVerify.MinutiaeMode = document.frmmain.templateFormat.value;
            return;
        }



        function fnCapture(idx) {

            document.frmmain.objFP[idx].Capture();
            var result = document.frmmain.objFP[idx].ErrorCode;
            if (result == 0) {

                var strmin = document.frmmain.objFP[idx].MinTextData;
                document.frmmain.min[idx].value = strmin;
                document.frmmain.btnguardar.disabled = false;
                document.frmmain.btnCapture1.disabled = true;

            }
            else
                alert('failed - ' + result);
            return;
        }

        function fnRegister(e) {
            var strmin1 = document.frmmain.min[0].value;
            var strmin2 = document.frmmain.min[1].value;
            if (objVerify.RegisterForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                alert('susses - ' + objVerify.ErrorCode);
            } else
                alert('failed - ' + objVerify.ErrorCode);
            e.preventdefault();
        }

        function fnVerifyEx() {
            var strmin1 = document.frmmain.min[0].value;
            var strmin2 = document.frmmain.min[1].value;
            var strmin3 = document.frmmain.min[2].value;
            if (objVerify.VerifyExForText(strmin1, strmin2, strmin3) && objVerify.ErrorCode == 0)
                alert('Success - matched');
            else
                alert('Failed - ' + objVerify.ErrorCode);
            return;
        }

        function fnVerify() {
            var strmin1 = document.frmmain.min[0].value;
            var strmin2 = document.frmmain.min[2].value;
            if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                alert('sucess - ' + objVerify.ErrorCode);
            }
            else
                alert('Failed - ' + objVerify.ErrorCode);
            return;
        }

        function fnSetimage() {
            document.frmmain.objFP.ImageTextData = document.frmmain.img1.value;
            return;
        }



        function CallService() {

            var formularios = document.forms["frmmain"].elements[11].value;
            if (formularios = 1) {
                var formulario = document.forms[0];
                for (var i = 0; i < formulario.cosa.length; i++) {
                    if (formulario.cosa[i].checked) {
                        break;
                    }
                }
            }
            var porElementos = document.forms["frmmain"].elements[10].value;
            var strmin1 = document.frmmain.min[0].value;
            var webServiceURL = 'http://Xpinnserver/WebServices/WSBiometria.asmx?op=ValidarHuellaSECUGEN';
            var variable_param = gup('variable');

            var soapMessage = '<?xml version="1.0" encoding="utf-8"?><soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">  <soap:Body>    <ValidarHuellaSECUGEN xmlns="http://tempuri.org/">      <pIdentificacion>' + variable_param + '</pIdentificacion>      <pHuella>' + strmin1 + '</pHuella>      <pHuellaTemplate>' + strmin1 + '</pHuellaTemplate>      <pDedo>3</pDedo>      <pMatch_score>45</pMatch_score>    </ValidarHuellaSECUGEN>  </soap:Body></soap:Envelope>';
            $.ajax({
                url: "http://Xpinnserver/WebServices/WSBiometria.asmx?op=ValidarHuellaSECUGEN",
                type: "POST",
                dataType: "xml",
                data: soapMessage,
                contentType: "text/xml; charset=\"utf-8\"",
                success: OnSuccess,
                error: OnError
            });
            return false;
        }

        function OnSuccess(data, status) {
            var variable_param = gup('variable');
            var variables_params = gups('variables');

            $("#resultado_json").empty();
            $("#resultado_json").append("Identificación: " + variable_param + "<br>");
            $("#resultados_json").empty();
            $("#resultados_json").append("Numero Autorización: " + variables_params + "<br>");
            alert("Datos Grabados Correctamente");
        }

        function OnError(request, status, error) {
            alert('error');
        }


        function gup(name) {
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var tmpURL = window.location.href;
            var results = regex.exec(tmpURL);
            if (results == null)
                return "";
            else
                return results[1];
        }

        function gups(numero) {
            var regexS = "[\\?&]" + numero + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var tmpURL = window.location.href;
            var results = regex.exec(tmpURL);
            if (results == null)
                return "";
            else
                return results[1];
        }

        $(document).ready(function () {
            var variable_param = gup('variable');
            var variables_params = gups('variables');

            jQuery.support.cors = true;
            $("#resultado_json").empty();
            $("#resultado_json").append("Identificación: " + variable_param + "<br>");
            $("#resultados_json").empty();
            $("#resultados_json").append("Numero Autorización: " + variables_params + "<br>");
        });

    </script>
    <style type="text/css">
        .style1
        {
            width: 16px;
            align: center;
        }
        .style2
        {
            width: 23px;
        }
        .style3
        {
            color: #0000FF;
        }
        </style>
</header>
<body style="overflow-y:hidden; overflow-x:hidden;">
    <img src="../../../Images/logoInterna.jpg" 
        style="width: 134px; height: 55px;" /><br />
    <br />
    <object id="objVerify" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:8D613732-7D38-4664-A8B7-A24049B96117"
        name="objVerify" viewastext>
    </object>
    <strong><span class="style1" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
        class="style3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Validar Huella&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></strong><br />
    <form name="frmmain" style="width: 562px">

        <input type="radio" name="cosa" value="1" checked="checked" style="visibility:hidden" 
            />&nbsp;
        <input type="radio" name="cosa" value="2" style="visibility:hidden" />&nbsp;
        <input type="radio" name="cosa" value="3" style="visibility:hidden" />&nbsp;
        <input type="radio" name="cosa" value="4" style="visibility:hidden" />&nbsp;
        <input type="radio" name="cosa" value="5" style="visibility:hidden" />&nbsp;
        <select name="dev" style="visibility: hidden">
            <option value="3" style="visibility: hidden" />
            FDU04
        </select>
        <select name="inc" style="visibility: hidden; margin-left: 0px;">
            <option value="-1">-1</option>
            <option value="0">0</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
        </select>
        <input type="radio" name="cosa" value="6" style="visibility:hidden" >&nbsp;
        <input type="radio" name="cosa" value="7" style="visibility:hidden">&nbsp;
        <input type="radio" name="cosa" value="8" style="visibility:hidden">&nbsp;
        <input type="radio" name="cosa" value="9" style="visibility:hidden">&nbsp;
        <input type="radio" name="cosa" value="10" style="visibility:hidden">&nbsp;
        <select name="templateFormat" style="visibility: hidden">
            <option value="256">ANSI 378</option>
        </select>
        <br />
        <table style="width: 525px">
            <caption align="left" style="width: 524px">
            <img src="Manoizquierda.png"                   
                style="width: 119px; height: 180px; visibility: hidden;" />
            <object id="objFP0" style="left: 0px; width: 149px; top: 0px; height: 182px; text-align: center;"
                height="182" width="149" classid="CLSID:D547FDD7-82F6-44e8-AFBA-7553ADCEE7C8"
                name="objFP" viewastext>
                <param name="CodeName" value="1">
            </object>
            <object id="objFP">
                <param name="CodeName" value="1">
            </object>
            <img src="2.png"     
                
                    style="width: 143px; height: 177px; margin-left: 5px; margin-top: 0px; visibility: hidden;" /></caption>
            <tr>
                <td>
                    <strong align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    </strong>&nbsp;</td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                </td>
            <tr>
                <td>
                    <strong align="left">&nbsp;&nbsp; </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div>
            &nbsp;<input 
                type="button" name="open" value="Inicializar Lector" onclick="fnOpenDevice();" 
                
                
                style="width: 100px; font-size: x-small; background-color: #0099FF; height: 30px; margin-left: 4px; color: #FFFFFF;" />&nbsp;&nbsp;
            <input type="button" name="btnCapture1" value="Capturar" 
                onclick="fnCapture(0);"   disabled
                
                style="font-size: x-small; background-color: #0099FF; height: 32px; color: #FFFFFF;" />&nbsp;&nbsp;&nbsp;
            <input type="button" name="btnguardar" value="Guardar" 
                onclick="CallService(); return false;"   
            
                style="font-size: x-small; background-color: #0099FF; height: 29px; color: #FFFFFF;"/>

                <input type="button" name="btncerrar" value="Cerrar" onclick="cerrar();"               
                style="font-size: x-small; background-color: #0099FF; height: 28px; color: #FFFFFF; width: 95px;"/>
            <div id='resultado_json' />
        </div>
        <div id='resultados_json' />
        </div>
        <br />
        <input type=text name=min style="visibility:hidden" src="chrome-extension://hehijbfgiekmjfkfjpbkbammjbdenadd/nhc.htm#url=http://localhost:3870/Xpinn.Arion.Web/Page/Aportes/Biometria/master.aspx"><br>
        <input type="hidden" name=min style="visibility:hidden"><br>
    </form>
    <form name="textos" style="width: 562px">       
              <strong id="inicializar" >Conecte el lector al PC y presione botón para Inicializar</strong>
               <strong id="Strong1" style="visibility:hidden">Ponga el dedo en el lector para capturar su huella</strong>
              </form> 
    &nbsp;                                   
    </body>
</html>