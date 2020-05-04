<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlGiro.ascx.cs" Inherits="ctlGiro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    
.textbox
{
    background: #f4f5ff;
    border: 1px solid #d7e6e9;
    padding: 4px;
    font-size: x-small;
    font-family: "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
    margin-left: 0px;
    color: #000066;
    text-align: left;
}

</style>

<asp:UpdatePanel ID="upFormaDesembolso" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td style="width: 175px; text-align: left">
                    <strong>Forma de Desembolso</strong>
                </td>
                <td style="height: 25px">

                    <asp:DropDownList 
                        ID="DropDownFormaDesembolso" 
                        runat="server" 
                        Style="margin-left: 0px;
                        text-align: left" 
                        Width="320px" 
                        Height="28px" 
                        CssClass="textbox" 
                        AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownFormaDesembolso_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
        </table>
        <asp:Panel ID="panelCheque" runat="server">
            <table>
                <tr>
                    <td style="width: 175px; text-align: left">
                        Banco de donde se Gira
                    </td>
                    <td style="height: 25px">
                        <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left" Height="28px"
                            Width="320px" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 175px; text-align: left; height: 25px;">
                        Cuenta de donde se Gira
                    </td>
                    <td style="height: 25px">
                        <asp:DropDownList ID="ddlCuentaOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                            Width="320px" Height="28px" CssClass="textbox">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelTrans" runat="server">
            <table>
                <tr>
                    <td style="width: 175px; text-align: left">
                        Entidad
                    </td>
                    <td colspan="3" style="height: 25px">
                        <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                            Width="320px" Height="28px" CssClass="textbox">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 175px; text-align: left">
                        Numero de Cuenta
                    </td>
                    <td style="height: 25px">
                        <asp:TextBox ID="txtnumcuenta" runat="server" Width="125px" CssClass="textbox" Style="text-align: left" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtnumcuenta" ValidChars="-,.()" />
                    </td>
                    <td style="width: 100px; text-align: center">
                        Tipo Cuenta
                    </td>
                    <td style="height: 25px">
                        <asp:DropDownList ID="ddlTipo_cuenta" runat="server" Style="margin-left: 0px; text-align: left"
                            Width="150px" Height="28px" CssClass="textbox">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlCuentaAhorroVista" runat="server">
            <table>
                <tr>
                    <td style="text-align: left">
                        <table style="width: 159%">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:Label ID="lblCuentaAhorroVista" runat="server" Style="text-align: left" Text="Numero Cuenta"></asp:Label>
                                </td>
                                <td style="width: 151px; text-align: left;">
                                    <asp:DropDownList ID="ddlCuentaAhorroVista" runat="server" CssClass="textbox" Style="margin-left: 0px; text-align: left" Width="102%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownFormaDesembolso" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlEntidadOrigen" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
