<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlTasa.ascx.cs" Inherits="ctlTasa" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/Controles/ctlNumeroConDecimales.ascx" TagName="Decimal" TagPrefix="ucNumeroConDecimales" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<table cellpadding="0" cellspacing="0" style="width: 100%">
    <asp:Panel ID="pnTipoTasa" runat="server" Visible="false">
        <tr>
            <td style="text-align: left">Tipo tasa
            <asp:RadioButtonList ID="rbCalculoTasa" runat="server"
                RepeatDirection="Horizontal" Style="font-size: small" AutoPostBack="True"
                OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged">
                <asp:ListItem Value="0" Selected="True">Ninguna</asp:ListItem>
                <asp:ListItem Value="1">Tasa fija</asp:ListItem>
                <asp:ListItem Value="2">Histórico fijo</asp:ListItem>
                <asp:ListItem Value="3">Histórico variable</asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="panelHistorico" runat="server" Visible="false">
        <tr>
            <td>

                <table style="text-align: left">
                    <tr>
                        <td style="text-align: left">Tipo Histórico<br />
                            <asp:DropDownList ID="ddlHistorico" runat="server" Width="200px"
                                Style="font-size: xx-small" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Spread<br />
                            <ucNumeroConDecimales:Decimal ID="txtDesviacion" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="panelFijo" runat="server">
        <tr>
            <td>
                <table style="text-align: left">
                    <tr>                        
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTipoTasa" runat="server" Width="200px" CssClass="form-control"
                                Style="font-size: small" />
                        </td>
                        <td style="text-align: left; background-color:transparent; border:none;">
                            <asp:TextBox ID="txtTasa" runat="server" CssClass="form-control" Width="100px" OnTextChanged="txtTasa_TextChanged" Visible="false" AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </asp:Panel>
</table>