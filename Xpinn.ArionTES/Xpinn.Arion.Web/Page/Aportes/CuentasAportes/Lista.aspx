<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
    <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 92%">
                    <tr>
                        <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                            <strong>Criterios de Búsqueda:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px; text-align: left;">
                            Linea Aporte
                            <br />
                            <asp:DropDownList ID="DdlLineaAporte" runat="server" Width="154px" AutoPostBack="True"
                                CssClass="textbox" OnSelectedIndexChanged="DdlLineaAporte_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 15px; text-align: left;">
                            Número Aporte
                            <br />
                            <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                        </td>
                        <td style="height: 15px; text-align: left;">
                            Identificación<br />
                            <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="157px"></asp:TextBox>
                        </td>
                        <td style="height: 15px; text-align: left;">
                            Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="157px" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="height: 15px; text-align: left;">
                            Código de nómina<br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="157px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Fecha Apertura&nbsp;&nbsp;<br />
                            <asp:TextBox ID="txtFecha_apertura" runat="server" CssClass="textbox" MaxLength="128" />
                            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtFecha_apertura"></asp:CalendarExtender>
                            <br />
                            <asp:CompareValidator ID="cvFecha_apertura" runat="server" ControlToValidate="txtFecha_apertura"
                                Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small" ToolTip="Formato fecha"
                                ValidationGroup="vgGuardar" Width="200px" />
                        </td>
                        <td style="text-align: left">
                            Fecha Vencimiento&nbsp;<br />
                            <asp:TextBox ID="txtFecha_vencimiento" runat="server" CssClass="textbox" MaxLength="128" />
                            <asp:CalendarExtender ID="txtFecha_vencimiento_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtFecha_vencimiento"></asp:CalendarExtender>
                            <br />
                            <asp:CompareValidator ID="cvFecha_vencimiento" runat="server" ControlToValidate="txtFecha_vencimiento"
                                Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small" ToolTip="Formato fecha"
                                Type="Date" ValidationGroup="vgGuardar" Width="200px" />
                        </td>
                        <td class="tdI" style="text-align: left">
                            Estado<br />
                            <asp:DropDownList ID="DdlEstado" runat="server" Width="154px" CssClass="textbox">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                <asp:ListItem Value="3">Cerrado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" style="text-align: left">
                            Ordenado Por
                            <br />
                            <asp:DropDownList ID="DdlOrdenadorpor" runat="server" Width="154px" AutoPostBack="True"
                                CssClass="textbox" OnSelectedIndexChanged="DdlOrdenadorpor_SelectedIndexChanged">
                                <asp:ListItem Value="Numero_Aporte">NUMEROCUENTA</asp:ListItem>
                                <asp:ListItem Value="NOM_LINEA_APORTE">LINEA</asp:ListItem>
                                <asp:ListItem>IDENTIFICACION</asp:ListItem>
                                <asp:ListItem Value="NOMBRE">NOMBRES</asp:ListItem>
                                <asp:ListItem Value="FECHA_PROXIMO_PAGO">FECHAVENCIMIENTO</asp:ListItem>
                                <asp:ListItem>ESTADO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="90%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_aporte" Style="font-size: x-small">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_aporte" HeaderText="Num. Aporte">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo" HeaderText="Saldo Total" DataFormatString="{0:n2}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" DataFormatString="{0:n2}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="valor_base" HeaderText="Valor Base" DataFormatString="{0:n2}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="estado_Linea" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Principal" HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chkprincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                            Enabled="False" EnableViewState="true" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></EditItemTemplate>
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
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
            <br />
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="3">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha de Carga<br />
                        <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                    </td>
                    <td style="text-align: left">
                        Formato de fecha<br />
                        <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                        </asp:DropDownList>
                    </td>
                     <td style="text-align: left">
                        <asp:FileUpload ID="fupArchivoPersona" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; font-size: x-small">
                        <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Cod Linea Aporte, Oficina (Opcional), Cod Persona, Fecha Apertura, Cuota, Código Periodicidad, Forma Pago ( 1 = Caja , 2 = Nómina), Fecha Prox Pago, Porc Distribución (Opcional), Estado, Código Empresa ( Obligatorio si es Nómina).
                    </td>
                </tr>
                 <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:Button ID="btnCargarAportes" runat="server" CssClass="btn8" OnClick="btnCargarAportes_Click"
                            Height="22px" Text="Cargar Aportes" Width="150px" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <table cellpadding="2" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pErrores" runat="server">
                            <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                        <asp:Label ID="lblMostrarDetalles" runat="server" />
                                        <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
                                <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                    <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                                        margin-bottom: 0px;">
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
                            <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelData" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" Width="90%" AutoGenerateColumns="False"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting"
                                    DataKeyNames="cod_linea_aporte" Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Cod Persona">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_periodicidad" HeaderText="Cod Periodicidad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_empresa" HeaderText="Cod Empresa">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec Prox Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="porcentaje_distribucion" HeaderText="Porc Distribución" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
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
                    <td style="text-align: center; font-size: large;">
                        Importación de datos generada correctamente.
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
