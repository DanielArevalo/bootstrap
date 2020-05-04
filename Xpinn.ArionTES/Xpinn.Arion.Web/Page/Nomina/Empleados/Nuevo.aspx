<%@ Page Title=".: Empleados :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagPrefix="uc1" TagName="ctlFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <asp:MultiView ID="mvFinal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <asp:Panel ID="panelDatos" runat="server">
                <table border="0">
                    <tr>
                        <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                            <strong>Datos Básicos</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="3">
                            <asp:Panel ID="Panel7" runat="server" Width="100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 120px; text-align: left;">Identificación*&nbsp;
                                        <span style="font-size: xx-small">
                                            <asp:RequiredFieldValidator
                                                ID="rfvIdentificacion" runat="server"
                                                ControlToValidate="txtIdentificacionE" Display="Dynamic"
                                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                Style="font-size: x-small" />
                                        </span>
                                        </td>
                                        <td style="width: 155px;">
                                            <asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox" Width="120px">
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                        <td class="gridIco" style="width: 17px; text-align: left;">No.<br />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtIdentificacionE" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                                                MaxLength="20" Width="110px" AutoPostBack="True" OnTextChanged="txtIdentificacionE_TextChanged" />
                                            <br />
                                        </td>
                                        <td style="text-align: right;">Tipo Persona
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="rblTipo_persona" runat="server" AutoPostBack="True"
                                                        RepeatDirection="Horizontal" Width="155px" Enabled="False"
                                                        Style="font-size: x-small">
                                                        <asp:ListItem Selected="True">Natural</asp:ListItem>
                                                        <asp:ListItem>Jurídica</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align: left;">Oficina&nbsp;
                                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" AutoPostBack="true" 
                                            Width="160px" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvOficina" runat="server"
                                                ControlToValidate="ddlOficina" Display="Dynamic"
                                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                  Style="font-size: x-small" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="80" Visible="False" Width="100px" />
                                            <asp:TextBox ID="txtCodEmpleado" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="80" Visible="False" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="pnlInformacionPersona" Visible="false">
                    <table border="0">
                        <tr>
                            <td class="tdI" colspan="3">
                                <asp:Panel ID="PanelDatos1" runat="server" Width="100%">
                                    <hr style="width: 100%" />
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left;">Primer Nombre
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtPrimer_nombreE" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase" Width="284px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left;">Segundo Nombre<br />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase" Width="326px" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">Primer Apellido
                                            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtPrimer_apellidoE" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small;"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase" Width="284px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left;">Segundo Apellido
                                            <br />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase" Width="326px" />
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
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left;">
                                                <hr style="width: 100%" />
                                            </td>
                                            <td rowspan="3">&nbsp; </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%;"><strong>Dirección Residencia</strong> </td>
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
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="PanelDirCor" runat="server" Width="100%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <hr style="height: -15px; width: 100%;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left"><strong>Información de Correspondencia</strong> </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc1:direccion ID="txtDirCorrespondencia" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="PanelDatos2" runat="server" Width="100%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left;">Barrio Correspondencia </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="160px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBarrioCorrespondencia" runat="server" ControlToValidate="ddlBarrioCorrespondencia" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">Teléfono Correspondencia <span style="font-size: xx-small">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTelCorrespondencia" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                            </span></td>
                                            <td style="text-align: left; font-size: x-small;">
                                                <asp:TextBox ID="txtTelCorrespondencia" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="20" Width="100px" />
                                            </td>
                                            <td style="text-align: left;">Ciudad Correspondencia </td>
                                            <td colspan="2" style="text-align: left;">
                                                <asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="164px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCiuCorrespondencia" runat="server" ControlToValidate="ddlCiuCorrespondencia" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">&nbsp; </td>
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
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="Panel2" runat="server" Width="110%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left; width:15%">Celular
                                <asp:RequiredFieldValidator ID="rfvCelular" runat="server" ControlToValidate="txtCelular" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="50" onkeypress="return isNumber(event)" Width="148px" />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Actividad CIIU </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlActividadE" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="158px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvActividadE" runat="server" ControlToValidate="ddlActividadE" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Teléfono Residencia
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtTelefonoE" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTelefonoE" runat="server" CssClass="textbox" MaxLength="128" Width="156px" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTelefonoE">
                                                </asp:FilteredTextBoxExtender>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="Panel3" runat="server" Width="110%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left;">Fecha Nacimiento </td>
                                            <td style="text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechanacimiento" runat="server" AutoPostBack="True" CssClass="textbox" MaxLength="10" OnTextChanged="txtFechanacimiento_TextChanged" Width="148px"> </asp:TextBox>
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtFechanacimiento" />
                                                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtFechanacimiento" Display="Dynamic" EmptyValueBlurredText="Fecha No Valida" EmptyValueMessage="Fecha Requerida" ForeColor="Red" InvalidValueBlurredMessage="Fecha No Valida" InvalidValueMessage="Fecha No Valida" Style="font-size: x-small" TooltipMessage="Seleccione una Fecha" />
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechanacimiento" TodaysDateFormat="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        &nbsp;
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Fecha Expedición Cédula
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtFechaexpedicion" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtFechaexpedicion" runat="server" CssClass="textbox" MaxLength="10" Width="150px" />
                                                <asp:MaskedEditExtender ID="mskFechaexpedicion" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtFechaexpedicion" />
                                                <asp:MaskedEditValidator ID="mevFechaexpedicion" runat="server" ControlExtender="mskFechaexpedicion" ControlToValidate="txtFechaexpedicion" Display="Dynamic" EmptyValueBlurredText="Fecha No Valida" EmptyValueMessage="Fecha Requerida" ForeColor="Red" InvalidValueBlurredMessage="Fecha No Valida" InvalidValueMessage="Fecha No Valida" Style="font-size: x-small" TooltipMessage="Seleccione una Fecha" />
                                                <asp:CalendarExtender ID="txtFechaexpedicion_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaexpedicion" TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                                &nbsp; </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Ciudad Exped. Cédula </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlLugarExpedicion" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="164px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLugarExpedicion" runat="server" ControlToValidate="ddlLugarExpedicion" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small" />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="Panel4" runat="server" Width="110%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left;">Sexo<br style="font-size: x-small" />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:RadioButtonList ID="rblSexo" runat="server" CellPadding="0" CellSpacing="0" Height="22px" RepeatDirection="Horizontal" Style="font-size: small; text-align: left;" Width="139px">
                                                    <asp:ListItem Selected="True">F</asp:ListItem>
                                                    <asp:ListItem>M</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Nivel Educativo<br style="font-size: x-small" />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="158px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvNivelEscolaridad" runat="server" ControlToValidate="ddlNivelEscolaridad" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small" />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Ciudad Nacimiento </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlLugarNacimiento" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="164px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLugarNacimiento" runat="server" ControlToValidate="ddlLugarNacimiento" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small" />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="Panel5" runat="server" Width="110%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left; height: 31px;">Estado Civil<br />
                                            </td>
                                            <td style="text-align: left; height: 31px;">
                                                <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="156px">
                                                    <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server" ControlToValidate="ddlEstadoCivil" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" InitialValue="Seleccione un item" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left; height: 31px;">&nbsp;&nbsp; Personas a Cargo </td>
                                            <td style="text-align: left; height: 31px;">
                                                <asp:TextBox ID="txtPersonasCargo" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" MaxLength="100" Visible="true" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPersonasCargo" runat="server" ControlToValidate="txtPersonasCargo" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                            </td>
                                            <td style="text-align: left; height: 31px;">&nbsp;&nbsp; Edad </td>
                                            <td style="text-align: left; height: 31px;">
                                                <asp:UpdatePanel ID="upEdad" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtEdadCliente" runat="server" CssClass="textbox" Enabled="False" Width="158px"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvEdad" runat="server" ControlToValidate="txtEdadCliente" ErrorMessage="Debe estar entre 0 y 90" ForeColor="Red" MaximumValue="90" MinimumValue="0" Style="font-size: xx-small"  > </asp:RangeValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="text-align: left; height: 31px;">&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:Panel ID="Panel6" runat="server" Width="110%">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="text-align: left;">Profesión</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtProfecion" runat="server" CssClass="textbox" MaxLength="100" Style="text-transform: uppercase" Visible="true" Width="148px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Estrato </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" MaxLength="100" Visible="true" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEstraro" runat="server" ControlToValidate="txtEstrato" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; E-Mail</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="120" Width="292px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                                <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ></asp:RegularExpressionValidator>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <asp:UpdatePanel ID="upTipoVivienda" runat="server">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-right: 7px;">
                                            <tr>
                                                <td colspan="4" style="text-align: left;">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">Tipo Vivienda </td>
                                                <td colspan="3" style="text-align: left;">
                                                    <asp:RadioButtonList ID="rblTipoVivienda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged" RepeatDirection="Horizontal" Width="370px">
                                                        <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                                                        <asp:ListItem Value="A">Arrendada</asp:ListItem>
                                                        <asp:ListItem Value="F">Familiar</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">Nombre Arrendador </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128" Style="text-align: left; text-transform: uppercase" Width="299px" />
                                                </td>
                                                <td style="text-align: left">&nbsp;&nbsp; Teléfono Arrendador </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" MaxLength="128" onkeypress="return isNumber(event)" Style="margin-left: 0px" Width="148px" />
                                                    <asp:FilteredTextBoxExtender ID="txtTelefonoarrendador_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTelefonoarrendador">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">Antigüedad en la Vivienda (Meses)<br />
                                                    <span style="font-size: xx-small">&nbsp;</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtAntiguedadlugar" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" MaxLength="8" Style="text-align: left" Width="100px" />
                                                    <asp:FilteredTextBoxExtender ID="txtAntiguedadlugar_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtAntiguedadlugar">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                                <td style="text-align: left;">&nbsp;&nbsp; Valor Arriendo </td>
                                                <td style="text-align: left;">
                                                    <uc2:decimales ID="txtValorArriendo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: left;">
                                                    <hr />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="3" style="width: 100%">
                                <%--<asp:Panel ID="panelNegocio" runat="server" Visible="true">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" style="text-align: left;"><strong>Información Laboral</strong> </td>
                                            <td style="text-align: left">&nbsp; </td>
                                            <td style="text-align: left">&nbsp; </td>
                                            <td style="text-align: left; width: 185px;">&nbsp; </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">Empresa </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="120" Width="280px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtEmpresa" runat="server" ControlToValidate="txtEmpresa" Display="Dynamic" ErrorMessage="Debe ingresar el nombre de la empresa" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp; Cargo </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox" Width="166px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Teléfono Empresa </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" MaxLength="128" Width="140px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtTelEmpresa" runat="server" ControlToValidate="txtTelefonoempresa" Display="Dynamic" ErrorMessage="Debe ingresar el telefóno de la empresa" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">Fecha Contratacion</td>
                                            <td style="text-align: left;">&nbsp;<asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtFechaContratacion1" runat="server" CssClass="textbox" MaxLength="10" Style="margin-top: 1px"   Width="148px"> </asp:TextBox>
                                                    <asp:MaskedEditExtender ID="txtFechaContratacion1_MaskedEditExtender" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtFechaContratacion1" />
                                                    <asp:CalendarExtender ID="txtFechaContratacion1_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaContratacion1" TodaysDateFormat="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                    &nbsp;
                                                    <asp:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtFechaContratacion1" Display="Dynamic" EmptyValueBlurredText="Fecha No Valida" EmptyValueMessage="Fecha Requerida" ForeColor="Red" InvalidValueBlurredMessage="Fecha No Valida" InvalidValueMessage="Fecha No Valida" Style="font-size: x-small" TooltipMessage="Seleccione una Fecha"   />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Antigüedad&nbsp;(Meses) </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtAntiguedadlugarEmpresa" runat="server" CssClass="textbox" MaxLength="128" Width="158px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtAntiguedadEmpresa" runat="server" ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small"   />
                                                <asp:CompareValidator ID="cvtxtAntiguedadlugarEmpresa" runat="server" ControlToValidate="txtAntiguedadlugarEmpresa" Display="Dynamic" ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: x-small" Type="Integer"   />
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Tipo Sueldo </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTipoSueldo" runat="server" CssClass="textbox" Width="140px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">Tipo Contrato </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox" Width="288px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Ciudad </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlCiu0" runat="server" CssClass="textbox" Width="166px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">&nbsp; Nomina Empleado </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlNomina" runat="server" AppendDataBoundItems="True" CssClass="textbox" TabIndex="19" Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">Actividad Económica </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlActividadE0" runat="server" CssClass="textbox" Width="288px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">&nbsp;&nbsp;Tiene Parentesco Con Empleados de la Entidad &nbsp; </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlparentesco" runat="server" CssClass="textbox" Width="166px">
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                    <asp:ListItem Value="2">No </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">&nbsp; Sueldo </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtSueldo" runat="server" CssClass="textbox" MaxLength="12" onkeypress="return isNumber(event)" Width="148px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Jornada Laboral </td>
                                            <td style="text-align: left">
                                                <asp:RadioButtonList ID="rblJornadaLaboral" runat="server" RepeatDirection="Horizontal" TabIndex="45">
                                                    <asp:ListItem Selected="True" Value="1">Tiempo Total</asp:ListItem>
                                                    <asp:ListItem Value="2">Tiempo Parcial</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="text-align: left;">Celular Empresa
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTelCell0" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small"   />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTelCell0" runat="server" CssClass="textbox" MaxLength="50" onkeypress="return isNumber(event)" Width="148px" />
                                            </td>
                                            <td style="text-align: left;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left"><strong>Dirección de la Empresa</strong></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <uc1:direccion ID="txtDireccionEmpresa" runat="server" Text="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                            </td>
                        </tr>
                    </table>

                    <asp:Accordion ID="acoPersona" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContenido"
                        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                        <Panes>
                            <asp:AccordionPane ID="acoInfAcademica" runat="server" Visible="True">
                                <Header>
                                <asp:Image ID="imgExpandGeneral" runat="server" DescriptionUrl="../../../Images/expand.png" />Información Academica</Header>
                                <Content>
                                    <asp:UpdatePanel ID="upInformacionAcademica" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <div>
                                                    <asp:GridView
                                                        ID="gvInformacionAcademica"
                                                        runat="server"
                                                        Width="1023px"
                                                        TabIndex="50"
                                                        AutoGenerateColumns="False"
                                                        BackColor="White"
                                                        BorderColor="#DEDFDE"
                                                        BorderWidth="1px"
                                                        CellPadding="4"
                                                        DataKeyNames="consecutivo"
                                                        ForeColor="Black"
                                                        ShowFooter="True"
                                                        Style="font-size: xx-small"
                                                        OnRowCommand="gvInformacionAcademica_RowCommand"
                                                        OnRowDataBound="gvInformacionAcademica_RowDataBound"
                                                        OnRowDeleting="gvInformacionAcademica_RowDeleting"
                                                        ShowHeaderWhenEmpty="True"
                                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Delete"
                                                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Detalle" Width="16px" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="btnadd" runat="server" CommandName="AddNew" Text="Agregar" CssClass="btn8" />
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Semestre" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="semenestre" runat="server" Text='<%# Bind("semestre") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtsemenestre" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSemestre" runat="server" Text='<%# Bind("semestre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Profesion" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="profesion" runat="server" Text='<%# Bind("profesion") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtprofesion" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProfesion" runat="server" Text='<%# Bind("profesion") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario De Estudio" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtHorarioEstudio" runat="server" Text='<%# Bind("horarioestudio") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlhorarioestudio" runat="server" Width="140px" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Diurno </asp:ListItem>
                                                                        <asp:ListItem Value="2"> Nocturno -</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlItemhorarioestudio" Enabled="false" runat="server" Width="140px" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Diurno </asp:ListItem>
                                                                        <asp:ListItem Value="2"> Nocturno -</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha De Inicio" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtFechaInicio" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_inicio")) %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <uc1:ctlFecha runat="server" ID="ctlFechainicio" />
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaInicio" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_inicio")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Titulo Obtenido" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="titulo" runat="server" Text='<%# Bind("titulo") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txttitulo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTiTuloObtenido" runat="server" Text='<%# Bind("titulo_obtenido") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Establecimiento" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="establecimiento" runat="server" Text='<%# Bind("establecimiento") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtestablecimiento" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstablecimiento" runat="server" Text='<%# Bind("establecimiento") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha De Terminacion" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtFechaTermino" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_terminacion")) %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <uc1:ctlFecha runat="server" ID="ctlFechatermino" />
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaTerminacion" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_terminacion")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario de titulo" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlhorarititulo" runat="server" Width="140px" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Diurno </asp:ListItem>
                                                                        <asp:ListItem Value="2"> Nocturno -</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlItemhorarititulo" Enabled="false" runat="server" Width="140px" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Diurno </asp:ListItem>
                                                                        <asp:ListItem Value="2"> Nocturno -</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estudia?" SortExpression="descripcion">
                                                                <FooterTemplate>
                                                                    <asp:CheckBox ID="chkFooterEstudia" runat="server" />
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkItemEstudia" runat="server" Checked='<%# Convert.ToBoolean(Eval("estudia")) %>' Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </asp:AccordionPane>
                            <asp:AccordionPane ID="apExperienciaLaboral" runat="server" Visible="True">
                                <Header>
                               <asp:Image ID="Image1" runat="server" DescriptionUrl="../../../Images/expand.png" />Información De Experiencia Laboral</Header>
                                <Content>
                                    <asp:UpdatePanel ID="upExperienciaLaboral" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <asp:Panel runat="server">
                                                    <div>
                                                        <asp:GridView
                                                            ID="gvExperienciaLaboral"
                                                            runat="server"
                                                            Width="1023px"
                                                            TabIndex="50"
                                                            AutoGenerateColumns="False"
                                                            BackColor="White"
                                                            BorderColor="#DEDFDE"
                                                            BorderWidth="1px"
                                                            CellPadding="4"
                                                            DataKeyNames="consecutivo"
                                                            ForeColor="Black"
                                                            ShowFooter="True"
                                                            Style="font-size: xx-small"
                                                            OnRowCommand="gvExperienciaLaboral_RowCommand"
                                                            OnRowDataBound="gvExperienciaLaboral_RowDataBound"
                                                            OnRowDeleting="gvExperienciaLaboral_RowDeleting"
                                                            ShowHeaderWhenEmpty="True"
                                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Delete"
                                                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Detalle" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="btnadd" runat="server" CommandName="AddNewe" Text="Agregar" CssClass="btn8" />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre De La Empresa" SortExpression="descripcion">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="nombre" runat="server" Text='<%# Bind("nombre_empresa") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombreEmpresa" runat="server" Text='<%# Bind("nombre_empresa") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cargo" SortExpression="descripcion">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlEditCargo" CssClass="dropdown" Width="148px" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlFooterCargo" CssClass="dropdown" Width="148px" />
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlItemCargo" CssClass="dropdown" Width="148px" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha De Inicio" SortExpression="descripcion">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtFechaIngreso" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_ingreso")) %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <uc1:ctlFecha runat="server" ID="ctlFechaingreso" CssClass="textbox" />
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_ingreso")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha De Retiro" SortExpression="descripcion">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtFechaRetiro" runat="server" Text='<%#string.Format("{0:d}", Eval("fecha_retiro")) %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <uc1:ctlFecha runat="server" ID="ctlFecharetiro" />
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaRetiro" runat="server" Text='<%# string.Format("{0:d}", Eval("fecha_retiro")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Motivo De Retiro" SortExpression="descripcion">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="motivo" runat="server" Text='<%# Bind("motivo_retiro") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtmotivo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMotivoRetiro" runat="server" Text='<%# Bind("motivo_retiro") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </asp:AccordionPane>
                            <asp:AccordionPane ID="apInformacionFamiliar" runat="server" Visible="True">
                                <Header>
                                <asp:Image ID="Image2" runat="server" DescriptionUrl="../../../Images/expand.png" />Información Familiar</Header>
                                <Content>
                                    <asp:UpdatePanel ID="upInformacionFamiliar" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <div>
                                                    <asp:GridView
                                                        ID="gvfam"
                                                        runat="server"
                                                        Width="1023px"
                                                        TabIndex="50"
                                                        AutoGenerateColumns="False"
                                                        BackColor="White"
                                                        BorderColor="#DEDFDE"
                                                        BorderWidth="1px"
                                                        CellPadding="4"
                                                        DataKeyNames="consecutivo"
                                                        ForeColor="Black"
                                                        ShowFooter="True"
                                                        Style="font-size: xx-small"
                                                        OnRowCommand="gvfam_RowCommand"
                                                        ShowHeaderWhenEmpty="True"
                                                        OnRowDataBound="gvfam_RowDataBound"
                                                        OnRowDeleting="gvfam_RowDeleting"
                                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Delete"
                                                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Detalle" Width="16px" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="btnadd" runat="server" CommandName="AddNewf" Text="Agregar" CssClass="btn8" />
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre Del Familiar" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="nombre" runat="server" Text='<%# Bind("nombrefamiliar") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtnombrefamiliar" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelnombre3" runat="server" Text='<%# Bind("nombrefamiliar") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Parentesco" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlEditparenteco" runat="server" Width="140px" CssClass="textbox">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlFooterparenteco" runat="server" Width="140px" CssClass="textbox">
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlItemparenteco" runat="server" Width="140px" CssClass="textbox" Enabled="false">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo De Identificacion" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlEdittipoidentificacion" runat="server" Width="140px" CssClass="textbox">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlFootertipoidentificacion" runat="server" Width="140px" CssClass="textbox">
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlItemtipoidentificacion" runat="server" Width="140px" CssClass="textbox">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nª Identificacion" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="identificacionfamiliar" runat="server" Text='<%# Bind("identificacionfamiliar") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtidentificacionfamiliar" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelidentificacion3" runat="server" Text='<%# Bind("identificacionfamiliar") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Profesion Del Familiar" SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="profesion" runat="server" Text='<%# Bind("profesion") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtprofesion" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labelprofesion3" runat="server" Text='<%# Bind("profesion") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Vive Con El " SortExpression="descripcion">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="convive" runat="server" Text='<%# Bind("convivefamiliar") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlconvive" runat="server" Width="140px" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Si </asp:ListItem>
                                                                        <asp:ListItem Value="2"> No </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlItemconvive" runat="server" Width="140px" Enabled="false" CssClass="textbox">
                                                                        <asp:ListItem Value="1"> Si </asp:ListItem>
                                                                        <asp:ListItem Value="2"> No </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                </asp:Panel>
            </asp:Panel>
        </asp:View>

        <asp:View runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>
</asp:Content>
