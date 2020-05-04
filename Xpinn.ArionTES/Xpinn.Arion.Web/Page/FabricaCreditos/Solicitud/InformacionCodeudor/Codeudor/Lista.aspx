<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Codeudores :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Visible="false">
    </asp:Panel>
    <table id="Table1" runat="server" width="1000">
        <tr>
            <td class="tdI" colspan="2"  style="text-align: left;">
                &nbsp;</td>
            <td class="tdI" colspan="2"  style="text-align: right">
                <asp:ImageButton ID="btnGuardar" runat="server" 
                    ImageUrl="~/Images/btnGuardar.jpg" onclick="btnGuardar_Click1" 
                    ValidationGroup="vgGuardar" />
                <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConsultar_Click1" />
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4" style="height: 47px; text-align:left">
                Numero Radicación<asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <br />
                <asp:TextBox ID="txtNumeroRadicacion" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4">
                <asp:GridView ID="gvListaAfliados" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True"
                    OnRowEditing="gvLista_RowEditing" PageSize="4" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                    DataKeyNames="COD_PERSONA" style="font-size: x-small" 
                    onrowdeleting="gvListaAfliados_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TIPO_PERSONA" HeaderText="Tipo_persona" Visible="false" />
                        <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificacion" />
                        <asp:BoundField DataField="DIGITO_VERIFICACION" HeaderText="Digito_verificacion" Visible="false" />
                        <asp:BoundField DataField="TIPO_IDENTIFICACION" HeaderText="Tipo_identificacion" Visible="false" />
                        <asp:BoundField DataField="FECHAEXPEDICION" HeaderText="Fechaexpedicion" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADEXPEDICION" HeaderText="Codciudadexpedicion" Visible="false" />
                        <asp:BoundField DataField="SEXO" HeaderText="Sexo" Visible="false" />
                        <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre" />
                        <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre" />
                        <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido" />
                        <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido" />
                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razon_social" Visible="false" />
                        <asp:BoundField DataField="FECHANACIMIENTO" HeaderText="Fechanacimiento" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADNACIMIENTO" HeaderText="Codciudadnacimiento" Visible="false" />
                        <asp:BoundField DataField="CODESTADOCIVIL" HeaderText="Codestadocivil" Visible="false" />
                        <asp:BoundField DataField="CODESCOLARIDAD" HeaderText="Codescolaridad" Visible="false" />
                        <asp:BoundField DataField="CODACTIVIDAD" HeaderText="Codactividad" Visible="false" />
                        <asp:BoundField DataField="DIRECCION" HeaderText="Direccion" Visible="false" />
                        <asp:BoundField DataField="TELEFONO" HeaderText="Telefono" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADRESIDENCIA" HeaderText="Codciudadresidencia" Visible="false" />
                        <asp:BoundField DataField="ANTIGUEDADLUGAR" HeaderText="Antiguedadlugar" Visible="false" />
                        <asp:BoundField DataField="TIPOVIVIENDA" HeaderText="Tipovivienda" Visible="false" />
                        <asp:BoundField DataField="ARRENDADOR" HeaderText="Arrendador" Visible="false" />
                        <asp:BoundField DataField="TELEFONOARRENDADOR" HeaderText="Telefonoarrendador" Visible="false" />
                        <asp:BoundField DataField="CELULAR" HeaderText="Celular" Visible="false" />
                        <asp:BoundField DataField="EMAIL" HeaderText="Email" Visible="false" />
                        <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" Visible="false" />
                        <asp:BoundField DataField="TELEFONOEMPRESA" HeaderText="Telefonoempresa" Visible="false" />
                        <asp:BoundField DataField="CODCARGO" HeaderText="Codcargo" Visible="false" />
                        <asp:BoundField DataField="CODTIPOCONTRATO" HeaderText="Codtipocontrato" Visible="false" />
                        <asp:BoundField DataField="COD_ASESOR" HeaderText="Cod_asesor" Visible="false" />
                        <asp:BoundField DataField="RESIDENTE" HeaderText="Residente" Visible="false" />
                        <asp:BoundField DataField="FECHA_RESIDENCIA" HeaderText="Fecha_residencia" Visible="false" />
                        <asp:BoundField DataField="COD_OFICINA" HeaderText="Cod_oficina" Visible="false" />
                        <asp:BoundField DataField="TRATAMIENTO" HeaderText="Tratamiento" Visible="false" />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />
                        <asp:BoundField DataField="FECHACREACION" HeaderText="Fechacreacion" Visible="false" />
                        <asp:BoundField DataField="USUARIOCREACION" HeaderText="Usuariocreacion" Visible="false" />
                        <asp:BoundField DataField="FECULTMOD" HeaderText="Fecultmod" Visible="false" />
                        <asp:BoundField DataField="USUULTMOD" HeaderText="Usuultmod" Visible="false" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td class="tdD" colspan="2" >
            </td>
        </tr>
    </table>

    <asp:Panel ID="Panel2" runat="server" Width="1000" >
        <table>
            <tr>
                <td style="text-align: center;color: #FFFFFF; background-color: #0066FF; height: 26px;"  
                    colspan="4">
                    &nbsp;<strong style="text-align: center">DATOS DEL CODEUDOR</strong>
                </td>
            </tr>
            <tr>                                            
                <td class="tdI" style="height: 51px; width: 289px; text-align:left">
                    Identificación *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvIdentificacion"
                        runat="server" ControlToValidate="txtIdentificacion" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: x-small" />
                    <br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                        MaxLength="20" Width="218px" AutoPostBack="True" 
                        ontextchanged="txtIdentificacion_TextChanged" />
                    <asp:TextBox ID="txtcodpersona" runat="server" AutoPostBack="True" 
                        CssClass="textbox" MaxLength="20" ontextchanged="txtIdentificacion_TextChanged" 
                        Width="26px" Visible="False" />
                </td>
                <td class="tdI" style="height: 51px; width: 289px; text-align:left">
                    <br />
                    <asp:ImageButton ID="btnConsultar0" runat="server" 
                        ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConsultar0_Click" 
                        Height="26px" />                     
                </td>
                <td class="tdD" style="height: 51px; text-align:left; width: 214px;">                    
                    Tipo identificación*<asp:DropDownList ID="ddlTipo" runat="server" 
                        CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="height: 51px; text-align:left; width: 392px;">
                    Ciudad expedición<br />
                    <asp:DropDownList ID="ddlLugarExpedicion" runat="server" CssClass="dropdown" 
                        Width="306px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <hr width="1000"/>
        <table>    
            <tr>
                <td class="tdI" style="width: 400px; text-align:left" >
                    Primer Nombre *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvNombre"
                        runat="server" ControlToValidate="txtPrimer_nombre" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: x-small" />
                    <br />
                    <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="220px" sytle="text-transform:uppercase" />
                </td>
                <td class="tdI" style="width: 400px; text-align:left" >
                    Segundo Nombre<br />
                    <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" 
                        MaxLength="128" Widht="220px" Width="220px" style="text-transform:uppercase" />
                </td>
                <td class="tdI" style="width: 400px; text-align:left">
                    Primer Apellido *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rvApellido"
                        runat="server" ControlToValidate="txtPrimer_apellido" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: x-small" />
                    <br />
                    <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox"
                        MaxLength="128" Widht="220px" Width="220px" style="text-transform:uppercase" />
                    <br />
                </td>
                <td class="tdI" style="width: 400px; text-align:left">
                    Segundo apellido
                    <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox"
                        MaxLength="128" Widht="220px" Width="220px" style="text-transform:uppercase" />
                </td>
            </tr>            
        </table>
        <hr width="1000"/>
        <table>
            <tr>
                <td style="width: 286px; text-align:left" >
                    Ciudad Nacimiento *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvCiudadNacimiento"
                        runat="server" ControlToValidate="ddlLugarNacimiento" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: x-small" />
                    <br />
                    <asp:DropDownList ID="ddlLugarNacimiento" runat="server" CssClass="dropdown" 
                        Height="35px" Width="220px">
                    </asp:DropDownList>
                </td>         
                <td>
                    Fecha Nacimiento&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHANACIMIENTO0" runat="server"
                    ControlToValidate="txtFechanacimiento" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                    Type="Date" ValidationGroup="vgGuardar" /><br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="128"
                                OnTextChanged="txtFechanacimiento_TextChanged" AutoPostBack="True" />
                            <asp:MaskedEditExtender ID="mskFechanacimiento" runat="server" TargetControlID="txtFechanacimiento"
                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />
                            <asp:MaskedEditValidator ID="mevFechanacimiento" runat="server" ControlExtender="mskFechanacimiento"
                                ControlToValidate="txtFechanacimiento" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                                Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                                InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar" ForeColor="Red" />
                            <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimiento" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>   
                <td style="width: 95px">
                 Sexo&nbsp;&nbsp;<asp:RadioButtonList ID="rblSexo" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">F</asp:ListItem>
                        <asp:ListItem>M</asp:ListItem>
                    </asp:RadioButtonList>
                    Edad<br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblEdad" runat="server"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                </td>
            </tr>
        </table>
        <hr width="1000"/>
        <table width="1000px">
            <tr>
                <td class="tdI" style="height: 51px; width: 984px; text-align:left" colspan="5">
                    <strong>Dirección *</strong>
                    <uc1:direccion ID="txtDireccion" runat="server" />
                </td>
            </tr>
        </table>
        <table width="1000px">
            <tr>            
                <td class="tdD" style="height: 51px; text-align:left; width: 415px;">
                    Barrio<br />
                    <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="dropdown" 
                        Height="25px" Width="402px">
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    Teléfono *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvTelefono"
                        runat="server" ControlToValidate="txtTelefono" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                        style="font-size: x-small" />
                    <br />
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" 
                        MaxLength="50" />
                    <asp:CompareValidator ID="cvtxtTelefono" runat="server" ControlToValidate="txtTelefono"
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    </td>
                <td class="tdD" style="height: 51px; text-align:left">
                </td>
            </tr>
        </table>
        <table width="1000px">
            <tr>
                <td class="tdI" style="height: 51px; width: 984px; text-align:left">
                    Tipo Vivienda<br />
                    <asp:UpdatePanel ID="upTipoVivienda" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rblTipoVivienda" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged" 
                                AutoPostBack="True" Width="295px">
                                <asp:ListItem Value="P">Propia</asp:ListItem>
                                <asp:ListItem Value="A" Selected="True">Arrendada</asp:ListItem>
                                <asp:ListItem Value="F">Familiar</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
                </td>
               </tr>
                <tr>
                <td class="tdI" style="height: 51px; width: 397px; text-align:left">
                    Arrendador<br />
                    <asp:UpdatePanel ID="upArrendador" runat="server" Widht="376px">
                        <ContentTemplate>
                            <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="404px" style="text-transform:uppercase" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                
                </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    Teléfono Arrendador<br />
                    <asp:UpdatePanel ID="upTelefonoarrendador" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" MaxLength="128" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>        
                </td>
                <td class="tdD" style="height: 51px; text-align:left">
                    Valor Arriendo<br />
                    <asp:UpdatePanel ID="upValorArriendo" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtValorArriendo" runat="server" CssClass="textbox" MaxLength="128" />
                            <asp:MaskedEditExtender ID="mskValorArriendo" runat="server" TargetControlID="txtValorArriendo"
                                Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                                DisplayMoney="Left" ErrorTooltipEnabled="True" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="tdD" style="height: 51px; text-align:left; width: 254px;">
                    Estado Civil<br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" 
                                onselectedindexchanged="ddlEstadoCivil_SelectedIndexChanged" 
                                AutoPostBack="True" CssClass="dropdown" Width="244px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>  
                <td class="tdD" style="height: 51px; text-align:left; width: 137px;">
                    Numero Hijos
                    <asp:CompareValidator ID="cvNumeroHijos" runat="server" ControlToValidate="txtNumeroHijos"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtNumeroHijos" runat="server" CssClass="textbox" 
                        MaxLength="2" Width="101px" />
                </td>
                <td class="tdD" style="height: 51px; text-align:left; width: 188px;">
                    # Personas a Cargo
                    <asp:CompareValidator ID="cvNumeroPersonasCargo" runat="server" 
                        ControlToValidate="txtNumeroPersonasCargo" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtNumeroPersonasCargo" runat="server" CssClass="textbox" 
                        MaxLength="2" />
                </td>
                <td class="tdD" style="height: 51px; text-align:left; width: 259px;">
                    Nivel Academico<br />
                    <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" CssClass="dropdown" 
                        Width="247px">
                    </asp:DropDownList>
                </td>
                <td class="tdI" style="width: 984px; text-align:left;">
                    Ocupación<br />
                    <asp:TextBox ID="txtOcupacion" runat="server" CssClass="textbox" 
                        MaxLength="20" Width="225px" />
                </td>
            </tr>
        </table>
        <hr width="1000"/>
        <table width="1000">
            <tr>
                <td class="tdI" style="width: 1254px; text-align:left;" colspan="3">
                    Empresa Donde Trabaja<br />
                    <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="128" 
                        Width="276px" />
                </td>
                <td class="tdI" style="text-align:left;">
                    Cargo<br />
                    <asp:DropDownList ID="ddlCargo" runat="server" 
                        onselectedindexchanged="ddlCargo_SelectedIndexChanged" CssClass="dropdown" 
                        Width="151px">
                    </asp:DropDownList>
                </td>
                <td class="tdI" style="text-align:left;">
                    Salario<br />
                    <asp:TextBox ID="txtSalario" runat="server" CssClass="textbox" MaxLength="128" />
                    <asp:MaskedEditExtender ID="mskSalario" runat="server" TargetControlID="txtSalario"
                        Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />
                </td>
                <td class="tdI" style="width: 1389px; text-align:left;">
                    Antiguedad Laboral<br />
                    <asp:CompareValidator ID="cvAntiguedadLaboral" runat="server" 
                        ControlToValidate="txtAntiguedadLaboral" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <asp:TextBox ID="txtAntiguedadLaboral" runat="server" CssClass="textbox" 
                        MaxLength="128" />               
                </td>
                <td class="tdD" colspan="3" style="width: 1040px; text-align:left;">
                    Tipo de Contrato<br />
                    <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="dropdown" 
                        Width="221px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="1000">
            <tr>
                <td class="tdI" style="width: 1000px; text-align:left">
                    <strong>Direccion Trabajo</strong><br />
                    <uc1:direccion ID="txtDireccionTrabajo" runat="server" />
                </td>
            </tr>
        </table>
        <table width="1000">
            <tr>
                <td class="tdD" style="text-align:left;">
                    Teléfono Trabajo
                    <br />
                    <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" 
                        MaxLength="50" />
                </td>
                <td class="tdI" style="width: 1254px; text-align:left;">
                    Celular
                    <br />
                    <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" 
                        MaxLength="50" />                                               
                </td>
            </tr>            
        </table>
        <hr />
        <table style="width: 1000px">            
            <tr>
                <td class="tdI" style="width: 521px; text-align:left;">
                    Parentesco con el Titular del Credito<br />
                    <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="dropdown" 
                        Height="28px" Width="461px">
                    </asp:DropDownList>                                               
                </td>
                <td class="tdD" colspan="2" style="text-align:left;">
                    Opinion que Tiene del Titular del Crédito<asp:RadioButtonList 
                        ID="rblOpinion" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="B">Buena</asp:ListItem>
                        <asp:ListItem Value="R">Regular</asp:ListItem>
                        <asp:ListItem Value="M">Mala</asp:ListItem>
                    </asp:RadioButtonList>                
                </td>
            </tr>
            <tr>
                <td class="tdI" colspan="2" style="text-align:left;">
                    ¿Sabe la Responsabilidad que Asume Como Codeudor del Crédito?<br />
                    <asp:RadioButtonList ID="rblResponsabilidad"
                        runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:RadioButtonList>                               
                </td>
                <td class="gridIco" style="width: 274px">
                    &nbsp;</td>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <hr width="1000" style="border-style: inset; left:0"/>
    <asp:Panel ID="PanelListaCon" runat="server">
        <table style="height: 7px; width: 990px;" border="1">
            <tr>
                <td class="tdI" style="height: 51px;">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False">Por favor ingresar datos del conyuge</asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>            
            <asp:Panel ID="pnlConyuge" runat="server" Width="1000">
                <table width="1000">
                    <tr>
                        <td style="text-align: center;color: #FFFFFF; background-color: #0066FF; height: 26px;"  
                            colspan="4">
                            &nbsp;<strong style="text-align: center">DATOS DEL CONYUGE</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; " colspan="4">
                            <asp:GridView ID="gvListaConyuge" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" DataKeyNames="COD_PERSONA" 
                                HeaderStyle-CssClass="gridHeader" 
                                OnPageIndexChanging="gvLista_PageIndexChanging" 
                                OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" 
                                OnRowEditing="gvLista_RowEditing" 
                                OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                                PagerStyle-CssClass="gridPager" PageSize="2" RowStyle-CssClass="gridItem" 
                                style="font-size: x-small" Visible="false" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="COD_PERSONA" HeaderStyle-CssClass="gridColNo" 
                                        HeaderText="Cod_persona" ItemStyle-CssClass="gridColNo" />
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo0" runat="server" CommandName="Select" 
                                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar0" runat="server" CommandName="Delete" 
                                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gI" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TIPO_PERSONA" HeaderText="Tipo_persona" 
                                        Visible="false" />
                                    <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificacion" />
                                    <asp:BoundField DataField="DIGITO_VERIFICACION" 
                                        HeaderText="Digito_verificacion" Visible="false" />
                                    <asp:BoundField DataField="TIPO_IDENTIFICACION" 
                                        HeaderText="Tipo_identificacion" Visible="false" />
                                    <asp:BoundField DataField="FECHAEXPEDICION" HeaderText="Fechaexpedicion" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CODCIUDADEXPEDICION" 
                                        HeaderText="Codciudadexpedicion" Visible="false" />
                                    <asp:BoundField DataField="SEXO" HeaderText="Sexo" Visible="false" />
                                    <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer_nombre" />
                                    <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo_nombre" />
                                    <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer_apellido" />
                                    <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo_apellido" />
                                    <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razon_social" 
                                        Visible="false" />
                                    <asp:BoundField DataField="FECHANACIMIENTO" HeaderText="Fechanacimiento" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CODCIUDADNACIMIENTO" 
                                        HeaderText="Codciudadnacimiento" Visible="false" />
                                    <asp:BoundField DataField="CODESTADOCIVIL" HeaderText="Codestadocivil" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CODESCOLARIDAD" HeaderText="Codescolaridad" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CODACTIVIDAD" HeaderText="Codactividad" 
                                        Visible="false" />
                                    <asp:BoundField DataField="DIRECCION" HeaderText="Direccion" Visible="false" />
                                    <asp:BoundField DataField="TELEFONO" HeaderText="Telefono" Visible="false" />
                                    <asp:BoundField DataField="CODCIUDADRESIDENCIA" 
                                        HeaderText="Codciudadresidencia" Visible="false" />
                                    <asp:BoundField DataField="ANTIGUEDADLUGAR" HeaderText="Antiguedadlugar" 
                                        Visible="false" />
                                    <asp:BoundField DataField="TIPOVIVIENDA" HeaderText="Tipovivienda" 
                                        Visible="false" />
                                    <asp:BoundField DataField="ARRENDADOR" HeaderText="Arrendador" 
                                        Visible="false" />
                                    <asp:BoundField DataField="TELEFONOARRENDADOR" HeaderText="Telefonoarrendador" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CELULAR" HeaderText="Celular" Visible="false" />
                                    <asp:BoundField DataField="EMAIL" HeaderText="Email" Visible="false" />
                                    <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" Visible="false" />
                                    <asp:BoundField DataField="TELEFONOEMPRESA" HeaderText="Telefonoempresa" 
                                        Visible="false" />
                                    <asp:BoundField DataField="CODCARGO" HeaderText="Codcargo" Visible="false" />
                                    <asp:BoundField DataField="CODTIPOCONTRATO" HeaderText="Codtipocontrato" 
                                        Visible="false" />
                                    <asp:BoundField DataField="COD_ASESOR" HeaderText="Cod_asesor" 
                                        Visible="false" />
                                    <asp:BoundField DataField="RESIDENTE" HeaderText="Residente" Visible="false" />
                                    <asp:BoundField DataField="FECHA_RESIDENCIA" HeaderText="Fecha_residencia" 
                                        Visible="false" />
                                    <asp:BoundField DataField="COD_OFICINA" HeaderText="Cod_oficina" 
                                        Visible="false" />
                                    <asp:BoundField DataField="TRATAMIENTO" HeaderText="Tratamiento" 
                                        Visible="false" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />
                                    <asp:BoundField DataField="FECHACREACION" HeaderText="Fechacreacion" 
                                        Visible="false" />
                                    <asp:BoundField DataField="USUARIOCREACION" HeaderText="Usuariocreacion" 
                                        Visible="false" />
                                    <asp:BoundField DataField="FECULTMOD" HeaderText="Fecultmod" Visible="false" />
                                    <asp:BoundField DataField="USUULTMOD" HeaderText="Usuultmod" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; width: 229px;">
                            Primer nombre<br />
                            <asp:TextBox ID="txtPrimer_nombreConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" style="text-transform:uppercase" Width="220px" />
                        </td>
                        <td class="tdI" style="text-align:left; width: 234px;">
                            Segundo nombre<br />
                            <asp:TextBox ID="txtSegundo_nombreConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" style="text-transform:uppercase" Width="220px" />
                        </td>
                        <td class="tdI" style="text-align:left; width: 235px;">
                            Primer apellido<br />
                            <asp:TextBox ID="txtPrimer_apellidoConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" ontextchanged="txtPrimer_apellidoConyuge_TextChanged" 
                                style="text-transform:uppercase" Width="220px" />
                        </td>
                        <td class="tdI" style="text-align:left">
                            Segundo apellido<br />
                            <asp:TextBox ID="txtSegundo_apellidoConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" style="text-transform:uppercase" Width="220px" />
                        </td>
                    </tr>
                </table>
                <table width="1000">
                    <tr>
                        <td class="tdI" style="text-align:left; width: 201px;">
                            Tipo identificación<br />
                            <asp:DropDownList ID="ddlTipoConyuge" runat="server" CssClass="dropdown" 
                                Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" style="text-align:left; width: 179px;">
                            Identificación
                            <br />
                            <asp:TextBox ID="txtIdentificacionConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </td>
                        <td class="tdI" style="text-align:left">
                            Ciudad expedición<br />
                            <asp:DropDownList ID="ddlLugarExpedicionConyuge" runat="server" 
                                CssClass="dropdown" Width="202px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" style="text-align:left">
                            Fecha nacimiento <asp:CompareValidator ID="cvFECHANACIMIENTO0Conyuge" 
                                runat="server" ControlToValidate="txtFechanacimientoConyuge" Display="Dynamic" 
                                ErrorMessage="Formato de Fecha (dd/mm/aaaa)" ForeColor="Red" 
                                Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" 
                                Type="Date" ValidationGroup="vgGuardar" />
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtFechanacimientoConyuge" runat="server" CssClass="textbox" 
                                        MaxLength="128" AutoPostBack="True" 
                                        ontextchanged="txtFechanacimientoConyuge_TextChanged" />
                                    <asp:MaskedEditExtender ID="mskFechanacimientoConyuge" runat="server" TargetControlID="txtFechanacimientoConyuge"
                                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />
                                    <asp:MaskedEditValidator ID="mevFechanacimientoConyuge" runat="server" ControlExtender="mskFechanacimientoConyuge"
                                        ControlToValidate="txtFechanacimientoConyuge" EmptyValueMessage="Fecha Requerida"
                                        InvalidValueMessage="Fecha No Valida" Display="Dynamic" TooltipMessage="Seleccione una Fecha"
                                        EmptyValueBlurredText="Fecha No Valida" InvalidValueBlurredMessage="Fecha No Valida"
                                        ValidationGroup="vgGuardar" ForeColor="Red" />
                                    <asp:CalendarExtender ID="txtFechanacimientoConyuge_CalendarExtender" runat="server"
                                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimientoConyuge"
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>                            
                        </td>
                        <td>
                            Edad<br />
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblEdadConyuge" runat="server"></asp:Label>
                                    <br />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtFechanacimientoConyuge" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </td>
                    </tr>
                </table>
                <table width="1000">
                    <tr>
                        <td class="tdI" style="text-align:left; width: 439px;">
                            Dirección<br />
                            <uc1:direccion ID="txtDireccionConyuge" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            Teléfono<asp:CompareValidator ID="cvtxtTelefonoConyuge" runat="server" 
                                ControlToValidate="txtTelefonoConyuge" Display="Dynamic" 
                                ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                ValidationGroup="vgGuardar" /><br />
                            <asp:TextBox ID="txtTelefonoConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </td>
                    </tr>
                </table>
                <table width="1000">
                    <tr>
                        <td class="tdI" style="text-align:left">
                            Cargo<br />
                            <asp:DropDownList ID="ddlCargoConyuge" runat="server" CssClass="dropdown" 
                                Width="187px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" style="text-align:left; width: 236px;">
                            Empresa Donde Trabaja<br />
                            <asp:TextBox ID="txtEmpresaConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="248px" />
                        </td>
                        <td style="text-align:left; width: 150px;">
                            Salario
                            <br />
                            <asp:TextBox ID="txtSalarioConyuge" runat="server" CssClass="textbox" MaxLength="128" />
                            <asp:MaskedEditExtender ID="mskSalarioConyuge" runat="server" TargetControlID="txtSalarioConyuge"
                                Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                                DisplayMoney="Left" ErrorTooltipEnabled="True" />
                        </td>
                        <td class="tdI" style="text-align:left">
                            Antiguedad Laboral
                            <asp:CompareValidator ID="cvAntiguedadLaboralConyuge" runat="server" 
                                ControlToValidate="txtAntiguedadLaboralConyuge" Display="Dynamic" 
                                ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                ValidationGroup="vgGuardar" />
                            <br />
                            <asp:TextBox ID="txtAntiguedadLaboralConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </td>
                        <td class="tdI" style="text-align:left">
                            Tipo de Contrato<br />
                            <asp:DropDownList ID="ddlTipoContratoConyuge" runat="server" 
                                CssClass="dropdown" Width="170px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>                
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlEstadoCivil" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel4" runat="server">
        <table  width="1000" style="height: 500px">
            <tr>
                <td>
                    <iframe id="ifReferenciasCod" frameborder="0" scrolling="no" src="../Referencias/Lista.aspx"
                        width="100%" onload="autoResize(this.id);" style="height: 500px"></iframe>
                </td>
            </tr>
        </table>
    </asp:Panel>


    <%--Panel con datos del conyugue que no se usan--%>

    <asp:Panel ID="Panel3" runat="server" Visible="False">
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td class="tdD">
                    Ciudadresidencia<br />
                    <asp:DropDownList ID="ddlLugarResidencia" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Tipo persona<asp:RadioButtonList ID="rblTipoPersona" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
                        <asp:ListItem Value="J">Juridica</asp:ListItem>
                    </asp:RadioButtonList>
                    Razon social<br />
                    <asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Fecha expedición&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHAEXPEDICION0" runat="server"
                        ControlToValidate="txtFechaexpedicion" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                        Type="Date" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Actividad<br />
                    <asp:DropDownList ID="ddlActividad" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    Cod oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_OFICINA2" runat="server"
                        ControlToValidate="txtCod_oficina" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" />
                    <asp:CompareValidator ID="cvCOD_OFICINA0" runat="server" ControlToValidate="txtCod_oficina"
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox>
                    <br />
                    Cod asesor&nbsp;&nbsp;<asp:CompareValidator ID="cvCOD_ASESOR" runat="server" ControlToValidate="txtCod_asesor"
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Fecha residencia&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHA_RESIDENCIA" runat="server"
                        ControlToValidate="txtFecha_residencia" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                        Type="Date" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Residente&nbsp;&nbsp;<asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:RadioButtonList>
                    Email&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Tratamiento&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Antiguedad lugar&nbsp;&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR0" runat="server"
                        ControlToValidate="txtAntiguedadlugar" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" MaxLength="128" />
                    <br />
                    Digito verificación&nbsp;&nbsp;<asp:CompareValidator ID="cvDIGITO_VERIFICACION0"
                        runat="server" ControlToValidate="txtDigito_verificacion" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" MaxLength="128" />
                </td>
            </tr>
            <tr>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

    <%--Panel con datos del conyugue que no se usan--%>

    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td class="tdD">
                    &nbsp;</td>
            </tr>                                   
            <tr>
                <td class="tdD">
                    <asp:UpdatePanel ID="upTipoViviendaConyuge" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rblTipoViviendaConyuge" runat="server" 
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                                <asp:ListItem Value="A">Arrendada</asp:ListItem>
                                <asp:ListItem Value="F">Familiar</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    Ciudadnacimiento&nbsp;<br />
                    <asp:DropDownList ID="ddlLugarNacimientoConyuge" runat="server" 
                        CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Arrendador<asp:UpdatePanel ID="upArrendadorConyuge" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtArrendadorConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    Valor Arriendo<asp:UpdatePanel ID="upValorArriendoConyuge" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtValorArriendoConyuge" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    Teléfono Arrendador&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefonoarrendador1Conyuge" 
                        runat="server" ControlToValidate="txtTelefonoarrendadorConyuge" 
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" 
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <asp:UpdatePanel ID="upTelefonoarrendadorConyuge" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtTelefonoarrendadorConyuge" runat="server" 
                                CssClass="textbox" MaxLength="128" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    Numero Hijos
                    <asp:CompareValidator ID="cvNumeroHijosConyuge" runat="server" 
                        ControlToValidate="txtNumeroHijosConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtNumeroHijosConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Estado Civil<br />
                    <asp:DropDownList ID="ddlEstadoCivilConyuge" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Numero Personas a Cargo
                    <asp:CompareValidator ID="cvNumeroPersonasCargoConyuge" runat="server" 
                        ControlToValidate="txtNumeroPersonasCargoConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtNumeroPersonasCargoConyuge" runat="server" 
                        CssClass="textbox" MaxLength="128" />
                    <br />
                    Nivel Academico<br />
                    <asp:DropDownList ID="ddlNivelEscolaridadConyuge" runat="server" 
                        CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Ocupación<br />
                    <asp:TextBox ID="txtOcupacionConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Teléfono Trabajo&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefonoempresaConyuge" 
                        runat="server" ControlToValidate="txtTelefonoempresaConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtTelefonoempresaConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    <br />
                    Direccion Trabajo<uc1:direccion ID="txtDireccionTrabajoConyuge" runat="server" 
                        Text=" " />
                    <br />
                    Parentesco con el Titular del Credito<br />
                    <asp:DropDownList ID="ddlParentescoConyuge" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Celular&nbsp;<asp:CompareValidator ID="cvtxtCelularConyuge" runat="server" 
                        ControlToValidate="txtCelularConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    &nbsp;<asp:TextBox ID="txtCelularConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Sabe la Responsabilidad que Asume Como Codeudor del Crédito<br />
                    <asp:RadioButtonList ID="rblResponsabilidadConyuge" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:RadioButtonList>
                    Opinion que Tiene del Titular del Crédito<asp:RadioButtonList 
                        ID="rblOpinionConyuge" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="B">Buena</asp:ListItem>
                        <asp:ListItem Value="R">Regular</asp:ListItem>
                        <asp:ListItem Value="M">Mala</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    Barrio<br />
                    <asp:DropDownList ID="ddlBarrioConyuge" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Ciudadresidencia<br />
                    <asp:DropDownList ID="ddlLugarResidenciaConyuge" runat="server" 
                        CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                    Tipo persona<asp:RadioButtonList ID="rblTipoPersonaConyuge" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
                        <asp:ListItem Value="J">Juridica</asp:ListItem>
                    </asp:RadioButtonList>
                    Razon social<br />
                    <asp:TextBox ID="txtRazon_socialConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Fecha expedición&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHAEXPEDICION0Conyuge" 
                        runat="server" ControlToValidate="txtFechaexpedicionConyuge" Display="Dynamic" 
                        ErrorMessage="Formato de Fecha (dd/mm/aaaa)" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" 
                        Type="Date" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtFechaexpedicionConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Actividad<br />
                    <asp:DropDownList ID="ddlActividadConyuge" runat="server">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="ddlEstadoConyuge" runat="server">
                    </asp:DropDownList>
                    Cod oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_OFICINA2Conyuge" 
                        runat="server" ControlToValidate="txtCod_oficinaConyuge" Display="Dynamic" 
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationGroup="vgGuardar" />
                    <asp:CompareValidator ID="cvCOD_OFICINA0Conyuge" runat="server" 
                        ControlToValidate="txtCod_oficinaConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtCod_oficinaConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128">1</asp:TextBox>
                    <br />
                    Cod asesor&nbsp;&nbsp;<asp:CompareValidator ID="cvCOD_ASESORConyuge" runat="server" 
                        ControlToValidate="txtCod_asesorConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtCod_asesorConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Fecha residencia&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHA_RESIDENCIAConyuge" 
                        runat="server" ControlToValidate="txtFecha_residenciaConyuge" Display="Dynamic" 
                        ErrorMessage="Formato de Fecha (dd/mm/aaaa)" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" 
                        Type="Date" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtFecha_residenciaConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Residente&nbsp;&nbsp;<asp:RadioButtonList ID="rblResidenteConyuge" runat="server" 
                        AutoPostBack="True" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:RadioButtonList>
                    Email&nbsp;&nbsp;<asp:TextBox ID="txtEmailConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Tratamiento&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtTratamientoConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Antiguedad lugar&nbsp;&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR0Conyuge" 
                        runat="server" ControlToValidate="txtAntiguedadlugarConyuge" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtAntiguedadlugarConyuge" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <br />
                    Sexo&nbsp;&nbsp;<asp:RadioButtonList ID="rblSexoConyuge" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">F</asp:ListItem>
                        <asp:ListItem>M</asp:ListItem>
                    </asp:RadioButtonList>
                    Digito verificación&nbsp;&nbsp;<asp:CompareValidator ID="cvDIGITO_VERIFICACION0Conyuge" 
                        runat="server" ControlToValidate="txtDigito_verificacionConyuge" 
                        Display="Dynamic" ErrorMessage="Solo se admiten números enteros" 
                        ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtDigito_verificacionConyuge" runat="server" 
                        CssClass="textbox" MaxLength="128" />
                </td>
            </tr>
            <tr>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>                           

</asp:Content>
