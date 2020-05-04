<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Persona1 :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_persona&nbsp;*<br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Tipo_persona&nbsp;<br />
                       <asp:TextBox ID="txtTipo_persona" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Identificacion&nbsp;<br />
                       <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Digito_verificacion&nbsp;<br />
                       <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_identificacion&nbsp;<br />
                       <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fechaexpedicion&nbsp;<br />
                       <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codciudadexpedicion&nbsp;<br />
                       <asp:TextBox ID="txtCodciudadexpedicion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Sexo&nbsp;<br />
                       <asp:TextBox ID="txtSexo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Primer_nombre&nbsp;<br />
                       <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Segundo_nombre&nbsp;<br />
                       <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Primer_apellido&nbsp;<br />
                       <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Segundo_apellido&nbsp;<br />
                       <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Razon_social&nbsp;<br />
                       <asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fechanacimiento&nbsp;<br />
                       <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codciudadnacimiento&nbsp;<br />
                       <asp:TextBox ID="txtCodciudadnacimiento" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Codestadocivil&nbsp;<br />
                       <asp:TextBox ID="txtCodestadocivil" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codescolaridad&nbsp;<br />
                       <asp:TextBox ID="txtCodescolaridad" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Codactividad&nbsp;<br />
                       <asp:TextBox ID="txtCodactividad" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Direccion&nbsp;<br />
                       <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Telefono&nbsp;<br />
                       <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codciudadresidencia&nbsp;<br />
                       <asp:TextBox ID="txtCodciudadresidencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Antiguedadlugar&nbsp;<br />
                       <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipovivienda&nbsp;<br />
                       <asp:TextBox ID="txtTipovivienda" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Arrendador&nbsp;<br />
                       <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Telefonoarrendador&nbsp;<br />
                       <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Celular&nbsp;<br />
                       <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Email&nbsp;<br />
                       <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Empresa&nbsp;<br />
                       <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Telefonoempresa&nbsp;<br />
                       <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Codcargo&nbsp;<br />
                       <asp:TextBox ID="txtCodcargo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codtipocontrato&nbsp;<br />
                       <asp:TextBox ID="txtCodtipocontrato" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cod_asesor&nbsp;<br />
                       <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Residente&nbsp;<br />
                       <asp:TextBox ID="txtResidente" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fecha_residencia&nbsp;<br />
                       <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cod_oficina&nbsp;*<br />
                       <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Tratamiento&nbsp;<br />
                       <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Estado&nbsp;<br />
                       <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="false"/>
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
                       Usuultmod&nbsp;<br />
                       <asp:TextBox ID="txtUsuultmod" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>