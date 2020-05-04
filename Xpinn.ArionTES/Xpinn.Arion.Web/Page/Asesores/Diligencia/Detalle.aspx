<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Diligencia :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Código diligencia<br />
                       <asp:TextBox ID="txtCodigo_diligencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Numero radicación&nbsp;<br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Fecha diligencia&nbsp;<br />
                       <asp:TextBox ID="txtFecha_diligencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Tipo diligencia&nbsp;<br />
                       <asp:TextBox ID="txtTipo_diligencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Atendió<br />
                       <asp:TextBox ID="txtAtendio" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Respuesta&nbsp;<br />
                       <asp:TextBox ID="txtRespuesta" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Acuerdo&nbsp;<asp:CheckBox 
                               ID="chkAprueba" runat="server" Enabled="False" />
                           <br />
                       </td>
                       <td class="tdD">
                       Fecha acuerdo&nbsp;<br />
                       <asp:TextBox ID="txtFecha_acuerdo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valor acuerdo&nbsp;<br />
                       <asp:TextBox ID="txtValor_acuerdo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Anexo&nbsp;<br />
                       <asp:TextBox ID="txtAnexo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Observación&nbsp;<br />
                       <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Código usuario registra<br />
                       <asp:TextBox ID="txtCodigo_usuario_regis" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   </table>
</asp:Content>