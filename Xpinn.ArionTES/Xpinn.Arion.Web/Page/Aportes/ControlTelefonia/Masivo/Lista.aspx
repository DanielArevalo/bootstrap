<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%--<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
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
                        <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Nro. Linea Telefónica, Nro. Identificación, Fecha Activación(dd/MM/yyyy),Fecha Vencimiento(dd/MM/yyyy)-Opcional,  Cod. Plan, Cod. Linea Servicio, Fecha Primer Cuota(dd/MM/yyyy), Valor Cuota, Cod. Periocidad, Cod. Forma Pago, Cod. Empresa - Opcional. 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:Button ID="btnCargarLineas" runat="server" CssClass="btn8" OnClick="btnCargarLineas_Click"
                            Height="22px" Text="Cargar Lineas" Width="150px" />
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
                                    DataKeyNames="num_linea_telefonica ,identificacion_titular" Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />

                                        <asp:BoundField DataField="num_linea_telefonica" HeaderText="Nro. Linea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion_titular" HeaderText="Identificación Titular">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_activacion" HeaderText="Fecha Activación"  DataFormatString="{0:d}" HtmlEncode="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}" HtmlEncode="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_plan" HeaderText="Cod. Plan">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_linea_servicio" HeaderText="Cod. Línea Servicio">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_primera_cuota" HeaderText="Fecha Primera Cuota" DataFormatString="{0:d}" HtmlEncode="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_periodicidad" HeaderText="Cod. Periodicidad">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Cod. Forma Pago">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_empresa" HeaderText="Cod. Empresa">
                                            <ItemStyle HorizontalAlign="Center" />
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
            <table style="width: 100%;" id="tbnew">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">Importación de datos generada correctamente.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td id="infApor_no" runat="server" visible="false">
                        <asp:GridView ID="GridView1" runat="server" Width="90%" AutoGenerateColumns="False"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField DataField="num_linea_telefonicaR" HeaderText="Nro. Línea Telefónica">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion_titular" HeaderText="Nro. Identificación">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lab_infApor_no" runat="server" Text="Lineas no exitosas en la importación, ERROR EN LOS DATOS SUMINISTRADOS." />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="606px">
            </asp:Panel>
            <table style="width: 80%">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="98%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            DataKeyNames="cod_linea_aporte"
                            Style="text-align: left">
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
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit"
                                            ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado_Linea" HeaderText="Estado" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
