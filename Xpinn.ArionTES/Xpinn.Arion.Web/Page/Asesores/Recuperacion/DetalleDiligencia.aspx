<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalleDiligencia.aspx.cs" Inherits="DetalleDiligencia" MasterPageFile="~/General/Master/site.master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">
 <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                           Código cliente:<br />
                       <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" 
                               Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Numero radicación&nbsp;<br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Código diligencia<br />
                       <asp:TextBox ID="txtCodigo_diligencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
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
                           <asp:Button ID="btnAbrirAnexo" runat="server" CssClass="btn8" 
                               onclick="btnAbrirAnexo_Click" Text="Abrir anexo" Visible="False" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Observación&nbsp;<br />
                       <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Codigo usuario registra<br />
                       <asp:TextBox ID="txtCodigo_usuario_regis" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   </table>
</asp:Content>
