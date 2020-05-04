<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="DatosCliente.aspx.cs" Inherits="Lista" Title=".: Xpinn - Datos del Cliente :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td>
                <strong>
                    <label id="lblNombre" runat="server"></label>
                </strong>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="color: #FFFFFF; background-color: #0066FF; width: 100%;">
                <strong>Datos Básicos</strong>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Label ID="Lbledad" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel7" runat="server" Width="980px">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 130px; text-align: left;">Identificación*&nbsp;
                                <asp:TextBox ID="txtCod_personaE" runat="server" CssClass="textbox" Enabled="False"
                                    MaxLength="128" Visible="False" />
                                <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacionE"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    Style="font-size: xx-small" />
                            </td>
                            <td style="width: 155px;">
                                <asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox" Width="150px" />
                            </td>
                            <td style="width: 17px; text-align: left;">No.
                            </td>
                            <td style="width: 108px; text-align: left;">
                                <asp:TextBox ID="txtIdentificacionE" runat="server" CssClass="textbox" MaxLength="20"
                                    Width="97px" Enabled="false" />
                            </td>
                            <td style="width: 20px; text-align: right;">&nbsp;
                            </td>
                            <td style="width: 150px; text-align: left;">Tipo Persona
                            </td>
                            <td style="width: 150px; text-align: left;">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:RadioButtonList ID="rblTipo_persona" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="180px">
                                            <asp:ListItem Text="Natural" Value="N" />
                                            <asp:ListItem Text="Jurídica" Value="J" />
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left;">&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="PanelDatos1" runat="server" Width="980px">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 110px; text-align: left;">Primer Nombre
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtPrimer_nombreE"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    Style="font-size: x-small" />
                            </td>
                            <td style="width: 300px;">
                                <asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox" MaxLength="128"
                                    Style="text-transform: uppercase" Width="284px" Enabled="false" />
                                <br />
                            </td>
                            <td style="width: 130px; text-align: left;">Segundo Nombre
                            </td>
                            <td style="width: 340px; text-align: left;">
                                <asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" Enabled="false"
                                    MaxLength="128" Style="text-transform: uppercase" Width="326px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 110px;">Primer Apellido
                                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtPrimer_apellidoE"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    Style="font-size: xx-small;" />
                            </td>
                            <td style="width: 300px;">
                                <asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox" Enabled="false"
                                    MaxLength="128" Style="text-transform: uppercase" Width="284px" />
                            </td>
                            <td style="width: 130px; text-align: left">Segundo Apellido
                            </td>
                            <td style="width: 340px; text-align: left; font-size: x-small;">
                                <asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" Enabled="false"
                                    MaxLength="128" Style="text-transform: uppercase" Width="326px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 110px;">
                                <asp:Label ID="lblrazonsocial" text="Razón Social" runat="server" /></td>
                            <td style="width: 300px;">
                                <asp:TextBox ID="txtrazonsocial" runat="server" CssClass="textbox" 
                                    Enabled="false" MaxLength="128" Style="text-transform: uppercase" 
                                    Width="284px" />
                            </td>
                            <td style="width: 130px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 340px; text-align: left; font-size: x-small;">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel1" runat="server" Width="100%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 1125px">
                                <hr style="width: 100%;" />
                            </td>
                            <td rowspan="3">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 100%;">
                                <strong>Dirección Residencia </strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 100%">
                                <%--<uc1:direccion ID="txtDireccionE" runat="server" />--%>
                                <asp:TextBox ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 100%">
                <asp:Panel ID="PanelDirCor" runat="server" Width="100%">
                    <table style="width: 100%; text-align: left;">
                        <tr>
                            <td style="text-align: left; width: 100%">
                                <hr style="width: 100%;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 100%">
                                <strong>Información de Correspondencia</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 100%">
                                <%--<uc1:direccion ID="txtDirCorrespondencia" runat="server" />--%>
                                <asp:TextBox ID="txtDirCorrespondencia" runat="server" Width="90%" CssClass="textbox" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelDatos2" runat="server" Width="100%">
                    <table style="width: 100%; text-align: left;">
                        <tr>
                            <td class="logo" style="text-align: left; width: 175px;">Barrio Correspond.&nbsp;
                            </td>
                            <td class="logo" style="text-align: left; width: 179px;">
                                <asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" AppendDataBoundItems="True"
                                    CssClass="textbox" Width="156px">
                                    <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvBarrioCorrespondencia" runat="server" ControlToValidate="ddlBarrioCorrespondencia"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue=""
                                    SetFocusOnError="True" Style="font-size: xx-small" ValidationGroup="vgGuardar" />--%>
                            </td>
                            <td style="text-align: left; width: 159px;">Tel. Correspond.&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTelCorrespondencia"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    Style="font-size: xx-small" />
                            </td>
                            <td style="font-size: x-small;">
                                <asp:TextBox ID="txtTelCorrespondencia" runat="server" CssClass="textbox" MaxLength="20"
                                    Width="99px" />
                            </td>
                            <td style="text-align: left;">Ciudad Correspond.
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" AppendDataBoundItems="True"
                                    CssClass="textbox" Width="132px">
                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCiuCorrespondencia" runat="server" ControlToValidate="ddlCiuCorrespondencia"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item"
                                    SetFocusOnError="True" Style="font-size: xx-small" />
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
    <asp:Panel ID="PanelDatPersNatural" runat="server" Visible="true">
        <table style="text-align: left; width: 100%">
            <tr>
                <td style="width: 891px; text-align: left;">Celular
                    <asp:RequiredFieldValidator ID="rfvCelular" runat="server"
                        ControlToValidate="txtCelular" Display="Dynamic" ErrorMessage="Campo Requerido"
                        ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />
                </td>
                <td style="width: 150px">
                    <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="50" Width="140px" />
                </td>
                <td class="logo" style="width: 1746px; text-align: left;">Actividad CIIU
                </td>
                <td style="width: 152px;">
                    <asp:DropDownList ID="ddlActividadE" runat="server" Width="160px" CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvActividadE" runat="server"
                        ControlToValidate="ddlActividadE" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small"
                        InitialValue="Seleccione un item" />
                </td>
                <td style="width: 1262px; text-align: left;">Teléfono Residencia
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                        ControlToValidate="txtTelefonoE" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />
                </td>
                <td>
                    <asp:TextBox ID="txtTelefonoE" runat="server" CssClass="textbox" MaxLength="128" Width="150px" />
                    <asp:FilteredTextBoxExtender ID="txtTelefonoE_FilteredTextBoxExtender"
                        runat="server" Enabled="True" FilterType="Numbers"
                        TargetControlID="txtTelefonoE">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 891px;">F.Nacimiento
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                        ControlToValidate="txtFechanacimiento" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />--%>
                </td>
                <td style="width: 117px; font-size: x-small;">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFechanacimiento" runat="server" CssClass="textbox" MaxLength="1"
                                OnTextChanged="txtFechanacimiento_TextChanged" AutoPostBack="True" Width="140px">
                            </asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtFechanacimiento"
                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />
                            <%--<asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                ControlToValidate="txtFechanacimiento" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                                Display="Dynamic" TooltipMessage="Seleccione una Fecha"
                                EmptyValueBlurredText="Fecha No Valida" InvalidValueBlurredMessage="Fecha No Valida"
                                ValidationGroup="vgGuardar" ForeColor="Red" Style="font-size: x-small" />--%>
                            <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimiento" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 1746px; text-align: left;">Fecha Exped.Cédula                    
                </td>
                <td style="width: 152px; font-size: x-small;">
                    <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox"
                        MaxLength="10" Width="150px" />
                    <asp:MaskedEditExtender ID="mskFechaexpedicion" runat="server" TargetControlID="txtFechaexpedicion"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                        ErrorTooltipEnabled="True" />
                   <%-- <asp:MaskedEditValidator ID="mevFechaexpedicion" runat="server" ControlExtender="mskFechaexpedicion"
                        ControlToValidate="txtFechaexpedicion" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                        Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                        InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar"
                        ForeColor="Red" Style="font-size: x-small" />--%>
                    <asp:CalendarExtender ID="txtFechaexpedicion_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaexpedicion" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td style="width: 1262px; text-align: left;">Ciudad Exped.Cédula
                </td>
                <td style="font-size: x-small;">
                    <asp:DropDownList ID="ddlLugarExpedicion" runat="server" Width="158px" CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvLugarExpedicion" runat="server"
                        ControlToValidate="ddlLugarExpedicion" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small"
                        InitialValue="Seleccione un item" />
                </td>
            </tr>
            <tr>
                <td style="width: 891px;">Sexo<br style="font-size: x-small" />
                </td>
                <td style="width: 150px; font-size: x-small;">
                    <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal"
                        Style="font-size: small; text-align: left;" Width="139px"
                        CellPadding="0" CellSpacing="0">
                        <asp:ListItem Selected="True">F</asp:ListItem>
                        <asp:ListItem>M</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="width: 1746px;">Nivel Educativo<br style="font-size: x-small" />
                </td>
                <td style="width: 152px;">
                    <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" Width="160px"
                        CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvNivelEscolaridad" runat="server"
                        ControlToValidate="ddlNivelEscolaridad" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small"
                        InitialValue="Seleccione un item" />
                </td>
                <td style="width: 1262px;">Ciudad Nacimiento
                </td>
                <td>
                    <asp:DropDownList ID="ddlLugarNacimiento" runat="server" Width="160px" CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvLugarNacimiento" runat="server"
                        ControlToValidate="ddlLugarNacimiento" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small"
                        InitialValue="Seleccione un item" />
                </td>
            </tr>
            <tr>
                <td style="width: 891px;">Estado Civil<br />
                </td>
                <td style="width: 150px;">
                    <asp:DropDownList ID="ddlEstadoCivil" runat="server"
                        Width="150px" CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server"
                        ControlToValidate="ddlEstadoCivil" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small"
                        InitialValue="Seleccione un item" />
                </td>
                <td style="width: 1746px;">Personas a Cargo
                </td>
                <td style="width: 152px;">
                    <asp:TextBox ID="txtPersonasCargo" runat="server" CssClass="textbox" MaxLength="100" Width="150px" Visible="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPersonasCargo" runat="server"
                        ControlToValidate="txtPersonasCargo" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />
                    <br />
                </td>
                <td style="width: 1262px;">Edad del Cliente<br />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtEdadCliente" runat="server" Enabled="False" CssClass="textbox" Width="150px">
                            </asp:TextBox>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 891px;">Profesión</td>
                <td style="width: 150px;">
                    <asp:TextBox ID="txtProfecion" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                        MaxLength="100" Visible="true" Width="140px"></asp:TextBox>
                </td>
                <td style="width: 1746px;">Estrato
                </td>
                <td style="width: 152px;">
                    <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox"
                        MaxLength="100" Visible="true" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEstraro" runat="server"
                        ControlToValidate="txtEstrato" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />
                </td>
                <td style="width: 1262px;">Email</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtemails" runat="server" CssClass="textbox"
                                Enabled="true" Width="160px" MaxLength="100" Visible="true"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
<%--                    <asp:RequiredFieldValidator ID="rfvtxtRazon_socialE" runat="server"
                        ControlToValidate="txtEstrato" Display="Dynamic"
                        ErrorMessage="Debe ingresar el email" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />--%>
                    <asp:TextBox ID="txtRazon_socialE" runat="server" CssClass="textbox"
                        MaxLength="100" Visible="false" Width="16px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%; margin-right: 7px;">
            <tr>
                <td class="tdI" colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 1103px;">Tipo Vivienda
                </td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <td style="text-align: left; width: 1103px;">Nombre Arrendador
                </td>
                <td style="text-align: left; width: 428px">
                    <asp:UpdatePanel ID="upTipoVivienda" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128"
                                Width="299px" Style="text-align: left; text-transform: uppercase" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 947px; text-align: left">&nbsp; Teléfono Arrendador
                </td>
                <td style="width: 241px; text-align: left">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox"
                                MaxLength="128" Width="148px" Style="margin-left: 0px" />
                            <asp:FilteredTextBoxExtender ID="txtTelefonoarrendador_FilteredTextBoxExtender"
                                runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txtTelefonoarrendador">
                            </asp:FilteredTextBoxExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda"
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 1103px;">Antigüedad en la Vivienda (Meses)<br />
                    <span style="font-size: xx-small">&nbsp;</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                        ControlToValidate="txtAntiguedadlugar" Display="Dynamic"
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        Style="font-size: xx-small" />
                </td>
                <td style="text-align: left; width: 428px;">
                    <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox"
                        MaxLength="8" Width="100px" Style="text-align: left" />
                    <asp:FilteredTextBoxExtender ID="txtAntiguedadlugar_FilteredTextBoxExtender"
                        runat="server" Enabled="True" FilterType="Numbers"
                        TargetControlID="txtAntiguedadlugar">
                    </asp:FilteredTextBoxExtender>
                    <br />
                    <span style="font-size: xx-small">&nbsp;</span>
                </td>
                <td style="width: 947px; text-align: left;">&nbsp; Valor Arriendo
                </td>
                <td style="font-size: x-small; width: 241px; text-align: left;"
                    align="left">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <uc1:decimales ID="txtValorArriendo" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda"
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left;">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Salario</td>
                <td style="text-align: left;"> 
                    <asp:TextBox ID="txtSalario" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" ></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left;">
                    <hr />
                </td>
            </tr>
        </table>
        <asp:Panel ID="NEGOCIOMV" runat="server" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left;" colspan="2">
                        <strong>Información Laboral</strong>
                    </td>
                    <td style="width: 145px; text-align: left">&nbsp;
                    </td>
                    <td style="text-align: left">&nbsp;
                    </td>
                    <td style="text-align: left; width: 185px;">&nbsp;                                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 220px;">Empresa
                    </td>
                    <td style="width: 267px; text-align: left; font-size: xx-small;">
                        <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="120"
                            Width="270px"></asp:TextBox>
<%--                        <asp:RequiredFieldValidator ID="rfvtxtEmpresa" runat="server"
                            ControlToValidate="txtEmpresa" Display="Dynamic" ErrorMessage="Debe ingresar el nombre de la empresa"
                            ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"
                            ValidationGroup="vgGuardar" />--%>
                        <br />
                    </td>
                    <td style="text-align: right; width: 145px;">Cargo
                    </td>
                    <td style="width: 131px; text-align: left; font-size: xx-small;">
                        <asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox" Width="158px">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="text-align: right; width: 185px;">Teléfono Empresa
                    </td>
                    <td style="text-align: left; font-size: xx-small;">
                        <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox"
                            MaxLength="128" Width="140px"></asp:TextBox>
<%--                        <asp:CompareValidator ID="cvtxtTelefonoempresa2" runat="server"
                            ControlToValidate="txtTelefonoempresa" Display="Dynamic"
                            ErrorMessage="Sólo se admiten números enteros" ForeColor="Red"
                            Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: x-small"
                            Type="Integer" ValidationGroup="vgGuardar" />--%>
                        <%--<asp:RequiredFieldValidator ID="rfvtxtTelEmpresa" runat="server"
                            ControlToValidate="txtTelefonoempresa" Display="Dynamic"
                            ErrorMessage="Debe ingresar el telefóno de la empresa" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: xx-small" />--%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 218px;">E-mail
                    </td>
                    <td style="width: 267px; text-align: left;">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="120"
                            Width="270px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server"
                            ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Campo Requerido"
                            ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"
                            ValidationGroup="vgGuardar" />--%>
<%--                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="E-Mail no valido!" ForeColor="Red"
                            Style="font-size: x-small"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                    </td>
                    <td style="width: 145px; text-align: right;">Antigüedad&nbsp;(Meses)
                    </td>
                    <td style="width: 131px; text-align: left;">
                        <asp:TextBox ID="txtAntiguedadlugarEmpresa" runat="server" CssClass="textbox"
                            MaxLength="128" Width="150px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvtxtAntiguedadEmpresa" runat="server"
                            ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small" ValidationGroup="vgGuardar" />--%>
<%--                        <asp:CompareValidator ID="cvtxtAntiguedadlugarEmpresa" runat="server"
                            ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic"
                            ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                            SetFocusOnError="True" Style="font-size: xx-small" Type="Integer"
                            ValidationGroup="vgGuardar" />--%>
                    </td>
                    <td style="text-align: right; width: 185px;">Celular                                     
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtTelCell0" runat="server" CssClass="textbox"
                            MaxLength="128" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 218px;">Tipo Contrato
                    </td>
                    <td style="width: 267px; text-align: left">
                        <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox"
                            Width="280px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 145px; text-align: right">Ciudad</td>
                    <td style="width: 131px">
                        <asp:DropDownList ID="ddlCiu0" runat="server" CssClass="textbox"
                            Width="158px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 185px" class="logo">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 218px; text-align: left">Actividad Económica                            
                    </td>
                    <td style="text-align: left; width: 267px;">
                        <asp:DropDownList ID="ddlActividadE0" runat="server" CssClass="textbox"
                            Width="280px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right;" colspan="3">Tiene Parentesco Con Empleados de la Entidad &nbsp;
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlparentesco" runat="server" CssClass="textbox"
                            Width="140px">
                            <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                            <asp:ListItem Value="1">si</asp:ListItem>
                            <asp:ListItem Value="2">No </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td class="tdI" style="width: 1051px">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 330px; text-align: left">
                                    <strong>Dirección de la Empresa</strong></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <uc1:direccion ID="txtDireccionEmpresa" runat="server" Text="0" />
                                </td>
                            </tr>   
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td class="tdI" style="width: 1051px">
                        <asp:Panel ID="Panel13" runat="server" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 101px">
                                        <asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True"
                                            RepeatDirection="Horizontal" Visible="False">
                                            <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 163px">
                                        <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox"
                                            MaxLength="128" Visible="False" />
                                    </td>
                                    <td style="width: 155px">
                                        <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox"
                                            MaxLength="128" Visible="False" />
                                    </td>
                                    <td style="width: 155px">
                                        <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox"
                                            MaxLength="128" Visible="False" />
                                        <asp:CalendarExtender ID="txtFecha_residencia_CalendarExtender" runat="server"
                                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtFecha_residencia" TodaysDateFormat="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="width: 129px">
                                        <asp:DropDownList ID="ddlEstado" runat="server" Visible="False" CssClass="textbox"
                                            Width="121px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCod_oficina" runat="server" CssClass="textbox"
                                            MaxLength="128" Visible="False">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox"
                                            MaxLength="128" Visible="False" />
                                        <uc1:fecha ID="txtFechaIngreso" runat="server" Visible="false"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="tdD">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>    
    <%--------------------------------------------------------------------------------------------------------------------------%>    <%------------------------------------------------Campos que no son visibles------------------------------------------------%>
    <asp:Panel ID="Panel9" runat="server" Width="980px" Visible="False">
        <table style="width: 100%;">
            <tr>
                <td style="width: 10px">&nbsp;<asp:Label ID="lblAsesor" runat="server" Text="A001" Visible="False"></asp:Label>&nbsp;
                </td>
                <td class="logo" style="width: 299px">
                    <asp:DropDownList ID="ddlLugarResidenciaE" runat="server" CssClass="textbox" Width="285px"
                        Style="text-align: left" Visible="False">
                    </asp:DropDownList>
                </td>
                <td style="width: 337px; text-align: left">
                    <asp:DropDownList ID="ddlBarrioResidencia" runat="server"
                        Width="326px" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc1:mensajegrabar ID="ctlMensajeEstado" runat="server" />
    <script type="text/javascript">
        (function () {
            $("#separador").css({ "position": "relative", "top": "149px", "border": "solid 2px #929292", "border-bottom": "none", "border-left": "none", "border-right": "none", "left": "-19px" });
        });
    </script>
</asp:Content>
