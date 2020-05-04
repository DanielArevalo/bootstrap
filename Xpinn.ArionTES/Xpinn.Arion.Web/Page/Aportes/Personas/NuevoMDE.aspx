<%@ Page Title=".: Expinn - Personas :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="NuevoMDE.aspx.cs" Inherits="NuevoMDE" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagName="Persona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc4" %>

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
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                var hoy = new Date();
                alert("Eliga una fecha inferior a la Actual! " + hoy.toDateString());
                sender._selectedDate = new Date();
                //pasando fecha seleccionada
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                window.__doPostBack('__Page', '');
            }
        }
    </script>

    <table border="0" cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td colspan="9" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                <strong>Datos Básicos</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" width="35%">Identificación*
                <br />
                <asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox" Width="125px" TabIndex="1">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;" width="35%">
                <table><tr>
                    <td>
                        No.Identificación
                        <br />
                        <asp:TextBox ID="txtIdentificacionE" runat="server" CssClass="textbox" MaxLength="20"
                            Width="120px" OnTextChanged="txtIdentificacionE_TextChanged" AutoPostBack="true"
                            TabIndex="2" onkeypress="return ValidNum(event);" />
                    </td>
                    <td>
                        <br />
                        &nbsp;&nbsp;<asp:CheckBox ID="checkOficinaVirtual" runat="server" Checked="true" Text="Acceso Oficina Virtual" />
                    </td>
                </tr></table>
            </td>
            <td rowspan="5" style="text-align: center;" width="30%">
                <asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                    Height="20px" ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" />
                <asp:HiddenField ID="hdFileName" runat="server" />
                <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                <br />
                <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                <br />
                <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
                    Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" />
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
                    MaxLength="80" Visible="False" Width="120px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">Tipo Persona<br />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipo_persona" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                            Width="155px" Enabled="False">
                            <asp:ListItem Selected="True">Natural</asp:ListItem>
                            <asp:ListItem>Jurídica</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="text-align: left;">Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="180px" TabIndex="3">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">F.Expedición<br />
                <uc1:fecha ID="txtFechaexpedicion" runat="server" Enabled="True" TabIndex="4" />
            </td>
            <td style="text-align: left;">Ciud.Expedición<br />
                <asp:DropDownList ID="ddlLugarExpedicion" runat="server" Width="180px" CssClass="textbox"
                    TabIndex="5">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" width="35%">Primer Nombre<br />
                <asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox" MaxLength="100"
                    Style="text-transform: uppercase" Width="90%" TabIndex="6" />
                <asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtPrimer_nombreE"
                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
            </td>
            <td style="text-align: left;" width="35%">Segundo Nombre<br />
                <asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" MaxLength="100"
                    Style="text-transform: uppercase" Width="90%" TabIndex="7" />
                <asp:FilteredTextBoxExtender ID="fte51" runat="server" Enabled="True" TargetControlID="txtSegundo_nombreE"
                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" width="35%">Primer Apellido<br />
                <asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox" MaxLength="100"
                    Width="90%" Style="text-transform: uppercase" TabIndex="8" />
                <asp:FilteredTextBoxExtender ID="fte52" runat="server" Enabled="True" TargetControlID="txtPrimer_apellidoE"
                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
            </td>
            <td style="text-align: left;" width="35%">Segundo Apellido
                <br />
                <asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" MaxLength="100"
                    Width="90%" Style="text-transform: uppercase" TabIndex="9" />
                <asp:FilteredTextBoxExtender ID="fte53" runat="server" Enabled="True" TargetControlID="txtSegundo_apellidoE"
                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
            </td>
        </tr>
    </table>

    <br />
    <table cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                <strong>Datos de Localización</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 100%" colspan="3">
                <hr style="width: 100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 60%;" colspan="2">
                <strong>Dirección Residencia</strong>
            </td>
            <td rowspan="10" style="width: 40%; text-align: left;"></td>
        </tr>
        <tr>
            <td style="text-align: left;" colspan="2">
                <label id="msgTd" style="color: #FF3333; box-shadow: 0 0 1px 0px #FF3333;">Por favor seleccione un tipo de residencia</label>
                <uc4:Direccion ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox required" TabIndex="11" required="required"></uc4:Direccion>
                <%--<asp:TextBox ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox" TabIndex="10"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 60%" colspan="2">
                <hr style="width: 100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 60%" colspan="2">
                <strong>Información de Correspondencia</strong>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left; width: 60%">
                <asp:TextBox ID="txtDirCorrespondencia" runat="server" Width="90%" CssClass="textbox"
                    TabIndex="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">Ciudad
                <asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" CssClass="textbox" Width="180px"
                    TabIndex="12" ToolTip="Ciudad de Correspondencia">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;">Barrio
                <asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" CssClass="textbox"
                    Width="180px" TabIndex="13">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">Teléfono
                <asp:TextBox ID="txtTelCorrespondencia" runat="server" CssClass="textbox" MaxLength="20"
                    Width="100px" TabIndex="14" />
                <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtTelCorrespondencia" ValidChars="-()" />
            </td>
            <td style="text-align: left;">Celular
                <asp:TextBox ID="txtcelular" runat="server" CssClass="textbox" MaxLength="20"
                    Width="100px" TabIndex="15" />
                <asp:FilteredTextBoxExtender ID="ftb13" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtcelular" ValidChars="-()" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">Ubicación/Zona *
                <asp:DropDownList ID="ddlZona" runat="server" Width="170px" CssClass="textbox required"
                    AppendDataBoundItems="True" TabIndex="11" required="required">
                    <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="3" style="width: 100%">
                <hr style="width: 100%; text-align: left; margin-left: 0px;" />
            </td>
        </tr>
    </table>

    <asp:UpdatePanel ID="upDatosGen" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="3" style="width: 100%">
                <tr>
                    <td colspan="6" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos Generales</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Fec.Nacimiento
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtFechanacimiento" runat="server" AutoPostBack="True" CssClass="textbox"
                            MaxLength="10" TabIndex="18" OnTextChanged="txtFechanacimiento_TextChanged"
                            Width="148px" />
                        <asp:MaskedEditExtender ID="MEEfecha" runat="server" TargetControlID="txtFechanacimiento"
                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" />
                        <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            OnClientDateSelectionChanged="checkDate" TargetControlID="txtFechanacimiento"></asp:CalendarExtender>
                    </td>
                    <td style="text-align: left;">Edad del Cliente
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEdadCliente" runat="server" CssClass="textbox" Enabled="False"
                            Width="40px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">País Nacimiento
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlPais" runat="server" TabIndex="19" CssClass="textbox"
                            Width="170px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Sexo<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:RadioButtonList ID="rblSexo" runat="server" CellPadding="0" CellSpacing="0"
                            Height="22px" TabIndex="20" RepeatDirection="Horizontal" Style="font-size: xx-small; text-align: left;"
                            Width="139px">
                            <asp:ListItem Selected="True">F</asp:ListItem>
                            <asp:ListItem>M</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left;">Nivel Educativo<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" TabIndex="21" CssClass="textbox"
                            Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Ciudad Nacimiento
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlLugarNacimiento" runat="server" TabIndex="19" CssClass="textbox"
                            Width="170px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">E-Mail<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="180px" />
                    </td>
                    <td style="text-align: left;">Estrato<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox" Width="40px" />
                    </td>
                    <td style="text-align: left;">&nbsp;
                    </td>
                    <td style="text-align: left;">&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <table cellpadding="0" cellspacing="3" style="width: 100%">
        <tr>
            <td style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                <strong>Información Adicional</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <div style="overflow: scroll; max-height: 280px">
                    <asp:GridView ID="gvInfoAdicional" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="0" DataKeyNames="" ForeColor="Black" GridLines="Both" OnRowDataBound="gvInfoAdicional_RowDataBound"
                        PageSize="10" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="False"
                        Style="font-size: xx-small" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblidinfadicional" runat="server" Text='<%# Bind("idinfadicional") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblcod_infadicional" runat="server" Text='<%# Bind("cod_infadicional") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblopcionaActivar" runat="server" Text='<%# Bind("tipo") %>' Visible="false"></asp:Label><asp:TextBox
                                        ID="txtCadena" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("valor") %>' Visible="false" Width="240px"></asp:TextBox><asp:TextBox
                                            ID="txtNumero" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor") %>' Visible="false" Width="150px"> </asp:TextBox><asp:FilteredTextBoxExtender
                                                ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNumero"
                                                ValidChars="" />
                                    <uc1:fecha ID="txtctlfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                        style="font-size: xx-small; text-align: left" Text='<%# Eval("valor", "{0:" + FormatoFecha() + "}") %>'
                                        TipoLetra="xx-Small" Visible="false" />
                                    <asp:Label ID="lblValorDropdown" runat="server" Text='<%# Bind("valor") %>' Visible="false"></asp:Label><asp:Label
                                        ID="lblDropdown" runat="server" Text='<%# Bind("items_lista") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
                                            ID="ddlDropdown" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Visible="false"
                                            Width="160px">
                                        </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <br />
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnImp_Click"
                        Text=" Importar " />
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <hr />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="upAfiliacion" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="3" style="width: 100%">
                <tr>
                    <td style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Afiliación</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 185px">
                        <strong>Tipo Cliente</strong> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkAsociado" runat="server" Text=" Asociado " AutoPostBack="true" Checked="true"
                            OnCheckedChanged="chkAsociado_CheckedChanged" /><br />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="panelAfiliacion" runat="server">
                <table cellpadding="0" cellspacing="3" style="width: 100%">
                    <tr>
                        <td style="text-align: left; width: 155px">Fecha de Afiliación<br />
                            <asp:TextBox ID="txtcodAfiliacion" runat="server" CssClass="textbox" Style="text-align: right"
                                Visible="false" Width="100px" />
                            <uc1:fecha ID="txtFechaAfili" runat="server" Enabled="True" style="width: 140px" />
                        </td>
                        <td style="text-align: left; width: 170px">Estado<br />
                            <asp:DropDownList ID="ddlEstadoAfi" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                CssClass="textbox" OnSelectedIndexChanged="ddlEstadoAfi_SelectedIndexChanged"
                                Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 140px">Fecha de Rétiro<br />
                            <asp:Panel ID="panelFecha" runat="server">
                                <uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True" style="width: 140px" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: left; width: 170px">Forma de Pago<br />
                            <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="95%">
                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" AutoPostBack="True"
                                Width="180px" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Valor<br />
                            <uc1:decimales ID="txtValorAfili" runat="server" style="text-align: right;" />
                        </td>
                        <td style="text-align: left;">Fecha de 1er Pago<br />
                            <uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
                        </td>
                        <td style="text-align: left;">Nro Cuotas<br />
                            <asp:TextBox ID="txtCuotasAfili" runat="server" CssClass="textbox" Style="text-align: right"
                                Width="100px" />
                            <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
                                ValidChars="" />
                        </td>
                        <td style="text-align: left;">Periodicidad<br />
                            <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">Asesor Comercial:
                                                    <br />
                            <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkAsistioUltAsamblea" runat="server" Text="Asistio a la Ultima Asamblea"
                                TextAlign="Left" />
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlEstadoAfi" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <table cellpadding="1" cellspacing="4" style="width: 70%">
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblCodTutor" runat="server" Visible="false" />
                <strong>Datos del Tutor</strong><br />
                <ctl:Persona ID="ctlPersona" runat="server" Width="400px" />  <br />    
            </td>
            <td style="text-align: left;">Parentesco:                                          
                <asp:DropDownList ID="ddlParentesco" runat="server" Width="150px" CssClass="textbox" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccione un Item"></asp:ListItem>
                </asp:DropDownList></p>
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelFinal" runat="server" Visible="false" Height="600px">
        <div style="text-align: left">
            <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Cerrar Informe"
                OnClick="btnVerData_Click" Width="280px" Height="30px" />
        </div>
        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
            height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
    </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>

</asp:Content>

