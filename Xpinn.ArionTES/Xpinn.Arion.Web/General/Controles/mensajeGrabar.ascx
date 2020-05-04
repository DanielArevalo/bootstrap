<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mensajeGrabar.ascx.cs" Inherits="mensajeGrabar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .style1
    {
        height: 2px;
    }
    .ocultar
    {
        display: none;
    }
</style>

<script type="text/javascript">

    function deshabilitar(boton) {
        //document.getElementById(boton).disabled = true;
        document.getElementById(boton).style.display = 'none';
        document.getElementById('cphMain_ctlMensaje_btnCancelar').style.display = 'none';
        document.getElementById('btnGuardar').style.display = 'none';
        //document.getElementById('<%= btnClone.ClientID %>').style.display = 'inline';
    }
</script>

<asp:HiddenField ID="hfMensaje" runat="server" />    

<asp:ModalPopupExtender ID="mpeMensaje" runat="server"  DropShadow="True" Drag="True" 
    PopupControlID="panelMensaje" TargetControlID="hfMensaje"
    BackgroundCssClass="backgroundColor" >
</asp:ModalPopupExtender>

<asp:Panel ID="panelMensaje" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
    <div id="popupcontainer" style="width: 500px">
        <table style="width: 100%;">
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium;">
                    <asp:Label ID="lblMensaje" runat="server" 
                        style="font-size: medium" Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="5" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnClone" runat="server" Text="Continuar" Width="100px" Enabled="false" CssClass="ocultar"
                        BorderStyle="Outset" BorderWidth="1px" />
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" Width="100px" Enabled="true" onclientclick="deshabilitar(this.id)"
                        onclick="btnContinuar_Click" BorderStyle="Outset" BorderWidth="1px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="100px" 
                        onclick="btnCancelar_Click" BorderStyle="Outset" BorderWidth="1px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" class="style1" height="5" >
                    </td>
            </tr>
        </table>       
    </div>
</asp:Panel> 
