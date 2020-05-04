<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Georeferencia :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Codgeoreferencia&nbsp;*<br />
                       <asp:TextBox ID="txtCodgeoreferencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Latitud&nbsp;*<br />
                       <asp:TextBox ID="txtLatitud" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Longitud&nbsp;*<br />
                       <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Observaciones&nbsp;<br />
                       <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fechacreacion&nbsp;*<br />
                       <asp:TextBox ID="txtFechacreacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuariocreacion&nbsp;*<br />
                       <asp:TextBox ID="txtUsuariocreacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fecultmod&nbsp;<br />
                       <asp:TextBox ID="txtFecultmod" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuultmod&nbsp;*<br />
                       <asp:TextBox ID="txtUsuultmod" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                   <tr>
                       <td class="tdI">
                           <asp:GridView ID="GridView1" runat="server">
                           </asp:GridView>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
    </table>


</asp:Content>