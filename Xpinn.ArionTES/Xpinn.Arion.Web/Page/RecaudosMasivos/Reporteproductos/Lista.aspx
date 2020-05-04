<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master"  AutoEventWireup="true" EnableEventValidation ="false" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reporte Productos en Mora :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 843px">
                <tr>
                    <td style="text-align: left; width: 110px">
                        Fec. Reporte<br />
                        <ucFecha:fecha ID="txtFechaReporte" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 214px">
                        Empresa Recaudo<br />
                        <asp:DropDownList ID="ddlempresarecaudo" runat="server" CssClass="textbox" Width="98%" />
                    </td>
                    <td style="text-align: left; width: 149px">
                        Tipo Producto<br />
                        <asp:DropDownList ID="ddlTipo_Producto" runat="server" CssClass="textbox" 
                            Width="98%" AutoPostBack="True" 
                            onselectedindexchanged="ddlTipo_Producto_SelectedIndexChanged" />
                    </td>
                    <td style="text-align: left; width: 188px">
                        Linea De Producto<br />
                        <asp:DropDownList ID="ddlLineaProducto" runat="server" CssClass="textbox" Width="98%" />
                    </td>
                    <td style="width: 234px">
                        Rango De Dias De Mora<br />
                        <asp:TextBox ID="txtMora1" runat="server" CssClass="textbox" Width="52px" />
                        <strong>a</strong>
                        <asp:TextBox ID="txtMora2" runat="server" CssClass="textbox" Width="44px" />
                    </td>
                    <td style="text-align: left; width: 118px;">
                        Oficina<asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" 
                            Width="160px" >
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server" EnableEventValidation="false">
                <table style="width: 99%">
                    <tr>
                        <td>
                            <asp:Button ID="btnExportar" Width="110px" Height="17px" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                                Text="Exportar a Excel" Visible="false"/>
                            <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" Text="Visualizar informe" Visible="false"
                                Width="116px" Height="17px" OnClick="btnImprimir_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" EnableEventValidation="false" Width="99%"
                                GridLines="Horizontal" AutoGenerateColumns="False"  PageSize="30" AllowPaging="true"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="width: 840px;"  RowStyle-CssClass="gridItem"
                                DataKeyNames="cod_persona" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField  DataField="nom_empresa" HeaderText="Empresa Recaudo">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombres" HeaderText="Nombres" >
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="apellidos" HeaderText="Apellidos" >
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea Producto">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numero_producto" HeaderText="Numero Producto">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha Proximo Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor En Mora" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager" ></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                    </table>
                   </asp:panel>
                   </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <table>
            <tr>
                        <td style="text-align: left; width: 123%;">
                            <asp:Button ID="btnRegresarOrden" runat="server" CssClass="btn8" OnClientClick="btnInforme4_Click"
                                Text="Regresar" OnClick="btnInforme4_Click" Height="25px" Width="120px" />
                            &#160;&#160;
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                Text="Imprimir" OnClick="btnImprime_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 123%">
                            <iframe id="frmPrint" name="IframeName" src="../../Reportes/Reporte.aspx" height="500px"
                                runat="server" style="border-style: dotted; float: left; width: 287%;"></iframe>
                        </td>
                    </tr>
                <tr>
                    <td style="width: 123%">
                        <br />
                        <br />
                        <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="109%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="500px">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ReporteProductos\rptProductoo.rdlc" EnableExternalImages="True">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td style="width: 123%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="panelimpresion" runat="server" EnableEventValidation="false">
        <table style="width: 99%">                       
            <tr>
                <td>
                    <asp:GridView ID="gvimpresion" runat="server" EnableEventValidation="false" Width="99%"
                        GridLines="Horizontal" AutoGenerateColumns="False"  PageSize="30"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="width: 840px;"  RowStyle-CssClass="gridItem"
                        DataKeyNames="cod_persona" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" ControlStyle-Font-Size="Large">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="nom_empresa" HeaderText="Empresa Recaudo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Linea Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_producto" HeaderText="Numero Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha Proximo Pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Valor En Mora" DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager" ></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="Label1" runat="server" Visible="False" />
                </td>                        
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
