<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Auditoria :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                    <tr>
                        <td class="tdI">
                       Codigo Registro&nbsp;*<br />
                       <asp:TextBox ID="txtCod_auditoria" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fecha&nbsp;*<br />
                       <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuario&nbsp;*<br />
                       <asp:DropDownList ID="txtCodusuario" runat="server" CssClass="dropdown" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Opción&nbsp;*<br />
                       <asp:DropDownList ID="txtCodopcion" runat="server" CssClass="dropdown" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       IP&nbsp;*<br />
                       <asp:TextBox ID="txtIp" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Navegador&nbsp;*<br />
                       <asp:TextBox ID="txtNavegador" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Acción&nbsp;*<br />
                       <asp:DropDownList ID="txtAccion" runat="server" CssClass="dropdown" Enabled="false">
                           <asp:ListItem Value="1">Crear</asp:ListItem>
                           <asp:ListItem Value="4">Modificar</asp:ListItem>
                           <asp:ListItem Value="5">Eliminar</asp:ListItem>
                           </asp:DropDownList>
                       </td>
                       </td>
                       <td class="tdD">
                       Tabla&nbsp;*<br />
                       <asp:TextBox ID="txtTabla" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="2">
                       Detalle&nbsp;*<br />
                       <asp:TextBox ID="txtDetalle" style="resize:none" runat="server" CssClass="textbox" Enabled="false" Width="300px" Height="100px" TextMode="MultiLine"/>
                       &nbsp;
                       </td>
                       </table>
</asp:Content>