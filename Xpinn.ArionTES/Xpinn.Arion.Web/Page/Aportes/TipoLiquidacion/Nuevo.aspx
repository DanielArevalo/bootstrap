<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Periodicidad :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
            Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtTipoLiquidacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtTipoLiquidacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
            Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Periodicidad&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvPeriodicidad" runat="server" ControlToValidate="ddlPeriodicidad" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="dropdown" 
                    Height="26px" Width="240px" AutoPostBack="True" >
                   
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left; height: 58px;" colspan="2">
                Tipo Saldo Base&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvTipoSaldoBase" runat="server" ControlToValidate="ddlTipoSaldoBase" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="ddlTipoSaldoBase" runat="server" CssClass="dropdown" 
                    Height="26px" Width="240px">
                    <asp:ListItem Value="1">Actual</asp:ListItem>
                    <asp:ListItem Value="2">Promedio</asp:ListItem>
                    <asp:ListItem Value="3">Minimo</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD" style="height: 58px">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Linea Aporte Base*&nbsp; 
                <asp:RequiredFieldValidator ID="rfvTipoInt" runat="server" ControlToValidate="ddlLineaBase" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlLineaBase" runat="server" Height="26px" 
                    Width="240px" CssClass="dropdown">                  
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Linea Aporte Afecta<asp:RequiredFieldValidator ID="rfvTipAmo" runat="server" ControlToValidate="ddlLineaAfecta" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlLineaAfecta" runat="server" Height="26px" Width="240px" 
                    CssClass="dropdown">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 144px;">
                &nbsp;</td>
            <td class="tdI" style="text-align:left">
                &nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>