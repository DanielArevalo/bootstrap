<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlTasa.ascx.cs" Inherits="ctlTasa" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="Decimal" TagPrefix="ucNumeroConDecimales" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<table cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="text-align: left">
            Tipo de Tasa
            <asp:RadioButtonList ID="rbCalculoTasa" runat="server" 
                RepeatDirection="Horizontal" style="font-size: small" AutoPostBack="True" 
                onselectedindexchanged="rbCalculoTasa_SelectedIndexChanged" >
                <asp:ListItem Value="0" Selected="True">Ninguna</asp:ListItem>
                <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                <asp:ListItem Value="2">Histórico Fijo</asp:ListItem>
                <asp:ListItem Value="3">Histórico Variable</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="panelHistorico" runat="server">
                <table style="text-align: left">
                    <tr>
                        <td style="text-align: left">
                            Tipo Histórico<br />
                            <asp:DropDownList ID="ddlHistorico" runat="server" Width="200px" 
                                style="font-size: xx-small" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            Spread<br />
                            <ucNumeroConDecimales:Decimal ID="txtDesviacion" runat="server"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>       
            <asp:Panel ID="panelFijo" runat="server">
                <table style="text-align: left">
                    <tr>
                        <td style="text-align: left">
                            Tasa<br />
                            <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="100px"  ontextchanged="txtTasa_TextChanged" AutoPostBack="True" />
                            <asp:FilteredTextBoxExtender ID="ftb15" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtTasa" ValidChars="," />
                        </td>
                        <td style="text-align: left">
                            Tipo de Tasa<br />
                            <asp:DropDownList ID="ddlTipoTasa" runat="server" Width="200px" CssClass="textbox"
                                Style="font-size: xx-small" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>