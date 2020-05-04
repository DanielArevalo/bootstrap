<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - CalificacionReferencias :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Tipo calificación referencia<br />
                       <asp:TextBox ID="txtTipocalificacionref" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           Nombre<br />
                       <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
    </table>
</asp:Content>