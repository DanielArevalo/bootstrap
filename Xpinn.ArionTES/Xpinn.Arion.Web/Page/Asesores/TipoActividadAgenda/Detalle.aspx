<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - AgendaTipoActividad :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Id tipo<br />
                       <asp:TextBox ID="txtIdtipo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           Descripción<br />
                       <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
    </table>
</asp:Content>