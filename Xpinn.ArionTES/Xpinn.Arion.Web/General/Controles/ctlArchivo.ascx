<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlArchivo.ascx.cs" Inherits="ctlArchivo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:HiddenField ID="hfArchivo" runat="server" />    

<asp:ModalPopupExtender ID="mpeArchivo" runat="server"  DropShadow="True" Drag="True" 
    PopupControlID="panelArchivo" TargetControlID="hfArchivo"
    BackgroundCssClass="backgroundColor" >
</asp:ModalPopupExtender>

<asp:Panel ID="panelArchivo" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
    <div id="popupcontainer" style="width: 500px">
        <table style="width: 100%;">
            <tr>
                <td colspan="3" style="line-height: 10px" >
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium;">
                    <strong>Descripción</strong><br />
                    <asp:TextBox ID="txtDescripcion" runat="server" Width="400px" CssClass="textbox"></asp:TextBox><br />
                    <asp:FileUpload ID="fuArchivo" runat="server" Width="400px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="line-height: 10px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptarArchivo" runat="server" Text="Aceptar" Width="108px" 
                        onclick="btnAceptarArchivo_Click" BorderStyle="Outset" BorderWidth="1px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="line-height: 10px">
                    &nbsp;</td>
            </tr>
        </table>       
    </div>
</asp:Panel> 