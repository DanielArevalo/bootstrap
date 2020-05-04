<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Ubicación :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipocomprobante" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
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
                Cód.Cuenta&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodCuenta" runat="server" ControlToValidate="ddlCodCuenta" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlCodCuenta" runat="server" style="text-align:left" CssClass="textbox" Width="120px"                     
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                </asp:DropDownList> 
            </td>        
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cód.Cuenta Depreciación&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodCuentaDepreciacion" runat="server" ControlToValidate="ddlCodCuentaDepreciacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlCodCuentaDepreciacion" runat="server" style="text-align:left" CssClass="textbox" Width="120px"                     
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                </asp:DropDownList> 
            </td>        
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cód.Cuenta Depreciación Gasto&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodCuentaDepreciacionGasto" runat="server" ControlToValidate="ddlCodCuentaDepreciacionGasto" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlCodCuentaDepreciacionGasto" runat="server" style="text-align:left" CssClass="textbox" Width="120px"                     
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                </asp:DropDownList> 
            </td>        
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cód.Cuenta Gasto Venta/Baja&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodCuentaGasto" runat="server" ControlToValidate="ddlCodCuentaGasto" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlCodCuentaGasto" runat="server" style="text-align:left" CssClass="textbox" Width="120px"                     
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                </asp:DropDownList> 
            </td>        
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cód.Cuenta Ingreso Venta/Baja&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodCuentaIngreso" runat="server" ControlToValidate="ddlCodCuentaIngreso" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlCodCuentaIngreso" runat="server" style="text-align:left" CssClass="textbox" Width="120px"                     
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                </asp:DropDownList> 
            </td>        
        </tr>
    </table>
</asp:Content>