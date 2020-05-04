<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Vehiculos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_vehiculo&nbsp;*<br />
                       <asp:TextBox ID="txtCod_vehiculo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;*<br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Marca&nbsp;<br />
                       <asp:TextBox ID="txtMarca" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Placa&nbsp;<br />
                       <asp:TextBox ID="txtPlaca" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Modelo&nbsp;<br />
                       <asp:TextBox ID="txtModelo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Valorcomercial&nbsp;<br />
                       <asp:TextBox ID="txtValorcomercial" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorprenda&nbsp;<br />
                       <asp:TextBox ID="txtValorprenda" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>