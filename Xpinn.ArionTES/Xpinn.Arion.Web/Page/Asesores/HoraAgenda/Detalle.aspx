<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - AgendaHora :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Id hora<br />
                       <asp:TextBox ID="txtIdhora" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Hora&nbsp;<br />
                       <asp:TextBox ID="txtHora" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo&nbsp;<br />
                           <asp:DropDownList ID="ddlTipo" runat="server" CssClass="dropdown" 
                               Enabled="False">
                               <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                               <asp:ListItem Value="1">a.m.</asp:ListItem>
                               <asp:ListItem Value="2">p.m.</asp:ListItem>
                           </asp:DropDownList>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>