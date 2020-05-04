<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Usuario :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Tipo de Pago&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtTipoPago" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtTipoPago" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Caja&nbsp;*&nbsp;<br />            
            <asp:DropDownList ID="ddlCaja" runat="server">
                <asp:ListItem Value="S">Si</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>