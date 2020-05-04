<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Persona1 :." %>
<%@ Register src="../../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <table border="0" cellpadding="5" cellspacing="0" width="100%">       
        <tr>
            <td class="tdI">
                Cod persona&nbsp;*&nbsp;<br />
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
                    MaxLength="128" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI">
                Tipo identificación&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlTipo" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                &nbsp; Identificación *&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Primer nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Segundo nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Primer apellido&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Segundo apellido&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Ciudadresidencia<br />
                <asp:DropDownList ID="ddlLugarResidencia" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Dirección&nbsp;&nbsp;<br />
                <uc1:direccion ID="txtDireccion" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                <asp:Panel ID="Panel2" runat="server">
                    Razon social<asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox"
                        MaxLength="128" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Actividad<br />
                <asp:DropDownList ID="ddlActividad" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Tipo persona
                <asp:RadioButtonList ID="rblTipoPersona" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="N" Selected="True">Natural</asp:ListItem>
                    <asp:ListItem Value="J">Juridica</asp:ListItem>
                </asp:RadioButtonList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Digito verificación&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Fecha expedición&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Ciudad expedición<br />
                <asp:DropDownList ID="ddlLugarExpedicion" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD">
                Sexo&nbsp;&nbsp;<asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">F</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Escolaridad<br />
                <asp:DropDownList ID="ddlNivelEscolaridad" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD">
                Estado civil&nbsp;&nbsp;<br />
                &nbsp;<asp:DropDownList ID="ddlEstadoCivil" runat="server">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Ciudadnacimiento&nbsp;<br />
                <asp:DropDownList ID="ddlLugarNacimiento" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Fecha nacimiento&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                &nbsp; Antiguedad lugar&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                &nbsp; Tipo vivienda&nbsp;&nbsp;<asp:RadioButtonList ID="rblTipoVivienda" runat="server"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                    <asp:ListItem Value="A">Arrendada</asp:ListItem>
                    <asp:ListItem Value="F">Familiar</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Cod oficina&nbsp;*&nbsp;<br />
                <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Celular&nbsp;&nbsp;<asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono arrendador&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Arrendador&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono empresa&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" 
                    MaxLength="128" />
                <br />
            </td>
            <td class="tdD">
                Cargo&nbsp;<br />
                <asp:DropDownList ID="ddlCargo" runat="server">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Email&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Empresa&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Residente&nbsp;&nbsp;<asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <br />
            </td>
            <td class="tdD">
                Fecha residencia&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Tipo contrato<br />
                <asp:DropDownList ID="ddlTipoContrato" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD">
                Cod asesor&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD">
                Tratamiento&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                &nbsp;
            </td>
            <td class="tdD">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI">
                &nbsp;
            </td>
            <td class="tdD">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>