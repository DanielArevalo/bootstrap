<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Persona1 :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
    <table>
        <tr>
            <td class="tdI" style="height: 51px">
                Tipo identificación&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlTipo" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD" style="height: 51px">
                &nbsp; Identificación *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgConsultar" />
                <br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Primer nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Segundo nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Primer apellido&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Segundo apellido&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox" MaxLength="128" />
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
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefono" runat="server" ControlToValidate="txtTelefono"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                <asp:Panel ID="Panel2" runat="server">
                    Razon social<asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox" MaxLength="128" />
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
                Digito verificación&nbsp;&nbsp;<asp:CompareValidator ID="cvDIGITO_VERIFICACION0"
                    runat="server" ControlToValidate="txtDigito_verificacion" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Fecha expedición&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHAEXPEDICION0" runat="server"
                    ControlToValidate="txtFechaexpedicion" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                    Type="Date" ValidationGroup="vgGuardar" />
                <br />
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
                Fecha nacimiento&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHANACIMIENTO0" runat="server"
                    ControlToValidate="txtFechanacimiento" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                    Type="Date" ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                &nbsp; Antiguedad lugar&nbsp;&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR0"
                    runat="server" ControlToValidate="txtAntiguedadlugar" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
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
                Tratamiento&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Celular&nbsp;<asp:CompareValidator ID="cvtxtCelular" runat="server" ControlToValidate="txtCelular"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                &nbsp;<asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono arrendador&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefonoarrendador1"
                    runat="server" ControlToValidate="txtTelefonoarrendador" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Arrendador&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono empresa&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefonoempresa" runat="server"
                    ControlToValidate="txtTelefonoempresa" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" MaxLength="128" />
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
                Fecha residencia&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHA_RESIDENCIA" runat="server"
                    ControlToValidate="txtFecha_residencia" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" ToolTip="Formato fecha" /><br />
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
                Cod asesor&nbsp;&nbsp;<asp:CompareValidator ID="cvCOD_ASESOR" runat="server" ControlToValidate="txtCod_asesor"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" /><br />
                <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <br />
            </td>
            <td class="tdD">
                &nbsp;</td>
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

    <asp:Panel ID="Panel3" runat="server" Visible="False">
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td class="tdI">
                    <br />
                </td>
                <td class="tdD">
                    &nbsp;
                    <asp:DropDownList ID="ddlEstado" runat="server">
                    </asp:DropDownList>
                    Cod oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_OFICINA2" runat="server" 
                        ControlToValidate="txtCod_oficina" Display="Dynamic" 
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationGroup="vgGuardar" />
                    <asp:CompareValidator ID="cvCOD_OFICINA0" runat="server" 
                        ControlToValidate="txtCod_oficina" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox" 
                        MaxLength="128">1</asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
     
    
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('ctl00_cphMain_txtCOD_PERSONA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
