<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - BienesRaices :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_bien&nbsp;*<br />
                       <asp:TextBox ID="txtCod_bien" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;*<br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo&nbsp;<br />
                       <asp:TextBox ID="txtTipo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Matricula&nbsp;<br />
                       <asp:TextBox ID="txtMatricula" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorcomercial&nbsp;<br />
                       <asp:TextBox ID="txtValorcomercial" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Valorhipoteca&nbsp;<br />
                       <asp:TextBox ID="txtValorhipoteca" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
    </table>
</asp:Content>