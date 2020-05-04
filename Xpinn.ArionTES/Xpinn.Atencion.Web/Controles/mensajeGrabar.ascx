<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mensajeGrabar.ascx.cs" Inherits="mensajeGrabar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .modalBackground
{
    background-color:black;
    filter:alpha(opacity=50);
    opacity:0.5;
}
.pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:500px;
    text-align:center;
    background-color:White;
}
</style>

<asp:HiddenField ID="hfMensaje" runat="server" />    

<asp:ModalPopupExtender ID="mpeMensaje" runat="server" DropShadow="True" Drag="True" BackgroundCssClass="modalBackground" 
    PopupControlID="panelMensaje" TargetControlID="hfMensaje" >
</asp:ModalPopupExtender>

<asp:Panel ID="panelMensaje" runat="server" BackColor="White" CssClass="pnlBackGround" Style="text-align: right;padding:5px" BorderWidth="1px" Width="500px" >
    <div id="popupcontainer" style="width: 500px">
        <table style="width: 100%;">
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium;">
                    <asp:Label ID="lblMensaje" runat="server" 
                        style="font-size: medium; text-align: center" Width="100%"></asp:Label>
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
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" Width="110px" style="padding:5px 10px;border-radius:0" CssClass="btn btn-primary"
                        onclick="btnContinuar_Click" BorderStyle="Outset" BorderWidth="1px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="110px" style="padding:5px 10px;border-radius:0" CssClass="btn btn-primary"
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
