<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - consultasdatacredito :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Numerofactura&nbsp;*<br />
                       <asp:TextBox ID="txtNumerofactura" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fechaconsulta&nbsp;<br />
                       <asp:TextBox ID="txtFechaconsulta" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cedulacliente&nbsp;<br />
                       <asp:TextBox ID="txtCedulacliente" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Usuario&nbsp;<br />
                       <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Ip&nbsp;<br />
                       <asp:TextBox ID="txtIp" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Oficina&nbsp;<br />
                       <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorconsulta&nbsp;<br />
                       <asp:TextBox ID="txtValorconsulta" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>