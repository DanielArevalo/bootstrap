<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 80%">
    <tr>
        <td class="tdI">
        </td>
    </tr> 
    <tr>
        <td class="tdI" style="text-align:left">
            C&oacute;digo
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" enabled="false" 
                Width="134px"></asp:TextBox>
        </td>
        <td class="tdD" style="text-align:left">
             Tipo Moneda
            <asp:DropDownList ID="ddlMoneda" CssClass="dropdown"  runat="server" 
                Height="25px" Width="163px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdI" style="text-align:left">
            Línea * 
            <asp:TextBox ID="txtLinea" runat="server" MaxLength="50" CssClass="textbox" Width="206px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLinea" runat="server" 
                ErrorMessage="Campo Requerido" ControlToValidate="txtLinea" Display="Dynamic" 
                ForeColor="Red" ValidationGroup="vgGuardar"/>
        </td>
        <td style="text-align:left">
            Tipo Liquidación &#160;&#160;
            <asp:DropDownList ID="ddlTipoLiquidacion" CssClass="dropdown"  runat="server" 
                Height="25px" Width="238px">
            </asp:DropDownList> 
        </td>
    </tr>
    
</table>

</asp:Content>



