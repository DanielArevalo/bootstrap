<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="grid" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td style="text-align: left; width: 150px">
                        Fecha Inicial<br />
                        <ucFecha:fecha ID="txtFechaInicial" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Fecha Final<br />
                        <ucFecha:fecha ID="txtFechaFinal" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left;" colspan="3">
                        Línea de Servicio<br />
                        <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="5">
                        <strong>Datos de la Renovación</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">
                        Fec Inicial Vigencia<br />
                        <ucFecha:fecha ID="txtFIniVigencia" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Fec Final Vigencia<br />
                        <ucFecha:fecha ID="txtFFinVigencia" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left;">
                        Tipo Incremento<br />
                        <asp:DropDownList ID="ddlTipoIncremento" runat="server" CssClass="textbox" Width="300px"
                            OnSelectedIndexChanged="ddlTipoIncremento_SelectedIndexChanged" AutoPostBack="true" />
                    </td>
                    <td style="text-align: left;">
                        Valor Cuota<br />
                        <asp:TextBox ID="txtVrCta" runat="server" CssClass="textbox" style="text-align:right"/>
                        <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtVrCta"
                            FilterType="Custom, Numbers" ValidChars="" />
                        <uc1:decimales ID="txtVrCuota" runat="server" />
                    </td>
                    <td style="text-align: left;">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="100px" Style="text-align: right" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtPlazo"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Listado de Servicios a Renovar</strong><br />
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                                DataKeyNames="numero_servicio,cod_persona" Style="font-size: x-small" 
                                onrowdeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" Visible="false">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec. Prox Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Saldo" DataField="saldo" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                    <asp:BoundField HeaderText="Nom. Proveedor" DataField="nombre_proveedor" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTexto" runat="server" Text="Total servicios a renovar : " />
                            <asp:TextBox ID="txtVrTotal" runat="server" Width="160px" Enabled="false" CssClass="textbox"
                                Style="text-align: right" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>       
    <center>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
            Visible="False" />
    </center>
    <asp:Panel ID="pNoRenovar" runat="server">
        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles" runat="server" />
                    <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pConsultaNo" runat="server" Width="100%">
            <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                <table width="100%">                    
                    <tr>
                        <td style="text-align:left">
                            <strong>Listado de Servicios a No Renovar</strong><br />
                            <asp:GridView ID="gvNoRenovacion" runat="server" Width="100%" GridLines="Horizontal"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio,cod_persona" 
                                Style="font-size: x-small" onrowdeleting="gvNoRenovacion_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_edit.jpg" ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" Visible="false">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec. Prox Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Saldo" DataField="saldo" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                    <asp:BoundField HeaderText="Nom. Proveedor" DataField="nombre_proveedor" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pConsultaNo" TextLabelID="lblMostrarDetalles" />
    </asp:Panel>

     </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlTipoIncremento" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowDeleting" />
        <asp:AsyncPostBackTrigger ControlID="gvNoRenovacion" EventName="RowDeleting" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
</asp:Content>
