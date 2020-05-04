<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlFormatoDocum.ascx.cs" Inherits="ctlFormatoDocum" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:HiddenField ID="hfFormatos" runat="server" />
<asp:ModalPopupExtender ID="mpeFormatos" runat="server" DropShadow="True" Drag="True"
    PopupControlID="panelFormatos" TargetControlID="hfFormatos" BackgroundCssClass="backgroundColor">
</asp:ModalPopupExtender>
<asp:Panel ID="panelFormatos" runat="server" BackColor="White" Style="text-align: right;
    padding: 10px" BorderWidth="1px" Width="500px">
    <asp:UpdatePanel ID="upFormatos" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblTipo" runat="server" Visible="false" />
                        <asp:ImageButton ID="btnCancFormat" runat="server" ImageUrl="~/Images/gr_elim.jpg"
                            ToolTip="Eliminar" OnClick="btnCancFormat_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #2BA7FF">
                        <strong>Seleccione el Formato a Imprimir</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center">
                        <asp:DropDownList ID="ddlFormatosImp" runat="server" CssClass="textbox" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;<asp:Label ID="lblErrorFI" runat="server" Visible="false" Style="color: Red;
                            font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnImpresion" runat="server" Text="Continuar" Width="150px" Height="28px"
                            BorderStyle="Outset" BorderWidth="1px" OnClick="btnImpresion_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>

