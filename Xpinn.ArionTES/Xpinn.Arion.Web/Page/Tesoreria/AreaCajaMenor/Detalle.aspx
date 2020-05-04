<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Area :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Código<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" Width="128px" Enabled="false" />
            </td>
            <td class="tdD" style="text-align:left; width: 363px;">
                Fecha de Constitución<br />
                <asp:TextBox ID="txtfechaConst" runat="server" CssClass="textbox" MaxLength="128" Width="128px" Enabled="false" />
            </td>       
            <td class="tdI" style="text-align:left; width: 363px;">
                Responsable<br />
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" MaxLength="128" Width="316px" Enabled="false" />
            </td>                             
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Descripción<br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="255px" Enabled="false" />
            </td> 
            <td class="tdD" style="text-align:left">
                Valor Base<br />
                <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" MaxLength="128" Enabled="false" OnTextChanged="txtValor_TextChanged"/>
            </td>  
            <td class="tdI" style="text-align:left">
                Monto Mínimo<br />
                <asp:TextBox ID="txtVMinimo" runat="server" CssClass="textbox" MaxLength="128" Enabled="false" OnTextChanged="txtVMinimo_TextChanged" />
            </td>  
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>