<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Personas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }
        
    </script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">       
        <asp:View ID="vwDetalleCliente" runat="server"  EnableTheming="True">
            <asp:Panel ID="panelDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos Básicos</strong>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3">
                        <asp:Panel ID="Panel7" runat="server" Width="100%">
                            <table style="width:100%" cellpadding="0">
                                <tr>
                                    <td style="width: 120px; text-align:left;">
                                        Identificación*&nbsp;
                                        <span style="font-size: xx-small">
                                        <asp:RequiredFieldValidator 
                                            ID="rfvIdentificacion" runat="server" 
                                            ControlToValidate="txtIdentificacionE" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            style="font-size: x-small" ValidationGroup="vgConsultar" />
                                        </span>
                                    </td>
                                    <td style="width: 155px;">
                                        <asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox" Width="120px">
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td class="gridIco" style="width: 17px; text-align: left;">
                                        No.<br />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtIdentificacionE" runat="server" CssClass="textbox" onkeypress="return ValidNum(event);"
                                            MaxLength="20" Width="110px" AutoPostBack="True" ontextchanged="txtIdentificacionE_TextChanged" />
                                        <br />
                                    </td>
                                    <td style="text-align:right;">
                                        Tipo Persona
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rblTipo_persona" runat="server" AutoPostBack="True"                                                     
                                                    RepeatDirection="Horizontal" Width="155px" Enabled="False" 
                                                    style="font-size: x-small">
                                                    <asp:ListItem Selected="True">Natural</asp:ListItem>
                                                    <asp:ListItem>Jurídica</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>                                    
                                    <td style="text-align: left;">
                                        Oficina&nbsp;
                                        <asp:DropDownList ID="txtCod_oficina" runat="server" CssClass="textbox" 
                                            Width="160px" AppendDataBoundItems="True">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvOficina" runat="server" 
                                            ControlToValidate="txtCod_oficina" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgGuardar" style="font-size: x-small" />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
                                            MaxLength="80" Visible="False" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3">
                        <asp:Panel ID="PanelDatos1" runat="server" Width="100%">
                            <hr style="width: 100%" />
                            <table style="width:100%; " cellpadding="0">
                                <tr>
                                    <td style="text-align:left;">
                                        Primer Nombre
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                            ControlToValidate="txtPrimer_nombreE" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            style="font-size: x-small" ValidationGroup="vgGuardar" />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox" 
                                            MaxLength="128" style="text-transform:uppercase" Width="284px" />
                                        <br />
                                    </td>
                                    <td style="text-align:left;">
                                        Segundo Nombre<br />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" 
                                            MaxLength="128" style="text-transform:uppercase" Width="326px" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        Primer Apellido
                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" 
                                            ControlToValidate="txtPrimer_apellidoE" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgGuardar" style="font-size: x-small;" />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox" 
                                            MaxLength="128" Width="284px" style="text-transform:uppercase"/>
                                        <br />
                                    </td>
                                    <td style="text-align:left;">
                                        Segundo Apellido
                                        <br />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" 
                                            MaxLength="128" Width="326px" style="text-transform:uppercase"/>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3">
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <table style="width:100%;" cellpadding="0">
                                <tr>
                                    <td style="text-align:left;">
                                        <hr style="width: 100%" />
                                    </td>
                                    <td rowspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; width: 100%;">
                                        <strong>Dirección Residencia</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <uc1:direccion ID="txtDireccionE" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3" style="width:100% " >
                        <asp:Panel ID="PanelDirCor" runat="server" Width="100%">
                            <table style="width:100%; text-align:left;" cellpadding="0">
                                <tr>
                                    <td>
                                        <hr style="height: -15px; width: 100%;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <strong>Información de Correspondencia</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:direccion ID="txtDirCorrespondencia" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelDatos2" runat="server" Width="100%">
                            <table style="width:100%; text-align:left;" cellpadding="0">
                                <tr>
                                    <td style="text-align:left;">
                                        Barrio Correspondencia
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" 
                                            CssClass="textbox" Width="160px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBarrioCorrespondencia" runat="server" 
                                            ControlToValidate="ddlBarrioCorrespondencia" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                                            InitialValue="Seleccione un item" />
                                    </td>
                                    <td style="text-align:left;">
                                        Teléfono Correspondencia
                                        <span style="font-size: xx-small">
                                        <asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator21" runat="server" 
                                            ControlToValidate="txtTelCorrespondencia" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            style="font-size: x-small" ValidationGroup="vgGuardar" />
                                        </span>
                                    </td>
                                    <td style="text-align:left; font-size: x-small;">
                                        <asp:TextBox ID="txtTelCorrespondencia" runat="server" CssClass="textbox" 
                                            MaxLength="20" Width="100px" />                                        
                                    </td>
                                    <td style="text-align:left;" >
                                        Ciudad Correspondencia                                        
                                    </td>
                                    <td colspan="2" style="text-align:left;">
                                        <asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" CssClass="textbox" 
                                            AppendDataBoundItems="True" Width="164px"> 
                                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCiuCorrespondencia" runat="server" 
                                            ControlToValidate="ddlCiuCorrespondencia" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                                            InitialValue="Seleccione un item" />
                                    </td>
                                    <td style="text-align:left;" >      
                                        &nbsp;                                  
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI" colspan="9">
                                        <hr style="width: 100%; text-align: left; margin-left: 0px;" /> 
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>         
            </table>       
            <table style="text-align: left; width: 100%" cellpadding="0">
                <tr>
                    <td style="text-align: left;">
                        Celular
                        <asp:RequiredFieldValidator ID="rfvCelular" runat="server" 
                            ControlToValidate="txtCelular" Display="Dynamic" ErrorMessage="Campo Requerido" 
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                            style="font-size: xx-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" 
                            MaxLength="50" Width="148px" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Actividad CIIU
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlActividadE" runat="server" Width="158px" CssClass="textbox" AppendDataBoundItems="True">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvActividadE" runat="server" 
                            ControlToValidate="ddlActividadE" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Teléfono Residencia
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                            ControlToValidate="txtTelefonoE" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtTelefonoE" runat="server" CssClass="textbox"
                            MaxLength="128" Width="156px" />
                        <asp:FilteredTextBoxExtender ID="txtTelefonoE_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txtTelefonoE">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Fecha Nacimiento                        
                    </td>
                    <td style="text-align: left;">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="10"
                                    OnTextChanged="txtFechanacimiento_TextChanged" AutoPostBack="True" 
                                    ValidationGroup="vgGuardar" Width="148px"> </asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtFechanacimiento"
                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                    ControlToValidate="txtFechanacimiento" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                                    Display="Dynamic" TooltipMessage="Seleccione una Fecha" 
                                    EmptyValueBlurredText="Fecha No Valida" InvalidValueBlurredMessage="Fecha No Valida"
                                    ValidationGroup="vgGuardar" ForeColor="Red" style="font-size: x-small" />
                                <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimiento" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                    &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp;Fecha Expedición Cédula
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                            ControlToValidate="txtFechaexpedicion" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" 
                            MaxLength="10" Width="150px" />
                        <asp:MaskedEditExtender ID="mskFechaexpedicion" runat="server" TargetControlID="txtFechaexpedicion"
                            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        <asp:MaskedEditValidator ID="mevFechaexpedicion" runat="server" ControlExtender="mskFechaexpedicion"
                            ControlToValidate="txtFechaexpedicion" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                            Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                            InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar" 
                            ForeColor="Red" style="font-size: x-small" />
                        <asp:CalendarExtender ID="txtFechaexpedicion_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaexpedicion" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                            &nbsp;
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Ciudad Exped. Cédula                        
                    </td>
                    <td style="text-align: left;" >
                        <asp:DropDownList ID="ddlLugarExpedicion" runat="server" Width="164px" 
                            CssClass="textbox" AppendDataBoundItems="True">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLugarExpedicion" runat="server" 
                            ControlToValidate="ddlLugarExpedicion" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Sexo<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" 
                            Height="22px" style="font-size: small; text-align: left;" Width="139px" 
                            CellPadding="0" CellSpacing="0" >
                            <asp:ListItem Selected="True">F</asp:ListItem>
                            <asp:ListItem>M</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Nivel Educativo<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" Width="158px" 
                            CssClass="textbox" AppendDataBoundItems="True">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvNivelEscolaridad" runat="server" 
                            ControlToValidate="ddlNivelEscolaridad" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Ciudad Nacimiento                        
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlLugarNacimiento" runat="server" Width="164px" 
                            CssClass="textbox" AppendDataBoundItems="True">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLugarNacimiento" runat="server" 
                            ControlToValidate="ddlLugarNacimiento" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Estado Civil<br /> 
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlEstadoCivil" runat="server" 
                            Width="156px" CssClass="textbox" AppendDataBoundItems="True">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>                        
                        <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server" 
                            ControlToValidate="ddlEstadoCivil" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp; Personas a Cargo
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPersonasCargo" runat="server" CssClass="textbox" 
                            MaxLength="100" Width="150px" Visible="true" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPersonasCargo" runat="server" 
                            ControlToValidate="txtPersonasCargo" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: x-small" />
                    </td>
                    <td style="text-align:left;">
                        &nbsp;&nbsp; Edad del Cliente
                    </td>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="upEdad" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtEdadCliente" runat="server" Enabled="False" 
                                    CssClass="textbox" Width="158px"></asp:TextBox>
                                <asp:RangeValidator ID="rvEdad" runat="server" 
                                    ControlToValidate="txtEdadCliente" ErrorMessage="Debe estar entre 0 y 90" 
                                    ForeColor="Red" MaximumValue="90" MinimumValue="0" 
                                    ValidationGroup="vgGuardar" style="font-size: xx-small"> </asp:RangeValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Profesión</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtProfecion" runat="server" CssClass="textbox" 
                            style="text-transform:uppercase" MaxLength="100" Visible="true" Width="148px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">                           
                        &nbsp;&nbsp; Estrato
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox" MaxLength="100" 
                            Visible="true" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEstraro" runat="server" 
                            ControlToValidate="txtEstrato" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            style="font-size: xx-small" ValidationGroup="vgGuardar" />                        
                    </td>
                    <td style="text-align: left;">
                        &nbsp;&nbsp;
                    </td>
                    <td style="text-align:left;">                        
                    </td>
                    <td style="text-align:left;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width:100%; margin-right: 7px;" cellpadding="0">
                <tr>
                    <td style="text-align:left;" colspan="4">
                        <hr  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        Tipo Vivienda
                    </td>
                    <td style="text-align:left;" colspan="3">
                        <asp:UpdatePanel ID="upTipoVivienda" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rblTipoVivienda" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged" RepeatDirection="Horizontal"
                                Width="370px">
                                <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                                <asp:ListItem Value="A">Arrendada</asp:ListItem>
                                <asp:ListItem Value="F">Familiar</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        Nombre Arrendador
                    </td>
                    <td style="text-align:left;">
                        <asp:UpdatePanel ID="upArrendador" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128"
                                    Width="299px" style="text-align: left; text-transform:uppercase" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align:left">
                            &nbsp;&nbsp; Teléfono Arrendador
                    </td>
                    <td style="text-align:left">
                        <asp:UpdatePanel ID="upTelefonoArrendador" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" 
                                    MaxLength="128" Width="148px" 
                                    style="margin-left: 0px" />
                                <asp:FilteredTextBoxExtender ID="txtTelefonoarrendador_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txtTelefonoarrendador"></asp:FilteredTextBoxExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        Antigüedad en la Vivienda (Meses)<br /> <span style="font-size: xx-small">&nbsp;</span>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                            ControlToValidate="txtAntiguedadlugar" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: x-small" />--%>
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" 
                            MaxLength="8" Width="100px" style="text-align: left" />
                        <asp:FilteredTextBoxExtender ID="txtAntiguedadlugar_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txtAntiguedadlugar"></asp:FilteredTextBoxExtender>                        
                    </td>
                    <td style="text-align:left;">
                        &nbsp;&nbsp; Valor Arriendo
                    </td>
                    <td style="text-align:left;">
                        <asp:UpdatePanel ID="upValorArriendo" runat="server">
                            <ContentTemplate>                                                
                                <uc2:decimales ID="txtValorArriendo" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left;">
                        <hr />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelNegocio" runat="server" Visible="true">
                <table style="width:100%;" cellpadding="0">
                    <tr>
                        <td style="text-align:left; " colspan="2">
                            <strong>Información Laboral</strong>
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left; width: 185px;">
                            &nbsp;                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            Empresa
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="120" 
                                Width="280px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEmpresa" runat="server" 
                                ControlToValidate="txtEmpresa" Display="Dynamic" ErrorMessage="Debe ingresar el nombre de la empresa" 
                                ForeColor="Red" SetFocusOnError="True" style="font-size: x-small" 
                                ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp; Cargo
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox" Width="166px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp;Teléfono Empresa                        
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="140px"></asp:TextBox>
                            <asp:CompareValidator ID="cvtxtTelefonoempresa2" runat="server" 
                                ControlToValidate="txtTelefonoempresa" Display="Dynamic" 
                                ErrorMessage="Sólo se admiten números enteros" ForeColor="Red" 
                                Operator="DataTypeCheck" SetFocusOnError="True" style="font-size: x-small" 
                                Type="Integer" ValidationGroup="vgGuardar" />
                            <asp:RequiredFieldValidator ID="rfvtxtTelEmpresa" runat="server" 
                                ControlToValidate="txtTelefonoempresa" Display="Dynamic" 
                                ErrorMessage="Debe ingresar el telefóno de la empresa" ForeColor="Red" SetFocusOnError="True" 
                                ValidationGroup="vgGuardar" style="font-size: x-small" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            E-mail
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="120" 
                                Width="280px" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" 
                                ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Campo Requerido" 
                                ForeColor="Red" SetFocusOnError="True" style="font-size: x-small" 
                                ValidationGroup="vgGuardar" />
                            <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" 
                                ControlToValidate="txtEmail" ErrorMessage="E-Mail no valido!" ForeColor="Red" 
                                style="font-size: x-small" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp;Antigüedad&nbsp;(Meses)
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtAntiguedadlugarEmpresa" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="158px" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtAntiguedadEmpresa" runat="server" 
                                ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic" 
                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                style="font-size: x-small" ValidationGroup="vgGuardar" />
                            <asp:CompareValidator ID="cvtxtAntiguedadlugarEmpresa" runat="server" 
                                ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic" 
                                ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck" 
                                SetFocusOnError="True" style="font-size: x-small" Type="Integer" 
                                ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp;Celular                                     
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtTelCell0" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            Tipo Contrato
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox" 
                                Width="288px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp;Ciudad
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlCiu0" runat="server" CssClass="textbox" 
                                    Width="166px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left;"> 
                            Actividad Económica                            
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlActividadE0" runat="server" CssClass="textbox" 
                                Width="288px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;&nbsp;Tiene Parentesco Con Empleados de la Entidad &nbsp;
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlparentesco" runat="server" CssClass="textbox" 
                                Width="166px">
                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                <asp:ListItem Value="1">si</asp:ListItem>
                                <asp:ListItem Value="2">No </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>                    
                <table style="width:100%;" cellpadding="0">
                    <tr>
                        <td style="text-align:left">
                            <strong>Dirección de la Empresa</strong></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <uc1:direccion ID="txtDireccionEmpresa" runat="server" Text="0" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelDatosOcultos" runat="server" Visible="false">
                <table style="width:100%;" cellpadding="0">
                    <tr>
                        <td style="text-align:left">
                            <asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True" 
                                RepeatDirection="Horizontal" Visible="False"> 
                                <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" 
                                MaxLength="128" Visible="False" />
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" 
                                MaxLength="128" Visible="False" />
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" 
                                MaxLength="128" Visible="False" />
                            <asp:CalendarExtender ID="txtFecha_residencia_CalendarExtender" runat="server" 
                                DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                TargetControlID="txtFecha_residencia" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                        </td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlEstado" runat="server" Visible="False" 
                                Width="121px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" 
                                MaxLength="128" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>                    
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:center">                        
                        <asp:ImageButton ID="btnGuardar" runat="server" 
                            ImageUrl="~/Images/btnGuardar.jpg"  
                            ValidationGroup="vgGuardar" onclick="btnGuardar_Click"/>
                    </td>
                    <td style="text-align:left">
                        &nbsp;
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
              
    <table style="width: 80%">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

   
</asp:Content>
