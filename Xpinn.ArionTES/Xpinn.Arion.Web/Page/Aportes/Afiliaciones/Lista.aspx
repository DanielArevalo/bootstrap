<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script src="../../../Scripts/PCLBryan.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <br />
                <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                        <div style="float: left; color: #0066FF; font-size: small">
                            Búsqueda Avanzada
                        </div>
                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                            <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pBusqueda" runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                                <strong>Criterios de Búsqueda:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" width="14%">Tipo de Persona<br />
                                <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="43%" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="N">Natural</asp:ListItem>
                                    <asp:ListItem Value="J">Juridica</asp:ListItem>
                                    <asp:ListItem Value="M">Menor de Edad</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left;" width="14%">Código<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="40%"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" width="14%"">Ciudad<br />
                                <asp:DropDownList ID="ddlCiudad" runat="server" Width="43%" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left;" width="14%">Tipo de Rol<br />
                                <asp:DropDownList ID="ddlTipoRol" runat="server" Width="43%" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Asociado"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Tercero"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left;" width="14%">Mes de Cumpleaños<br />
                                <asp:DropDownList ID="ddlMesCumpleaños" Width="43%" runat="server" Height="25px"
                                    CssClass="textbox" AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                </asp:DropDownList>
                        </tr>
                        <tr>
                            <td style="text-align: left;" width="14%">Identificación<br />
                                <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="40%"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" width="14%">Nombre<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="true" Width="40%"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" width="14%">Apellido<br />
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="true" Width="40%"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" width="14%">Razón Social<br />
                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" Enabled="true"
                                    Width="40%"></asp:TextBox>
                            </td>                       
                            <td style="text-align: left;" width="14%">Dia Cumpleaños<br />
                                <asp:TextBox ID="txtDiaCumpleaños" Width="40%" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" width="14%">Código de nómina<br />
                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
                    ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="False"
                    TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                    CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" ExpandedSize="150" />
            </asp:Panel>
                                <hr width="100%" />
            <table style="width:60%">
                <tr style="text-align:center">
                <td style="text-align: right">
                     Ordenar Por&nbsp;
            <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="dropdown" Width="200px"
                AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="1" Text="Codigo"></asp:ListItem>
                <asp:ListItem Value="2" Text="Tipo Persona"></asp:ListItem>
                <asp:ListItem Value="3" Text="Identificación"></asp:ListItem>
                <asp:ListItem Value="4" Text="Digito Verificación"></asp:ListItem>
                <asp:ListItem Value="5" Text="Tipo Identificación"></asp:ListItem>
                <asp:ListItem Value="6" Text="Fecha Expedición"></asp:ListItem>
                <asp:ListItem Value="7" Text="Ciudad Expedición"></asp:ListItem>
                <asp:ListItem Value="8" Text="Sexo"></asp:ListItem>
                <asp:ListItem Value="9" Text="Primer Nombre"></asp:ListItem>
                <asp:ListItem Value="10" Text="Segundo Nombre"></asp:ListItem>
                <asp:ListItem Value="11" Text="Primer Apellido"></asp:ListItem>
                <asp:ListItem Value="12" Text="Segundo Apellido"></asp:ListItem>
                <asp:ListItem Value="13" Text="Razón Social"></asp:ListItem>
                <asp:ListItem Value="14" Text="Fecha Nacimiento"></asp:ListItem>
            </asp:DropDownList>
                </td>
                    <td>
                        <asp:Button ID="btnCorreo" Visible="false" runat="server" CssClass="btn8"  OnClick="btnCorreo_Click" Text="Enviar correo a asociados" />
                    </td>
                   </tr>
            </table>
            
            <div class="AlphabetPager" style="text-align: left">
                <asp:Repeater ID="rptAlphabets" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtAlphabet" runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>'
                            OnClick="Alphabet_Click" OnClientClick="Alphabet_Click" />
                        <asp:Label ID="lblAlphabet" runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_persona" Style="font-size: x-small">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_persona" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_tipo_persona" HeaderText="Tipo Persona">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_afiliacion" HeaderText="Valor Afiliacion" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="digito_verificacion" HeaderText="D.V.">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="razon_social" HeaderText="Razón Social">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codciudadexpedicion" HeaderText="Ciudad">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="email" HeaderText="E-Mail">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Biometria">
                                    <ItemTemplate>
                                        <asp:Image ID="imgBiometria" runat="server" ImageUrl="~/Images/icoBiometria.jpg"
                                            Visible="false" /><asp:Label ID="lblBiometria" runat="server" Visible="false" Text='<%# Bind("biometria") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label1" runat="server" Visible="False" />
            <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                Visible="False" />
            <div style="font-size: x-small">
                Mostrar&nbsp;<asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdown"
                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Width="74px" AutoPostBack="True" />
                &nbsp;Registros por Página
            </div>
            <br />
            <br />
            <br />
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
            <br />
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="4">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Fecha de Carga<br />
                        <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                    </td>
                    <td style="text-align: left">Formato de fecha<br />
                        <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">Tipo de Persona<br />
                        <asp:UpdatePanel ID="updPanel" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblTipoPersona" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="true"
                                    Width="220px" OnSelectedIndexChanged="rblTipoPersona_SelectedIndexChanged">
                                    <asp:ListItem Value="N" Selected="True">Naturales</asp:ListItem>
                                    <asp:ListItem Value="J">Juridicas</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        <asp:FileUpload ID="fupArchivoPersona" runat="server" />
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="4" style="text-align:left">Tipo de Carga:<br />
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <ContentTemplate>
                         <asp:RadioButtonList ID="rblTipoCarga" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="true"
                                    Width="220px" OnSelectedIndexChanged="rblTipoCarga_SelectedIndexChanged">
                                    <asp:ListItem Value="P" Selected="True">Personas</asp:ListItem>
                                    <asp:ListItem Value="C">Cuentas Bancarias</asp:ListItem>
                                </asp:RadioButtonList>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panelNatural" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; font-size: x-small">
                                                <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Identificación, Tipo
                                                Identificación, Fecha de Expedición, Cod Ciudad Expedición, Sexo(M - F), 1er Nombre,
                                                2do Nombre, 1er Apellido, 2do Apellido, Fecha Nacimiento, Cod Ciudad Nacimiento,
                                                Cod Estado Civil, Cod Escolaridad, Cod Actividad, Dirección, Teléfono, Cod Ciudad
                                                Resid, Tipo Vivienda (P, A, F), Celular, Email, Cod Oficina, Estado, Dirección Correspondencia,
                                                Telef Correspon, Cod Ciudad Correspon, Barrio Correspon, Personas a cargo, Profesión,
                                                Estrato, Activ economica Empresa, Empleado Entidad, Jornada Laboral, Ocupación.
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelJuridica" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; font-size: x-small">
                                                <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Identificación, Digito Verificación, Tipo Identificación,
                                            Fecha de Expedición, Cod Ciudad Expedición, Razon Social, Cod Actividad, Dirección, Teléfono, Email, Cod Oficina,
                                            Estado, Regimen, Tipo Acto Creación, Nro Acto Creación, Celular.
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelCuentasBancarias" Visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; font-size: x-small">
                                                <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Codigo de la persona, tipo de cuenta,número de cuenta, código de banco, sucursal, fecha de apertura, principal.
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTipoPersona" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnCargarPersonas" runat="server" CssClass="btn8" OnClick="btnCargarPersonas_Click"
                            Height="22px" Text="Cargar Personas" Width="200px" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <table cellpadding="2" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pErrores" runat="server">
                            <asp:Panel ID="pEncBusqueda1" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                        <asp:Label ID="lblMostrarDetalles1" runat="server" />
                                        <asp:ImageButton ID="imgExpand1" runat="server" ImageUrl="~/Images/expand.jpg" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
                                <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                    <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                        <Columns>
                                            <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:CollapsiblePanelExtender ID="cpeDemo1" runat="Server" CollapseControlID="pEncBusqueda1"
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda1" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                ImageControlID="imgExpand1" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles1" />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelNatural2" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting" DataKeyNames="identificacion"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo de Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaexpedicion" HeaderText="Fecha Expedición" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codciudadexpedicion" HeaderText="Ciudad Expedición">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sexo" HeaderText="Sexo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechanacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codciudadnacimiento" HeaderText="Ciudad Nacimiento">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codestadocivil" HeaderText="Estado Civil">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codescolaridad" HeaderText="Nivel Educativo">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codactividadStr" HeaderText="Actividad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codciudadresidencia" HeaderText="Ciudad Residencia">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipovivienda" HeaderText="Tipo de Vivienda">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipovivienda" HeaderText="Tipo de Vivienda">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="celular" HeaderText="Celular">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="E-Mail">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dirCorrespondencia" HeaderText="Direc Correspondencia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telCorrespondencia" HeaderText="Telef Correspondencia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ciuCorrespondencia" HeaderText="Ciudad Correspondencia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="barrioCorrespondencia" HeaderText="Barrio Correspondencia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numPersonasaCargo" HeaderText="Personas a Cargo">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="profecion" HeaderText="Profesión">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estrato" HeaderText="Estrato">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActividadEconomicaEmpresaStr" HeaderText="Actividad Económica">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empleado_entidad" HeaderText="Es Empleado de la Entidad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jornada_laboral" HeaderText="Jornada Laboral">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ocupacionApo" HeaderText="Ocupación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelJuridica2" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvJuridica" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" OnRowDeleting="gvJuridica_RowDeleting" DataKeyNames="identificacion"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="digito_verificacion" HeaderText="D.V.">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo de Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaexpedicion" HeaderText="Fecha Expedición" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codciudadexpedicion" HeaderText="Ciudad Expedición">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_social" HeaderText="Razón Social">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codactividadStr" HeaderText="Actividad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="E-Mail">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="regimen" HeaderText="Régimen">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_acto_creacion" HeaderText="Tipo Acto Creación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_acto_creacion" HeaderText="Num Acto Creación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="celular" HeaderText="Celular">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelCuentasBancarias2" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 150%">
                                <asp:GridView ID="gvCuentasBancarias" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" OnRowDeleting="gvCuentasBancarias_RowDeleting" DataKeyNames="cod_persona"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Codigo de la persona">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_cuenta" HeaderText="Tipo de cuenta">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_cuenta" HeaderText="Número de cuenta" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_banco" HeaderText="Código de banco">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sucursal" HeaderText="Sucursal">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha de apertura"  DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="principal" HeaderText="Principal">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">Importación de datos generada correctamente
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
