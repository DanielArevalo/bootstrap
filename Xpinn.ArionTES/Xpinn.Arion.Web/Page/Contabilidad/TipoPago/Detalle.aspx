<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Tipo de Pago :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Tipo de Pago<br />
            <asp:TextBox ID="txtTipoPago" runat="server" CssClass="textbox" MaxLength="128" Enabled="False" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Descripción<br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" Enabled="True" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Caja&nbsp;*&nbsp;<br />            
            <asp:DropDownList ID="ddlCaja" runat="server" Enabled="False">
                <asp:ListItem Value="S">Si</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>