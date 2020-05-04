<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Persona1 :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="cuFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../../../General/Controles/direccion.ascx" TagName="direccion"TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td align="left" style="width: 472px">
                Primer Nombre
                <asp:RequiredFieldValidator ID="rfvPrimerNombre" runat="server" 
                    ControlToValidate="txtPrimer_nombre" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small"  />
                <br />
                <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox"  Width="335px"
                    MaxLength="128"  />
            </td>
            <td align="left">
                Segundo Nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" MaxLength="128" Width="335px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 472px">
                Primer Apellido&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="rfvPrimerApellido" runat="server" 
                    ControlToValidate="txtPrimer_apellido" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small"  />
                <br />
                <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" MaxLength="128" Width="335px" />
            </td>
            <td align="left">
                Segundo Apellido&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox" MaxLength="128" Width="335px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 472px">
                Tipo Identificación&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlTipo" runat="server"  Width="335px">
                </asp:DropDownList>
            </td>
            <td align="left">
                Identificación *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvIdentificacion"
                    runat="server" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" MaxLength="20" Width="335px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 472px">
                Ciudad Expedición<br />
                <asp:DropDownList ID="ddlLugarExpedicion" runat="server"  Width="335px">
                </asp:DropDownList>
            </td>
            <td align="left">
                Celular&nbsp;
                <asp:RequiredFieldValidator ID="rfvCelular"
                    runat="server" ControlToValidate="txtCelular" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <asp:CompareValidator ID="cvtxtCelular" runat="server" ControlToValidate="txtCelular"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="50" Width="335px" />
                <br />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="font-weight: 700">
                Información Laboral
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 472px">
                Empresa&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="128" Width="335px" />
            </td>
            <td align="left">
                Cargo&nbsp;<br />
                <asp:DropDownList ID="ddlCargo" runat="server"  Width="335px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 472px">
                Antiguedad Cargo&nbsp;&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR0" runat="server"
                    ControlToValidate="txtAntiguedadlugar" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" MaxLength="8" Width="335px" />
            </td>
            <td align="left">
                Tipo Contrato<br />
                <asp:DropDownList ID="ddlTipoContrato" runat="server"  Width="335px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>           
            <td align="left" style="width: 472px">
                Teléfono&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefono" runat="server" ControlToValidate="txtTelefono"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="20"  Width="335px"/>
            </td>
            <td align="left" style="width: 472px">                  
                Email<br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"   Width="335px"
                    MaxLength="128"  />
            </td>
        </tr>
        <tr>
             <td align="left" style="width: 472px">
                Sexo<asp:RadioButtonList ID="rblSexo" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">F</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="left" style="width: 472px">                   
                Fecha Nacimiento<br />
                <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="1"
                    AutoPostBack="True" Width="148px">
                </asp:TextBox>
                <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimiento" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                <br />
            </td>
        </tr>
        <tr>           
            <td>
            </td>       
        </tr>
    </table>
    <table>     
        <tr> 
            <td align="left" style="width: 671px">
                Dirección<uc1:direccion ID="txtDireccion" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel3" runat="server" Visible="False" Style="margin-right: 111px">
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <br />
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlActividad" runat="server"   Width="335px">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="ddlEstado" runat="server"  Width="335px">
                    </asp:DropDownList>
                    Cod oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_OFICINA2" runat="server"
                        ControlToValidate="txtCod_oficina" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: xx-small" />
                    <asp:CompareValidator ID="cvCOD_OFICINA0" runat="server" ControlToValidate="txtCod_oficina"
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" style="font-size: xx-small" />
                    <br />
                    <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox>
                    <br />
                    Total Ingresos<br />
                    <asp:TextBox ID="txtSalario" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Residente&nbsp;&nbsp;<asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:RadioButtonList>
                    Cod asesor&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox>
                    <br />
                    Fecha Residencia&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" MaxLength="128">01/01/2000</asp:TextBox>
                    <br />
                    Teléfono Arrendador&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Arrendador&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Tratamiento&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Tipo Vivienda<asp:RadioButtonList ID="rblTipoVivienda" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                        <asp:ListItem Value="A">Arrendada</asp:ListItem>
                        <asp:ListItem Value="F">Familiar</asp:ListItem>
                    </asp:RadioButtonList>
                    Ciudad Nacimiento&nbsp;<br />
                    <asp:DropDownList ID="ddlLugarNacimiento" runat="server">
                    </asp:DropDownList>
                    <br />
                    Escolaridad<br />
                    <asp:DropDownList ID="ddlNivelEscolaridad" runat="server">
                    </asp:DropDownList>
                    <br />
                    Estado Civil&nbsp;&nbsp;<br />
                    &nbsp;<asp:DropDownList ID="ddlEstadoCivil" runat="server">
                    </asp:DropDownList>
                    <br />
                    Fecha Expedición<br />
                    <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Tipo persona
                    <asp:RadioButtonList ID="rblTipoPersona" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
                        <asp:ListItem Value="J">Juridica</asp:ListItem>
                    </asp:RadioButtonList>
                    Digito Verificación&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Ciudad Residencia<br />
                    <asp:DropDownList ID="ddlLugarResidencia" runat="server">
                    </asp:DropDownList>
                    <br />
                    Teléfono Empresa&nbsp;<asp:CompareValidator ID="cvtxtTelefonoempresa" runat="server"
                        ControlToValidate="txtTelefonoempresa" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                        ValidationGroup="vgGuardar" style="font-size: xx-small" />
                    <br />
                    <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" MaxLength="128" />
                    <asp:Panel ID="Panel2" runat="server">
                        Razon Social<asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox" MaxLength="128" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
