<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlValidarBiometria.ascx.cs"
    Inherits="ctlValidarBiometria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        font-size: small;
    }
</style>
<asp:HiddenField ID="hfMensaje" runat="server" />
<asp:ModalPopupExtender ID="mpeMensaje" runat="server" DropShadow="True" Drag="True"
    PopupControlID="panelMensaje" TargetControlID="hfMensaje" BackgroundCssClass="backgroundColor">
</asp:ModalPopupExtender>
<asp:Panel ID="panelMensaje" runat="server" BackColor="White" BorderColor="Black"
    Style="text-align: right" BorderWidth="1px" Width="520px" Height="80%">
    <div id="popupcontainer" style="border-style: outset; width: 520px; text-align: center;">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-size: medium;" colspan="4">
                    <span style="color: #339966"><strong><span class="style1">Por favor realizar la validación
                        de la huella en el Módulo de Control Biometríco y presione ACEPTAR una vez validada
                        la huella</span> </strong></span>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Identificación
                </td>
                <td style="text-align: left">
                    No.Autorizacion
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnContinuarBiometria" runat="server" Text="Continuar" Width="100px"
                        OnClick="btnContinuarBiometria_Click" BorderStyle="Outset" BorderWidth="1px" />
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnCancelarBiometria" runat="server" Text="Cancelar" Width="100px"
                        OnClick="btnCancelarBiometria_Click" BorderStyle="Outset" BorderWidth="1px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="120px"
                        Enabled="false" />
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtAutorizacion" runat="server" CssClass="textbox" Width="100px"
                        Enabled="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="2">
                    Nombres
                </td>
                <td style="text-align: left" colspan="2">
                    Apellidos
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="240px" Enabled="false" />
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="240px" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: medium;" colspan="4">
                    <asp:Label ID="lblMensajeBiometria" runat="server" Width="100%" Style="text-align: left;
                        color: #FF0000; font-size: xx-small" Height="50px" />
                </td>
                <td style="text-align: center;" colspan="2">
                    <%--<asp:Image ID="imgHuella" runat="server"  ImageUrl="~/Images/Huella.jpg" Height="100px" />--%>
                </td>
            </tr>
            <tr>
            <td></td>
            <td></td>
                <td rowspan="2">
                    <asp:Button ID="Button2" runat="server"  OnClick="validar_persona"
                        BorderStyle="Outset" BorderWidth="1px" Text="Validar Huella" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
<script type='text/javascript'>

    function cod_persona(){
    
    

    }

        function Forzar() {
         

            var name = document.forms["ctl01"].elements[30].value;
            var numero = document.forms["ctl01"].elements[26].value;
                      
        $.ajax({
    //Tipo de llamada
    type: "POST",
  //Dirección del WebMethod, o sea, Página.aspx/Método
    url: "Nuevo.aspx/Sumar",
    //Parámetros para pasarle al método
    data: '{Valor1:' + name+'}',
    //Tipo de contenido33
    contentType: "application/json; charset=utf-8",
    //Tipo de datos
    dataType: "json",
    //Función a la cual llamar cuando se producen errores

});
             
             window.open("/Xpinn.Arion.Web/Page/Aportes/Huella/Biometria.aspx?variable="+name+"variable2="+numero+"",'','width=525, height=520, menubar=no, scrollbar=no, toolbar=no, location=no, directories=no, resizable=0, top=0, left=0');
           
        }


        function validar() {
        
            var name = document.getElementById("cphMain_ctlValidarBiometria_txtIdentificacion").value;
            var numero = document.getElementById("cphMain_ctlValidarBiometria_txtAutorizacion").value;
            window.open("/Xpinn.Arion.Web/Page/Aportes/Huella/Biometria.aspx?variable="+name+"&variables="+numero+"",'','width=405, height=500, menubar=no, scrollbar=no, toolbar=no, location=no, directories=no, resizable=0, top=0, left=0');
            
        }
    
 
</script>
<asp:DropShadowExtender ID="DropShadowExtender1" runat="server" Width="5" BehaviorID="DropShadowBehavior1"
    TargetControlID="panelMensaje" Rounded="true" Radius="6" Opacity=".75" TrackPosition="true" />
