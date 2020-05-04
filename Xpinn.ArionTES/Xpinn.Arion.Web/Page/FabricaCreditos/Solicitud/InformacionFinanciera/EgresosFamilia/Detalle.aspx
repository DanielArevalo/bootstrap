<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - EgresosFamilia :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCod_egreso" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       <asp:TextBox ID="txtEgresos" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Pago Deudas&nbsp;<br />
                       <asp:TextBox ID="txtPagodeudas" runat="server" CssClass="textbox" Enabled="false"/>
                           <br />
                       </td>
                       <td class="tdD">
                       Alimentacion&nbsp;<br />
                       <asp:TextBox ID="txtAlimentacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Vivienda&nbsp;<br />
                       <asp:TextBox ID="txtVivienda" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Educacion&nbsp;<br />
                       <asp:TextBox ID="txtEducacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Serviciospublicos&nbsp;<br />
                       <asp:TextBox ID="txtServiciospublicos" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Transporte&nbsp;<br />
                       <asp:TextBox ID="txtTransporte" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Otros&nbsp;<br />
                       <asp:TextBox ID="txtOtros" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
    </table>
</asp:Content>