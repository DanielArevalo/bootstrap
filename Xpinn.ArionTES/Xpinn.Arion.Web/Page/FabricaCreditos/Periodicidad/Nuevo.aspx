<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Periodicidad :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Cod.Periodicidad&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtCodPeriodicidad" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtCodPeriodicidad" runat="server" CssClass="textbox" MaxLength="128" />
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
            Número de Días&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNumeroDias" runat="server" ControlToValidate="txtNumeroDias" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtNumeroDias" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="121px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Número de Meses&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNumeroMeses" runat="server" ControlToValidate="txtNumeroMeses" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtNumeroMeses" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="117px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Períodos Anuales&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvPeriodosAnuales" runat="server" ControlToValidate="txtPeriodosAnuales" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtPeriodosAnuales" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="117px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Tipo de Calendario&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoCalendario" runat="server" ControlToValidate="ddlTipoCalendario" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />            
            <asp:DropDownList ID="ddlTipoCalendario" runat="server" CssClass="dropdown" 
                    Width="175px">
                <asp:ListItem Value="1">Comercial</asp:ListItem>
                <asp:ListItem Value="2">Calendario</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>