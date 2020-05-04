<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Empleados :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarEmpleados.ascx" TagName="ctlListarEmpleados" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register src="../../../General/Controles/ctlProcesoContable.ascx" tagname="procesocontable" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />

    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewPrincipal">
            <table border="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <table id="tbCriterios" border="0" width="100%">
                                    <tr>
                                        <td style="width: 20%">Tipo Nómina<br />
                                            <asp:DropDownList ID="ddlTipoNomina" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="dropdown" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" Width="70%">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%">Año<br />
                                            <asp:TextBox ID="txtAño" runat="server" CssClass="textbox" MaxLength="300" onkeypress="return isNumber(event)" Width="70%" />
                                        </td>
                                        <td style="width: 20%">Semestre<br />
                                            <asp:DropDownList ID="ddlSemestre" runat="server" AppendDataBoundItems="true" CssClass="dropdown" Width="70%">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                                <asp:ListItem Text="Primer Semestre" Value="1" />
                                                <asp:ListItem Text="Segundo Semestre" Value="2" />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%">&nbsp;</td>
                                        <td style="width: 20%">Fecha Pago<br />
                                            <asp:TextBox ID="txtFechaPago" MaxLength="10" CssClass="textbox"
                                                runat="server" Width="80%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                                PopupButtonID="imagenCalendarioFin"
                                                TargetControlID="txtFechaPago"
                                                Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioFin" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table id="tbBotones" border="0" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <table style="width: 100%; text-align: center">
                                        <tr>
                                            <td style="text-align: center; padding-right: 20px">
                                                <asp:TextBox ID="txtusuariocreacion" runat="server" CssClass="textbox" Enabled="false" Visible="false" />
                                                <asp:Button runat="server" ID="btnLiquidacionDefinitiva" Width="200px" Text="Generar Liquidacion" CssClass="btn8" OnClick="btnLiquidacionDefinitiva_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server" ID="updatePanelListaDetalle" Visible="false">
                <ContentTemplate>
                    <table border="0" width="100%">
                        <tr>
                            <td style="text-align:left;">
                                <strong>Filtro Liquidaciones Generadas</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:70%;">
                                    <tr>
                                        <td>
                                            <label>Nombre</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtNombre" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <label>Identificación</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="return isNumber(event)" ID="txtIdentificacion" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <label>Código Empleado</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCodigoEmpleado" onkeypress="return isNumber(event)" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnFiltrar" runat="server" CssClass="btn8" Text="Filtrar" Width="100px" OnClick="btnFiltrar_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista"
                                    runat="server"
                                    AllowPaging="True"
                                    AutoGenerateColumns="False"
                                    GridLines="Horizontal"
                                    PageSize="50"
                                    HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True"
                                    Width="100%"
                                    OnPageIndexChanging="gvLista_PageIndexChanging"
                                    DataKeyNames="consecutivo"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="identificacion_empleado" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="fechaterminacion" HeaderText="Fecha Terminación" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="salario" HeaderText="Salario" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:#,##0.00}" />
                                        <asp:BoundField DataField="diasliquidar" HeaderText="Dias Liquidados" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="valortotalpagar" HeaderText="Valor a Pagar" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:#,##0.00}" />
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <cc1:ButtonGrid runat="server" ID="btnVerRecibo" OnClick="btnVerRecibo_Click" Width="100px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Ver Recibo" CssClass="btn8" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <cc1:ButtonGrid runat="server" ID="btnVerNovedades" OnClick="btnVerNovedades_Click" Width="130px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Ver Novedades" CssClass="btn8" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <cc1:ButtonGrid runat="server" ID="btnAgregarNovedades" OnClick="btnAgregarNovedades_Click" Width="100px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="+ Novedad" CssClass="btn8" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                                <asp:Label ID="lblTotalNominaIdenti" Text="Total Liquidación:" Visible="false" runat="server" />
                                <asp:Label ID="lblTotalNomina" Font-Bold="true" Visible="false" runat="server" />
                                <br />
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:ModalPopupExtender ID="mpeAgregarNovedades" runat="server" PopupControlID="panelActualizarNovedad"
                TargetControlID="hfFechaIni" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hfFechaIni" runat="server" />
            <div style="text-align: center">
                <asp:Panel ID="panelActualizarNovedad" runat="server" BackColor="White" Style="text-align: left" Width="550px">
                    <div style="border: 2px groove #2E9AFE; width: 550px; background-color: #FFFFFF; padding: 10px">
                        <asp:UpdatePanel ID="upModificacionTasa" runat="server">
                            <ContentTemplate>
                                <table width="550px">
                                    <tr>
                                        <td class="gridHeader" style="text-align: center;" colspan="3">Agregar Novedad
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Codigo Empleado :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtCodigoEmpleadoModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                            <asp:HiddenField runat="server" ID="hiddenIndexSeleccionado" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Identificacion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtIdentificacionModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nombres :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtNombresModal" Enabled="false" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Tipo Novedad :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlTipoNovedadModal" CssClass="dropdown" AppendDataBoundItems="true" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoNovedadModal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px; height: 32px;">Valor :
                                        </td>
                                        <td style="text-align: left; width: 400px; height: 32px;" colspan="2">
                                            <asp:TextBox ID="txtValorModal" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            <asp:Label ID="lblmsjModalNovedadNueva" runat="server" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnAgregarNovedadModal" runat="server" Height="30px" Text="Guardar" Width="100px"
                                        OnClick="btnAgregarNovedadModal_Click" CssClass="btn8" />
                                    &#160;&#160;&#160;&#160;
                        <asp:Button ID="btnCloseReg1" runat="server" CssClass="btn8"
                            Height="30px" Text="Cancelar" Width="100px" OnClick="btnCloseReg1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>

            <asp:ModalPopupExtender ID="mpRecibo" runat="server" PopupControlID="pnlRecibo"
                TargetControlID="hdfechaIni2" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdfechaIni2" runat="server" />
            <div style="text-align: center">
                <asp:Panel ID="pnlRecibo" runat="server" BackColor="White" Style="text-align: left" Width="550px">
                    <div style="border: 2px groove #2E9AFE; width: 550px; background-color: #FFFFFF; padding: 10px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="550px">
                                    <tr>
                                        <td class="gridHeader" style="text-align: center;" colspan="3">Recibo pago
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Codigo Empleado :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtCodigoEmpleadoReciboModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Identificacion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtIdentificacionReciboModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nombres :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtNombreReciboModal" Enabled="false" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="2">
                                            <asp:GridView ID="gvReciboModal"
                                                runat="server"
                                                AllowPaging="false"
                                                AutoGenerateColumns="False"
                                                GridLines="Horizontal"
                                                HorizontalAlign="Center"
                                                ShowHeaderWhenEmpty="True"
                                                Width="100%"
                                                DataKeyNames="consecutivo"
                                                Style="font-size: x-small">
                                                <Columns>
                                                    <asp:BoundField DataField="descripcionNovedad" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="${0:#,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnCerrarReciboModal" runat="server" Height="30px" Text="Cerrar" Width="100px"
                                        OnClick="btnCerrarReciboModal_Click" CssClass="btn8" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>

            <asp:ModalPopupExtender ID="mpNovedades" runat="server" PopupControlID="pnlNovedadesModal"
                TargetControlID="hiddenNovedades" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hiddenNovedades" runat="server" />
            <div style="text-align: center">
                <asp:Panel ID="pnlNovedadesModal" runat="server" BackColor="White" Style="text-align: left" Width="550px">
                    <div style="border: 2px groove #2E9AFE; width: 550px; background-color: #FFFFFF; padding: 10px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="550px">
                                    <tr>
                                        <td class="gridHeader" style="text-align: center;" colspan="3">Novedades Creadas
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Codigo Empleado :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtCodigoEmpleadoNovedadesModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Identificacion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtIdentificacionesNovedadesModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nombres :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtNombresNovedadesModal" Enabled="false" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="2">
                                            <asp:GridView ID="gvNovedades"
                                                runat="server"
                                                AllowPaging="false"
                                                AutoGenerateColumns="False"
                                                GridLines="Horizontal"
                                                HorizontalAlign="Center"
                                                ShowHeaderWhenEmpty="True"
                                                Width="100%"
                                                Style="font-size: x-small"
                                                DataKeyNames="consecutivo"
                                                OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvNovedades_RowDeleting">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                        <ItemStyle Width="16px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="descripcionNovedad" HeaderText="Descripcion Novedad" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor Novedad" DataFormatString="${0:#,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnCerrarNovedadesModal" runat="server" Height="30px" Text="Cerrar" Width="100px"
                                        OnClick="btnCerrarNovedadesModal_Click" CssClass="btn8" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>

        </asp:View>
        <asp:View ID="vFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View runat="server" ID="viewImprimir">
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnImprimirDesprendibles" runat="server" Height="30px" Text="Imprimir Desprendibles"
                                OnClick="btnImprimirDesprendibles_Click" CssClass="btn8" />
                            <asp:Button ID="btnImprimirListado" runat="server" Height="30px" Text="Imprimir Listado"
                                OnClick="btnImprimiListado_Click" CssClass="btn8" Style="margin-left: 10px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlReporte" Visible="false">
                                <rsweb:ReportViewer ID="rvReporteDesprendible" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%">
                                </rsweb:ReportViewer>
                                <rsweb:ReportViewer ID="rvReportePlanilla" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%">
                                </rsweb:ReportViewer>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="viewComprobante" runat="server">
            <asp:Panel ID="Panel2" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Panel ID="panelGeneral" runat="server">
                            </asp:Panel>
                            <asp:Panel ID="panelProceso" runat="server" Width="100%">
                                <uc2:procesoContable ID="ctlproceso" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensajeGuardar" runat="server" />
</asp:Content>
