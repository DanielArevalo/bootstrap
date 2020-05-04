<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlConfirmar.ascx.cs" Inherits="ctlConfirmar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:HiddenField ID="hfMensaje" runat="server" />    

<asp:ModalPopupExtender ID="mpeMensaje" runat="server"  DropShadow="True" Drag="True" 
    PopupControlID="panelMensaje" TargetControlID="hfMensaje"
    BackgroundCssClass="backgroundColor" >
</asp:ModalPopupExtender>

<asp:Panel ID="panelMensaje" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
    <div id="popupcontainer" style="width: 500px">
        <table style="width: 100%;">
            <tr>
                <td colspan="3" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium;">
                   <br />
                    <asp:Label ID="lblMensaje" runat="server" 
                        style="font-size: medium" Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnCtlAceptar" runat="server" Text="Aceptar" Width="120px" 
                        onclick="btnCtlAceptar_Click" BorderStyle="Outset" BorderWidth="1px" />
                    <asp:Button ID="btnCtlCancelar" runat="server" Text="Cancelar" Width="120px" 
                        onclick="btnCtlCancelar_Click" BorderStyle="Outset" BorderWidth="1px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    &nbsp;</td>
            </tr>
        </table>       
    </div>
</asp:Panel> 
