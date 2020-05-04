<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tercero :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }
        function MostrarCIIUPrincipal(pDescripcion) {
            document.getElementById('<%= txtActividadCIIU.ClientID %>').value = pDescripcion;
        }

    </script>

    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEscoger" runat="server" EnableTheming="True">
            <asp:Panel ID="PanelTipoComprobante" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">TIPO DE PERSONA
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td>Escoja el tipo de persona a crear
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:RadioButton ID="rbJuridica" runat="server" Text="Juridica"
                                AutoPostBack="True" OnCheckedChanged="rbJuridica_CheckedChanged" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:RadioButton ID="rbNatural" runat="server" Text="Natural"
                                AutoPostBack="True" OnCheckedChanged="rbNatural_CheckedChanged" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:ImageButton ID="imgAceptar" runat="server"
                                ImageUrl="~/Images/btnAceptar.jpg" OnClick="imgAceptar_Click" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwDetalleCliente" runat="server" EnableTheming="True">
            <br />
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="logo" style="text-align: left" colspan="4">&nbsp;
                        </td>
                        <td class="tdD">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" style="text-align: left">Código*
                            <br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="140px" />
                            &nbsp
                        </td>
                        <td style="text-align: left">Nit *<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" AutoPostBack="True"
                                OnTextChanged="txtIdentificacion_TextChanged" onkeypress="return ValidNum(event);" />
                            <asp:Label ID="lblRayita" runat="server" Text="-"></asp:Label>
                            <asp:TextBox ID="txtDigitoVerificacion" runat="server" CssClass="textbox" Enabled="false"
                                Width="30px" />
                            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacion"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                            <asp:FilteredTextBoxExtender ID="ftbeIdentificacion" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txtIdentificacion" ValidChars="-+=">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td class="logo" colspan="2" style="text-align: left">
                            <br />
                            <asp:Button ID="btnCambiarTipoPersona" CssClass="btn8" runat="server" Text="Cambiar A Persona Natural" OnClick="btnCambiarTipoPersona_Click" />
                        </td>
                        <td rowspan="6" style="padding-left: 15px; vertical-align: top; text-align: center">
                            <asp:HiddenField ID="hdFileName" runat="server" />
                            <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                            <asp:FileUpload ID="fuFotoJaLo" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                                ClientIDMode="Static" Height="20px" ToolTip="Seleccionar el archivo que contiene la foto"
                                Width="200px" /><br />
                            <asp:Image ID="imgFotoJaLo" runat="server" Height="170px" Width="200px" ClientIDMode="Static" /><br />
                            <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
                                Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align: left">Razón Social*<br />
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" MaxLength="128"
                                Style="text-transform: uppercase" Width="574px" />
                            <asp:RequiredFieldValidator ID="rfvRazonSocia" runat="server" ControlToValidate="txtRazonSocial"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align: left">Sigla *<br />
                            <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase"
                                Width="574px" />
                            <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSigla"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align: left">Dirección *<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" MaxLength="128"
                                Style="text-transform: uppercase" Width="574px" />
                            <asp:RequiredFieldValidator ID="rfvDirección" runat="server" ControlToValidate="txtDireccion"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" colspan="2" style="text-align: left">Ciudad *<br />
                            <asp:DropDownList ID="ddlCiudad" runat="server" Width="340px" CssClass="dropdown"
                                Height="25px" AppendDataBoundItems="True">
                                <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server"
                                ControlToValidate="ddlCiudad" Display="Dynamic"
                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="vgGuardar" Style="font-size: xx-small"
                                InitialValue="Seleccione un item" />
                        </td>
                        <td style="text-align: left">Telefóno*<br />
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" />
                            <asp:RequiredFieldValidator ID="rfvTelefóno" runat="server" ControlToValidate="txtTelefono"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" colspan="2" style="text-align: left">E-Mail*<br />
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="340px" />
                            <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                Style="font-size: x-small" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left">Fecha Creación/Expedic.*<br />
                            <ucFecha:Fecha ID="txtFecha" runat="server" />
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align: left">Actividad*<br />
                            <asp:DropDownList ID="ddlActividad" runat="server" Width="574px" CssClass="dropdown"
                                Height="25px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvActividad" runat="server"
                                ControlToValidate="ddlActividad" Display="Dynamic"
                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="vgGuardar" Style="font-size: xx-small"
                                InitialValue="Seleccione un item" />
                            <br />
                        </td>
                        <td style="text-align: left">Fecha de Constitución<br />
                            <ucFecha:Fecha ID="txtFecConstitucion" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align: left">Regimen*<br />
                            <asp:DropDownList ID="ddlRegimen" runat="server" Width="574px" CssClass="dropdown"
                                Height="25px">
                                <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="C">COMUN</asp:ListItem>
                                <asp:ListItem Value="S">SIMPLIFICADO</asp:ListItem>
                                <asp:ListItem Value="E">ESPECIAL</asp:ListItem>
                                <asp:ListItem Value="GCA">GRAN CONTRIBUYENTE AUTORETENEDOR</asp:ListItem>
                                <asp:ListItem Value="GCNA">GRAN CONTRIBUYENTE NO AUTORETENEDOR</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRegimen" runat="server"
                                ControlToValidate="ddlRegimen" Display="Dynamic"
                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="vgGuardar" Style="font-size: xx-small"
                                InitialValue="Seleccione un item" />
                            <br />
                        </td>

                        <td>Actividad CIIU
                            <br />
                            <%--<asp:UpdatePanel ID="upRecoger" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <asp:TextBox ID="txtActividadCIIU" CssClass="textbox" runat="server" Width="145px" TabIndex="16" />
                            <asp:PopupControlExtender ID="txtRecoger_PopupControlExtender" runat="server"
                                Enabled="True" ExtenderControlID="" TargetControlID="txtActividadCIIU"
                                PopupControlID="panelLista" OffsetY="22">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="panelLista" runat="server" Height="200px" Width="400px"
                                BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:TextBox ID="txtBuscarCodigo" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="90%" placeholder="Código"></asp:TextBox>
                                        </td>
                                        <td style="width: 45%">
                                            <asp:TextBox ID="txtBuscarDescripcion" CssClass="textbox" runat="server" Width="90%" placeholder="Descripción"></asp:TextBox>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:ImageButton ID="imgBuscar" ImageUrl="~/Images/Lupa.jpg" Height="25px" runat="server" OnClick="imgBuscar_Click" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvActividadesCIIU" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" DataKeyNames="ListaId" OnRowDataBound="gvActividadesCIIU_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("ListaIdStr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("ListaDescripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Sel.">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="false" ToolTip="Seleccionar" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <%--</ContentTemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvActividadesCIIU" EventName="RowDataBound" />
                                </triggers>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
                <br>
                <br>
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
</asp:Content>
