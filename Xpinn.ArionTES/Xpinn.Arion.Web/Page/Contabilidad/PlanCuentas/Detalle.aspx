<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Plan de Cuentas :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" colspan="2" style="text-align:left">
                Cod.Cuenta<br />
                <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" MaxLength="128" Enabled="False" />
            </td>
            <td class="tdD" style="text-align:left">
                <asp:CheckBox ID="chkEstado" runat="server" Text="Activa" Enabled="False" />
            </td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" colspan="3" style="text-align:left">
                Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" Width="672px" Enabled="False" />
            </td>
            <td class="tdI" style="text-align:left">
                &nbsp;</td>
            <td class="tdI" style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="logo" style="width: 151px; text-align:left">
                Tipo&nbsp;<br />
                <asp:DropDownList ID="ddlTipo" runat="server"  Enabled="False"
                    Width="120px" CssClass="textbox">
                    <asp:ListItem Value="D">Dèbito</asp:ListItem>
                    <asp:ListItem Value="C">Crédito</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td class="logo" style="width: 81px; text-align:left">
                Nivel<br />
                <asp:TextBox ID="txtNivel" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="69px" Enabled="False" />
            </td>
            <td class="tdD" style="text-align:left">
                Depende de<br />
                <asp:DropDownList ID="ddlDependede" runat="server" CssClass="textbox" Width="427px" Enabled="False">
                </asp:DropDownList>
            </td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="logo" style="text-align:left" colspan="2">
                Moneda<br />
                <asp:DropDownList ID="ddlMonedas" runat="server" 
                    Width="226px" CssClass="textbox" Enabled="False">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="60%" >
        <tr>
            <td class="tdI" style="text-align:left;">
                <asp:CheckBox ID="chkTerceros" runat="server" Text="Maneja Terceros" Enabled="False" />
            </td>
            <td class="tdD" style="text-align:left;">
                <asp:CheckBox ID="chkCentroCosto" runat="server" Text="Maneja Centro de Costo" Enabled="False" />
            </td>
            <td class="tdD" style="text-align:left;">
                <asp:CheckBox ID="chkCentroGestion" runat="server" Text="Maneja Centro de Gestión" Enabled="False" />
            </td>
            <td class="tdD">
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left;">
                <asp:CheckBox ID="chkImpuestos" runat="server" Text="Maneja Impuestos" Enabled="False" />
                <br />
            </td>
            <td class="tdD" style="text-align:left; width: 207px;">
                <asp:CheckBox ID="chkCuentaPagar" runat="server" 
                    Text="Maneja Cuenta por Pagar" Enabled="False" />
            </td>
            <td style="text-align:left">
                &nbsp;</td>
            <td class="tdD" style="text-align:left">                
                &nbsp;</td>
            <td class="tdD" style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="width: 153px">
                &nbsp;</td>
            <td class="tdD" colspan="2">
                &nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>