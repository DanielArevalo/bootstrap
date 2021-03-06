<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Servicio Carga :." Async="True" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table>
            <tr>
                <td style="text-align: left" colspan="3">
                    <strong>Datos para carga de Auxilios</strong>
                </td>
            </tr>
            <tr>            
                <td style="text-align: left;">
                    Archivo<br />
                    <asp:FileUpload ID="flpArchivo" runat="server" />
                </td>
            </tr>
        </table>
        <div style="width: 100%; text-align: left; font-size: xx-small">
            <br />
            <b>Separador del archivo : </b>&nbsp;&nbsp;&nbsp;| <br /><br />
            <b>Estructura de archivo: Fecha de Solicitud, Identificación, Codigo Linea de Axilio, Valor Solicitado</b>
        </div>
    </asp:Panel>
    <hr style="width: 100%" />
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
            <div style="border-style: none; border-width: medium;overflow:scroll; max-height:500px; background-color: #f5f5f5">
                <table width="100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/Images/btnExportar.jpg"
                                OnClick="btnExportar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvInconsistencia" runat="server" Width="100%" GridLines="Horizontal"
                                AutoGenerateColumns="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_registro" Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField DataField="numero_registro" HeaderText="No. Fila" ItemStyle-Width="50"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalIncon" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
        <br />
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de servicios a cargar:</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: scroll; max-height: 600px;">
                        <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                            PageSize="40" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" Style="font-size: x-small" DataKeyNames="cod_persona, numero_auxilio"
                            OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" EnablePersistedSelection="True" ViewStateMode="Enabled" OnPageIndexChanged="gvLista_PageIndexChanged" OnPageIndexChanging="gvLista_PageIndexChanging">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:CommandField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_persona" HeaderText="Cod Persona" Visible="false">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_linea_auxilio" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_solicitado" HeaderText="Valor solicitado">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>                    
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                    </div>
                    <center><asp:Label ID="lblTotalRegs" runat="server" Visible="False" /></center>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
