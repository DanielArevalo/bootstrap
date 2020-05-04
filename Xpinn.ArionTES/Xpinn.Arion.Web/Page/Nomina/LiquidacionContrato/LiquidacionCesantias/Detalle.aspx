<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation= "false"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Liquidación Nómina :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarEmpleados.ascx" TagName="ctlListarEmpleados" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />

    <asp:MultiView runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewPrincipal">
            <table border="0" width="100%">
                <tr>
                    <td>
                    <asp:Panel ID="PanelEncabezado" runat="server">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <table id="tbCriterios" border="0" width="100%">
                                    <tr>
                                        <td style="width: 20%">Tipo Nómina<br />
                                            <asp:DropDownList ID="ddlTipoNomina" runat="server" Autopostback="true" AppendDataBoundItems="true" CssClass="dropdown" Width="70%" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%">Fecha Inicio<br />
                                            <asp:TextBox ID="txtFechaInicio" runat="server" AutoPostBack="true" Enabled="false" CssClass="textbox" MaxLength="10" OnTextChanged="txtFechaLiquidacion_TextChanged" Width="80%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" TargetControlID="txtFechaInicio"></asp:CalendarExtender>
                                            <img id="imagenCalendarioInicio" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="width: 20%">Fecha Fin<br />
                                            <asp:TextBox ID="txtFechaFin" MaxLength="10" CssClass="textbox"
                                                runat="server" Width="80%" AutoPostBack="true"   Enabled="false"  OnTextChanged="txtFechaLiquidacion_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                                PopupButtonID="imagenCalendarioFin"
                                                TargetControlID="txtFechaFin"
                                                Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <img id="imagenCalendarioFin" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="width: 20%">Centro Costo<br />
                                            <asp:DropDownList ID="ddlCentroCosto" runat="server" AppendDataBoundItems="true" CssClass="dropdown" Width="70%">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </asp:Panel>
                        <table id="tbBotones" border="0" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <table style="width: 100%; text-align: center">
                                        <tr>
                                            <td style="text-align: right; padding-right: 20px">
                                                <asp:Button runat="server" ID="btnLiquidacionDefinitiva" Width="200px" Text="Liquidacion Definitiva" CssClass="btn8" OnClick="btnLiquidacionDefinitiva_Click" />
                                            </td>
                                            <td style="text-align: left; padding-left: 20px">
                                                <asp:Button runat="server" ID="btnLiquidacionPrueba" Width="200px" Text="Liquidacion de Prueba" CssClass="btn8" OnClick="btnLiquidacionPrueba_Click" />
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
            <asp:UpdatePanel runat="server"  ID="updatePanelLiquidacionGeneradas" Visible="false">
                <ContentTemplate>
                    <table border="0" width="100%">
                        <tr>
                            <td style="text-align:left;">
                                <strong>Filtro Liquidaciones Generadas</strong>
                                <asp:TextBox ID="txtNomina" runat="server" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:70%;">
                                    <tr>
                                        <td style="height: 42px">
                                            <label>Nombre</label>
                                        </td>
                                        <td style="height: 42px">
                                            <asp:TextBox runat="server" ID="txtNombre" CssClass="textbox"/>
                                        </td>
                                        <td style="height: 42px">
                                            <label>Identificación</label>
                                        </td>
                                        <td style="height: 42px">
                                            <asp:TextBox runat="server" onkeypress="return isNumber(event)" ID="txtIdentificacion" CssClass="textbox"/>
                                        </td>
                                        <td style="height: 42px">
                                            <label>Código Empleado</label>
                                        </td>
                                        <td style="height: 42px">
                                            <asp:TextBox runat="server" ID="txtCodigoEmpleado" onkeypress="return isNumber(event)" CssClass="textbox"/>
                                        </td>
                                        <td style="height: 42px">
                                            <asp:Button ID="btnFiltrar" runat="server" CssClass="btn8" Text="Filtrar" Width="100px" OnClick="btnFiltrar_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <div style="text-align: left">
                                                <strong>Novedades:</strong>
                                                <asp:Button ID="btnCargarDatos" runat="server" CssClass="btn8" OnClick="btnCargarDatos_Click" OnClientClick="btnCargarDatos_Click" Text="Cargar Datos" Width="150px" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            </div>
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
                                    PageSize="20"
                                    HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True"
                                    Width="100%"
                                    OnPageIndexChanging="gvLista_PageIndexChanging"
                                    DataKeyNames="consecutivo"
                                    Style="font-size: x-small" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDataBound="gvLista_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado" ItemStyle-HorizontalAlign="Center" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="identificacion_empleado" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="salario" HeaderText="Salario" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:C}"  ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="dias" HeaderText="Dias Liquidados" ItemStyle-HorizontalAlign="Center" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="valortotalapagar" HeaderText="Valor a Pagar" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:C}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco"><ItemTemplate><cc1:ButtonGrid runat="server" ID="btnVerRecibo" OnClick="btnVerRecibo_Click" Width="100px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Ver Recibo" CssClass="btn8" /></ItemTemplate><HeaderStyle CssClass="gridIco" /></asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco"><ItemTemplate><cc1:ButtonGrid runat="server" ID="btnVerNovedades" OnClick="btnVerNovedades_Click" Width="130px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Ver Novedades" CssClass="btn8" /></ItemTemplate><HeaderStyle CssClass="gridIco" /></asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco"><ItemTemplate><cc1:ButtonGrid runat="server" ID="btnAgregarNovedades" OnClick="btnAgregarNovedades_Click" Width="100px"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="+ Novedad" CssClass="btn8" /></ItemTemplate><HeaderStyle CssClass="gridIco" /></asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                                <asp:Label ID="lblTotalNominaIdenti" Text="Total Nomina:" Visible="false" runat="server" />
                                <asp:Label ID="lblTotalNomina" Font-Bold="true" Visible="false" runat="server" />
                                <br />
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 53px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:ModalPopupExtender ID="mpeAgregarNovedades" runat="server" PopupControlID="panelActualizarNovedad"
                TargetControlID="hfFechaIni" BackgroundCssClass="backgroundColor"></asp:ModalPopupExtender>
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
                                        <td style="text-align: left; width: 150px;">Concepto :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlConceptoModal" CssClass="dropdown" AppendDataBoundItems="true" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlConceptoModal_SelectedIndexChanged">
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
                                        <td style="text-align: left; width: 150px;">Descripcion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtDescripcionModal" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Centro Costo :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlCentroCostoModal" CssClass="dropdown" AppendDataBoundItems="true" Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nomina :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlNominaModal" Enabled="false" CssClass="dropdown" AppendDataBoundItems="true" Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Tipo:
                                        </td>
                                        <td style="text-align: left; width: 150px;" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkBoxValorModal" Text="Valor" TextAlign="Left" runat="server" Font-Size="Small" Width="100px" OnCheckedChanged="chkBoxValorModal_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkBoxCantidadModal" Text="Cantidad" TextAlign="Left" runat="server" Visible="false" Font-Size="Small" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
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
                TargetControlID="hdfechaIni2" BackgroundCssClass="backgroundColor"></asp:ModalPopupExtender>
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
                                                    <asp:BoundField DataField="descripcion_concepto" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="valorconcepto" HeaderText="Valor" DataFormatString="${0:#,##0.00}" ItemStyle-HorizontalAlign="Right" />
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
                TargetControlID="hiddenNovedades" BackgroundCssClass="backgroundColor"></asp:ModalPopupExtender>
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
                                               OnRowDeleting="gvNovedades_RowDeleting">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True"><ItemStyle Width="16px" /></asp:CommandField>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion Novedad" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="valorconcepto" HeaderText="Valor Novedad" DataFormatString="${0:#,##0.00}" ItemStyle-HorizontalAlign="Right" />
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
        <asp:View runat="server" ID="viewEmpleados">
            <uc1:ctlListarEmpleados ID="ctlEmpleados" ModoSeleccionCheckBox="true" OnOnErrorControl="ctlEmpleados_OnErrorControl" runat="server" />
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
                            <asp:TextBox ID="txtCodigo" runat="server" Visible="false" Enabled="false" CssClass="textbox" onkeypress="return isNumber(event)" />
                             <asp:TextBox ID="txtusuariocreacion" runat="server" Visible="false" Enabled="false" CssClass="textbox"/>
                           <asp:Button ID="btnImprimirDesprendibles" runat="server" Height="30px" Text="Imprimir Desprendibles"
                                OnClick="btnImprimirDesprendibles_Click" CssClass="btn8" />
                            <asp:Button ID="btnImprimirPlanilla" runat="server" Height="30px" Text="Imprimir Planilla"
                                OnClick="btnImprimirPlanilla_Click" CssClass="btn8" Style="margin-left: 10px" />
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
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%"></rsweb:ReportViewer>
                                <rsweb:ReportViewer ID="rvReportePlanilla" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%"></rsweb:ReportViewer>
                            </asp:Panel>
                        </td>
                         <td>
                             &nbsp;</td>
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
        <asp:View runat="server" ID="viewComprobante">
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
    <uc4:mensajegrabar ID="ctlMensajeGuardarPrueba" runat="server" />


    <br />
            <asp:HiddenField ID="hfCarga" runat="server" />

         
    
    <asp:MultiView runat="server" ID="mvCargar">
        <asp:View ID="vwCargaDatos" runat="server">
            <table width="100%">
                <tr>
                    <td class="gridHeader" colspan="2" style="text-align: center"><strong>Seleccione el Archivo</strong> </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; font-size: xx-small; width: 100%"><strong>Tipo de Archivo de Carga : &nbsp;&nbsp;&nbsp; .txt
                        <br />
                        Separador de Campos de Carga : &nbsp;&nbsp;&nbsp; |
                        <br />
                        Estructura de archivo a cargar: &nbsp;&nbsp;&nbsp; Identificación, Código Concepto,Valor Novedad</strong><br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Archivo </td>
                    <td style="text-align: left; width: 60%">
                        <asp:FileUpload ID="flpArchivo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblmsjCarga" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnAceptarCarga" runat="server" CssClass="btn8" Height="30px" OnClick="btnAceptarCarga_Click" Text="Aceptar" Width="100px" />
                        &nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="btnRegresarNovedad" runat="server" CssClass="btn8" Height="30px" OnClick="btnRegresarNovedad_Click" Text="Regresar" Width="100px" />
                        <br />
                        <asp:GridView ID="gvNovedadesLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                            DataKeyNames="consecutivo" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" 
                     PagerStyle-CssClass      ="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: small" Width="100%">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="False">
                                
   <ItemStyle Width="16px" />
                                
           </asp:CommandField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" Visible="False">
                                    
                        <ItemTemplate>
                                        
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                        
  </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                    
   <ItemStyle CssClass="gridIco" />
                                    
 </asp:TemplateField>
                                <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado">
                                
                                <ItemStyle HorizontalAlign="center" />
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                
                                <ItemStyle HorizontalAlign="center" />
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="codigonomina" HeaderText="Nómina">
                                
                                <ItemStyle HorizontalAlign="center" />
                                
                                </asp:BoundField>
                                  <asp:BoundField DataField="codigoconcepto" HeaderText="Cod. Concepto">
                                
                                <ItemStyle HorizontalAlign="center" />
                                
                                </asp:BoundField>                                
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción Novedad">
                                
                                <ItemStyle HorizontalAlign="center" />
                                
                                </asp:BoundField>                                
                                <asp:BoundField DataField="valorconcepto" HeaderText="Valor">
                                

                                <ItemStyle HorizontalAlign="center" />
                                
                             </asp:BoundField>
                                 <asp:BoundField DataField="codigocentrocosto" HeaderText="Centro Costo">
                                
                           <ItemStyle HorizontalAlign="center" />
                                 </asp:BoundField>

                               
                             
                               
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    
</asp:Content>
