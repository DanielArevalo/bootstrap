<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
      <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/css/select2.min.css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

  
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="2">
                <tr style="text-align: left">
                    <td class="logo" style="text-align: left; font-size: small;" colspan="3">
                        <strong>
                            <asp:Label ID="lblInicial" runat="server" Text="Seleccion el Archivo con los Datos de los Recaudos"></asp:Label>
                        </strong>
                        <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#359AF2"
                            Text="Label" Visible="False"></asp:Label>
                        &nbsp;     
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td colspan="4" style="text-align: left;">Empresa Recaudadora
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="323px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td colspan="4" style="text-align: left;">Estructura de Archivo&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="dropdown" Width="323px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td class="logo" style="width: 350px; text-align: left; font-size: x-small;"
                        colspan="2">
                        <asp:FileUpload ID="FileUploadMetas" runat="server" />
                    </td>
                    <td>&nbsp;                         
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 100px; text-align: left">
                        <asp:Panel ID="pConsulta" runat="server">
                            <asp:Button ID="btnCargarRecaudos" runat="server" CssClass="btn8"
                                OnClick="btnCargarRecaudos_Click" Text="Cargar Recaudos"
                                Width="145px" />
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn8"
                                OnClick="btnGuardar_Click" Text="Guardar Recaudos" Width="145px" />
                        </asp:Panel>
                    </td>
                    <td class="logo" style="width: 179px; text-align: left" colspan="2">
                        <asp:Button ID="btnGenerarAPartirNovedades" runat="server" CssClass="btn8"
                            OnClick="btnGenerarAPartirNovedades_Click" Text="Generar a partir de novedades" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 160px; text-align: left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="true" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                        <asp:DropDownList ID="ddlNovedad" runat="server" CssClass="dropdown" Width="323px" />
                    </td>
                    <td class="logo" style="width: 179px; text-align: left">
                        <asp:Label ID="lblNumeroLista" runat="server" Visible="false" Text="Número de Aplicación" /><br />
                        <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:CheckBox ID="chkPermitePaginar" Text="Permitir paginacion?" Checked="true" runat="server" OnCheckedChanged="chkPermitePaginar_CheckedChanged" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
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
                                    <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                        <Columns>
                                            <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50" ItemStyle-HorizontalAlign="Left" />
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
                            <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
                                CollapseControlID="pEncBusqueda" Collapsed="True"
                                CollapsedImage="~/Images/expand.jpg"
                                CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg"
                                ExpandedText="(Click Aqui para Ocultar Detalles...)" ImageControlID="imgExpand"
                                SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
                            <br />
                        </asp:Panel>
                        <div style="overflow: scroll; max-height: 1000px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                                    OnClick="btnExportar_Click" Text="Exportar a Excel" />
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="30"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMovGeneral_PageIndexChanging"
                                            SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                            <Columns>
                                                <asp:BoundField DataField="cod_cliente" HeaderText="Cod." ItemStyle-Width="50" />
                                                <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Codigo Nómina" />
                                                <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit" />
                                                <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" />
                                                <asp:BoundField DataField="numero_producto" HeaderText="Número de Producto" />
                                                <asp:BoundField DataField="fecha_recaudo" HeaderText="Fecha Recaudo"
                                                    DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="tipo_aplicacion" HeaderText="Tipo Aplicacion" />
                                                <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                                <asp:BoundField DataField="valor" HeaderText="Valor a Aplicar"
                                                    DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
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
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Recaudos Aplicados Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar"
                                OnClick="btnFinal_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAplicar" runat="server" Text="Aplicar el Recaudo"
                                OnClick="btnAplicar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>

    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">Esta Seguro de Grabar los Datos de la Carga para su Aplicación?
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8"
                            Width="182px" OnClick="btnParar_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>
