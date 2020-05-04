<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<%@ Register src="~/General/Controles/ctlGiro.ascx" tagname="giro" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="panelGeneral" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table style="width: 650px; text-align: center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="text-align: left; width: 200px">Num. Auxilio<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">Fec. Solicitud<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">Fec. Aprobación<br />
                                    <ucFecha:fecha ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">Fec. Desembolso<br />
                                    <ucFecha:fecha ID="txtFechaDesembolso" runat="server" CssClass="textbox" />
                                    <span style="color: #FF3300">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 200px">Asociado<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox"
                                        Width="50px" Visible="false" />
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true" Enabled="false"
                                        Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                                    <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px" Enabled="false"
                                        OnClick="btnConsultaPersonas_Click" Text="..." />
                                </td>
                                <td style="text-align: left;" colspan="2">Nombre<br />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Enabled="false"
                                        Width="280px" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                                <td style="text-align: left;">Oficina<br />
                                    <asp:TextBox ID="txtoficina" runat="server" CssClass="textbox" Width="100px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 200px">Linea de Auxilio<br />
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="95%" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                        AutoPostBack="True" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">Cupos<br />
                                    <asp:TextBox ID="txtCupos" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                                </td>
                                <td style="text-align: left;" colspan="2">Monto Disponible<br />
                                    <uc1:decimales ID="txtMontoDisp" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 200px">Valor Solicitado<br />
                                    <uc1:decimales ID="txtValorSoli" runat="server" Enabled="false" />
                                </td>
                                <td style="text-align: left;" colspan="3">Detalle<br />
                                    <asp:TextBox ID="txtDetalle" runat="server" CssClass="textbox" Width="80%" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 180px">Valor Aprobado<br />
                                    <uc1:decimales ID="txtValorAproba" runat="server" Enabled="false" />
                                </td>
                                <td style="text-align: left;" colspan="3">Observaciones<br />
                                    <asp:TextBox ID="txtObservacionAproba" runat="server" CssClass="textbox" Width="80%" Enabled="false"
                                        TextMode="MultiLine" />
                                </td>
                            </tr>
                        </table>
                        <hr style="width: 100%" />
                        <asp:Panel ID="panelProveedor" runat="server">
                            <table border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td style="text-align: left;" colspan="6">
                                        <span style="font-weight: bold">
                                            <asp:Label ID="lblTitOrden" runat="server" Text="PROVEEDOR PARA LA ORDEN DE SERVICIO:" /></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="3">
                                        <asp:Label ID="lblTitIdentific" runat="server" Text="Identificación Proveedor" /><br />
                                        <asp:TextBox ID="txtIdentificacionprov" runat="server" CssClass="textbox" Width="130px"
                                            MaxLength="20" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:Label ID="lblTitNombre" runat="server" Text="Nombre Proveedor" /><br />
                                        <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox" Width="450px"
                                            MaxLength="200" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblPreImpreso" runat="server" Text="Nro Orden" /><br />
                                        <asp:TextBox ID="txtPreImpreso" runat="server" CssClass="textbox" Width="120px" MaxLength="100" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                            <hr style="width: 100%" />
                        </asp:Panel>

                        <table width="100%">
                            <tr>
                                <td style="text-align: left">
                                    <asp:UpdatePanel ID="updDistribucion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <strong>
                                                <asp:CheckBox ID="chkDistribuir" Visible="false" runat="server" Text="Distribución de Giros" AutoPostBack="true"
                                                    OnCheckedChanged="chkDistribuir_CheckedChanged" />
                                            </strong>
                                            <asp:Panel ID="panelDistribucion" runat="server">
                                                <div style="text-align: left">
                                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="25px" /><br />
                                                </div>
                                                <asp:GridView ID="gvDistribucion" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                    HeaderStyle-Height="30px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                    ShowFooter="True" DataKeyNames="idgiro" ForeColor="Black" GridLines="Horizontal"
                                                    Style="font-size: x-small" OnRowDeleting="gvDistribucion_RowDeleting" OnRowDataBound="gvDistribucion_RowDataBound">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Código" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idgiro") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCod_persona" runat="server" Text='<%# Bind("cod_persona") %>' Visible="false" />
                                                                <cc1:TextBoxGrid ID="txtIdentificacionD" runat="server" Width="120px" CssClass="textbox"
                                                                    CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" Text='<%# Bind("identificacion") %>'
                                                                    OnTextChanged="txtIdentificacionD_TextChanged" />
                                                                <asp:FilteredTextBoxExtender ID="ftb120" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                    TargetControlID="txtIdentificacionD" ValidChars="-" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <cc1:TextBoxGrid ID="txtNombre" runat="server" Width="300px" CssClass="textbox" Text='<%# Bind("nombre") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipo" runat="server" Visible="false" Text='<%# Bind("tipo") %>' />
                                                                <cc1:DropDownListGrid ID="ddlTipo" runat="server" CssClass="textbox" Width="130px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Total :
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <uc1:decimalesGridRow ID="txtValor" runat="server" Text='<%# Eval("valor") %>' style="text-align: right"
                                                                    Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="100" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalVr" runat="server" />
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="gridHeader" />
                                                    <HeaderStyle CssClass="gridHeader" />
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>
                                                <asp:Label ID="lblErrorDist" runat="server" Style="font-size: x-small; color: Red" />
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkDistribuir" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAdicionarFila" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="gvDistribucion" EventName="RowDeleting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>

                        <uc3:giro ID="ctlGiro" runat="server" />

                        <table style="width: 740px">
                            <tr>
                                <td style="width: 740px; text-align: center">
                                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                        <td style="text-align: center; font-size: large; color: Red">Solicitud de Auxilio Aprobada&nbsp;correctamente.<br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
